using System.Collections.Generic;
using BaseTool.Generic.Utils;
using BaseTool.UI.Views;

namespace BaseTool.UI
{
    public static class Navigation
    {
        public static NavigationEntry CurrentView => _history.Count != 0 ? _history.Peek() : null;

        private static Stack<NavigationEntry> _history = new Stack<NavigationEntry>();

        private static List<AView> _registeredViews = new List<AView>();

        private static Tree<AView> _treeViews = new Tree<AView>();

        public static void RegisterView(AView page)
        {
            _registeredViews.Add(page);
            if(!page.Parent)
            {
                _treeViews.AddChild(page.Tree);
            }
        }

        private static bool TryGetRegisteredView<T>(out T view) where T : AView
        {
            view = null;
            foreach(AView aView in _registeredViews)
            { 
                if(aView is T castedPage)
                {
                    view = castedPage;
                    return true;
                }
            }
            return false;
        }

        private static bool TryGetRegisteredViews<T>(out T[] views) where T : AView
        {
            List<T> foundViews = new List<T>();
            foreach(AView aView in _registeredViews)
            { 
                if(aView is T castedPage)
                {
                    foundViews.Add(castedPage);
                }
            }
            views = foundViews.ToArray();
            return foundViews.Count > 0;
        }

        public static void Open<T>(NavigationArgs args = null) where T : AView
        {
            if(TryGetRegisteredView(out T view))
            {
                Open(view, args);
            }
        }

        public static void Open(AView view, NavigationArgs args = null)
        {
            var navigationEntry = CurrentView;
            navigationEntry?.View.Display(false);
            navigationEntry?.View.OnNavigateTo(view, args);
            _history.Push(new NavigationEntry(view, args));
            view.Display(true);
            view?.OnNavigateFrom(navigationEntry?.View, args);
        }

        public static void Open<T1, T2>(NavigationArgs args = null) where T1 : AView where T2 : AView
        {
            if(TryGetRegisteredViews(out T1[] views))
            {
                foreach(T1 aView in views)
                {
                    if(aView.FindInChildren(out T2 childView))
                    {
                        Open(childView, args);
                    }
                }
            }
        }

        public static void Close<T>() where T : AView
        {
            if (TryGetRegisteredView(out T view))
            {
                view.Display(false);
            }
        }

        public static void Back()
        {
            if(_history.Count != 0)
            {
                NavigationEntry navigationEntry = _history.Pop();
                navigationEntry?.View.Display(false);
                if(CurrentView != null)
                    navigationEntry?.View.OnNavigateTo(CurrentView.View, CurrentView.Args);

                CurrentView?.View.Display(true);
                CurrentView?.View.OnNavigateFrom(navigationEntry?.View, navigationEntry?.Args);
            }
        }
    }
}