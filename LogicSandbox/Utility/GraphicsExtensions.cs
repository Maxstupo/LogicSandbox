namespace Maxstupo.LogicSandbox.Utility {

    using System.Drawing;
    using System.Drawing.Drawing2D;

    public static class GraphicsExtensions {

        public static void DrawRoundedRectangle(this Graphics graphics, Pen pen, float x, float y, float w, float h, float cornerRadius) {
            using (GraphicsPath path = RoundedRect(x, y, w, h, cornerRadius))
                graphics.DrawPath(pen, path);
        }

        public static void FillRoundedRectangle(this Graphics graphics, Brush brush, float x, float y, float w, float h, float cornerRadius) {
            using (GraphicsPath path = RoundedRect(x, y, w, h, cornerRadius))
                graphics.FillPath(brush, path);
        }

        public static GraphicsPath RoundedRect(float x, float y, float w, float h, float radius) {
            GraphicsPath path = new GraphicsPath();
            if (radius <= 0) {
                path.AddRectangle(new RectangleF(x, y, w, h));
                return path;
            }

            float diameter = radius * 2f;

            RectangleF arc = new RectangleF(x, y, diameter, diameter);

            // top left arc  
            path.AddArc(arc, 180f, 90f);

            // top right arc  
            arc.X = (x + w) - diameter;
            path.AddArc(arc, 270f, 90f);

            // bottom right arc  
            arc.Y = (y + h) - diameter;
            path.AddArc(arc, 0f, 90f);

            // bottom left arc 
            arc.X = x;
            path.AddArc(arc, 90f, 90f);

            path.CloseFigure();
            return path;
        }

    }

}