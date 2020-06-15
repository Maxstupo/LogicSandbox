namespace Maxstupo.LogicSandbox.Utility {

    using System.Drawing;

    public sealed class TextStyle {

        public Color Color { get; set; } = Color.Black;

        public string FontFamily { get; set; } = "Arial";

        public float FontSize { get; set; } = 7f;

        public FontStyle FontStyle { get; set; } = FontStyle.Regular;


        public Font CreateFont() {
            return new Font(FontFamily, FontSize, FontStyle);
        }

        public Brush CreateBrush() {
            return new SolidBrush(Color);
        }

    }

}