namespace Maxstupo.LogicSandbox.Shapes {

    using System.Drawing;

    /// <summary>
    /// Represents a concrete implemention of a circle shape. Supports outlines and rounded corners.
    /// </summary>
    public class CircleShape : Shape {

        /// <summary>Returns the radius of the circle.</summary>
        public float Radius { get => Width / 2f; set => Width = value * 2f; }

        /// <summary>Returns the diameter of the circle.</summary>
        public override float Width { get => base.Width; set { base.Width = value; base.Height = value; } }

        /// <summary>Returns the diameter of the circle.</summary>
        public override float Height { get => base.Height; set { base.Height = value; base.Width = value; } }


        public CircleShape() : this(0, 0, 0, null) { }

        public CircleShape(float x, float y, float diameter, Shape parent = null) : base(x, y, diameter, diameter, parent) { }

        public override bool ContainsPoint(float x, float y) {
            float dx = x - GlobalX;
            float dy = y - GlobalY;
            return dx * dx + dy * dy <= Radius * Radius;
        }

        protected override void DrawShape(Graphics g) {

            // Background
            if (BackgroundColor.HasValue) {
                using (Brush brush = new SolidBrush(BackgroundColor.Value))
                    g.FillEllipse(brush, GlobalX - Radius, GlobalY - Radius, Width, Height);
            }

            // Outline
            if (OutlineThickness > 0 && OutlineColor.HasValue) {
                float ht = OutlineThickness / 2f;

                float x = (GlobalX - Radius) + ht;
                float y = (GlobalY - Radius) + ht;
                float w = Width - OutlineThickness;
                float h = Height - OutlineThickness;

                using (Pen pen = new Pen(OutlineColor.Value, OutlineThickness))
                    g.DrawEllipse(pen, x, y, w, h);
            }

        }

        protected override float CalculateXByPercentage(float x) {
            return x * Width / 2f;
        }

        protected override float CalculateYByPercentage(float y) {
            return y * Height / 2f;
        }

    }

}