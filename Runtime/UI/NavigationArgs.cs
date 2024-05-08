namespace BaseTool.UI
{
    public class NavigationArgs
    {
        private static NavigationArgs _empty;
        public static NavigationArgs Empty => _empty ??= new NavigationArgs();
    }
}