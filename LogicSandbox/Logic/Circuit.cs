namespace Maxstupo.LogicSandbox.Logic {
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using Maxstupo.LogicSandbox.Logic.Components;

    public class Circuit {

        public List<DigitalComponent> Components { get; } = new List<DigitalComponent>();
   
        private readonly List<Wire> wires = new List<Wire>();



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
            wires.Add(new Wire(pinA, pinB));
        }

    }

}