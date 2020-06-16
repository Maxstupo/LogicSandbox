namespace Maxstupo.LogicSandbox.Logic {
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using Maxstupo.LogicSandbox.Logic.Components;
    using Maxstupo.LogicSandbox.Shapes;

    public class Pin : CircleShape {

        public static Color PinHighColor { get; set; } = Color.Red;
        public static Color PinLowColor { get; set; } = Color.Black;
        public static Color PinSelectedColor { get; set; } = Color.FromArgb(200, 200, 0);


        public string Id { get; }

        public string FullId => $"{((DigitalComponent) Parent).Id}.{Id}";

        public Polarity Polarity { get; }

        private bool value = false;
        public bool Value {
            get => value;
            set {
                this.value = value;
                OutlineColor = IsMouseOver ? PinSelectedColor : Value ? PinHighColor : PinLowColor;
            }
        }

        public Pin(DigitalComponent component, string id, Polarity polarity, float x, float y, float diameter) : base(x, y, diameter, component) {
            Id = id;
            Polarity = polarity;

            BackgroundColor = (Polarity == Polarity.Input) ? Color.FromArgb(255, 204, 0) : Color.White;

            OutlineColor = Color.Black;
            OutlineThickness = 1;

            OnMouseEnter += Pin_OnMouseEnter;
            OnMouseLeave += Pin_OnMouseLeave;
        }

        private void Pin_OnMouseLeave(object sender, EventArgs e) {
            OutlineColor = Value ? PinHighColor : PinLowColor;
        }

        private void Pin_OnMouseEnter(object sender, EventArgs e) {
            OutlineColor = PinSelectedColor;
        }

    }

}