namespace Maxstupo.LogicSandbox.Forms {
    partial class FormMain {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileTsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.newTsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.openTsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.saveTsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsTsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exportTsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.exportAsImageTsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitTsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.editTsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.selectAllTsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.deselectAllTsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.invertSelectionTsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.viewTsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.centerTsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.circuitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createICToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.simulationToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toggleSimulationTsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.speedTsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.x025ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.x05ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.x075ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.x1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.x125ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.x15ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.x175ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.x2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.stepSimulationTsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsTsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.helpTsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.wikiTsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutTsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.lvComponentLibrary = new System.Windows.Forms.ListView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.canvas = new Maxstupo.LogicSandbox.Controls.CircuitCanvas();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.tsslPositions = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslSimulationState = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslSpring = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslSeleciton = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslZoom = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileTsmi,
            this.editTsmi,
            this.viewTsmi,
            this.circuitToolStripMenuItem,
            this.simulationToolStripMenuItem1,
            this.optionsTsmi,
            this.helpTsmi});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(800, 24);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileTsmi
            // 
            this.fileTsmi.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newTsmi,
            this.openTsmi,
            this.toolStripSeparator,
            this.saveTsmi,
            this.saveAsTsmi,
            this.toolStripSeparator2,
            this.exportTsmi,
            this.toolStripSeparator1,
            this.exitTsmi});
            this.fileTsmi.Name = "fileTsmi";
            this.fileTsmi.Size = new System.Drawing.Size(37, 20);
            this.fileTsmi.Text = "&File";
            // 
            // newTsmi
            // 
            this.newTsmi.Image = ((System.Drawing.Image)(resources.GetObject("newTsmi.Image")));
            this.newTsmi.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newTsmi.Name = "newTsmi";
            this.newTsmi.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newTsmi.Size = new System.Drawing.Size(186, 22);
            this.newTsmi.Text = "&New";
            this.newTsmi.Click += new System.EventHandler(this.newTsmi_Click);
            // 
            // openTsmi
            // 
            this.openTsmi.Image = ((System.Drawing.Image)(resources.GetObject("openTsmi.Image")));
            this.openTsmi.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openTsmi.Name = "openTsmi";
            this.openTsmi.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openTsmi.Size = new System.Drawing.Size(186, 22);
            this.openTsmi.Text = "&Open";
            this.openTsmi.Click += new System.EventHandler(this.openTsmi_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(183, 6);
            // 
            // saveTsmi
            // 
            this.saveTsmi.Image = ((System.Drawing.Image)(resources.GetObject("saveTsmi.Image")));
            this.saveTsmi.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveTsmi.Name = "saveTsmi";
            this.saveTsmi.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveTsmi.Size = new System.Drawing.Size(186, 22);
            this.saveTsmi.Text = "&Save";
            this.saveTsmi.Click += new System.EventHandler(this.saveTsmi_Click);
            // 
            // saveAsTsmi
            // 
            this.saveAsTsmi.Name = "saveAsTsmi";
            this.saveAsTsmi.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.saveAsTsmi.Size = new System.Drawing.Size(186, 22);
            this.saveAsTsmi.Text = "Save &As";
            this.saveAsTsmi.Click += new System.EventHandler(this.saveAsTsmi_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(183, 6);
            // 
            // exportTsmi
            // 
            this.exportTsmi.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportAsImageTsmi});
            this.exportTsmi.Name = "exportTsmi";
            this.exportTsmi.Size = new System.Drawing.Size(186, 22);
            this.exportTsmi.Text = "Export...";
            // 
            // exportAsImageTsmi
            // 
            this.exportAsImageTsmi.Name = "exportAsImageTsmi";
            this.exportAsImageTsmi.Size = new System.Drawing.Size(167, 22);
            this.exportAsImageTsmi.Text = "Export as Image...";
            this.exportAsImageTsmi.Click += new System.EventHandler(this.exportAsImageTsmi_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(183, 6);
            // 
            // exitTsmi
            // 
            this.exitTsmi.Name = "exitTsmi";
            this.exitTsmi.Size = new System.Drawing.Size(186, 22);
            this.exitTsmi.Text = "E&xit";
            this.exitTsmi.Click += new System.EventHandler(this.exitTsmi_Click);
            // 
            // editTsmi
            // 
            this.editTsmi.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem,
            this.toolStripSeparator4,
            this.selectAllTsmi,
            this.deselectAllTsmi,
            this.invertSelectionTsmi});
            this.editTsmi.Name = "editTsmi";
            this.editTsmi.Size = new System.Drawing.Size(39, 20);
            this.editTsmi.Text = "&Edit";
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.deleteToolStripMenuItem.Text = "&Delete Selection";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(189, 6);
            // 
            // selectAllTsmi
            // 
            this.selectAllTsmi.Name = "selectAllTsmi";
            this.selectAllTsmi.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.selectAllTsmi.Size = new System.Drawing.Size(192, 22);
            this.selectAllTsmi.Text = "Select &All";
            this.selectAllTsmi.Click += new System.EventHandler(this.selectAllTsmi_Click);
            // 
            // deselectAllTsmi
            // 
            this.deselectAllTsmi.Name = "deselectAllTsmi";
            this.deselectAllTsmi.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.deselectAllTsmi.Size = new System.Drawing.Size(192, 22);
            this.deselectAllTsmi.Text = "D&eselect All";
            this.deselectAllTsmi.Click += new System.EventHandler(this.deselectAllTsmi_Click);
            // 
            // invertSelectionTsmi
            // 
            this.invertSelectionTsmi.Name = "invertSelectionTsmi";
            this.invertSelectionTsmi.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.invertSelectionTsmi.Size = new System.Drawing.Size(192, 22);
            this.invertSelectionTsmi.Text = "&Invert Selection";
            this.invertSelectionTsmi.Click += new System.EventHandler(this.invertSelectionTsmi_Click);
            // 
            // viewTsmi
            // 
            this.viewTsmi.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.centerTsmi});
            this.viewTsmi.Name = "viewTsmi";
            this.viewTsmi.Size = new System.Drawing.Size(44, 20);
            this.viewTsmi.Text = "&View";
            // 
            // centerTsmi
            // 
            this.centerTsmi.Name = "centerTsmi";
            this.centerTsmi.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.centerTsmi.Size = new System.Drawing.Size(151, 22);
            this.centerTsmi.Text = "&Center";
            this.centerTsmi.Click += new System.EventHandler(this.centerTsmi_Click);
            // 
            // circuitToolStripMenuItem
            // 
            this.circuitToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createICToolStripMenuItem});
            this.circuitToolStripMenuItem.Name = "circuitToolStripMenuItem";
            this.circuitToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.circuitToolStripMenuItem.Text = "Circuit";
            // 
            // createICToolStripMenuItem
            // 
            this.createICToolStripMenuItem.Name = "createICToolStripMenuItem";
            this.createICToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.createICToolStripMenuItem.Text = "Create IC...";
            this.createICToolStripMenuItem.Click += new System.EventHandler(this.createICToolStripMenuItem_Click);
            // 
            // simulationToolStripMenuItem1
            // 
            this.simulationToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toggleSimulationTsmi,
            this.speedTsmi,
            this.toolStripSeparator3,
            this.stepSimulationTsmi});
            this.simulationToolStripMenuItem1.Name = "simulationToolStripMenuItem1";
            this.simulationToolStripMenuItem1.Size = new System.Drawing.Size(76, 20);
            this.simulationToolStripMenuItem1.Text = "&Simulation";
            // 
            // toggleSimulationTsmi
            // 
            this.toggleSimulationTsmi.Name = "toggleSimulationTsmi";
            this.toggleSimulationTsmi.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Space)));
            this.toggleSimulationTsmi.Size = new System.Drawing.Size(170, 22);
            this.toggleSimulationTsmi.Text = "&Pause";
            this.toggleSimulationTsmi.Click += new System.EventHandler(this.toggleSimulationTsmi_Click);
            // 
            // speedTsmi
            // 
            this.speedTsmi.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.x025ToolStripMenuItem,
            this.x05ToolStripMenuItem,
            this.x075ToolStripMenuItem,
            this.x1ToolStripMenuItem,
            this.x125ToolStripMenuItem,
            this.x15ToolStripMenuItem,
            this.x175ToolStripMenuItem,
            this.x2ToolStripMenuItem});
            this.speedTsmi.Name = "speedTsmi";
            this.speedTsmi.Size = new System.Drawing.Size(170, 22);
            this.speedTsmi.Text = "Speed";
            this.speedTsmi.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.speedTsmi_DropDownItemClicked);
            // 
            // x025ToolStripMenuItem
            // 
            this.x025ToolStripMenuItem.Name = "x025ToolStripMenuItem";
            this.x025ToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.x025ToolStripMenuItem.Tag = "0.25";
            this.x025ToolStripMenuItem.Text = "0.25";
            // 
            // x05ToolStripMenuItem
            // 
            this.x05ToolStripMenuItem.Name = "x05ToolStripMenuItem";
            this.x05ToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.x05ToolStripMenuItem.Tag = "0.5";
            this.x05ToolStripMenuItem.Text = "0.5";
            // 
            // x075ToolStripMenuItem
            // 
            this.x075ToolStripMenuItem.Name = "x075ToolStripMenuItem";
            this.x075ToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.x075ToolStripMenuItem.Tag = "0.75";
            this.x075ToolStripMenuItem.Text = "0.75";
            // 
            // x1ToolStripMenuItem
            // 
            this.x1ToolStripMenuItem.Checked = true;
            this.x1ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.x1ToolStripMenuItem.Name = "x1ToolStripMenuItem";
            this.x1ToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.x1ToolStripMenuItem.Tag = "1";
            this.x1ToolStripMenuItem.Text = "Normal";
            // 
            // x125ToolStripMenuItem
            // 
            this.x125ToolStripMenuItem.Name = "x125ToolStripMenuItem";
            this.x125ToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.x125ToolStripMenuItem.Tag = "1.25";
            this.x125ToolStripMenuItem.Text = "1.25";
            // 
            // x15ToolStripMenuItem
            // 
            this.x15ToolStripMenuItem.Name = "x15ToolStripMenuItem";
            this.x15ToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.x15ToolStripMenuItem.Tag = "1.5";
            this.x15ToolStripMenuItem.Text = "1.5";
            // 
            // x175ToolStripMenuItem
            // 
            this.x175ToolStripMenuItem.Name = "x175ToolStripMenuItem";
            this.x175ToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.x175ToolStripMenuItem.Tag = "1.75";
            this.x175ToolStripMenuItem.Text = "1.75";
            // 
            // x2ToolStripMenuItem
            // 
            this.x2ToolStripMenuItem.Name = "x2ToolStripMenuItem";
            this.x2ToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.x2ToolStripMenuItem.Tag = "2.0";
            this.x2ToolStripMenuItem.Text = "2";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(167, 6);
            // 
            // stepSimulationTsmi
            // 
            this.stepSimulationTsmi.Enabled = false;
            this.stepSimulationTsmi.Name = "stepSimulationTsmi";
            this.stepSimulationTsmi.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.stepSimulationTsmi.Size = new System.Drawing.Size(170, 22);
            this.stepSimulationTsmi.Text = "&Step";
            this.stepSimulationTsmi.Click += new System.EventHandler(this.stepSimulationTsmi_Click);
            // 
            // optionsTsmi
            // 
            this.optionsTsmi.Name = "optionsTsmi";
            this.optionsTsmi.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.optionsTsmi.Size = new System.Drawing.Size(61, 20);
            this.optionsTsmi.Text = "&Options";
            this.optionsTsmi.Click += new System.EventHandler(this.optionsTsmi_Click);
            // 
            // helpTsmi
            // 
            this.helpTsmi.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.wikiTsmi,
            this.toolStripSeparator5,
            this.aboutTsmi});
            this.helpTsmi.Name = "helpTsmi";
            this.helpTsmi.Size = new System.Drawing.Size(44, 20);
            this.helpTsmi.Text = "&Help";
            // 
            // wikiTsmi
            // 
            this.wikiTsmi.Name = "wikiTsmi";
            this.wikiTsmi.Size = new System.Drawing.Size(116, 22);
            this.wikiTsmi.Text = "&Wiki...";
            this.wikiTsmi.Click += new System.EventHandler(this.wikiTsmi_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(113, 6);
            // 
            // aboutTsmi
            // 
            this.aboutTsmi.Enabled = false;
            this.aboutTsmi.Name = "aboutTsmi";
            this.aboutTsmi.Size = new System.Drawing.Size(116, 22);
            this.aboutTsmi.Text = "&About...";
            // 
            // lvComponentLibrary
            // 
            this.lvComponentLibrary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvComponentLibrary.HideSelection = false;
            this.lvComponentLibrary.Location = new System.Drawing.Point(0, 0);
            this.lvComponentLibrary.Name = "lvComponentLibrary";
            this.lvComponentLibrary.Size = new System.Drawing.Size(139, 404);
            this.lvComponentLibrary.TabIndex = 2;
            this.lvComponentLibrary.UseCompatibleStateImageBehavior = false;
            this.lvComponentLibrary.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.LvComponentLibrary_ItemDrag);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lvComponentLibrary);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.canvas);
            this.splitContainer1.Size = new System.Drawing.Size(800, 404);
            this.splitContainer1.SplitterDistance = 139;
            this.splitContainer1.TabIndex = 3;
            // 
            // canvas
            // 
            this.canvas.AdditiveKey = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.None)));
            this.canvas.AllowDrop = true;
            this.canvas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.canvas.Circuit = null;
            this.canvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.canvas.GridLimit = 25000;
            this.canvas.GridSize = 20;
            this.canvas.GridVisible = true;
            this.canvas.InclusiveKey = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.None)));
            this.canvas.InvertedScrollWheel = false;
            this.canvas.Location = new System.Drawing.Point(0, 0);
            this.canvas.Name = "canvas";
            this.canvas.PanButton = System.Windows.Forms.MouseButtons.Middle;
            this.canvas.PanPositionX = 29681F;
            this.canvas.PanPositionY = 17955F;
            this.canvas.ScrollWheelMultiplier = 0.03F;
            this.canvas.ScrollWheelZoom = true;
            this.canvas.Size = new System.Drawing.Size(657, 404);
            this.canvas.TabIndex = 0;
            this.canvas.Zoom = 1F;
            this.canvas.ZoomMaximum = 5F;
            this.canvas.ZoomMinimum = 0.05F;
            this.canvas.ZoomMouseFocus = true;
            this.canvas.DragDrop += new System.Windows.Forms.DragEventHandler(this.Canvas_DragDrop);
            this.canvas.DragEnter += new System.Windows.Forms.DragEventHandler(this.Canvas_DragEnter);
            this.canvas.DragOver += new System.Windows.Forms.DragEventHandler(this.Canvas_DragOver);
            this.canvas.DragLeave += new System.EventHandler(this.Canvas_DragLeave);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslPositions,
            this.tsslSimulationState,
            this.tsslSpring,
            this.tsslSeleciton,
            this.tsslZoom});
            this.statusStrip.Location = new System.Drawing.Point(0, 428);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(800, 22);
            this.statusStrip.TabIndex = 4;
            this.statusStrip.Text = "statusStrip1";
            // 
            // tsslPositions
            // 
            this.tsslPositions.Name = "tsslPositions";
            this.tsslPositions.Size = new System.Drawing.Size(120, 17);
            this.tsslPositions.Text = "0.12, 0.23 [0.54, 0.123]";
            // 
            // tsslSimulationState
            // 
            this.tsslSimulationState.Name = "tsslSimulationState";
            this.tsslSimulationState.Size = new System.Drawing.Size(72, 17);
            this.tsslSimulationState.Text = "10 UPS @ 1x";
            // 
            // tsslSpring
            // 
            this.tsslSpring.Name = "tsslSpring";
            this.tsslSpring.Size = new System.Drawing.Size(411, 17);
            this.tsslSpring.Spring = true;
            this.tsslSpring.Text = "   ";
            // 
            // tsslSeleciton
            // 
            this.tsslSeleciton.Name = "tsslSeleciton";
            this.tsslSeleciton.Size = new System.Drawing.Size(147, 17);
            this.tsslSeleciton.Text = "12 components, 5 selected";
            // 
            // tsslZoom
            // 
            this.tsslZoom.Name = "tsslZoom";
            this.tsslZoom.Size = new System.Drawing.Size(35, 17);
            this.tsslZoom.Text = "125%";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Logic Sandbox";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.CircuitCanvas canvas;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileTsmi;
        private System.Windows.Forms.ToolStripMenuItem newTsmi;
        private System.Windows.Forms.ToolStripMenuItem openTsmi;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem saveTsmi;
        private System.Windows.Forms.ToolStripMenuItem saveAsTsmi;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exitTsmi;
        private System.Windows.Forms.ToolStripMenuItem editTsmi;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem selectAllTsmi;
        private System.Windows.Forms.ToolStripMenuItem optionsTsmi;
        private System.Windows.Forms.ToolStripMenuItem helpTsmi;
        private System.Windows.Forms.ToolStripMenuItem wikiTsmi;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem aboutTsmi;
        private System.Windows.Forms.ToolStripMenuItem deselectAllTsmi;
        private System.Windows.Forms.ToolStripMenuItem invertSelectionTsmi;
        private System.Windows.Forms.ToolStripMenuItem viewTsmi;
        private System.Windows.Forms.ToolStripMenuItem centerTsmi;
        private System.Windows.Forms.ToolStripMenuItem exportTsmi;
        private System.Windows.Forms.ToolStripMenuItem exportAsImageTsmi;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ListView lvComponentLibrary;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem circuitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createICToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripMenuItem simulationToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toggleSimulationTsmi;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem stepSimulationTsmi;
        private System.Windows.Forms.ToolStripStatusLabel tsslPositions;
        private System.Windows.Forms.ToolStripStatusLabel tsslSimulationState;
        private System.Windows.Forms.ToolStripStatusLabel tsslSpring;
        private System.Windows.Forms.ToolStripStatusLabel tsslSeleciton;
        private System.Windows.Forms.ToolStripStatusLabel tsslZoom;
        private System.Windows.Forms.ToolStripMenuItem speedTsmi;
        private System.Windows.Forms.ToolStripMenuItem x025ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem x05ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem x075ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem x1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem x125ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem x15ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem x175ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem x2ToolStripMenuItem;
    }
}

