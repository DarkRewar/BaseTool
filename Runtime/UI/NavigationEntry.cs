using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseTool.UI.Views;

namespace BaseTool.UI
{
    public class NavigationEntry
    {
        public AView View { get; private set; }

        public NavigationArgs Args { get; private set; }

        public NavigationEntry(AView view, NavigationArgs args)
        {
            View = view;
            Args = args;
        }
    }
}
