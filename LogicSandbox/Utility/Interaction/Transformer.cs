namespace Maxstupo.LogicSandbox.Utility.Interaction {

    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;

    public sealed class Transformer<T> where T : IRectangle {

        private sealed class TransformerItem {
            public T obj;
            public float originX;
            public float originY;
        }

        public bool IsMoving { get; private set; }

        public RectangleF Selection { get; private set; }
        private RectangleF originalSelection;

        private float originX;
        private float originY;

        private readonly List<TransformerItem> items = new List<TransformerItem>();
   
        public List<T> Items => items.Select(x => x.obj).ToList();

        public int Count => items.Count;


        public event EventHandler OnStartMove;
        public event EventHandler OnMoving;
        public event EventHandler OnEndMove;

        public bool AddItem(T item) {
            if (IsMoving)
                return false;
            if (items.Any(x => x.obj.Equals(item)))
                return false;

            items.Add(new TransformerItem {
                obj = item,
                originX = item.X,
                originY = item.Y
            });

            return true;
        }

        public void AddItem(IEnumerable<T> items) {
            foreach (T t in items)
                AddItem(t);
        }

        public void Clear() {
            items.Clear();
        }

        public void CalculateBounds() {
            float minX = float.MaxValue, minY = float.MaxValue;
            float maxX = float.MinValue, maxY = float.MinValue;

            foreach (TransformerItem item in items) {
                minX = Math.Min(minX, item.obj.X);
                minY = Math.Min(minY, item.obj.Y);
                maxX = Math.Max(maxX, item.obj.X + item.obj.Width);
                maxY = Math.Max(maxY, item.obj.Y + item.obj.Height);
            }

            Selection = new RectangleF(minX, minY, maxX - minX, maxY - minY);
        }


        public bool StartDrag(float x, float y) {
            if (IsMoving)
                return false;

            CalculateBounds();

            IsMoving = true;

            originX = x;
            originY = y;
            originalSelection = new RectangleF(Selection.X, Selection.Y, Selection.Width, Selection.Height);


            foreach (TransformerItem item in items) {
                item.originX = item.obj.X;
                item.originY = item.obj.Y;
            }

            OnStartMove?.Invoke(this, EventArgs.Empty);

            return true;
        }

        public bool Drag(float x, float y) {
            if (!IsMoving)
                return false;

            float dx = x - originX;
            float dy = y - originY;

            float nx = originalSelection.X + dx;
            float ny = originalSelection.Y + dy;

            Selection = new RectangleF(nx, ny, Selection.Width, Selection.Height);

            foreach (TransformerItem item in items) {
                item.obj.X = item.originX + dx;
                item.obj.Y = item.originY + dy;
            }

            OnMoving?.Invoke(this, EventArgs.Empty);

            return true;
        }

        public void EndDrag() {
            if (!IsMoving)
                return;
            IsMoving = false;
            OnEndMove?.Invoke(this, EventArgs.Empty);
        }

    }

}