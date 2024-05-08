namespace BaseTool.UI
{
    public class NavigationEntry
    {
        public View View { get; private set; }

        public NavigationArgs Args { get; private set; }

        public NavigationEntry(View view, NavigationArgs args)
        {
            View = view;
            Args = args;
        }
    }
}
