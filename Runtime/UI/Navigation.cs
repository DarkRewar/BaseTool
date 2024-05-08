using System.Collections.Generic;
using UnityEngine;

namespace BaseTool.UI
{
    public static class Navigation
    {
        public static NavigationEntry CurrentView => _history.Count != 0 ? _history.Peek() : null;

        private static Stack<NavigationEntry> _history = new Stack<NavigationEntry>();

        private static List<View> _registeredViews = new List<View>();

        private static Tree<View> _treeViews = new Tree<View>();

        internal static void RegisterView(View page)
        {
            _registeredViews.Add(page);
            if (!page.Parent)
            {
                _treeViews.AddChild(page.Tree);
            }

            if (_history.Count == 0 && page.IsVisible)
                _history.Push(new NavigationEntry(page, NavigationArgs.Empty));
        }

        private static bool TryGetRegisteredView<T>(out T view) where T : View
        {
            foreach (View aView in _registeredViews)
            {
                if (aView is T castedPage)
                {
                    view = castedPage;
                    return true;
                }
            }
            view = null;
            return false;
        }

        private static bool TryGetRegisteredViews<T>(out T[] views) where T : View
        {
            List<T> foundViews = new List<T>();
            foreach (View aView in _registeredViews)
            {
                if (aView is T castedPage)
                {
                    foundViews.Add(castedPage);
                }
            }
            views = foundViews.ToArray();
            return foundViews.Count > 0;
        }

        /// <summary>
        /// Open a view by its type. If the view does not exist,
        /// do nothing.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="args"></param>
        public static void Open<T>(NavigationArgs args = null) where T : View
        {
            if (TryGetRegisteredView(out T view))
            {
                Open(view, args);
            }
        }

        /// <summary>
        /// Open a view by its type. If the view does not exist,
        /// do nothing.
        /// </summary>
        /// <param name="view"></param>
        /// <param name="args"></param>
        public static void Open(View view, NavigationArgs args = null)
        {
            var navigationEntry = CurrentView;
            navigationEntry?.View.Display(false);
            navigationEntry?.View.OnNavigateTo(view, args);
            _history.Push(new NavigationEntry(view, args));
            view.Display(true);
            view?.OnNavigateFrom(navigationEntry?.View, args);
        }

        /// <summary>
        /// Try to open a view following the parenthood hierarchy.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="args"></param>
        public static void Open<T1, T2>(NavigationArgs args = null) where T1 : View where T2 : View
        {
            if (TryGetRegisteredViews(out T1[] views))
            {
                foreach (T1 aView in views)
                {
                    if (aView.TryFindInChildren(out T2 childView))
                    {
                        Open(childView, args);
                    }
                }
            }
        }

        /// <summary>
        /// Close the view by type used.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void Close<T>() where T : View
        {
            if (TryGetRegisteredView(out T view))
            {
                view.Display(false);
            }
        }

        /// <summary>
        /// If possible, the navigation will close the current view
        /// and open the previous one registered in the navigation
        /// history. <br/>
        /// If there is no history view left, it will stay on the
        /// active opened view.
        /// </summary>
        public static void Back()
        {
            if (_history.Count > 1)
            {
                NavigationEntry navigationEntry = _history.Pop();
                navigationEntry?.View.Display(false);
                if (CurrentView != null)
                    navigationEntry?.View.OnNavigateTo(CurrentView.View, CurrentView.Args);

                CurrentView?.View.Display(true);
                CurrentView?.View.OnNavigateFrom(navigationEntry?.View, navigationEntry?.Args);
            }
        }

        /// <summary>
        /// Will hide every views and empty the navigation history.<br/>
        /// Use this method with caution!
        /// </summary>
        public static void Clear()
        {
            foreach (var view in _registeredViews)
                view.Display(false);

            _history.Clear();
            _registeredViews.Clear();
            _treeViews = new Tree<View>();
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void BeforeSceneLoad()
        {
            _history.Clear();
            _registeredViews.Clear();
            _treeViews = new Tree<View>();
        }
    }
}