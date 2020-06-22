namespace Maxstupo.LogicSandbox.Logic.Components {

    using System.Collections.Generic;
    using System.Drawing;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class CircuitComponent : DigitalComponent {

        private Circuit internalCircuit;
        public Circuit InternalCircuit {
            get => internalCircuit;
            set {
                internalCircuit = value;
                InitIc();
            }
        }

        private readonly Dictionary<Pin, Pin> pinMapping = new Dictionary<Pin, Pin>();

        public CircuitComponent(string id) : this(id, 0, 0) {

        }

        public CircuitComponent(string id, float x, float y) : base(id, "IC", x, y, 34, 34) {
            ProcessInterval = -1;
        }

        /// <summary>
        /// Generates the pins for the component mapped to the internal circuit.
        /// </summary>
        protected void InitIc() {
            if (InternalCircuit == null)
                return;
            pinMapping.Clear();
            ClearPins();

            int pinIndex = 0;
            foreach (DigitalComponent component in InternalCircuit.Components) {
                if (component is PortIn) {
                    Pin icInputPin = AddPin($"in{pinIndex}", Polarity.Input);

                    Pin internalInputPin = component.GetPin("in0");

                    pinMapping.Add(icInputPin, internalInputPin);

                    pinIndex++;

                } else if (component is PortOut) {
                    Pin icOutputPin = AddPin($"out{pinIndex}", Polarity.Output);

                    Pin internalOutputPin = component.GetPin("out0");

                    pinMapping.Add(icOutputPin, internalOutputPin);

                    pinIndex++;

                }

            }

        }

        public override void ToJson(JsonTextWriter jtw) {
            base.ToJson(jtw);
            jtw.WritePropertyName("circuit"); InternalCircuit.ToJson(jtw);
        }

        public override void FromJson(JToken token) {
            base.FromJson(token);

            Circuit circuit = new Circuit();

            string json = token["circuit"].ToString();
            circuit.FromJson(json);

            InternalCircuit = circuit;
        }

        protected override void DrawSymbol(Graphics g) {

        }

        protected override bool Process(float deltaTime) {
            if (internalCircuit == null)
                return false;

            foreach (Pin icInputPin in GetPins(Polarity.Input)) {
                if (pinMapping.TryGetValue(icInputPin, out Pin internalInputPin))
                    internalInputPin.Value = icInputPin.Value;
            }

            bool needsRefresh = internalCircuit.Step(deltaTime);

            foreach (Pin icOutputPin in GetPins(Polarity.Output)) {
                if (pinMapping.TryGetValue(icOutputPin, out Pin internalOutputPin))
                    icOutputPin.Value = internalOutputPin.Value;
            }

            return needsRefresh;
        }

    }

}