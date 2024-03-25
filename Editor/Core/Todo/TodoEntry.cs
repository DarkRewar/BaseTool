using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UIElements;

namespace BaseTool.Editor.Todo
{
    public enum TodoType
    {
        Todo,
        Fixme
    }

    public class TodoEntry : VisualElement
    {
        #region FACTORY & TRAITS

        public new class UxmlFactory : UxmlFactory<TodoEntry, UxmlTraits>
        {
            public override string uxmlName => nameof(TodoEntry);

            public override string uxmlNamespace => "BaseTool.Editor.Components";
        }

        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            protected readonly UxmlEnumAttributeDescription<TodoType> _todoType = new()
            {
                defaultValue = TodoType.Todo,
                name = "todo-type"
            };

            protected readonly UxmlStringAttributeDescription _text = new()
            {
                defaultValue = "Todo text...",
                name = "text"
            };

            protected readonly UxmlStringAttributeDescription _file = new()
            {
                defaultValue = "Assets/Scripts/Test.cs:00",
                name = "file"
            };

            protected readonly UxmlStringAttributeDescription _tags = new()
            {
                defaultValue = "ui,core,editor",
                name = "tags"
            };

            protected readonly UxmlStringAttributeDescription _names = new()
            {
                defaultValue = "Rewar",
                name = "names"
            };

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
                if (ve is not TodoEntry todoEntry) return;

                todoEntry.Text = _text.GetValueFromBag(bag, cc);
                todoEntry.File = _file.GetValueFromBag(bag, cc);
                todoEntry.Tags = _tags.GetValueFromBag(bag, cc).Split(",").ToList();
                todoEntry.Names = _names.GetValueFromBag(bag, cc).Split(",").ToList();
                todoEntry.TodoType = _todoType.GetValueFromBag(bag, cc);
            }
        }

        #endregion

        public const string UssClassname = "todo-entry";
        public const string TodoContainerUssClassname = "todo-container";
        public const string BottomContainerUssClassname = "todo-entry__bottom-container";
        public const string TagUssClassname = "tag";
        public const string FileUssClassname = "todo-file";
        public const string NamesUssClassname = "todo-names";
        public const string TodoTypeUssClassname = "todo-type";
        public const string FixmeTypeUssClassname = "fixme-type";

        private TodoType _todoType;
        public TodoType TodoType
        {
            get => _todoType;
            set
            {
                _todoType = value;
                UpdateTodoType();
            }
        }

        private string _text;
        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                _todoLabel.text = _text;
            }
        }

        private string _file;
        public string File
        {
            get => _file;
            set
            {
                _file = value;
                _fileLabel.text = _file;
            }
        }

        public List<string> _names = new List<string>();
        public List<string> Names
        {
            get => _names;
            set
            {
                _names = value;
                _namesLabel.text = NamesToString;
            }
        }
        public string NamesToString => _names == null ? string.Empty : string.Join(", ", _names);

        public List<string> _tags = new List<string>();
        public List<string> Tags
        {
            get => _tags;
            set
            {
                _tags = value;
                UpdateTags();
            }
        }

        private Label _todoLabel;
        private Label _fileLabel;
        private Label _namesLabel;

        private VisualElement _todoContainer;
        private VisualElement _bottomContainer;

        public Action<string> OnTagClicked;

        public TodoEntry()
        {
            AddToClassList(UssClassname);
            UpdateTodoType();

            _todoContainer = new();
            _todoContainer.AddToClassList(TodoContainerUssClassname);
            Add(_todoContainer);

            _bottomContainer = new();
            _bottomContainer.AddToClassList(BottomContainerUssClassname);
            Add(_bottomContainer);

            _todoLabel = new Label(Text);
            _todoContainer.Add(_todoLabel);

            _namesLabel = new Label(NamesToString);
            _namesLabel.AddToClassList(NamesUssClassname);
            _bottomContainer.Add(_namesLabel);

            _fileLabel = new Label(File);
            _fileLabel.AddToClassList(FileUssClassname);
            _bottomContainer.Add(_fileLabel);
        }

        private void UpdateTags()
        {
            _todoContainer.Query(className: TagUssClassname).ForEach(tag => tag.RemoveFromHierarchy());
            if (Tags == null) return;
            foreach (var tag in Tags)
            {
                var tagLabel = new Label(tag);
                tagLabel.AddToClassList(TagUssClassname);
                tagLabel.RegisterCallback<ClickEvent, string>((evt, tag) =>
                {
                    evt.StopPropagation();
                    OnTagClicked?.Invoke(tag);
                }, tag);
                _todoContainer.Add(tagLabel);
            }
        }

        private void UpdateTodoType()
        {
            RemoveFromClassList(TodoTypeUssClassname);
            RemoveFromClassList(FixmeTypeUssClassname);
            switch (_todoType)
            {
                case TodoType.Todo: AddToClassList(TodoTypeUssClassname); break;
                case TodoType.Fixme: AddToClassList(FixmeTypeUssClassname); break;
                default: break;
            }
        }
    }
}
