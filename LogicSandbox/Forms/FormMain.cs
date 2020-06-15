namespace Maxstupo.LogicSandbox.Forms {

    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;
    using Maxstupo.LogicSandbox.Shapes;

    public partial class FormMain : Form {

        public FormMain() {
            InitializeComponent();

            canvas.Paint += Canvas_Paint;
        }

        private void FormMain_Load(object sender, EventArgs e) {
            canvas.Center();
        }

        // TEMP: 
        Shape shape = new RectangleShape(5, 4, 120, 32) {
            CornerRadius = 5,
            BackgroundColor=Color.Teal,
            OutlineColor = Color.Blue,
            OutlineThickness = 2
        };

        private void Canvas_Paint(object sender, PaintEventArgs e) {
            Graphics g = e.Graphics;

            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.CompositingMode = CompositingMode.SourceOver;

            shape.Draw(g);
        }

    }

}