namespace Maxstupo.LogicSandbox.Shapes {

    using System.Drawing;
    using Maxstupo.LogicSandbox.Utility;
    using Maxstupo.LogicSandbox.Utility.Interaction;

    /// <summary>
    /// Represents a 2D shape, that can have sub-shapes. Provides methods for drawing, and supports positioning relative to parent.
    /// </summary>
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


        public Shape(float x, float y, float width, float height, Shape parent = null) {
            X = x;
            Y = y;
            Width = width;
            Height = height;

            OnParentChange += (s, e) => UpdateGlobalPosition();
            Parent = parent;
        }


        protected abstract void DrawShape(Graphics g);

        public virtual void Draw(Graphics g) {
            DrawShape(g);

            foreach (Shape child in this)
                child.Draw(g);
        }


        public abstract bool ContainsPoint(float x, float y);


        private void UpdateGlobalPosition() {
            GlobalX = (Parent?.GlobalX ?? 0) + ((PercentagePosition && !IsRoot) ? Parent.CalculateXByPercentage(X) : X);
            GlobalY = (Parent?.GlobalY ?? 0) + ((PercentagePosition && !IsRoot) ? Parent.CalculateYByPercentage(Y) : Y);

            foreach (Shape child in this)
                child.UpdateGlobalPosition();
        }

        protected virtual float CalculateXByPercentage(float x) {
            return x * width;
        }

        protected virtual float CalculateYByPercentage(float y) {
            return y * height;
        }

    }

}