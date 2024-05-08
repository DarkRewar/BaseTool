using BaseTool.UI;
using UnityEngine;
using UnityEngine.UI;

public class MyThreadView : View
{
    [SerializeField] private Text _text;

    public override void OnNavigateFrom(View fromView, NavigationArgs args)
    {
        if (args is ThreadNavigationArgs threadArgs)
        {
            _text.text = $"{threadArgs.UserId} - {threadArgs.Token}";
        }
    }

    public override void OnNavigateTo(View toView, NavigationArgs args)
    {
        base.OnNavigateTo(toView, args);
    }
}
