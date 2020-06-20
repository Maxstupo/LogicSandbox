namespace Maxstupo.LogicSandbox.Logic.Components {
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using Maxstupo.LogicSandbox.Shapes;
    using Maxstupo.LogicSandbox.Utility;
    using Maxstupo.LogicSandbox.Utility.Interaction;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Represents a component (logic gate, button, etc) and provides all required logic and 
    /// methods for simulating and drawing the component.
    /// </summary>
    public abstract class DigitalComponent : RectangleShape, ISelectable {

        /// <summary>The outline color of the component when selected.</summary>
        public static Color OutlineColorSelected { get; set; } = Color.Blue;

        /// <summary>The outline color of the component when unselected.</summary>
        public static Color OutlineColorUnselected { get; set; } = Color.FromArgb(102, 102, 102);

        /// <summary>The diameter in pixels of the input and output pins.</summary>
        public static float PinDiameter { get; set; } = 10;


        /// <summary>A unique ID identifying this component.</summary>
        public string Id { get; }

        /// <summary>The text that will appear below the component. Set null or empty to disable.</summary>
        public string Label { get; set; }

        /// <summary>The text style for the <see cref="Label"/></summary>
        public TextStyle LabelStyle { get; } = new TextStyle();

        /// <summary>
        /// The amount of time that has passed since the last component update in milliseconds. See <see cref="Process"/>
        /// </summary>
        public float Ticks { get; protected set; }

        /// <summary>
        /// The interval in milliseconds between component updates. See <see cref="Process"/>
        /// </summary>
        public float ProcessInterval { get; protected set; } = 5;

        // A cache of pins used for quick look-up by their id.
        private readonly Dictionary<string, Pin> pins = new Dictionary<string, Pin>();

        public IReadOnlyList<Pin> Pins => pins.Values.ToList().AsReadOnly();

        public bool IsSelectable { get; protected set; } = true;

        public DigitalComponent(string id) : this(id, null, 0, 0, 0, 0) {
        }

        public DigitalComponent(string id, string label, float x, float y, float width, float height) : base(x, y, width, height) {
            Id = id;
            Label = label;

            // Default styling.
            BackgroundColor = Color.FromArgb(204, 204, 204);

            OutlineColor = OutlineColorUnselected;
            OutlineThickness = 2f;

            CornerRadius = 2f;
        }

        /// <summary>
        /// Writes the state of this component to the provided JSON writer.
        /// </summary>
        public virtual void ToJson(JsonTextWriter jtw) {
            jtw.WritePropertyName("x"); jtw.WriteValue(X);
            jtw.WritePropertyName("y"); jtw.WriteValue(Y);

            jtw.WritePropertyName("label"); jtw.WriteValue(Label);
        }

        /// <summary>
        /// Reads and sets the state of this component from the provided JToken.
        /// </summary>
        public virtual void FromJson(JToken token) {
            X = token["x"].Value<float>();
            Y = token["y"].Value<float>();
            Label = token["label"]?.Value<string>() ?? Label;
        }

        /// <summary>
        /// Adds a pin to this component, caches the ID, and updates position automatically.
        /// </summary>
        /// <returns>The new pin, or null if the provided ID is being used.</returns>
        protected Pin AddPin(string id, Polarity polarity) {
            if (pins.ContainsKey(id))
                return null;

            Pin pin = new Pin(this, id, polarity, 0, 0, PinDiameter);

            pins.Add(pin.Id, pin);

            UpdatePinPositions();

            return pin;
        }

        /// <summary>
        /// Clears all the pins attached to this component.
        /// </summary>
        protected void ClearPins() {
            foreach (Pin pin in pins.Values)
                pin.Parent = null;
            pins.Clear();
        }

        /// <summary>
        /// Updates all pin objects to have the correct spacing and positioning.
        /// </summary>
        protected virtual void UpdatePinPositions() {
            List<Pin> inputPins = GetPins(Polarity.Input).ToList();
            List<Pin> outputPins = GetPins(Polarity.Output).ToList();

            float inputSpacing = 1f / inputPins.Count;
            float outputSpacing = 1f / outputPins.Count;

            for (int i = 0; i < inputPins.Count; i++) {
                Pin pin = inputPins[i];
                pin.PercentagePosition = true;
                pin.X = 0;
                pin.Y = inputSpacing * i + (inputSpacing / 2f);
            }

            for (int i = 0; i < outputPins.Count; i++) {
                Pin pin = outputPins[i];
                pin.PercentagePosition = true;
                pin.X = 1;
                pin.Y = outputSpacing * i + (outputSpacing / 2f);

            }
        }

        /// <summary>
        /// Sets all pins with the specified polarity to the specified value.
        /// </summary>
        /// <returns>True if one or more pin states changed.</returns>
        protected bool SetPinValues(Polarity polarity, bool newValue) {
            bool stateChanged = false;

            foreach (Pin pin in GetPins(polarity)) {
                if (pin.Value != newValue)
                    stateChanged = true;

                pin.Value = newValue;
            }

            return stateChanged;
        }

        /// <summary>
        /// Called when the simulation requires this component to update its state.
        /// </summary>
        /// <param name="stepAmount">The amount of time to step by, in milliseconds.</param>
        /// <returns>True if the state was changed, false otherwise.</returns>
        protected abstract bool Process(float stepAmount);

        /// <summary>
        /// Simulates the component by stepping in time.
        /// </summary>
        /// <param name="stepAmount">The amount of time to step by, in milliseconds.</param>
        /// <returns>True if the state was changed, false otherwise.</returns>
        public virtual bool Step(float stepAmount) {
            Ticks += stepAmount;

            if (Ticks > ProcessInterval) {
                Ticks = 0;
                return Process(stepAmount);
            }

            return false;
        }

        /// <summary>Draw the component symbol (e.g. logic symbol)</summary>
        protected abstract void DrawSymbol(Graphics g);


        public override void Draw(Graphics g) {
            base.Draw(g);

            DrawSymbol(g);

            DrawLabel(g);
        }

        /// <summary>
        /// Draw the label of the component. Method does nothing if <see cref="Label"/> is null or empty.
        /// </summary>
        protected virtual void DrawLabel(Graphics g) {
            if (string.IsNullOrWhiteSpace(Label))
                return;

            using (Font font = LabelStyle.CreateFont()) {
                SizeF size = g.MeasureString(Label, font); // XXX: Cache Graphics.MeasureString?

                using (Brush brush = LabelStyle.CreateBrush()) {
                    float ox = Width / 2f - size.Width / 2f;
                    float oy = Height + 2f;

                    g.DrawString(Label, font, brush, GlobalX + ox, GlobalY + oy);
                }
            }
        }


        public void OnSelected() {
            OutlineColor = OutlineColorSelected;
        }

        public void OnDeselected() {
            OutlineColor = OutlineColorUnselected;
        }

        public Pin GetPin(string id) {
            return pins.TryGetValue(id, out Pin value) ? value : null;
        }

        /// <summary>
        /// Returns a enumerable of pins with the specified polarity.
        /// </summary>
        public IEnumerable<Pin> GetPins(Polarity polarity) {
            return pins.Values.Where(x => x.Polarity == polarity);
        }


    }

}