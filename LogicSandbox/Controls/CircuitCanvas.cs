namespace Maxstupo.LogicSandbox.Controls {

    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;
    using Maxstupo.LogicSandbox.Forms;
    using Maxstupo.LogicSandbox.Logic;
    using Maxstupo.LogicSandbox.Logic.Components;
    using Maxstupo.LogicSandbox.Shapes;
    using Maxstupo.LogicSandbox.Utility.Interaction;

    public class CircuitCanvas : Canvas {

        public Keys AdditiveKey { get; set; } = Keys.Control;

        public Keys InclusiveKey { get; set; } = Keys.Shift;

        public Selector<DigitalComponent> Selector { get; }

        private Transformer<DigitalComponent> transformer = new Transformer<DigitalComponent>();

        private Circuit circuit;
        public Circuit Circuit {
            get => circuit;
            set {
                circuit = value;
                Selector.ItemSource = circuit?.Components;
                Refresh();
            }
        }

        public event EventHandler OnCircuitChanged;

        private Pin selectedPin;
        private Shape componentOver;

        public CircuitCanvas() {
            Paint += CircuitCanvas_Paint;

            MouseDown += CircuitCanvas_MouseDown;
            MouseMove += CircuitCanvas_MouseMove;
            MouseUp += CircuitCanvas_MouseUp;
            MouseDoubleClick += CircuitCanvas_MouseDoubleClick;

            Selector = new Selector<DigitalComponent>();
            Selector.OnDragging += (s, e) => Refresh();
            Selector.OnEndDrag += Selector_OnEndDrag;

            transformer.OnMoving += (s, e) => Refresh();
        }

        protected override void Dispose(bool disposing) {
            base.Dispose(disposing);
            if (disposing) {
                foreach (FormCircuit cir in formCircuits)
                    cir.Dispose();
                formCircuits.Clear();
            }
                
        }

        /// <summary>
        /// Creates an IC from the provided components, the circuit provided must be the source of the components in the list.
        /// </summary>
        public CircuitComponent CreateIC(List<DigitalComponent> components) {
            Circuit internalCircuit = Circuit.CreateIC(components, Circuit);
            if (internalCircuit == null)
                return null;

            foreach (DigitalComponent component in components)
                Selector.Deselect(component);

            string time = DateTime.Now.Ticks.ToString();
            CircuitComponent circuitComponent = new CircuitComponent($"comp_ic_{time.Substring(time.Length - 4, 4)}", 0, 0) {
                InternalCircuit = internalCircuit
            };

            Circuit.AddComponent(circuitComponent);
            Refresh();

            return circuitComponent;
        }

        private void CircuitCanvas_MouseDoubleClick(object sender, MouseEventArgs e) {
            if (Selector.SelectedItems.Count != 1)
                return;

            RenameComponent(Selector.SelectedItems[0]);
        }

        public void RenameComponent(DigitalComponent component) {

            if (ModifierKeys.HasFlag(Keys.Control) && component is CircuitComponent cc) {



            } else {

                using (FormRenameComponent dialog = new FormRenameComponent(component))
                    dialog.ShowDialog(this);
            }
            Refresh();
        }

        private void CircuitCanvas_Paint(object sender, PaintEventArgs e) {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            circuit?.Draw(g);

            if (selectedPin != null)
                g.DrawLine(Pens.Blue, MouseWorldX, MouseWorldY, selectedPin.GlobalX, selectedPin.GlobalY);

            Selector.Draw(g);
        }

        private void CircuitCanvas_MouseDown(object sender, MouseEventArgs e) {
            if (circuit == null)
                return;

            if (e.Button == MouseButtons.Left) {

                componentOver = circuit.GetComponentOver();
                if (componentOver != null)
                    componentOver.UpdateMouseState(true);

                selectedPin = circuit.GetPinOver();
                if (selectedPin == null) {

                    bool additiveMode = ModifierKeys.HasFlag(AdditiveKey); // Previous selection not cleared.

                    if (!Selector.Start(MouseWorldX, MouseWorldY, additiveMode, (x, y) => circuit.GetComponentMouseOverOrDescentants())) {
                        OnCircuitChanged?.Invoke(this, EventArgs.Empty);

                        transformer.Clear();
                        transformer.AddItems(Selector.SelectedItems);
                        transformer.StartDrag(MouseWorldX, MouseWorldY);

                        Refresh();
                    }

                }

            } else if (e.Button == MouseButtons.Right) {

                Pin pin = circuit.GetPinOver();
                if (pin != null) {
                    circuit.RemoveConnectedWires(pin);
                    OnCircuitChanged?.Invoke(this, EventArgs.Empty);
                }

            }

        }

        private void CircuitCanvas_MouseUp(object sender, MouseEventArgs e) {
            if (circuit == null)
                return;

            if (e.Button == MouseButtons.Left) {

                if (componentOver != null)
                    componentOver.UpdateMouseState(false);


                bool additiveMode = ModifierKeys.HasFlag(AdditiveKey); // Previous selection not cleared.
                bool inclusiveMode = ModifierKeys.HasFlag(InclusiveKey); // Item must be inside of selection bounds. 

                Selector.EndDrag(MouseWorldX, MouseWorldY, additiveMode, inclusiveMode);
                transformer.EndDrag();

                if (selectedPin != null) {

                    Pin nextSelectedPin = circuit.GetPinOver();
                    if (nextSelectedPin != null) {
                        circuit.AddWire(selectedPin, nextSelectedPin);
                        OnCircuitChanged?.Invoke(this, EventArgs.Empty);
                    }

                    selectedPin = null;
                    Refresh();
                }

            }

        }

        private void CircuitCanvas_MouseMove(object sender, MouseEventArgs e) {
            if (circuit == null)
                return;

            bool needsRefresh = circuit.Update(MouseWorldX, MouseWorldY);

            Selector.Drag(MouseWorldX, MouseWorldY);
            transformer.Drag(MouseWorldX, MouseWorldY);

            if (needsRefresh || selectedPin != null)
                Refresh();
        }

        private void Selector_OnEndDrag(object sender, List<DigitalComponent> selectedItems) {
            transformer.Clear();
            transformer.AddItems(selectedItems);
            Refresh();
        }

        public void CenterSelection() {
            if (transformer.ItemCount > 0) {
                Center(transformer.Selection);
            } else {
                Center();
            }
        }

    }

}