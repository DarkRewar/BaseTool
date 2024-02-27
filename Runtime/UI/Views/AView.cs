using UnityEngine;

namespace BaseTool.UI.Views
{
    [RequireComponent(typeof(Canvas))]
    public abstract class AView : MonoBehaviour
    {
        protected Canvas _canvas;

        public Tree<AView> Tree { get; protected set; } = new Tree<AView>();

        public AView Parent => Tree.Parent != null ? Tree.Parent.Current : null;

        protected virtual void Awake()
        {
            _canvas = GetComponent<Canvas>();
            SetupTree();
            Navigation.RegisterView(this);
        }

        public virtual void Display(bool state)
        {
            _canvas.enabled = state;
            Parent?.Display(state);
        }

        /// <summary>
        /// Called when the <see cref="AView"/> is displayed.
        /// </summary>
        /// <param name="fromView">The previous view to navigate from.</param>
        /// <param name="args">The arguments passed to the current view.</param>
        public virtual void OnNavigateFrom(AView fromView, NavigationArgs args)
        {
        }

        /// <summary>
        /// Called when the <see cref="AView"/> is left and the <see cref="Navigation"/> move
        /// to another page.
        /// </summary>
        /// <param name="toView">The next view to navigate to.</param>
        /// <param name="args">The arguments passed to the next view.</param>
        public virtual void OnNavigateTo(AView toView, NavigationArgs args)
        {
        }

        protected void SetupTree()
        {
            Tree.Current = this;
            if (transform.parent)
            {
                AView parent = transform.parent.GetComponentInParent<AView>();
                if (parent != null)
                {
                    Tree.Parent = parent.Tree;
                    parent.Tree.AddChild(this);
                }
            }
        }

        internal bool FindInChildren<T2>(out T2 childView) where T2 : AView
        {
            foreach (Tree<AView> aView in Tree)
            {
                if (aView.Current is T2 child)
                {
                    childView = child;
                    return true;
                }

                if (aView.Current.FindInChildren(out childView))
                {
                    return true;
                }
            }
            childView = null;
            return false;
        }
    }
}