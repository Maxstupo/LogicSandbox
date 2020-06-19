namespace Maxstupo.LogicSandbox.Logic {

    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using Maxstupo.LogicSandbox.Logic.Components;
    using Maxstupo.LogicSandbox.Shapes;

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
        /// Adds a wire between to pins. The pins must have different polarities,
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
        /// <param name="stepAmount">The amount of time to step by, in milliseconds.</param>
        /// <returns>True if a state was changed, false otherwise.</returns>
        public bool Step(float stepRate) {
            bool needsRefresh = false;

            foreach (Wire wire in wires)
                needsRefresh |= wire.Step(stepRate);

            foreach (DigitalComponent comp in Components)
                needsRefresh |= comp.Step(stepRate);

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


    }

}