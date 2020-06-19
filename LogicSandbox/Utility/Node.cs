namespace Maxstupo.LogicSandbox.Utility {

    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a tree structure, each node has multiple children nodes and one parent node.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Node<T> : IEnumerable<T> where T : Node<T> {

        private T parent = null;
        /// <summary>
        /// The parent of this node. Updates child nodes in parent node automatically, and invokes required events.
        /// </summary>
        public T Parent {
            get => parent;
            set {
                if (value == this)
                    throw new ArgumentException("The parent cant be itself", nameof(Parent));

                if (parent != null && parent != value) {
                    T node = (T) this;
                    if (parent.children.Remove(node))
                        parent.OnChildrenChange(node, false);
                }

                if (value != null && value != parent) {
                    T node = (T) this;
                    value.children.Add(node);
                    value.OnChildrenChange(node, true);
                }

                parent = value;
                OnParentChanged();
            }
        }

        private readonly List<T> children = new List<T>();
        public IReadOnlyList<T> Children => children.AsReadOnly();

        /// <summary>
        /// True if the node has no parent.
        /// </summary>
        public bool IsRoot => parent == null;

        /// <summary>
        /// True if this node has no children and has a parent node.
        /// </summary>
        public bool IsLeaf => !IsRoot && children.Count == 0;

        /// <summary>
        /// The number of children this node contains.
        /// </summary>
        public int ChildCount => children.Count;

        /// <summary>
        /// Invoked when the parent node changes.
        /// </summary>
        protected virtual void OnParentChanged() {

        }

        /// <summary>
        /// Invoked when a child node is added or removed.
        /// </summary>
        /// <param name="node">The child object that was added/removed.</param>
        /// <param name="added">True if the node was added, false if removed.</param>
        protected virtual void OnChildrenChange(T node, bool added) {

        }

        public IEnumerator<T> GetEnumerator() {
            return children.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

    }

}