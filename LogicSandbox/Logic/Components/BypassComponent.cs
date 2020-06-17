namespace Maxstupo.LogicSandbox.Logic.Components {

    using System.Linq;

    /// <summary>
    /// A digital component that forwards the input signal to the output pins.
    /// </summary>
    public abstract class BypassComponent : DigitalComponent {

        public BypassComponent(string id, string label, float x, float y, float width, float height) : base(id, label, x, y, width, height) {
            ProcessInterval = -1;
        }

        protected override bool Process() {
            bool value = GetPins(Polarity.Input).Any(x => x.Value);

            return SetPinValues(Polarity.Output, value);
        }
    }

}
