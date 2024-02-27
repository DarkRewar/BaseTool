using System.Collections;
using System.Collections.Generic;

namespace BaseTool
{
    public class Tree<T> : IEnumerable<Tree<T>>
    {
        public Tree<T> Parent;

        public List<Tree<T>> Children { get; private set; }

        public T Current;

        public Tree<T> this[int index]
        {
            get => Children[index];
            set => Children[index] = value;
        }

        public Tree()
        {
            Children = new List<Tree<T>>();
        }

        public Tree(T currentObj) : base()
        {
            Current = currentObj;
        }

        public void AddChild(T child)
        {
            Children.Add(new Tree<T>(child));
        }

        public void AddChild(Tree<T> child)
        {
            Children.Add(child);
        }

        public IEnumerator<Tree<T>> GetEnumerator() => Children.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => Children.GetEnumerator();
    }
}