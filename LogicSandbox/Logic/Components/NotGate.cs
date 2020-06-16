namespace Maxstupo.LogicSandbox.Logic.Components {

    using System;
    using System.Drawing;

    public sealed class NotGate : DigitalComponent {

        public NotGate(string id, float x, float y) : base(id, "NOT", x, y, 34, 34) {
            new Pin(this, "in0", Polarity.Input, 0, 0.5f, 10).PercentagePosition = true;
            new Pin(this, "out0", Polarity.Output, 1, 0.5f, 10).PercentagePosition = true;
        }

        protected override void DrawSymbol(Graphics g) {
            float gx = GlobalX + Width * 0.29411f;
            float gy = GlobalY + Height * 0.23529f;
            g.DrawLine(Pens.Black, gx, gy, gx, gy + Height * 0.55882f);
            g.DrawLine(Pens.Black, gx, gy, gx + Width * 0.3611f, gy + Height * 0.55882f / 2);
            g.DrawLine(Pens.Black, gx, gy + Height * 0.55882f, gx + Width * 0.3611f, gy + Height * 0.55882f / 2);
            g.DrawEllipse(Pens.Black, gx + Width * 0.3611f, gy + Height * 0.55882f / 2 - 1.5f, 3, 3);
        }

    }

}