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

        public bool Displayed { get; private set; } = false;

        private float _previousTimeScale = 1;

        internal bool IsKeyPressed =>
            !Console.Settings.UseCustomInput
            && Input.GetKeyDown(Console.Settings.ToggleKeyCode)
            && (!Console.Settings.ToggleKeyCodeCtrl || Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
            && (!Console.Settings.ToggleKeyCodeAlt || Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt));

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

        private void OnDestroy() => Console.RemoveCommands();

        private void Update()
        {
            Console.ConsoleUpdate();

            if (Console.Settings.UseCustomInput) return;

#if ENABLE_LEGACY_INPUT_MANAGER
            if (!IsKeyPressed) return;
            Toggle();
#else
            enabled = false;
            throw new System.Exception("The old input system must be set to use Console Manager!");
#endif
        }

        /// <summary>
        /// Display or hide the console UI, based on <see cref="Displayed"/> value.
        /// </summary>
        public void Toggle()
        {
            if (!Displayed) _previousTimeScale = Time.timeScale;

            Displayed = !Displayed;
            _uiDocument.rootVisualElement.style.display =
                Displayed
                    ? DisplayStyle.Flex
                    : DisplayStyle.None;
            if (Displayed) _textField.ElementAt(0).Focus();

            Time.timeScale = Displayed ? Console.Settings.OpenedTimeScale : _previousTimeScale;
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