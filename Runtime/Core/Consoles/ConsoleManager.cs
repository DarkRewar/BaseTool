using System.Collections.Generic;
using UnityEngine;

namespace BaseTool
{
#if UNITY_2021_1_OR_NEWER
    [RequireComponent(typeof(UIDocument))]
    public class ConsoleManager : MonoBehaviour
    {
        private UIDocument _uiDocument;

        private ScrollView _scrollView;
        private TextField _textField;

        private bool _displayed = false;

        private void Awake()
        {
            _uiDocument = GetComponent<UIDocument>();
            _uiDocument.panelSettings = Resources.Load<PanelSettings>("ConsolePanelSettings");
            _uiDocument.visualTreeAsset = Resources.Load<VisualTreeAsset>("ConsoleView");
            _uiDocument.rootVisualElement.style.display = DisplayStyle.None;
        }

        private void OnEnable()
        {
            _scrollView = _uiDocument.rootVisualElement.Q<ScrollView>("ConsoleScroll");

            _textField = _uiDocument.rootVisualElement.Q<TextField>("ConsoleField");
            _textField.RegisterCallback<KeyDownEvent>(OnConsoleFieldKeyDown, TrickleDown.TrickleDown);
            _textField.RegisterCallback<NavigationSubmitEvent>(e =>
            {
                e.StopImmediatePropagation();
                _textField.ElementAt(0).Focus();
            }, TrickleDown.TrickleDown);
        }

        private void OnDestroy()
        {
            Console.RemoveCommand("help");
            Console.RemoveCommand("list");
        }

        private void Update()
        {
            Console.ConsoleUpdate();

#if ENABLE_LEGACY_INPUT_MANAGER
            if (!Input.GetKeyDown(KeyCode.F4)) return;
            Toggle();
#else
            enabled = false;
            throw new Exception("The old input system must be set to use Console Manager!");
#endif
        }

        private void Toggle()
        {
            _displayed = !_displayed;
            _uiDocument.rootVisualElement.style.display =
                _displayed
                    ? DisplayStyle.Flex
                    : DisplayStyle.None;
            if (_displayed) _textField.ElementAt(0).Focus();

            Time.timeScale = _displayed ? 0 : 1;
        }

        public void WriteLine(string txt)
        {
            _scrollView.Add(new Label(txt));
        }

        private void OnConsoleFieldKeyDown(KeyDownEvent evt)
        {
#if UNITY_2023_1_OR_NEWER
            if (evt.keyCode == KeyCode.Tab)
            {
                var completion = Console.TabComplete(_textField.text).TrimEnd();
                _textField.SetValueWithoutNotify(completion);
                _textField.cursorIndex = completion.Length;
                _textField.selectIndex = completion.Length;
            }

            if (evt.keyCode != KeyCode.Return) return;

            Console.EnqueueCommand(_textField.text);
            _textField.SetValueWithoutNotify(null);
            _uiDocument.rootVisualElement.schedule.Execute(_ => _textField.ElementAt(0).Focus()).ExecuteLater(50);
#else
            evt.StopImmediatePropagation();
            if (evt.keyCode == KeyCode.Tab)
            {
                var completion = Console.TabComplete(_textField.text).TrimEnd();
                _textField.SetValueWithoutNotify(completion);
            }

            if (evt.keyCode != KeyCode.Return) return;

            Console.EnqueueCommand(_textField.text);
            _textField.SetValueWithoutNotify(null);
            _uiDocument.rootVisualElement.schedule.Execute(_ => _textField.ElementAt(0).Focus()).ExecuteLater(50);
#endif
        }
    }
#else
    public class ConsoleManager : MonoBehaviour
    {
        public const int TextFieldHeight = 40;

        private bool _displayed = false;
        private string _inputField = string.Empty;
        private Vector2 _scrollViewVector;

        private List<string> _lines = new List<string>();

        private GUISkin _skin;

        private void Update()
        {
            Console.ConsoleUpdate();

#if ENABLE_LEGACY_INPUT_MANAGER
            if (!Input.GetKeyDown(KeyCode.F4)) return;
            Toggle();
#else
            enabled = false;
            throw new Exception("The old input system must be set to use Console Manager!");
#endif
        }

        private void Toggle()
        {
            _displayed = !_displayed;
            Time.timeScale = _displayed ? 0 : 1;
        }

        public void WriteLine(string txt)
        {
            if (_lines.Count > 30) _lines.RemoveAt(0);
            _lines.Add(txt);
            _scrollViewVector = new Vector2(0, TextFieldHeight * _lines.Count);
        }

        private void CreateSkinIfNull()
        {
            if (_skin) return;

            _skin = Instantiate(GUI.skin);

            _skin.box.normal.textColor = Color.black;
            _skin.box.stretchWidth = true;
            _skin.box.fixedHeight = 450;

            _skin.scrollView.fontSize = 30;
            _skin.label.fontSize = 30;

            _skin.textField.fontSize = 30;
            _skin.textField.stretchWidth = true;
        }

        private void OnGUI()
        {
            if (!_displayed) return;

            Event e = Event.current;
            if (e.type == EventType.KeyDown && e.keyCode == KeyCode.F4)
            {
                Toggle();
                return;
            }

            CreateSkinIfNull();
            GUI.skin = _skin;

            HandleScrollView();
            HandleTextField(e);
        }

        private void HandleScrollView()
        {
            var screenWidth = Screen.width;
            var screenHeight = Screen.height;
            var maxConsoleHeight = _skin.box.fixedHeight;
            GUI.BeginGroup(new Rect(0, screenHeight - maxConsoleHeight, screenWidth, maxConsoleHeight), _skin.box);

            _scrollViewVector = GUILayout.BeginScrollView(_scrollViewVector,
                GUILayout.MinWidth(screenWidth),
                GUILayout.Height(maxConsoleHeight - TextFieldHeight));

            foreach (var line in _lines)
            {
                GUILayout.Label(line);
            }
            GUI.EndScrollView();
            GUI.EndGroup();
        }

        private void HandleTextField(Event e)
        {
            var screenWidth = Screen.width;
            var screenHeight = Screen.height;

            ExecuteActions(e);

            GUI.SetNextControlName("CommandTextField");
            _inputField = GUI.TextField(
                new Rect(0, screenHeight - TextFieldHeight, screenWidth, TextFieldHeight),
                _inputField);
            GUI.FocusControl("CommandTextField");

            if (e.type == EventType.KeyDown && e.keyCode == KeyCode.Tab)
            {
                TextEditor state = (TextEditor)GUIUtility.GetStateObject(typeof(TextEditor), GUIUtility.keyboardControl);
                state.cursorIndex = _inputField.Length;
                state.selectIndex = _inputField.Length;
            }
        }

        private void ExecuteActions(Event e)
        {
            if (e.type != EventType.KeyDown || string.IsNullOrEmpty(_inputField)) return;

            switch (e.keyCode)
            {
                case KeyCode.Return:
                    Console.EnqueueCommand(_inputField);
                    _inputField = null;
                    break;
                case KeyCode.Tab:
                    _inputField = Console.TabComplete(_inputField).TrimEnd();
                    break;
                default: break;
            }
        }
    }
#endif
}