namespace Maxstupo.LogicSandbox.Utility.Interaction {

    using System;
    using System.Collections.Generic;
    using System.Drawing;

    /// <summary>
    /// Handles selecting rectangular items using a selection box. An optional interface <see cref="ISelectable"/> can be implemented on the selection objects to provide selection notifcations.
    /// </summary>
    public sealed class Selector<T> where T : IRectangle {

        public bool IsDragging { get; private set; }

        public RectangleF Selection { get; private set; }

        public bool ShowSelection { get; set; }
        public Color SelectionColor { get; set; } = Color.Blue;
        public float SelectionThickness { get; set; } = 1.0f;
        public float[] SelectionDashPattern { get; set; } = { 3, 3, 3, 3 };

        /// <summary>The item source for the selection process.</summary>
        public List<T> ItemSource { get; set; }


        private readonly List<T> selectedItems = new List<T>();
        /// <summary>The currently selected items.</summary>
        public IReadOnlyList<T> SelectedItems => selectedItems.AsReadOnly();


        public event EventHandler OnStartDrag;
        public event EventHandler OnDragging;
        public event EventHandler<List<T>> OnEndDrag;

        private float startX;
        private float startY;


        public Selector(List<T> items) {
            ItemSource = items ?? new List<T>();
        }

        public void StartDrag(float x, float y, bool showSelection = true) {
            if (IsDragging)
                return;

            startX = x;
            startY = y;

            IsDragging = true;
            ShowSelection = showSelection;

            OnStartDrag?.Invoke(this, EventArgs.Empty);
        }

        public bool Drag(float x, float y) {
            if (!IsDragging)
                return false;

            UpdateSelection(x, y);

            OnDragging?.Invoke(this, EventArgs.Empty);
            return true;
        }

        public bool EndDrag(float x, float y, bool appendSelection, bool containsMode) {
            if (!IsDragging)
                return false;

            UpdateSelection(x, y);

            IsDragging = false;
            ShowSelection = false;

            NotifyItems(false);

            if (!appendSelection)
                selectedItems.Clear();

            selectedItems.AddRange(GetSelectedItems(containsMode));

            NotifyItems(true);

            OnEndDrag?.Invoke(this, selectedItems);
            return true;
        }

        public void SelectAll() {
            selectedItems.Clear();
            selectedItems.AddRange(ItemSource);
            NotifyItems(true);
        }

        public void DeselectAll() {
            NotifyItems(false);
            selectedItems.Clear();
        }

        public void InvertSelection() {
            throw new NotImplementedException();
        }

        private void NotifyItems(bool isSelected) {
            foreach (T item in selectedItems) {
                if (item is ISelectable selectable) {
                    if (isSelected) {
                        selectable.OnSelected();
                    } else {
                        selectable.OnDeselected();
                    }
                }
            }
        }

        private IEnumerable<T> GetSelectedItems(bool containsMode) {
            foreach (T rect in ItemSource) {
                RectangleF componentRect = new RectangleF(rect.X, rect.Y, rect.Width, rect.Height);

                if (Selection.Contains(componentRect) || (!containsMode && Selection.IntersectsWith(componentRect)))
                    yield return rect;
            }
        }

        private void UpdateSelection(float x, float y) {
            float tlX = (x - startX < 0) ? x : startX;
            float tlY = (y - startY < 0) ? y : startY;

            float width = Math.Abs(x - startX);
            float height = Math.Abs(y - startY);

            Selection = new RectangleF(tlX, tlY, width, height);
        }

        public void Draw(Graphics g) {
            if (!IsDragging || !ShowSelection)
                return;

            using (Pen pen = new Pen(SelectionColor, SelectionThickness)) {
                pen.DashPattern = SelectionDashPattern;
                g.DrawRectangle(pen, Selection.X, Selection.Y, Selection.Width, Selection.Height);
            }
        }

    }

}