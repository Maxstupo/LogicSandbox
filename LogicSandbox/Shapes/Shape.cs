namespace Maxstupo.LogicSandbox.Shapes {

    using System;
    using System.Drawing;
    using Maxstupo.LogicSandbox.Utility;
    using Maxstupo.LogicSandbox.Utility.Interaction;

    /// <summary>
    /// Represents a 2D shape, that can have sub-shapes. Provides methods for drawing, and supports positioning relative to parent.
    /// </summary>
    // Implements rectangle interface as any shape can have a rectangular bounding-box
    public abstract class Shape : Node<Shape>, IRectangle {

        private float x;
        /// <summary>The local position along the x-axis.</summary>
        public float X { get => x; set { x = value; UpdateGlobalPosition(); } }

        private float y;
        /// <summary>The local position along the y-axis.</summary>
        public float Y { get => y; set { y = value; UpdateGlobalPosition(); } }


        private float width;
        /// <summary>The width of the shape.</summary>
        public virtual float Width { get => width; set { width = value; UpdateGlobalPosition(); } }

        private float height;
        /// <summary>The height of the shape.</summary>
        public virtual float Height { get => height; set { height = value; UpdateGlobalPosition(); } }


        /// <summary>The global position along the x-axis, factoring in locations of all ancestor shapes.</summary>
        public float GlobalX { get; private set; }

        /// <summary>The global position along the y-axis, factoring in locations of all ancestor shapes.</summary>
        public float GlobalY { get; private set; }


        private bool percentagePosition;
        /// <summary>If true, <see cref="X"/> and <see cref="Y"/> should be between zero and one, representing a percentage of the size of the component.</summary>
        public bool PercentagePosition {
            get => percentagePosition;
            set {
                if (percentagePosition != value) {
                    percentagePosition = value;

                    if (percentagePosition)
                        UpdateGlobalPosition();
                }
            }
        }

        public Color? BackgroundColor { get; set; } = Color.Gray;

        public Color? OutlineColor { get; set; } = null;
        public float OutlineThickness { get; set; } = 0;

        public float CornerRadius { get; set; } = 0;


        public bool IsMouseOver { get; private set; }
        public bool IsMousePressed { get; private set; }

        public event EventHandler OnMouseEntered;
        public event EventHandler OnMouseLeft;

        public event EventHandler OnMousePressed;
        public event EventHandler OnMouseReleased;

        public Shape(float x, float y, float width, float height) {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        protected override void OnParentChanged() {
            base.OnParentChanged();
            UpdateGlobalPosition();
        }

        protected abstract void DrawShape(Graphics g);

        public virtual void Draw(Graphics g) {
            DrawShape(g);

            foreach (Shape child in this)
                child.Draw(g);
        }


        public abstract bool ContainsPoint(float x, float y);

        /// <summary>
        ///  Updates the <see cref="IsMouseOver"/> and invokes required method events.
        /// </summary>
        /// <returns>True if some state changed, requiring the shape to be redrawn.</returns>
        public bool UpdateState(float x, float y) {
            UpdateCursorState(x, y, out bool needsRefresh);
            return needsRefresh;
        }

        /// <summary>
        /// Updates the <see cref="IsMouseOver"/> and invokes required method events. Don't call this method, use <see cref="UpdateState(float, float)"/>
        /// </summary>
        /// <param name="needsRefresh">True if some state changed, requiring the component to be redrawn.</param>
        /// <returns>True if the cursor is over this shape. Used for recursion.</returns>
        private bool UpdateCursorState(float x, float y, out bool needsRefresh) {
            bool childrenNeedRefresh = false;

            foreach (Shape childShape in this) {
                if (childShape.UpdateCursorState(x, y, out bool childNeedsRefresh)) {
                    if (IsMouseOver) { // If the cursor is over a child shape, reset our mouse over state.
                        IsMouseOver = false;
                        OnMouseLeave();
                    }

                    needsRefresh = childNeedsRefresh;
                    return true;
                }

                childrenNeedRefresh |= childNeedsRefresh;
            }

            bool isMouseOver = ContainsPoint(x, y);

            if (isMouseOver && !IsMouseOver) {
                IsMouseOver = true;
                OnMouseEnter();

                needsRefresh = true;
                return true;

            } else if (!isMouseOver && IsMouseOver) {
                IsMouseOver = false;
                OnMouseLeave();

                needsRefresh = true;
                return false;

            }

            needsRefresh = childrenNeedRefresh;
            return isMouseOver;
        }

        public void UpdateMouseState(bool isPressed) {
            if (isPressed && !IsMousePressed) {
                IsMousePressed = true;
                OnMousePress();
            } else if (!isPressed && IsMousePressed) {
                IsMousePressed = false;
                OnMouseRelease();
            }
                    }

        protected virtual void OnMouseEnter() {
            OnMouseEntered?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnMouseLeave() {
            OnMouseLeft?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnMousePress() {
            OnMousePressed?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnMouseRelease() {
            OnMouseReleased?.Invoke(this, EventArgs.Empty);
        }


        private void UpdateGlobalPosition() {
            GlobalX = (Parent?.GlobalX ?? 0) + ((PercentagePosition && !IsRoot) ? Parent.CalculateXByPercentage(X) : X);
            GlobalY = (Parent?.GlobalY ?? 0) + ((PercentagePosition && !IsRoot) ? Parent.CalculateYByPercentage(Y) : Y);

            foreach (Shape child in this)
                child.UpdateGlobalPosition();
        }


        /// <summary>
        /// Returns true if the <see cref="IsMouseOver"/> property is true for this shape or its descendants.
        /// </summary>
        public bool IsMouseOverOrDescentants() {
            if (IsMouseOver)
                return true;

            foreach (Shape child in this) {
                if (child.IsMouseOverOrDescentants())
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Returns the shape with its <see cref="IsMouseOver"/> property true and is the top-most shape.
        /// </summary>
        public Shape GetMouseOverTopMost() {
            foreach (Shape child in this) {
                Shape shape = child.GetMouseOverTopMost();
                if (shape != null)
                    return shape;
            }

            return IsMouseOver ? this : null;
        }

        protected virtual float CalculateXByPercentage(float x) {
            return x * width;
        }

        protected virtual float CalculateYByPercentage(float y) {
            return y * height;
        }

    }

}