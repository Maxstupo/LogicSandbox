namespace Maxstupo.LogicSandbox.Logic.Components {
    using System.Drawing;
    using System.Linq;
    using Maxstupo.LogicSandbox.Shapes;

    public abstract class ButtonComponent : DigitalComponent {

        public static Color ButtonPressedColor { get; set; } = Color.FromArgb(153, 153, 204);


        private bool buttonState; // The state of the button.
        public bool ButtonState {
            get => buttonState;
            set {
                if (buttonState != value) {
                    buttonState = value;
                    OnButtonStateChanged();
                }
            }
        }

        // The "button" component that the user clicks.
        protected Shape interactiveShape;

        public ButtonComponent(string id, string label, float x, float y, float width, float height) : base(id, label, x, y, width, height) {
            AddPin("in0", Polarity.Input);
            AddPin("out0", Polarity.Output);

            BackgroundColor = Color.FromArgb(204, 204, 255);
            ProcessInterval = -1;

            interactiveShape = new RectangleShape(0.25f, 0.25f, width / 2f, height / 2f) {
                Parent = this,
                CornerRadius = CornerRadius,
                BackgroundColor = BackgroundColor,
                OutlineColor = OutlineColorUnselected,
                OutlineThickness = OutlineThickness,
                PercentagePosition = true
            };

            interactiveShape.OnMouseEntered += (s, e) => IsSelectable = false;
            interactiveShape.OnMouseLeft += (s, e) => IsSelectable = true;

            interactiveShape.OnMousePressed += (s, e) => OnButtonPressed();
            interactiveShape.OnMouseReleased += (s, e) => OnButtonReleased();
        }

        protected virtual void OnButtonStateChanged() {
            interactiveShape.BackgroundColor = ButtonState ? ButtonPressedColor : BackgroundColor;
        }

        protected virtual void OnButtonPressed() {
            ButtonState = !ButtonState;
        }

        protected virtual void OnButtonReleased() {

        }

        protected override bool Process() {
            bool isPowered = GetPins(Polarity.Input).Any(x => x.Value);
            bool value = ButtonState && isPowered;

            return SetPinValues(Polarity.Output, value);
        }

        protected override void DrawSymbol(Graphics g) {

        }

    }

}