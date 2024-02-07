using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace BaseTool.Editor.Core.Wizard
{
    public class SetupWizard : EditorWindow
    {
        [SerializeField] private VisualTreeAsset _visualTreeAsset = default;

        private VisualElement _view;

        [MenuItem("Window/BaseTool/Setup")]
        public static void ShowWindow()
        {
            var window = GetWindow<SetupWizard>();
            window.minSize = new(600, 400);
            window.titleContent = new GUIContent("Setup Wizard");
        }

        public void CreateGUI()
        {
            // Each editor window contains a root VisualElement object
            VisualElement root = rootVisualElement;

            // Import UXML
            _view = _visualTreeAsset.Instantiate();
            root.Add(_view);

            var coreButton = _view.Q<Button>("CoreButton");
            coreButton.SetEnabled(false);

            BindButton("MovementButton", GlobalDefineInitializer.MovementDefine);
            BindButton("ShooterButton", GlobalDefineInitializer.ShooterDefine);
            BindButton("RPGButton", GlobalDefineInitializer.RPGDefine);
            // TODO: Re-enable when Roguelite features are implemented
            //BindButton("RogueliteButton", GlobalDefineInitializer.RogueliteDefine);
            BindButton("UIButton", GlobalDefineInitializer.UIDefine);

            var rogueButton = _view.Q<Button>("RogueliteButton");
            rogueButton.SetEnabled(false);

            var applyButton = _view.Q<Button>("ApplyButton");
            applyButton.RegisterCallback<ClickEvent>(OnApplyClicked);
        }

        private void BindButton(string buttonName, string categoryDefine)
        {
            var button = _view.Q<Button>(buttonName);
            button.RegisterCallback<ClickEvent>(ExecuteToggle);
            button.Q<Toggle>().SetValueWithoutNotify(GlobalDefineInitializer.HasGlobalDefine(categoryDefine));
        }

        private void ExecuteToggle(ClickEvent evt)
        {
            if (evt.target is not VisualElement ve) return;

            Toggle toggle = ve.Q<Toggle>();
            toggle.SetValueWithoutNotify(!toggle.value);
        }

        private void OnApplyClicked(ClickEvent evt)
        {
            if (!GlobalDefineInitializer.HasGlobalDefine(GlobalDefineInitializer.CoreDefine))
                GlobalDefineInitializer.AddGlobalDefine(GlobalDefineInitializer.CoreDefine);

            if (_view.Q<Button>("MovementButton").Q<Toggle>().value)
                GlobalDefineInitializer.AddGlobalDefine(GlobalDefineInitializer.MovementDefine);
            else
                GlobalDefineInitializer.RemoveGlobalDefine(GlobalDefineInitializer.MovementDefine);

            if (_view.Q<Button>("ShooterButton").Q<Toggle>().value)
                GlobalDefineInitializer.AddGlobalDefine(GlobalDefineInitializer.ShooterDefine);
            else
                GlobalDefineInitializer.RemoveGlobalDefine(GlobalDefineInitializer.ShooterDefine);

            if (_view.Q<Button>("RPGButton").Q<Toggle>().value)
                GlobalDefineInitializer.AddGlobalDefine(GlobalDefineInitializer.RPGDefine);
            else
                GlobalDefineInitializer.RemoveGlobalDefine(GlobalDefineInitializer.RPGDefine);

            if (_view.Q<Button>("RogueliteButton").Q<Toggle>().value)
                GlobalDefineInitializer.AddGlobalDefine(GlobalDefineInitializer.RogueliteDefine);
            else
                GlobalDefineInitializer.RemoveGlobalDefine(GlobalDefineInitializer.RogueliteDefine);

            if (_view.Q<Button>("UIButton").Q<Toggle>().value)
                GlobalDefineInitializer.AddGlobalDefine(GlobalDefineInitializer.UIDefine);
            else
                GlobalDefineInitializer.RemoveGlobalDefine(GlobalDefineInitializer.UIDefine);
        }
    }
}