namespace Maxstupo.LogicSandbox.Utility.Interaction {

    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;

    /// <summary>
    /// Handles selecting rectangular items using a selection box or clicking on items. An optional interface <see cref="ISelectable"/> can be implemented on the selection objects to provide selection notifcations.
    /// </summary>
    public sealed class Selector<T> where T : IRectangle {

        /// <summary>Returns true if the selection box is currently being dragged.</summary>
        public bool IsDragging { get; private set; }

        /// <summary>Returns the selection bounding box.</summary>
        public RectangleF Selection { get; private set; }

        /// <summary>If false, the <see cref="Draw(Graphics)"/> method wont draw the selection box.</summary>
        public bool ShowSelection { get; set; }
        public Color SelectionColor { get; set; } = Color.Blue;
        public float SelectionThickness { get; set; } = 1.0f;
        public float[] SelectionDashPattern { get; set; } = { 3, 3, 3, 3 };

        /// <summary>The item source for the selection process.</summary>
        public List<T> ItemSource { get; set; }


        private readonly List<T> selectedItems = new List<T>();
        /// <summary>The currently selected items.</summary>
        public IReadOnlyList<T> SelectedItems => selectedItems.AsReadOnly();

        /// <summary>Invoked when a rectangular selection operation begins. <see cref="StartDrag(float, float, bool)"/></summary>
        public event EventHandler OnStartDrag;

        /// <summary>Invoked when a rectangular selection operation updates. <see cref="Drag(float, float)"/></summary>
        public event EventHandler OnDragging;

        /// <summary>Invoked when a rectangular selection operation finishes. Provided event argument is the selected items. <see cref="EndDrag(float, float, bool, bool)"/></summary>
        public event EventHandler<List<T>> OnEndDrag;

        private float startX;
        private float startY;

        public Selector(List<T> items) {
            ItemSource = items ?? new List<T>();
        }

        public bool Start(float x, float y, bool additiveMode, Func<float, float, T> hitTest) {
            T item = (hitTest != null) ? hitTest(x, y) : default;
            if (item == null) {
                StartDrag(x, y);
                return true;
            }

            IReadOnlyList<T> selectedItems = SelectedItems;

            bool containsShape = selectedItems.Contains(item);

            if (additiveMode && !containsShape) {
                Select(item);

            } else if ((selectedItems.Count == 1 && !selectedItems[0].Equals(item)) || !containsShape) {
                DeselectAll();
            }

            if (selectedItems.Count == 0)
                Select(item);

            return false;
        }

        /// <summary>
        /// Starts a selection box drag operation. Invokes the <see cref="OnStartDrag"/> event. If a dragging operation is already taking place, no action will be taken.
        /// </summary>
        public void StartDrag(float x, float y, bool showSelection = true) {
            if (IsDragging)
                return;

            startX = x;
            startY = y;

            IsDragging = true;
            ShowSelection = showSelection;

            OnStartDrag?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Updates the current selection box drag operation with the provided position. Invokes the <see cref="OnDragging"/> event.
        /// </summary>
        /// <returns>True if we are in a dragging operation, false otherwise.</returns>
        public bool Drag(float x, float y) {
            if (!IsDragging)
                return false;

            UpdateSelection(x, y);

            OnDragging?.Invoke(this, EventArgs.Empty);
            return true;
        }

        /// <summary>
        /// Ends a selection box drag operation. Invokes the <see cref="OnEndDrag"/> event. Updates the <see cref="SelectedItems"/> list based off the <see cref="ItemSource"/> and notifies items of selection <see cref="ISelectable.OnSelected"/>
        /// </summary>
        /// <param name="appendSelection">If true, previous selected items wont be cleared.</param>
        /// <param name="containsMode">If true, items must be fully contained within the selection bounds.</param>
        /// <returns>True if we are in a dragging operation, false otherwise.</returns>
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

        /// <summary>Selects the specified item, and notifies it - <see cref="ISelectable.OnSelected"/></summary>
        public void Select(T item) {
            selectedItems.Add(item);
            NotifyItems(true);
        }

        /// <summary>Selects all currently available items from the <see cref="ItemSource"/>. Notifies items - <see cref="ISelectable.OnSelected"/></summary>
        public void SelectAll() {
            selectedItems.Clear();
            selectedItems.AddRange(ItemSource);
            NotifyItems(true);
        }

        /// <summary>Deselects all currently selected items. Notifies previously selected items - <see cref="ISelectable.OnDeselected"/></summary>
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

        /// <summary>Draw the selection bounding box only when the selection is being dragged and <see cref="ShowSelection"/> is true.</summary>
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