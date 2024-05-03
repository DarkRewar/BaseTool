using UnityEngine;
using UnityEngine.UIElements;

namespace BaseTool
{
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
}