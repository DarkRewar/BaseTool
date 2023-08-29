using System;
using UnityEngine;
using UnityEngine.UIElements;
using Object = System.Object;

namespace BaseTool.Core.Consoles
{
    [RequireComponent(typeof(UIDocument))]
    public class ConsoleManager : MonoBehaviour
    {
        [SerializeField] private PanelSettings _panelSettings;
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
            _textField.RegisterCallback<ChangeEvent<string>>(OnConsoleFieldChanged);
            _textField.RegisterCallback<KeyDownEvent>(OnConsoleFieldKeyDown);
        }

        private void Update()
        {
            Console.ConsoleUpdate();
            
            if (!Input.GetKeyDown(KeyCode.F4)) return;
            Toggle();
        }

        private void Toggle()
        {
            _displayed = !_displayed;
            _uiDocument.rootVisualElement.style.display = 
                _displayed 
                    ? DisplayStyle.Flex 
                    : DisplayStyle.None;
        }

        public void WriteLine(string txt)
        {
            _scrollView.Add(new Label(txt));
        }

        private void OnConsoleFieldChanged(ChangeEvent<string> evt)
        {
            //throw new NotImplementedException();
        }

        private void OnConsoleFieldKeyDown(KeyDownEvent evt)
        {
            if (evt.keyCode == KeyCode.Tab)
            {
                _textField.SetValueWithoutNotify(Console.TabComplete(_textField.text));
            }
            
            if (evt.keyCode != KeyCode.Return) return;

            Console.EnqueueCommand(_textField.text);
            _textField.SetValueWithoutNotify(null);
        }
    }
}