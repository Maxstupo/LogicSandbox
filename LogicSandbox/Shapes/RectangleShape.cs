namespace Maxstupo.LogicSandbox.Shapes {

    using System.Drawing;
    using Maxstupo.LogicSandbox.Utility;

    /// <summary>
    /// Represents a concrete implemention of a rectangle shape. Supports outlines and rounded corners.
    /// </summary>
    public class RectangleShape : Shape {

        public RectangleShape() : this(0, 0, 0, 0, null) { }

        public RectangleShape(float x, float y, float width, float height, Shape parent = null) : base(x, y, width, height, parent) { }

        public override bool ContainsPoint(float x, float y) {
            return !(x < GlobalX || y < GlobalY || x > (GlobalX + Width) || y > (GlobalY + Height));
        }

        protected override void DrawShape(Graphics g) {

            // Background
            if (BackgroundColor.HasValue) {
                using (Brush brush = new SolidBrush(BackgroundColor.Value)) {
                    if (CornerRadius <= 0) {
                        g.FillRectangle(brush, GlobalX, GlobalY, Width, Height);
                    } else {
                        g.FillRoundedRectangle(brush, GlobalX, GlobalY, Width, Height, CornerRadius);
                    }
                }
            }

            // Outline
            if (OutlineThickness > 0 && OutlineColor.HasValue) {
                float ht = OutlineThickness / 2f;

                float x = GlobalX + ht;
                float y = GlobalY + ht;
                float w = Width - OutlineThickness;
                float h = Height - OutlineThickness;

                using (Pen pen = new Pen(OutlineColor.Value, OutlineThickness)) {
                    if (CornerRadius <= 0) {
                        g.DrawRectangle(pen, x, y, w, h);
                    } else {
                        g.DrawRoundedRectangle(pen, x, y, w, h, CornerRadius);
                    }
                }
            }

        }

    }

}