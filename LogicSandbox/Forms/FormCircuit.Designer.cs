namespace Maxstupo.LogicSandbox.Forms {

    partial class FormCircuit {
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
            this.circuitCanvas1 = new Maxstupo.LogicSandbox.Controls.CircuitCanvas();
            this.SuspendLayout();
            // 
            // circuitCanvas1
            // 
            this.circuitCanvas1.AdditiveKey = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.None)));
            this.circuitCanvas1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.circuitCanvas1.Circuit = null;
            this.circuitCanvas1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.circuitCanvas1.GridLimit = 25000;
            this.circuitCanvas1.GridSize = 20;
            this.circuitCanvas1.GridVisible = true;
            this.circuitCanvas1.InclusiveKey = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.None)));
            this.circuitCanvas1.InvertedScrollWheel = false;
            this.circuitCanvas1.Location = new System.Drawing.Point(0, 0);
            this.circuitCanvas1.Name = "circuitCanvas1";
            this.circuitCanvas1.PanButton = System.Windows.Forms.MouseButtons.Middle;
            this.circuitCanvas1.PanPositionX = 311F;
            this.circuitCanvas1.PanPositionY = 176F;
            this.circuitCanvas1.ScrollWheelMultiplier = 0.03F;
            this.circuitCanvas1.ScrollWheelZoom = true;
            this.circuitCanvas1.Size = new System.Drawing.Size(622, 352);
            this.circuitCanvas1.TabIndex = 0;
            this.circuitCanvas1.Zoom = 1F;
            this.circuitCanvas1.ZoomMaximum = 5F;
            this.circuitCanvas1.ZoomMinimum = 0.05F;
            this.circuitCanvas1.ZoomMouseFocus = true;
            // 
            // FormCircuit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(622, 352);
            this.Controls.Add(this.circuitCanvas1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FormCircuit";
            this.Text = "FormCircuit";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.CircuitCanvas circuitCanvas1;
    }
}