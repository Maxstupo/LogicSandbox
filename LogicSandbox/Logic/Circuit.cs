namespace Maxstupo.LogicSandbox.Logic {

    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Maxstupo.LogicSandbox.Logic.Components;
    using Maxstupo.LogicSandbox.Shapes;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Represents a collection of components that are connected via wires.
    /// </summary>
    public class Circuit {

        /// <summary>The components of the circuit.</summary>
        public List<DigitalComponent> Components { get; } = new List<DigitalComponent>();


        // A cache used to look-up components using the id.
        private readonly Dictionary<string, DigitalComponent> lookup = new Dictionary<string, DigitalComponent>();

        private readonly List<Wire> wires = new List<Wire>();

        public int WireCount => wires.Count;

        public Circuit() {

        }

        /// <summary>
        /// Adds a component to the circuit. If the circuit already contains a component with the ID this method wont do anything.
        /// </summary>
        public void AddComponent(DigitalComponent component) {
            if (lookup.ContainsKey(component.Id)) {
                Console.WriteLine($"Component with ID already in circuit: {component.Id}");
                return;
            }

            lookup.Add(component.Id, component);
            Components.Add(component);

            Console.WriteLine($"Added {component.GetType().Name} component with ID \"{component.Id}\"");
        }

        /// <summary>
        /// Removes the given component from the circuit.
        /// </summary>
        public void RemoveComponent(DigitalComponent component) {
            if (component == null)
                return;
            RemoveComponent(component.Id);
        }

        /// <summary>
        /// Removes the given component from the circuit using its ID.
        /// </summary>
        public void RemoveComponent(string id) {
            if (id == null)
                return;

            if (lookup.TryGetValue(id, out DigitalComponent component)) {
                Console.WriteLine($"Removing component with ID \"{id}\"");

                foreach (Pin pin in component.Pins)
                    RemoveConnectedWires(pin);

                Components.RemoveAll(x => x.Id == id);
            } else {
                Console.WriteLine($"Component with ID \"{id}\" doesn't exist in circuit.");
            }
        }

        /// <summary>
        /// Adds a wire between two pins. The pins must have different polarities,
        /// and the input pin provided can't have any wires connected to it.
        /// If the criteria mentioned isn't met the method wont do anything.
        /// </summary>
        public void AddWire(Pin pinA, Pin pinB) {
            if (pinA == null || pinB == null)
                return;

            // Pins must have different polarities. (e.g Input <-> Output or Output <-> Input)
            if (pinA.Polarity == pinB.Polarity)
                return;

            // Prevent multiple wires connecting the same two pins.
            if (HasWireBetween(pinA, pinB))
                return;

            // Ensure input pins can only have one wire connected.
            Pin inputPin = pinB.Polarity == Polarity.Input ? pinB : pinA;
            if (GetPinWireCount(inputPin) > 0)
                return;

            wires.Add(new Wire(pinA, pinB));
        }

        /// <summary>
        /// Adds a wire using two fully qualified pin ids (component_id.pin_id).
        /// The pins must have different polarities,
        /// and the input pin provided can't have any wires connected to it.
        /// If the criteria mentioned isn't met the method wont do anything.
        /// </summary>
        public void AddWire(string pin1FullId, string pin2FullId) {
            string[] pin1Tokens = pin1FullId.Split('.');
            string[] pin2Tokens = pin2FullId.Split('.');

            if (pin1Tokens.Length != 2 || pin2Tokens.Length != 2)
                return;

            if (lookup.TryGetValue(pin1Tokens[0], out DigitalComponent c1) && lookup.TryGetValue(pin2Tokens[0], out DigitalComponent c2)) {
                Pin p1 = c1.GetPin(pin1Tokens[1]);
                Pin p2 = c2.GetPin(pin2Tokens[1]);

                if (p1 == null || p2 == null)
                    return;

                AddWire(p1, p2);
            }
        }

        /// <summary>
        /// Clears all wires and components. Circuit will be empty after calling this.
        /// </summary>
        public void Clear() {
            ClearWires();

            lookup.Clear();
            Components.Clear();
        }

        /// <summary>Clears all wires.</summary>
        public void ClearWires() {
            wires.Clear();
        }


        /// <summary>
        /// Draws all wires and components.
        /// </summary>
        public void Draw(Graphics g) {
            foreach (DigitalComponent component in Components)
                component.Draw(g);

            foreach (Wire wire in wires)
                wire.Draw(g);
        }

        /// <summary>
        /// Updates the components for user interaction.
        /// </summary>
        /// <param name="mx">The mouse position along the x-axis, in pixels.</param>
        /// <param name="my">The mouse position along the y-axis, in pixels.</param>
        /// <returns>True if the circuit needs to be redrawn.</returns>
        public bool Update(float mx, float my) {
            bool needsRefresh = false;

            foreach (Shape component in Components)
                needsRefresh |= component.UpdateState(mx, my);

            return needsRefresh;
        }

        /// <summary>
        /// Simulates the circuit by stepping in time.
        /// </summary>
        /// <param name="deltaTime">Delta between steps, in seconds.</param>
        /// <returns>True if a state was changed, false otherwise.</returns>
        public bool Step(float deltaTime) {
            bool needsRefresh = false;

            foreach (Wire wire in wires)
                needsRefresh |= wire.Step(deltaTime);

            foreach (DigitalComponent comp in Components)
                needsRefresh |= comp.Step(deltaTime);

            return needsRefresh;
        }


        /// <summary>
        /// Returns the first <see cref="Pin"/> the mouse is over.
        /// </summary>
        public Pin GetPinOver() {
            foreach (DigitalComponent component in Components) {
                foreach (Shape shape in component) {
                    if (shape.IsMouseOver && shape is Pin pin)
                        return pin;
                }
            }
            return null;
        }

        /// <summary>
        /// Returns the first component that has the mouse over it or one of its child elements.
        /// </summary>
        public DigitalComponent GetComponentMouseOverOrDescentants() {
            foreach (DigitalComponent component in Components) {
                if (component.IsMouseOverOrDescentants())
                    return component;
            }
            return null;
        }

        public Shape GetComponentOver() {
            foreach (DigitalComponent dc in Components) {
                Shape shape = dc.GetMouseOverTopMost();
                if (shape != null) return shape;
            }
            return null;
        }



        /// <summary>
        /// Removes all wires connected to the provided pin.
        /// </summary>
        public void RemoveConnectedWires(Pin pin) {
            if (pin == null)
                return;

            foreach (Wire wire in GetWires(pin).ToList()) {
                wires.Remove(wire);

                if (wire.P1.Polarity == Polarity.Input)
                    wire.P1.Value = false;

                if (wire.P2.Polarity == Polarity.Input)
                    wire.P2.Value = false;
            }
        }

        /// <summary>
        /// Returns the number of wires connected to the provided pin.
        /// </summary>
        public int GetPinWireCount(Pin pin) {
            return GetWires(pin).Count();
        }

        /// <summary>
        /// Returns all wires attached to the provided pin.
        /// </summary>
        public IEnumerable<Wire> GetWires(Pin pin) {
            string id = pin.FullId;
            return wires.Where(x => x.P1.FullId == id || x.P2.FullId == id);
        }

        /// <summary>
        /// Returns true if the provided pins are connected.
        /// </summary>
        public bool HasWireBetween(Pin p1, Pin p2) {
            string p1Id = p1.FullId;
            string p2Id = p2.FullId;

            return wires.Any(x => {
                string s1 = x.P1.FullId;
                string s2 = x.P2.FullId;

                return (s1 == p1Id || s1 == p2Id) && (s2 == p1Id || s2 == p2Id);
            });
        }

        /// <summary>
        /// Returns the JSON representation of this <see cref="Circuit"/>.
        /// </summary>
        public string ToJson() {
            StringBuilder sb = new StringBuilder();

            using (StringWriter sw = new StringWriter(sb)) {
                using (JsonTextWriter jtw = new JsonTextWriter(sw))
                    ToJson(jtw);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Creates a JSON representation of this <see cref="Circuit"/> using the provided JSON writer.
        /// </summary>
        public void ToJson(JsonTextWriter jtw) {
            jtw.Formatting = Formatting.Indented;

            jtw.WriteStartObject();
            {
                // Components
                jtw.WritePropertyName("components");
                jtw.WriteStartArray();
                {
                    foreach (DigitalComponent component in Components) {
                        jtw.WriteStartObject();
                        {
                            jtw.WritePropertyName("id"); jtw.WriteValue(component.Id);
                            jtw.WritePropertyName("type"); jtw.WriteValue(component.GetType().FullName.ToLower());

                            component.ToJson(jtw);
                        }
                        jtw.WriteEndObject();
                    }
                }
                jtw.WriteEndArray();

                // Wires
                jtw.WritePropertyName("wires");
                jtw.WriteStartArray();
                {
                    foreach (Wire wire in wires) {
                        jtw.Formatting = Formatting.Indented;
                        jtw.WriteStartArray();
                        {
                            jtw.Formatting = Formatting.None;
                            jtw.WriteValue(wire.P1.FullId);
                            jtw.WriteValue(wire.P2.FullId);
                        }
                        jtw.WriteEndArray();
                    }
                }
                jtw.Formatting = Formatting.Indented;
                jtw.WriteEndArray();
            }
            jtw.WriteEndObject();
        }

        /// <summary>
        /// Parses the provided JSON string, and updates this <see cref="Circuit"/> to represent it.
        /// </summary>
        public void FromJson(string json) {
            JObject obj = JObject.Parse(json);

            Clear();
            foreach (JToken token in obj["components"]) {

                string id = token["id"].Value<string>();
                string type = token["type"].Value<string>();

                Type componentType = Type.GetType(type, false, true);
                if (componentType == null) {
                    Console.WriteLine("Unknown component type: " + type);
                    continue;
                }

                DigitalComponent component = (DigitalComponent) Activator.CreateInstance(componentType, id);
                component.FromJson(token);

                AddComponent(component);
            }

            foreach (JToken token in obj["wires"]) {

                string p1FullId = token.First().Value<string>();
                string p2FullId = token.Last().Value<string>();

                AddWire(p1FullId, p2FullId);

            }

        }

    }

}