namespace Maxstupo.LogicSandbox.Utility {

    using System;
    using System.Collections;
    using System.Collections.Generic;
  
    /// <summary>
    /// Represents an event for <see cref="Node{T}"/> when a child node is added/removed.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class NodeChangeEvent<T> : EventArgs where T : Node<T> {
        
        public T Node { get; }

        /// <summary>
        /// If true the node was added, else it was removed.
        /// </summary>
        public bool Added { get; }

        /// <summary>
        /// Constructs a <see cref="NodeChangeEvent{T}"/>.
        /// </summary>
        /// <param name="node">The node in question.</param>
        /// <param name="added">If true the node was added, else it was removed.</param>
        public NodeChangeEvent(T node, bool added) {
            Node = node;
            Added = added;
        }

    }

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
                        parent.OnChildrenChange?.Invoke(parent, new NodeChangeEvent<T>(node, false));
                }

                if (value != null && value != parent) {
                    T node = (T) this;
                    value.children.Add(node);
                    value.OnChildrenChange?.Invoke(value, new NodeChangeEvent<T>(node, true));
                }

                parent = value;
                OnParentChange?.Invoke(this, EventArgs.Empty);
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
        /// Occurs when the parent node changes.
        /// </summary>
        public event EventHandler OnParentChange;

        /// <summary>
        /// Occurs when a child node is added or removed.
        /// </summary>
        public event EventHandler<NodeChangeEvent<T>> OnChildrenChange;

        public IEnumerator<T> GetEnumerator() {
            return children.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

    }

}