﻿namespace Maxstupo.LogicSandbox.Forms {
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
            this.canvas = new Maxstupo.LogicSandbox.Controls.Canvas();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileTsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.newTsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.openTsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.saveTsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsTsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitTsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.editTsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.undoTsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.redoTsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.cutTsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.copyTsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteTsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.selectAllTsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsTsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.helpTsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.wikiTsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutTsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // canvas
            // 
            this.canvas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.canvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.canvas.InvertedScrollWheel = false;
            this.canvas.Location = new System.Drawing.Point(0, 24);
            this.canvas.Name = "canvas";
            this.canvas.PanButton = System.Windows.Forms.MouseButtons.Middle;
            this.canvas.PanPositionX = 400F;
            this.canvas.PanPositionY = 213F;
            this.canvas.ScrollWheelMultiplier = 0.03F;
            this.canvas.ScrollWheelZoom = true;
            this.canvas.Size = new System.Drawing.Size(800, 426);
            this.canvas.TabIndex = 0;
            this.canvas.Zoom = 1F;
            this.canvas.ZoomMaximum = 5F;
            this.canvas.ZoomMinimum = 0.05F;
            this.canvas.ZoomMouseFocus = true;
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileTsmi,
            this.editTsmi,
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
            this.newTsmi.Size = new System.Drawing.Size(180, 22);
            this.newTsmi.Text = "&New";
            // 
            // openTsmi
            // 
            this.openTsmi.Image = ((System.Drawing.Image)(resources.GetObject("openTsmi.Image")));
            this.openTsmi.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openTsmi.Name = "openTsmi";
            this.openTsmi.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openTsmi.Size = new System.Drawing.Size(180, 22);
            this.openTsmi.Text = "&Open";
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(177, 6);
            // 
            // saveTsmi
            // 
            this.saveTsmi.Image = ((System.Drawing.Image)(resources.GetObject("saveTsmi.Image")));
            this.saveTsmi.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveTsmi.Name = "saveTsmi";
            this.saveTsmi.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveTsmi.Size = new System.Drawing.Size(180, 22);
            this.saveTsmi.Text = "&Save";
            // 
            // saveAsTsmi
            // 
            this.saveAsTsmi.Name = "saveAsTsmi";
            this.saveAsTsmi.Size = new System.Drawing.Size(180, 22);
            this.saveAsTsmi.Text = "Save &As";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(177, 6);
            // 
            // exitTsmi
            // 
            this.exitTsmi.Name = "exitTsmi";
            this.exitTsmi.Size = new System.Drawing.Size(180, 22);
            this.exitTsmi.Text = "E&xit";
            // 
            // editTsmi
            // 
            this.editTsmi.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoTsmi,
            this.redoTsmi,
            this.toolStripSeparator3,
            this.cutTsmi,
            this.copyTsmi,
            this.pasteTsmi,
            this.toolStripSeparator4,
            this.selectAllTsmi});
            this.editTsmi.Name = "editTsmi";
            this.editTsmi.Size = new System.Drawing.Size(39, 20);
            this.editTsmi.Text = "&Edit";
            // 
            // undoTsmi
            // 
            this.undoTsmi.Enabled = false;
            this.undoTsmi.Name = "undoTsmi";
            this.undoTsmi.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.undoTsmi.Size = new System.Drawing.Size(180, 22);
            this.undoTsmi.Text = "&Undo";
            // 
            // redoTsmi
            // 
            this.redoTsmi.Enabled = false;
            this.redoTsmi.Name = "redoTsmi";
            this.redoTsmi.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.redoTsmi.Size = new System.Drawing.Size(180, 22);
            this.redoTsmi.Text = "&Redo";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(177, 6);
            // 
            // cutTsmi
            // 
            this.cutTsmi.Enabled = false;
            this.cutTsmi.Image = ((System.Drawing.Image)(resources.GetObject("cutTsmi.Image")));
            this.cutTsmi.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cutTsmi.Name = "cutTsmi";
            this.cutTsmi.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.cutTsmi.Size = new System.Drawing.Size(180, 22);
            this.cutTsmi.Text = "Cu&t";
            // 
            // copyTsmi
            // 
            this.copyTsmi.Enabled = false;
            this.copyTsmi.Image = ((System.Drawing.Image)(resources.GetObject("copyTsmi.Image")));
            this.copyTsmi.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.copyTsmi.Name = "copyTsmi";
            this.copyTsmi.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyTsmi.Size = new System.Drawing.Size(180, 22);
            this.copyTsmi.Text = "&Copy";
            // 
            // pasteTsmi
            // 
            this.pasteTsmi.Enabled = false;
            this.pasteTsmi.Image = ((System.Drawing.Image)(resources.GetObject("pasteTsmi.Image")));
            this.pasteTsmi.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pasteTsmi.Name = "pasteTsmi";
            this.pasteTsmi.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.pasteTsmi.Size = new System.Drawing.Size(180, 22);
            this.pasteTsmi.Text = "&Paste";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(177, 6);
            // 
            // selectAllTsmi
            // 
            this.selectAllTsmi.Enabled = false;
            this.selectAllTsmi.Name = "selectAllTsmi";
            this.selectAllTsmi.Size = new System.Drawing.Size(180, 22);
            this.selectAllTsmi.Text = "Select &All";
            // 
            // optionsTsmi
            // 
            this.optionsTsmi.Name = "optionsTsmi";
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
            this.wikiTsmi.Enabled = false;
            this.wikiTsmi.Name = "wikiTsmi";
            this.wikiTsmi.Size = new System.Drawing.Size(180, 22);
            this.wikiTsmi.Text = "&Wiki...";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(177, 6);
            // 
            // aboutTsmi
            // 
            this.aboutTsmi.Enabled = false;
            this.aboutTsmi.Name = "aboutTsmi";
            this.aboutTsmi.Size = new System.Drawing.Size(180, 22);
            this.aboutTsmi.Text = "&About...";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.canvas);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Logic Sandbox";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
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
        private System.Windows.Forms.ToolStripMenuItem undoTsmi;
        private System.Windows.Forms.ToolStripMenuItem redoTsmi;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem cutTsmi;
        private System.Windows.Forms.ToolStripMenuItem copyTsmi;
        private System.Windows.Forms.ToolStripMenuItem pasteTsmi;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem selectAllTsmi;
        private System.Windows.Forms.ToolStripMenuItem optionsTsmi;
        private System.Windows.Forms.ToolStripMenuItem helpTsmi;
        private System.Windows.Forms.ToolStripMenuItem wikiTsmi;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem aboutTsmi;
    }
}

