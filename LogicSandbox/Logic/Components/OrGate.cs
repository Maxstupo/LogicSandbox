namespace Maxstupo.LogicSandbox.Logic.Components {
   
    using System.Drawing;
    using System.Linq;

    public sealed class OrGate : DigitalComponent {

        public OrGate(string id) : this(id, 0, 0) {

        }

        public OrGate(string id, float x, float y) : base(id, "OR", x, y, 34, 34) {

            AddPin("in0", Polarity.Input);
            AddPin("in1", Polarity.Input);
            AddPin("out0", Polarity.Output);

        }

        protected override bool Process(float stepAmount) {

            bool value = GetPins(Polarity.Input).Any(x => x.Value);

            return SetPinValues(Polarity.Output, value);
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