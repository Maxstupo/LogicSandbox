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
            this.simulationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createICToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsTsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.helpTsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.wikiTsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutTsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.lvComponentLibrary = new System.Windows.Forms.ListView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.canvas = new Maxstupo.LogicSandbox.Controls.Canvas();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileTsmi,
            this.editTsmi,
            this.viewTsmi,
            this.circuitToolStripMenuItem,
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
            this.exportAsImageTsmi.Size = new System.Drawing.Size(166, 22);
            this.exportAsImageTsmi.Text = "Export as Image...";
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
            this.deleteToolStripMenuItem.Text = "&Delete";
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
            this.simulationToolStripMenuItem,
            this.createICToolStripMenuItem});
            this.circuitToolStripMenuItem.Name = "circuitToolStripMenuItem";
            this.circuitToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.circuitToolStripMenuItem.Text = "Circuit";
            // 
            // simulationToolStripMenuItem
            // 
            this.simulationToolStripMenuItem.Enabled = false;
            this.simulationToolStripMenuItem.Name = "simulationToolStripMenuItem";
            this.simulationToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.simulationToolStripMenuItem.Text = "Simulation";
            // 
            // createICToolStripMenuItem
            // 
            this.createICToolStripMenuItem.Name = "createICToolStripMenuItem";
            this.createICToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.createICToolStripMenuItem.Text = "Create IC...";
            this.createICToolStripMenuItem.Click += new System.EventHandler(this.createICToolStripMenuItem_Click);
            // 
            // optionsTsmi
            // 
            this.optionsTsmi.Name = "optionsTsmi";
            this.optionsTsmi.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.optionsTsmi.Size = new System.Drawing.Size(61, 20);
            this.optionsTsmi.Text = "&Options";
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
            this.lvComponentLibrary.Size = new System.Drawing.Size(139, 426);
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
            this.splitContainer1.Size = new System.Drawing.Size(800, 426);
            this.splitContainer1.SplitterDistance = 139;
            this.splitContainer1.TabIndex = 3;
            // 
            // canvas
            // 
            this.canvas.AllowDrop = true;
            this.canvas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.canvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.canvas.InvertedScrollWheel = false;
            this.canvas.Location = new System.Drawing.Point(0, 0);
            this.canvas.Name = "canvas";
            this.canvas.PanButton = System.Windows.Forms.MouseButtons.Middle;
            this.canvas.PanPositionX = 18512F;
            this.canvas.PanPositionY = 11076F;
            this.canvas.ScrollWheelMultiplier = 0.03F;
            this.canvas.ScrollWheelZoom = true;
            this.canvas.Size = new System.Drawing.Size(657, 426);
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
            this.statusStrip.Location = new System.Drawing.Point(0, 428);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(800, 22);
            this.statusStrip.TabIndex = 4;
            this.statusStrip.Text = "statusStrip1";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Logic Sandbox";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.Canvas canvas;
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
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem circuitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem simulationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createICToolStripMenuItem;
    }
}

