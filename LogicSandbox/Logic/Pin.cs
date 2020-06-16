namespace Maxstupo.LogicSandbox.Logic {

    using System.Drawing;
    using Maxstupo.LogicSandbox.Logic.Components;
    using Maxstupo.LogicSandbox.Shapes;

    public class Pin : CircleShape {

        public string Id { get; }

        public Polarity Polarity { get; }

        public Pin(DigitalComponent component, string id, Polarity polarity, float x, float y, float diameter) : base(x, y, diameter, component) {
            Id = id;
            Polarity = polarity;

            BackgroundColor = (Polarity == Polarity.Input) ? Color.FromArgb(255, 204, 0) : Color.White;

            OutlineColor = Color.Black;
            OutlineThickness = 1;
        }

    }

}