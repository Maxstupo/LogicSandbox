namespace Maxstupo.LogicSandbox.Logic.Components {

    using System.Drawing;

    public sealed class Power : DigitalComponent {

        public Power(string id, float x, float y) : base(id, "PWR", x, y, 34, 34) {
            BackgroundColor = Color.FromArgb(255, 204, 204);

           new Pin(this, "out0", Polarity.Output, 1, 0.5f, 10) {
                PercentagePosition = true,
                Value = true
            };
        }

        protected override void DrawSymbol(Graphics g) {

        }

    }

}