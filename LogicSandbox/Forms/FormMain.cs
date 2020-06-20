namespace Maxstupo.LogicSandbox.Forms {

    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.IO;
    using System.Linq;
    using System.Text;
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

        private readonly ImageList componentThumbnailList = new ImageList { ColorDepth = ColorDepth.Depth32Bit };

        private Pin selectedPin;
        private Shape componentOver;

        /// <summary>The component that is currently being dragged in or out of the canvas area.</summary>
        private DigitalComponent pendingComponent;

        private string openFilepath = null;

        private bool unsavedChanges = false;
        private bool UnsavedChanges {
            get => unsavedChanges;
            set {
                unsavedChanges = value;
                UpdateTitle();
            }
        }

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

        private void UpdateTitle() {
            if (openFilepath == null) {
                Text = "Logic Sandbox";
            } else {
                Text = $"Logic Sandbox - {openFilepath}{(UnsavedChanges ? "*" : string.Empty)}";
            }
        }


        private void FormMain_Load(object sender, EventArgs e) {
            canvas.Zoom = 2; // TEMP: Zoom Level.
            canvas.Center();

            // TEMP: Allow auto discovery of components.
            // TEMP: move to more suitable location.
            AddComponentToLibrary(new Power("", 0, 0));
            AddComponentToLibrary(new Toggle("", 0, 0));
            AddComponentToLibrary(new PushOn("", 0, 0));
            AddComponentToLibrary(new PushOff("", 0, 0));
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

                componentOver = circuit.GetComponentOver();
                if (componentOver != null)
                    componentOver.UpdateMouseState(true);

                selectedPin = circuit.GetPinOver();
                if (selectedPin == null) {

                    bool additiveMode = ModifierKeys.HasFlag(AdditiveKey); // Previous selection not cleared.

                    if (!selector.Start(canvas.MouseWorldX, canvas.MouseWorldY, additiveMode, (x, y) => circuit.GetComponentMouseOverOrDescentants())) {
                        UnsavedChanges = true;
                        transformer.Clear();
                        transformer.AddItems(selector.SelectedItems);
                        transformer.StartDrag(canvas.MouseWorldX, canvas.MouseWorldY);

                        canvas.Refresh();
                    }

                }

            } else if (e.Button == MouseButtons.Right) {

                Pin pin = circuit.GetPinOver();
                if (pin != null) {
                    circuit.RemoveConnectedWires(pin);
                    UnsavedChanges = true;
                }

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

                if (componentOver != null)
                    componentOver.UpdateMouseState(false);


                bool additiveMode = ModifierKeys.HasFlag(AdditiveKey); // Previous selection not cleared.
                bool inclusiveMode = ModifierKeys.HasFlag(InclusiveKey); // Item must be inside of selection bounds. 

                selector.EndDrag(canvas.MouseWorldX, canvas.MouseWorldY, additiveMode, inclusiveMode);
                transformer.EndDrag();

                if (selectedPin != null) {

                    Pin nextSelectedPin = circuit.GetPinOver();
                    if (nextSelectedPin != null) {
                        circuit.AddWire(selectedPin, nextSelectedPin);
                        UnsavedChanges = true;
                    }

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


        /// <summary>
        /// Creates an IC from the provided components, the circuit provided must be the source of the components in the list.
        /// </summary>
        public void CreateIC(List<DigitalComponent> components, Circuit baseCircuit) {
            if (components.Count == 0)
                return;

            if (!components.Any(x => x is PortOut))
                return;

            Circuit internalCircuit = new Circuit();

            foreach (DigitalComponent component in components) {
                internalCircuit.AddComponent(component);

                // Add all wires that are contained to the components list.
                foreach (Pin pin in component.Pins) {
                    foreach (Wire wire in baseCircuit.GetWires(pin)) {

                        Pin otherPin = pin.FullId != wire.P1.FullId ? wire.P1 : wire.P2;

                        // Check if wire connects within the components list.
                        bool contained = false;
                        foreach (DigitalComponent component2 in components) {
                            foreach (Pin pin2 in component2.Pins) {
                                if (pin2.FullId == otherPin.FullId) {
                                    contained = true;
                                    break;
                                }
                            }
                            if (contained)
                                break;
                        }

                        if (contained)
                            internalCircuit.AddWire(wire.P1, wire.P2);

                    }
                }

                baseCircuit.RemoveComponent(component);
                selector.Deselect(component);
            }

            Console.WriteLine($"Created IC with {internalCircuit.Components.Count} components and {internalCircuit.WireCount} wires");

            string time = DateTime.Now.Ticks.ToString();
            CircuitComponent circuitComponent = new CircuitComponent($"comp_ic_{time.Substring(time.Length - 4, 4)}", 0, 0) {
                InternalCircuit = internalCircuit
            };

            baseCircuit.AddComponent(circuitComponent);
            canvas.Refresh();
            UnsavedChanges = true;
        }


        #region Menu Strip

        #region File Menu

        private void newTsmi_Click(object sender, EventArgs e) {
            NewFile();
        }

        private void openTsmi_Click(object sender, EventArgs e) {
            Open();
        }

        private void saveTsmi_Click(object sender, EventArgs e) {
            Save();
        }

        private void saveAsTsmi_Click(object sender, EventArgs e) {
            SaveAs();
        }

        private void exitTsmi_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        #endregion

        #region Simulation Menu

        private void createICToolStripMenuItem_Click(object sender, EventArgs e) {
            CreateIC(selector.SelectedItems.ToList(), circuit);
        }

        #endregion

        #region Edit Menu

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e) {
            foreach (DigitalComponent component in selector.SelectedItems.ToList()) {
                selector.Deselect(component);
                circuit.RemoveComponent(component);
            }
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

        #endregion

        #region View Menu

        private void centerTsmi_Click(object sender, EventArgs e) {
            if (transformer.ItemCount > 0) {
                canvas.Center(transformer.Selection);
            } else {
                canvas.Center();
            }
        }

        #endregion

        #region Help Menu

        private void wikiTsmi_Click(object sender, EventArgs e) {
            Process.Start(Resources.WikiUrl);
        }

        #endregion

        #endregion


        #region Drag & Drop Components From Library.

        private void LvComponentLibrary_ItemDrag(object sender, ItemDragEventArgs e) {
            if (pendingComponent != null)
                return;

            DoDragDrop(e.Item, DragDropEffects.Copy);
        }


        private void Canvas_DragDrop(object sender, DragEventArgs e) {
            pendingComponent = null;
            UnsavedChanges = true;
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
                pendingComponent = (DigitalComponent) Activator.CreateInstance((Type) item.Tag, $"c_{((Type) item.Tag).Name.ToLower()}_{time.Substring(time.Length - 4, 4)}", x, y);

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


        #region Loading / Saving

        public void CheckUnsavedChanges() {
            if (UnsavedChanges) {
                if (MessageBox.Show(this, "The current circuit has unsaved changes! Do you want to save before continuing?", "Unsaved Changes", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    Save();
            }
        }

        public void NewFile() {
            CheckUnsavedChanges();

            openFilepath = null;
            UnsavedChanges = false;

            circuit = new Circuit();
            selector.ItemSource = circuit.Components;
            canvas.Refresh();
        }


        public void Open() {
            using (OpenFileDialog dialog = new OpenFileDialog()) {
                dialog.Title = "Open Circuit...";
                dialog.CheckFileExists = true;
                dialog.CheckPathExists = true;
                dialog.Filter = Resources.FileFilter;

                if (dialog.ShowDialog(this) == DialogResult.OK)
                    Open(dialog.FileName);

            }
        }

        public void Open(string filename) {
            CheckUnsavedChanges();

            string json = File.ReadAllText(filename, new UTF8Encoding(false));
            circuit.FromJson(json);

            openFilepath = filename;
            UnsavedChanges = false;
        }

        public void Save() {
            if (openFilepath != null) {
                SaveAs(openFilepath);
            } else {
                SaveAs();
            }
        }

        public void SaveAs() {
            using (SaveFileDialog dialog = new SaveFileDialog()) {
                dialog.Title = "Save Circuit As...";
                dialog.AddExtension = true;
                dialog.Filter = Resources.FileFilter;

                if (dialog.ShowDialog(this) == DialogResult.OK)
                    SaveAs(dialog.FileName);
            }
        }

        public void SaveAs(string filepath) {
            string json = circuit.ToJson();
            File.WriteAllText(filepath, json, new UTF8Encoding(false));
            UnsavedChanges = false;
        }

        #endregion

    }

}