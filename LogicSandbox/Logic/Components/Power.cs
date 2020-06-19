namespace Maxstupo.LogicSandbox.Logic.Components {

    using System.Drawing;

    public sealed class Power : DigitalComponent {

        public Power(string id, float x, float y) : base(id, "PWR", x, y, 34, 34) {
            BackgroundColor = Color.FromArgb(255, 204, 204);

            Pin pin = AddPin("out0", Polarity.Output);
            pin.Value = true;
        }

        protected override void DrawSymbol(Graphics g) {

        }

        protected override bool Process(float stepAmount) {
            return false;
        }

    }

}