namespace Maxstupo.LogicSandbox.Forms {

    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Linq;
    using System.Windows.Forms;
    using Maxstupo.LogicSandbox.Logic;
    using Maxstupo.LogicSandbox.Logic.Components;
    using Maxstupo.LogicSandbox.Properties;
    using Maxstupo.LogicSandbox.Shapes;
    using Maxstupo.LogicSandbox.Utility.Interaction;

    public partial class FormMain : Form {

        private Keys AdditiveKey { get; set; } = Keys.Control;
        private Keys InclusiveKey { get; set; } = Keys.Shift;

        private Circuit circuit = new Circuit();

        private readonly Selector<DigitalComponent> selector;
        private readonly Transformer<DigitalComponent> transformer = new Transformer<DigitalComponent>();

        private Pin selectedPin;

        public FormMain() {
            InitializeComponent();

            selector = new Selector<DigitalComponent>(circuit.Components);
            selector.OnDragging += (s, e) => canvas.Refresh();
            selector.OnEndDrag += Selector_OnEndDrag;

            transformer.OnMoving += (s, e) => canvas.Refresh();


            canvas.Paint += Canvas_Paint;

            canvas.MouseDown += Canvas_MouseDown;
            canvas.MouseMove += Canvas_MouseMove;
            canvas.MouseUp += Canvas_MouseUp;
        }

        private void FormMain_Load(object sender, EventArgs e) {
            canvas.Zoom = 2; // TEMP: Zoom Level.
            canvas.Center();

            // TEMP: Components
            circuit.AddComponent(new Power("pwr0", -10, -60));
            circuit.AddComponent(new NotGate("not_gate0", 10, 60));
            circuit.AddComponent(new NotGate("not_gate1", -40, 20));
            circuit.AddComponent(new NotGate("not_gate2", 50, -20));
            circuit.AddComponent(new OrGate("or_gate0", -20, -50));
        }

        private void Canvas_Paint(object sender, PaintEventArgs e) {
            Graphics g = e.Graphics;

            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            circuit.Draw(g);

            if (selectedPin != null)
                g.DrawLine(Pens.Blue, canvas.MouseWorldX, canvas.MouseWorldY, selectedPin.GlobalX, selectedPin.GlobalY);

            selector.Draw(g);
        }

        private void Canvas_MouseDown(object sender, MouseEventArgs e) {
      
            if (e.Button == MouseButtons.Left) {

                selectedPin = circuit.GetPinOver();
                if (selectedPin == null) {

                    bool additiveMode = ModifierKeys.HasFlag(AdditiveKey); // Previous selection not cleared.

                    if (!selector.Start(canvas.MouseWorldX, canvas.MouseWorldY, additiveMode, (x, y) => circuit.GetComponentOver())) {

                        transformer.Clear();
                        transformer.AddItems(selector.SelectedItems);
                        transformer.StartDrag(canvas.MouseWorldX, canvas.MouseWorldY);

                        canvas.Refresh();
                    }

                }

            } else if (e.Button == MouseButtons.Right) {

                Pin pin = circuit.GetPinOver();
                if (pin != null)
                    circuit.RemoveConnectedWires(pin);

            }

        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e) {
            bool needsRefresh = circuit.Update(canvas.MouseWorldX, canvas.MouseWorldY);

            selector.Drag(canvas.MouseWorldX, canvas.MouseWorldY);
            transformer.Drag(canvas.MouseWorldX, canvas.MouseWorldY);

            if (needsRefresh || selectedPin != null)
                canvas.Refresh();
        }

        private void Canvas_MouseUp(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {

                bool additiveMode = ModifierKeys.HasFlag(AdditiveKey); // Previous selection not cleared.
                bool inclusiveMode = ModifierKeys.HasFlag(InclusiveKey); // Item must be inside of selection bounds. 

                selector.EndDrag(canvas.MouseWorldX, canvas.MouseWorldY, additiveMode, inclusiveMode);
                transformer.EndDrag();

                if (selectedPin != null) {

                    Pin nextSelectedPin = circuit.GetPinOver();
                    if (nextSelectedPin != null)
                        circuit.AddWire(selectedPin, nextSelectedPin);

                    selectedPin = null;
                    canvas.Refresh();
                }

            }

        }

        private void Selector_OnEndDrag(object sender, List<DigitalComponent> selectedItems) {
            transformer.Clear();
            transformer.AddItems(selectedItems);
            canvas.Refresh();
        }




        #region Menu Strip

        private void exitTsmi_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void selectAllTsmi_Click(object sender, EventArgs e) {
            selector.SelectAll();
            canvas.Refresh();
        }

        private void deselectAllTsmi_Click(object sender, EventArgs e) {
            selector.DeselectAll();
            canvas.Refresh();
        }

        private void invertSelectionTsmi_Click(object sender, EventArgs e) {
            selector.InvertSelection();
            canvas.Refresh();
        }

        private void centerTsmi_Click(object sender, EventArgs e) {
            if (transformer.ItemCount > 0) {
                canvas.Center(transformer.Selection);
            } else {
                canvas.Center();
            }
        }

        private void wikiTsmi_Click(object sender, EventArgs e) {
            Process.Start(Resources.WikiUrl);
        }

        #endregion

    }

}