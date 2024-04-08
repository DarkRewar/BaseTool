using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace BaseTool.Editor.Todo
{
    public class TodoWindow : EditorWindow
    {
        private readonly static Regex _regex = new Regex(@"//\s*(todo|fix|fixme)\s*(?:\s*\(([^)]+)\))?\s*:?[\s]+(.*)", RegexOptions.IgnoreCase);
        private readonly static Regex _asmRemoveNameRegex = new Regex(@"^(.*)/([\w.]+).asmdef$", RegexOptions.IgnoreCase);
        private const string EveryAssemblies = "[All]";
        private const string DefaultAssembly = "Assembly-CSharp";
        private const string NamePrefix = "@";
        private const string TagPrefix = "#";
        private const string AddTag = "[Add Tag]";

        [SerializeField] private VisualTreeAsset _visualTreeAsset = default;

        private VisualElement _view;

        private static string _filter = DefaultAssembly;
        private static List<string> _tagFilters = new List<string>();

        private HashSet<string> _assemblies = new HashSet<string>() { EveryAssemblies, DefaultAssembly };
        private HashSet<string> _tagsFound = new HashSet<string>();

        private ScrollView _scrollView;
        private VisualElement _tagContainer;
#if UNITY_2021_1_OR_NEWER
        private DropdownField _tagDropdown;
        private DropdownField _assemblyDropdown;
#endif

        [MenuItem("Window/BaseTool/Todo List")]
        public static void ShowWindow()
        {
            var window = GetWindow<TodoWindow>();
            window.minSize = new Vector2(600, 400);
            window.titleContent = new GUIContent("Todo List");
        }

        public void CreateGUI()
        {
            RetrieveVisualElements();
            UpdateTodoList();
        }

        private void RetrieveVisualElements()
        {
            VisualElement root = rootVisualElement;
            _view = _visualTreeAsset.Instantiate();
            root.Add(_view);

            _scrollView = _view.Q<ScrollView>();
            _tagContainer = _view.Q<VisualElement>("TagContainer");

#if UNITY_2021_1_OR_NEWER
            _tagDropdown = _view.Q<DropdownField>("TagDropdown");
            _tagDropdown.RegisterCallback<ChangeEvent<string>>(evt =>
            {
                if (evt.newValue.Equals(AddTag)) return;
                AddTagFilter(evt.newValue);
                _tagDropdown.index = 0;
            });

            _assemblyDropdown = _view.Q<DropdownField>();
            _assemblyDropdown.RegisterCallback<ChangeEvent<string>>((evt) =>
            {
                _filter = evt.newValue;
                UpdateTodoList();
            });
#endif
        }

        private void UpdateTodoList()
        {
            _scrollView?.contentContainer.Clear();
            _tagsFound.Clear();

            var assetIds = AssetDatabase.FindAssets("t:script", new[] { "Assets" });
            foreach (var assetId in assetIds)
            {
                var filePath = AssetDatabase.GUIDToAssetPath(assetId);
                var lines = File.ReadAllLines(filePath);
                for (int i = 0; i < lines.Length; i++)
                {
                    var line = lines[i];
                    if (_regex.IsMatch(lines[i]))
                    {
                        var assemblyName = GetAssembly(filePath);
                        var display = true;
                        _assemblies.Add(assemblyName);
                        if (!AssemblyMatchesFilter(assemblyName)) display = false;

                        var match = _regex.Match(line);
                        var lineNumber = i + 1;
                        var metaInfos = ParseTagsAndName(match.Groups[2].Value);
                        AddMetaInfos(metaInfos);

                        if (!MetaInfosMatchesFilter(metaInfos)) display = false;

                        if (!display) continue;
                        var todoType = match.Groups[1].Value.Trim().ToLower();
                        var todoEntry = AddTodoEntry(match.Groups[3].Value.Trim(), filePath, lineNumber, metaInfos);
                        todoEntry.TodoType = todoType switch
                        {
                            "fixme" => TodoType.Fixme,
                            "fix" => TodoType.Fixme,
                            "fixed" => TodoType.Fixme,
                            _ => TodoType.Todo,
                        };
                    }
                }
            }

#if UNITY_2021_1_OR_NEWER
            if (_assemblyDropdown != null)
            {
                _assemblyDropdown.choices = _assemblies.ToList();
                _assemblyDropdown.index = _assemblies.ToList().IndexOf(_filter);
            }
#endif

            UpdateFilters();
        }

        private void AddMetaInfos(TodoMetaData metaInfos)
        {
            if (metaInfos.IsNull) return;

            foreach (var tag in metaInfos.Tags)
            {
                _tagsFound.Add(tag);
            }
        }

        private void AddTagFilter(string tag)
        {
            _tagFilters.Add(tag);
            UpdateTodoList();
        }

        private void UpdateFilters()
        {
            var tags = _tagsFound.ToList();
            tags.Sort();

#if UNITY_2021_1_OR_NEWER
            _tagDropdown.choices = tags.Except(_tagFilters).Prepend(AddTag).ToList();
#endif

            _tagContainer.Query(className: "tag").ForEach(e => e.RemoveFromHierarchy());
            foreach (var tag in _tagFilters)
            {
                var tagLabel = new Label(tag);
                tagLabel.AddToClassList("tag");
                tagLabel.RegisterCallback<ClickEvent, string>((_, tagValue) =>
                {
                    _tagFilters.Remove(tagValue);
                    UpdateTodoList();
                }, tag);
                _tagContainer.hierarchy.Insert(_tagContainer.childCount - 1, tagLabel);
            }
        }

        private TodoEntry AddTodoEntry(string text, string filePath, int lineNumber, TodoMetaData metaInfos)
        {
            List<string> names = null;
            if (!metaInfos.IsNull && metaInfos.Names.Count > 0)
            {
                names = metaInfos.Names;
            }

            List<string> tags = null;
            if (!metaInfos.IsNull && metaInfos.Tags.Count > 0)
            {
                tags = metaInfos.Tags;
            }

            var todoEntry = new TodoEntry
            {
                Text = text,
                File = $"{filePath}:{lineNumber}",
                Tags = tags,
                Names = names
            };
            todoEntry.OnTagClicked += AddTagFilter;
            todoEntry.RegisterCallback<ClickEvent>(_ =>
            {
                var asset = AssetDatabase.LoadMainAssetAtPath(filePath);
                AssetDatabase.OpenAsset(asset, lineNumber);
            });
            _scrollView.contentContainer.Add(todoEntry);
            return todoEntry;
        }

        private TodoMetaData ParseTagsAndName(string value)
        {
            if (string.IsNullOrEmpty(value)) return default;

            List<string> names = new List<string>();
            List<string> tags = new List<string>();

            foreach (var entry in value.Split(' '))
            {
                if (entry.StartsWith(NamePrefix))
                {
                    names.Add(entry.Substring(NamePrefix.Length));
                }
                else if (entry.StartsWith(TagPrefix))
                {
                    tags.Add(entry.Substring(TagPrefix.Length));
                }
            }

            return new TodoMetaData(names, tags);
        }

        private bool AssemblyMatchesFilter(string assemblyName)
            => string.IsNullOrEmpty(_filter) || _filter.Equals(EveryAssemblies) || _filter.Equals(assemblyName);

        private bool MetaInfosMatchesFilter(TodoMetaData metaInfos) =>
            _tagFilters.Count == 0 ||
            (!metaInfos.IsNull && metaInfos.Tags.Count > 0 && _tagFilters.TrueForAll(tag => metaInfos.Tags.Contains(tag)));

        private string GetAssembly(string filePath)
        {
            var monoScript = AssetDatabase.LoadMainAssetAtPath(filePath) as MonoScript;
            if (monoScript == null || monoScript.GetClass() == null)
            {
                var asmdefs = AssetDatabase.FindAssets("t:asmdef", new[] { "Assets" });
                foreach (var asmdef in asmdefs)
                {
                    var asmPath = AssetDatabase.GUIDToAssetPath(asmdef);
                    var match = _asmRemoveNameRegex.Match(asmPath);
                    if (filePath.Contains(match.Groups[1].Value))
                        return match.Groups[2].Value;
                }
                return DefaultAssembly;
            }
            return monoScript.GetClass().Assembly.GetName().Name;
        }
    }
}
