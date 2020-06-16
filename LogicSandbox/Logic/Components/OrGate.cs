namespace Maxstupo.LogicSandbox.Logic.Components {

    using System;
    using System.Drawing;

    public sealed class OrGate : DigitalComponent {

        public OrGate(string id, float x, float y) : base(id, "NOT", x, y, 34, 34) {
            new Pin(this, "in0", Polarity.Input, 0, 0.25f, 10).PercentagePosition = true;
            new Pin(this, "in1", Polarity.Input, 0, 0.75f, 10).PercentagePosition = true;
            new Pin(this, "out2", Polarity.Output, 1, 0.5f, 10).PercentagePosition = true;
        }

        protected override void DrawSymbol(Graphics g) {
            // TODO: Incorrect symbol for OR gate.
            float gx = GlobalX + Width * 0.29411f;
            float gy = GlobalY + Height * 0.23529f;
            g.DrawLine(Pens.Black, gx, gy, gx, gy + Height * 0.55882f);
            g.DrawArc(Pens.Black, gx - (Height * 0.88235f / 2f), gy, Height * 0.88235f, Height * 0.55882f, -90, 180);
        }

    }

}