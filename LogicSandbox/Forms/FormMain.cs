namespace Maxstupo.LogicSandbox.Forms {

    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;

    public partial class FormMain : Form {
       
        public FormMain() {
            InitializeComponent();

            canvas.Paint += Canvas_Paint;
        }

        private void FormMain_Load(object sender, EventArgs e) {
            canvas.Center();
        }

        private void Canvas_Paint(object sender, PaintEventArgs e) {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;



        }
            
    }

}