namespace Maxstupo.LogicSandbox.Logic {

    using System;
    using System.Drawing;
    using Maxstupo.LogicSandbox.Logic.Components;
    using Maxstupo.LogicSandbox.Shapes;

    /// <summary>
    /// Represents a IO pin on a component, that can have a logic state and polarity.
    /// </summary>
    public class Pin : CircleShape {

        public static Color PinHighColor { get; set; } = Color.Red;
        public static Color PinLowColor { get; set; } = Color.Black;
        public static Color PinSelectedColor { get; set; } = Color.FromArgb(200, 200, 0);

        /// <summary>A local ID identifying this pin.</summary>
        public string Id { get; }

        /// <summary>A fully qualified ID, uniquely identifying this pin.</summary>
        public string FullId => $"{((DigitalComponent) Parent).Id}.{Id}";

        /// <summary>The polarity of this pin. Is it an input or output?</summary>
        public Polarity Polarity { get; }

        private bool value = false;
        /// <summary>The logic state of the pin. (true = logic high, false = logic low)</summary>
        public bool Value {
            get => value;
            set {
                this.value = value;
                OutlineColor = IsMouseOver ? PinSelectedColor : Value ? PinHighColor : PinLowColor;
            }
        }

        public Pin(DigitalComponent component, string id, Polarity polarity, float x, float y, float diameter) : base(x, y, diameter) {
            Id = id;
            Polarity = polarity;
            Parent = component;

            BackgroundColor = (Polarity == Polarity.Input) ? Color.FromArgb(255, 204, 0) : Color.White;

            OutlineColor = Color.Black;
            OutlineThickness = 1;
        }

        protected override void OnMouseEnter() {
            base.OnMouseEnter();
            OutlineColor = PinSelectedColor;
        }

        protected override void OnMouseLeave() {
            base.OnMouseLeave();
            OutlineColor = Value ? PinHighColor : PinLowColor;
        }
   

    }

}