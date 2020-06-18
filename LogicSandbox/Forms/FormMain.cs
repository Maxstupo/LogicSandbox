namespace Maxstupo.LogicSandbox.Forms {

    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;
    using Maxstupo.LogicSandbox.Logic;
    using Maxstupo.LogicSandbox.Logic.Components;
    using Maxstupo.LogicSandbox.Properties;
    using Maxstupo.LogicSandbox.Utility.Interaction;

    public partial class FormMain : Form {

        private Keys AdditiveKey { get; set; } = Keys.Control;
        private Keys InclusiveKey { get; set; } = Keys.Shift;

        private Circuit circuit = new Circuit();

        private readonly Selector<DigitalComponent> selector;
        private readonly Transformer<DigitalComponent> transformer = new Transformer<DigitalComponent>();

        private readonly ImageList componentThumbnailList = new ImageList { ColorDepth = ColorDepth.Depth32Bit };

        private Pin selectedPin;

        /// <summary>The component that is currently being dragged in or out of the canvas area.</summary>
        private DigitalComponent pendingComponent;

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

            // TEMP: Replace timer with something better.
            Timer timer = new Timer();
            timer.Tick += (s, e) => {
                if (circuit.Step(1))
                    canvas.Refresh();
            };
            timer.Interval = 10;
            timer.Start();
        }



        private void FormMain_Load(object sender, EventArgs e) {
            canvas.Zoom = 2; // TEMP: Zoom Level.
            canvas.Center();

            // TEMP: Allow auto discovery of components.
            // TEMP: move to more suitable location.
            AddComponentToLibrary(new Power("", 0, 0));
            AddComponentToLibrary(new NotGate("", 0, 0));
            AddComponentToLibrary(new OrGate("", 0, 0));
            AddComponentToLibrary(new PortIn("", 0, 0));
            AddComponentToLibrary(new PortOut("", 0, 0));

            componentThumbnailList.ImageSize = new Size(44, 44);

            lvComponentLibrary.View = View.LargeIcon;
            lvComponentLibrary.LargeImageList = componentThumbnailList;
        }

        // TEMP: move to more suitable location.
        private void AddComponentToLibrary(DigitalComponent dc) {
            Image thumbnail = GenerateComponentThumbnail(dc);

            componentThumbnailList.Images.Add(thumbnail);

            ListViewItem item = lvComponentLibrary.Items.Add(dc.Label, componentThumbnailList.Images.Count - 1);
            item.Tag = dc.GetType();
        }

        // TEMP: move to more suitable location.
        private Image GenerateComponentThumbnail(DigitalComponent dc) {
            dc.X += DigitalComponent.PinDiameter / 2f;

            int width = (int) (dc.Width + DigitalComponent.PinDiameter);
            int height = (int) dc.Height;

            Image thumbnail = new Bitmap(width, height + 1);
            using (Graphics g = Graphics.FromImage(thumbnail)) {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                dc.Draw(g);
            }

            dc.X -= DigitalComponent.PinDiameter / 2f;

            return thumbnail;
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

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e) {
            foreach (DigitalComponent component in selector.SelectedItems)
                circuit.RemoveComponent(component);
            canvas.Refresh();
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


        #region Drag & Drop Components From Library.

        private void LvComponentLibrary_ItemDrag(object sender, ItemDragEventArgs e) {
            if (pendingComponent != null)
                return;

            DoDragDrop(e.Item, DragDropEffects.Copy);
        }


        private void Canvas_DragDrop(object sender, DragEventArgs e) {
            pendingComponent = null;
        }

        private void Canvas_DragEnter(object sender, DragEventArgs e) {
            if (pendingComponent != null)
                return;

            e.Effect = DragDropEffects.Copy;

            if (e.Data.GetData(typeof(ListViewItem)) is ListViewItem item) {

                Point clientPosition = canvas.PointToClient(e.X, e.Y);
                float x = canvas.ScreenToWorldX(clientPosition.X);
                float y = canvas.ScreenToWorldY(clientPosition.Y);

                string time = DateTime.Now.Ticks.ToString();
                pendingComponent = (DigitalComponent) Activator.CreateInstance((Type) item.Tag, $"comp_{((Type) item.Tag).Name.ToLower()}_{time.Substring(time.Length - 4, 4)}", x, y);

                circuit.AddComponent(pendingComponent);

                canvas.Refresh();
            }

        }

        private void Canvas_DragOver(object sender, DragEventArgs e) {
            if (pendingComponent == null)
                return;

            Point clientPosition = canvas.PointToClient(e.X, e.Y);
            pendingComponent.X = canvas.ScreenToWorldX(clientPosition.X) - pendingComponent.Width / 2f;
            pendingComponent.Y = canvas.ScreenToWorldY(clientPosition.Y) - pendingComponent.Height / 2f;

            canvas.Refresh();
        }

        private void Canvas_DragLeave(object sender, EventArgs e) {
            if (pendingComponent == null)
                return;

            circuit.RemoveComponent(pendingComponent);
            pendingComponent = null;

            canvas.Refresh();
        }

        #endregion


    }

}