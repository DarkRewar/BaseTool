using UnityEngine;

namespace BaseTool.UI
{
    [RequireComponent(typeof(Canvas))]
    [HelpURL("https://github.com/DarkRewar/BaseTool?tab=readme-ov-file#view")]
    public abstract class View : MonoBehaviour
    {
        protected Canvas _canvas;

        public Tree<View> Tree { get; protected set; } = new Tree<View>();

        public View Parent => Tree.Parent != null ? Tree.Parent.Current : null;

        public bool IsVisible => _canvas.enabled && (!Parent || Parent.IsVisible);

        protected virtual void Awake()
        {
            _canvas = GetComponent<Canvas>();
            SetupTree();
            Navigation.RegisterView(this);
        }

        /// <summary>
        /// Force the view (and its possible parent) to hide or display.
        /// </summary>
        /// <param name="state"></param>
        public virtual void Display(bool state)
        {
            _canvas.enabled = state;
            Parent?.Display(state);
        }

        /// <summary>
        /// Called when the <see cref="View"/> is displayed.
        /// </summary>
        /// <param name="fromView">The previous view to navigate from.</param>
        /// <param name="args">The arguments passed to the current view.</param>
        public virtual void OnNavigateFrom(View fromView, NavigationArgs args) { }

        /// <summary>
        /// Called when the <see cref="View"/> is left and the <see cref="Navigation"/> move
        /// to another page.
        /// </summary>
        /// <param name="toView">The next view to navigate to.</param>
        /// <param name="args">The arguments passed to the next view.</param>
        public virtual void OnNavigateTo(View toView, NavigationArgs args) { }

        /// <summary>
        /// Initialize the tree view with parent/child architecture link.
        /// The current view check if it's inside another view and 
        /// create the tree by adding itself to its parent.
        /// </summary>
        protected void SetupTree()
        {
            Tree.Current = this;
            if (transform.parent)
            {
                View parent = transform.parent.GetComponentInParent<View>();
                if (parent != null)
                {
                    Tree.Parent = parent.Tree;
                    parent.Tree.AddChild(this);
                }
            }
        }

        /// <summary>
        /// Find a view inside another one.<br/>
        /// If it's one of its children and it exists, fills the
        /// out parameter with the view and returns true.<br/>
        /// If there is no view found, return false.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="childView"></param>
        /// <returns></returns>
        internal bool TryFindInChildren<T>(out T childView) where T : View
        {
            foreach (Tree<View> aView in Tree)
            {
                if (aView.Current is T child)
                {
                    childView = child;
                    return true;
                }

                if (aView.Current.TryFindInChildren(out childView))
                {
                    return true;
                }
            }
            childView = null;
            return false;
        }
    }
}