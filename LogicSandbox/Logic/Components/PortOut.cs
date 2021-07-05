namespace Maxstupo.LogicSandbox.Logic.Components {

    using System.Drawing;

    public class PortOut : BypassComponent {
        
        public PortOut(string id) : this(id, 0, 0) {

        }

        public PortOut(string id, float x, float y) : base(id, "OUT1", x, y) {

            AddPin("in0", Polarity.Input);
            AddPin("out0", Polarity.Output);

        }

        protected override void DrawSymbol(Graphics g) {
            g.FillEllipse(Brushes.White, GlobalX + Width / 2f - 18 / 2f, GlobalY + Height / 2f - 18 / 2f, 18, 18);
            g.FillEllipse(Brushes.Black, GlobalX + Width / 2f - 8 / 2f, GlobalY + Height / 2f - 8 / 2f, 8, 8);

            g.DrawEllipse(Pens.Black, GlobalX + Width / 2f - 18 / 2f, GlobalY + Height / 2f - 18 / 2f, 18, 18);
        }

    }

}