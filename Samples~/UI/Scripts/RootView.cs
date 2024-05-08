using BaseTool.UI;

public class ThreadNavigationArgs : NavigationArgs
{
    public int UserId;
    public string Token;
}

public class RootView : View
{
    public void OnHomeViewClicked()
    {
        Navigation.Open<HomeView>();
    }

    public void OnSettingsViewClicked()
    {
        Navigation.Open<RootView, SettingsView>();
    }

    public void OnMyThreadViewClicked()
    {
        var args = new ThreadNavigationArgs
        {
            UserId = 46553,
            Token = "123-456-789"
        };
        Navigation.Open<RootView, MyThreadView>(args);
    }

    public void OnFriendThreadViewClicked()
    {
        Navigation.Open<RootView, FriendThreadView>();
    }
}
