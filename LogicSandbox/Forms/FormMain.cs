namespace Maxstupo.LogicSandbox.Forms {

    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;
    using Maxstupo.LogicSandbox.Logic;
    using Maxstupo.LogicSandbox.Logic.Components;
    using Maxstupo.LogicSandbox.Properties;

    public partial class FormMain : Form {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        private Version Version => System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
        private string DisplayVersion {
            get {
                Version version = Version;

                string displayVersion = $"v{version.Major}.{version.Minor}.{version.Build}";

                if (version.Revision != 0)
                    displayVersion += $".{version.Revision}";

                return displayVersion;
            }
        }


        private Circuit circuit = new Circuit();
        private readonly CircuitSimulator simulator = new CircuitSimulator();

        private readonly ImageList componentThumbnailList = new ImageList { ColorDepth = ColorDepth.Depth32Bit };

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
            canvas.OnZoom += (s, e) => UpdateStatusBar();
            canvas.MouseMove += (s, e) => UpdateStatusBar();
            canvas.Selector.OnEndDrag += (s, e) => UpdateStatusBar();

            canvas.OnCircuitChanged += (s, e) => UnsavedChanges = true;
            canvas.Circuit = circuit;

            simulator.Circuit = circuit;
            simulator.OnUpsChanged += (s, e) => Invoke((MethodInvoker) delegate { UpdateStatusBar(); });
            simulator.OnStateChange += (s, e) => Invoke((MethodInvoker) delegate { canvas.Refresh(); });

            UpdateTitle();
        }

        private void UpdateStatusBar() {
            tsslPositions.Text = $"{canvas.MouseWorldX:00.00}, {canvas.MouseWorldY:00.00} [{canvas.PanPositionX:00.00}, {canvas.PanPositionY:00.00}]";
            tsslZoom.Text = $"{canvas.Zoom * 100.0:0.#}%";
            tsslSeleciton.Text = $"{circuit.Components.Count} components, {canvas.Selector.SelectedItems.Count} selected";
            tsslSimulationState.Text = $"{(simulator.IsRunning ? simulator.ActualUps : 0)} UPS @ {simulator.Speed}x {(simulator.IsRunning ? string.Empty : "(Paused)")}";
        }

        private void UpdateTitle() {
            string version = DisplayVersion;

            if (openFilepath == null) {
                Text = $"Logic Sandbox {version}";
            } else {
                Text = $"Logic Sandbox {version} - {openFilepath}{(UnsavedChanges ? "*" : string.Empty)}";
            }
        }


        private void FormMain_Load(object sender, EventArgs e) {
            canvas.Zoom = 1; // TEMP: Zoom Level.
            canvas.Center();

            // TEMP: Allow auto discovery of components.
            AddComponentToLibrary(new Power("", 0, 0));
            AddComponentToLibrary(new Toggle("", 0, 0));
            AddComponentToLibrary(new PushOn("", 0, 0));
            AddComponentToLibrary(new PushOff("", 0, 0));
            AddComponentToLibrary(new NotGate("", 0, 0));
            AddComponentToLibrary(new OrGate("", 0, 0));
            AddComponentToLibrary(new PortIn("", 0, 0));



            componentThumbnailList.ImageSize = AddComponentToLibrary(new PortOut("", 0, 0));

            lvComponentLibrary.View = View.LargeIcon;
            lvComponentLibrary.LargeImageList = componentThumbnailList;

            simulator.Start();
        }

        private Size AddComponentToLibrary(DigitalComponent dc) {
            Image thumbnail = DigitalComponent.GenerateThumbnail(dc);

            componentThumbnailList.Images.Add(thumbnail);

            ListViewItem item = lvComponentLibrary.Items.Add(dc.Label, componentThumbnailList.Images.Count - 1);
            item.Tag = dc.GetType();

            return thumbnail.Size;
        }


        private Image GetCircuitImage() {
            Image image = new Bitmap(canvas.Width, canvas.Height);

            using (Graphics g = Graphics.FromImage(image)) {
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                canvas.ApplyTransformsTo(g);
                circuit.Draw(g);
            }

            return image;
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

        private void exportAsImageTsmi_Click(object sender, EventArgs e) {
            using (SaveFileDialog dialog = new SaveFileDialog()) {
                dialog.Filter = "PNG File (*.png)|*.png";
                dialog.CheckPathExists = true;

                if (dialog.ShowDialog(this) == DialogResult.OK) {

                    using (Image image = GetCircuitImage())
                        image.Save(dialog.FileName, ImageFormat.Png);
                }
            }
        }

        private void exitTsmi_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        #endregion

        #region Circuit Menu

        private void createICToolStripMenuItem_Click(object sender, EventArgs e) {
            if (canvas.CreateIC(canvas.Selector.SelectedItems.ToList()) != null)
                UnsavedChanges = true;
        }

        #endregion

        #region Simulation Menu

        private void toggleSimulationTsmi_Click(object sender, EventArgs e) {
            simulator.Toggle();

            stepSimulationTsmi.Enabled = !simulator.IsRunning;
            speedTsmi.Enabled = simulator.IsRunning;

            toggleSimulationTsmi.Text = simulator.IsRunning ? "&Pause" : "&Play";
        }

        private void stepSimulationTsmi_Click(object sender, EventArgs e) {
            simulator.SingleStep();
        }

        private void speedTsmi_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e) {
            float speed = Convert.ToSingle(e.ClickedItem.Tag);
            simulator.Speed = speed;

            ToolStripDropDownItem tsi = (ToolStripDropDownItem) sender;
            foreach (ToolStripMenuItem item in tsi.DropDownItems.Cast<ToolStripMenuItem>())
                item.Checked = false;


            (e.ClickedItem as ToolStripMenuItem).Checked = true;
        }

        #endregion

        #region Edit Menu

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e) {
            foreach (DigitalComponent component in canvas.Selector.SelectedItems.ToList()) {
                canvas.Selector.Deselect(component);
                circuit.RemoveComponent(component);
            }
            UpdateStatusBar();
            canvas.Refresh();
        }

        private void selectAllTsmi_Click(object sender, EventArgs e) {
            canvas.Selector.SelectAll();
            canvas.Refresh();
        }

        private void deselectAllTsmi_Click(object sender, EventArgs e) {
            canvas.Selector.DeselectAll();
            canvas.Refresh();
        }

        private void invertSelectionTsmi_Click(object sender, EventArgs e) {
            canvas.Selector.InvertSelection();
            canvas.Refresh();
        }

        #endregion

        #region View Menu

        private void centerTsmi_Click(object sender, EventArgs e) {
            canvas.CenterSelection();
        }

        #endregion

        private void optionsTsmi_Click(object sender, EventArgs e) {
            using (FormOptions options = new FormOptions()) {
                if (options.ShowDialog(this) == DialogResult.OK) {

                }
            }
        }

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

                string time = DateTime.Now.Ticks.ToString();
                pendingComponent = (DigitalComponent) Activator.CreateInstance((Type) item.Tag, $"c_{((Type) item.Tag).Name.ToLower()}_{time.Substring(time.Length - 4, 4)}");

                circuit.AddComponent(pendingComponent);
                UpdateStatusBar();
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
            UpdateStatusBar();
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
            canvas.Selector.ItemSource = circuit.Components;
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

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e) {
            simulator.Stop();
        }
    }

}