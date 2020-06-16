namespace Maxstupo.LogicSandbox.Logic.Components {

    using System.Drawing;
    using Maxstupo.LogicSandbox.Shapes;
    using Maxstupo.LogicSandbox.Utility;
    using Maxstupo.LogicSandbox.Utility.Interaction;

    public abstract class DigitalComponent : RectangleShape, ISelectable {

        public static Color OutlineColorSelected { get; set; } = Color.Blue;

        public static Color OutlineColorUnselected { get; set; } = Color.FromArgb(102, 102, 102);


        public string Id { get; }

        public string Label { get; set; }

        public TextStyle LabelStyle { get; } = new TextStyle();


        public DigitalComponent(string id, string label, float x, float y, float width, float height) : base(x, y, width, height, null) {
            Id = id;
            Label = label;

            BackgroundColor = Color.FromArgb(204, 204, 204);

            OutlineColor = OutlineColorUnselected;
            OutlineThickness = 2f;

            CornerRadius = 2f;

        }

        /// <summary>Draw the component symbol, if one is valid for the given component.</summary>
        protected abstract void DrawSymbol(Graphics g);


        public override void Draw(Graphics g) {
            base.Draw(g);

            DrawSymbol(g);

            DrawLabel(g);
        }

        /// <summary>Draw the name (label) of the component.</summary>
        protected virtual void DrawLabel(Graphics g) {
            if (string.IsNullOrWhiteSpace(Label))
                return;

            using (Font font = LabelStyle.CreateFont()) {
                SizeF size = g.MeasureString(Label, font); // XXX: Cache Graphics.MeasureString?

                using (Brush brush = LabelStyle.CreateBrush()) {
                    float ox = Width / 2f - size.Width / 2f;
                    float oy = Height + 2f;

                    g.DrawString(Label, font, brush, GlobalX + ox, GlobalY + oy);
                }
            }
        }


        public void OnSelected() {
            OutlineColor = OutlineColorSelected;
        }

        public void OnDeselected() {
            OutlineColor = OutlineColorUnselected;
        }


    }

}