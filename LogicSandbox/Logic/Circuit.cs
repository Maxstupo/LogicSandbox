namespace Maxstupo.LogicSandbox.Logic {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using Maxstupo.LogicSandbox.Logic.Components;

    public class Wire {

        public Pin P1 { get; }

        public Pin P2 { get; }

        public Wire(Pin p1, Pin p2) {
            this.P1 = p1;
            this.P2 = p2;
        }

        public void Draw(Graphics g) {
            Pen pen = (P1.Value || P2.Value) ? Pens.Red : Pens.Blue;

            g.DrawLine(pen, P1.GlobalX, P1.GlobalY, P2.GlobalX, P2.GlobalY);
        }

    }

    public class Circuit {

        public List<DigitalComponent> Components { get; } = new List<DigitalComponent>();
        private readonly Dictionary<string, DigitalComponent> lookup = new Dictionary<string, DigitalComponent>();

        private readonly List<Wire> wires = new List<Wire>();

        public void Clear() {
            ClearWires();

            lookup.Clear();
            Components.Clear();
        }

        public void ClearWires() {
            wires.Clear();
        }

        public void AddComponent(DigitalComponent component) {
            if (lookup.ContainsKey(component.Id))
                return;
            lookup.Add(component.Id, component);
            Components.Add(component);
        }

        public void Draw(Graphics g) {
            foreach (DigitalComponent component in Components)
                component.Draw(g);

            foreach (Wire wire in wires)
                wire.Draw(g);
        }

        public bool Update(float x, float y) {
            bool needsRefresh = false;

            foreach (DigitalComponent component in Components)
                needsRefresh |= component.Update(x, y);

            return needsRefresh;
        }

        public Pin GetPinOver() {
            foreach (DigitalComponent component in Components) {
                foreach (Pin pin in component) {
                    if (pin.IsMouseOver)
                        return pin;
                }
            }
            return null;
        }

        public DigitalComponent GetComponentOver() {
            foreach (DigitalComponent component in Components) {
                if (component.IsMouseOver)
                    return component;
            }
            return null;
        }

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

        // Removes all wires connected to the given pin.
        public void RemoveConnectedWires(Pin pin) {
            if (pin == null)
                return;

            foreach (Wire wire in GetWires(pin).ToList())
                wires.Remove(wire);
        }


        //Returns the number of wires connected to the given pin.
        private int GetPinWireCount(Pin pin) {
            return GetWires(pin).Count();
        }    
        
        //Returns all wires attached to the given pin.
        private IEnumerable<Wire> GetWires(Pin pin) {
            string id = pin.FullId;
            return wires.Where(x => x.P1.FullId == id || x.P2.FullId == id);
        }

        // Returns true if p1 and p2 have a connection.
        private bool HasWireBetween(Pin p1, Pin p2) {
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