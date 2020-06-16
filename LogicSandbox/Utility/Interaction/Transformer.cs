namespace Maxstupo.LogicSandbox.Utility.Interaction {

    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;

    /// <summary>
    /// Handles moving a collection of items, while maintaining the original spacing between each item.
    /// </summary>
    public sealed class Transformer<T> where T : IRectangle {

        /// <summary>
        /// A inner class that has the item and a origin point used to ensure items maintain the original spacing relative to one another.
        /// </summary>
        private sealed class TransformerItem {

            public T Item { get; }

            public float OriginX { get; set; }
            public float OriginY { get; set; }

            public TransformerItem(T item) {
                Item = item;
                OriginX = item.X;
                OriginY = item.Y;
            }

        }

        /// <summary>Returns true when a move operation is currently occuring.</summary>
        public bool IsMoving { get; private set; }

        /// <summary>Returns the smallest bounding box possible, encompassing all items from this transformer.</summary>
        public RectangleF Selection { get; private set; }
        private RectangleF originalSelection;

        private float originX;
        private float originY;

        private readonly List<TransformerItem> items = new List<TransformerItem>();
        public int ItemCount => items.Count;

        /// <summary>Invoked when move operation starts. <see cref="StartDrag(float, float)"/></summary>
        public event EventHandler OnStartMove;

        /// <summary>Invoked when a move operation is updated. <see cref="Drag(float, float)"/></summary>
        public event EventHandler OnMoving;

        /// <summary>Invoked when a move operation finishes. <see cref="EndDrag"/></summary>
        public event EventHandler OnEndMove;


        /// <summary>
        /// Adds an item that will be moved when a move operation occurs. See <see cref="StartDrag(float, float)"/>
        /// </summary>
        /// <returns>True if the item was added, false if a move operation is running or the item has already been added.</returns>
        public bool AddItem(T item) {
            if (IsMoving)
                return false;
            if (items.Any(x => x.Item.Equals(item)))
                return false;

            items.Add(new TransformerItem(item));

            return true;
        }

        /// <summary>
        /// Adds all items, that will be moved when a move operation occurs. See <see cref="StartDrag(float, float)"/>
        /// </summary>
        public void AddItems(IEnumerable<T> items) {
            foreach (T t in items)
                AddItem(t);
            CalculateBounds();
        }

        /// <summary>
        /// Clears all items that would have been moved when a move operation occured.
        /// </summary>
        public void Clear() {
            items.Clear();
        }


        private void CalculateBounds() {
            float minX = float.MaxValue, minY = float.MaxValue;
            float maxX = float.MinValue, maxY = float.MinValue;

            foreach (TransformerItem item in items) {
                minX = Math.Min(minX, item.Item.X);
                minY = Math.Min(minY, item.Item.Y);
                maxX = Math.Max(maxX, item.Item.X + item.Item.Width);
                maxY = Math.Max(maxY, item.Item.Y + item.Item.Height);
            }

            Selection = new RectangleF(minX, minY, maxX - minX, maxY - minY);
        }

        /// <summary>
        /// Starts a move operation.
        /// </summary>
        /// <returns>True if the move operation started, false if a move operation was already running.</returns>
        public bool StartDrag(float x, float y) {
            if (IsMoving)
                return false;

            CalculateBounds();

            IsMoving = true;

            originX = x;
            originY = y;
            originalSelection = new RectangleF(Selection.X, Selection.Y, Selection.Width, Selection.Height);


            foreach (TransformerItem item in items) {
                item.OriginX = item.Item.X;
                item.OriginY = item.Item.Y;
            }

            OnStartMove?.Invoke(this, EventArgs.Empty);

            return true;
        }

        /// <summary>
        /// Updates a move operation based of the provided position. <see cref="OnMoving"/>
        /// </summary>
        /// <returns>True if a move operation is running.</returns>
        public bool Drag(float x, float y) {
            if (!IsMoving)
                return false;

            float dx = x - originX;
            float dy = y - originY;

            float nx = originalSelection.X + dx;
            float ny = originalSelection.Y + dy;

            Selection = new RectangleF(nx, ny, Selection.Width, Selection.Height);

            foreach (TransformerItem item in items) {
                item.Item.X = item.OriginX + dx;
                item.Item.Y = item.OriginY + dy;
            }

            OnMoving?.Invoke(this, EventArgs.Empty);

            return true;
        }

        /// <summary>
        /// Finishes a move operation. <see cref="OnEndMove"/>
        /// </summary>
        public void EndDrag() {
            if (!IsMoving)
                return;
            IsMoving = false;
            OnEndMove?.Invoke(this, EventArgs.Empty);
        }

    }

}