namespace Maxstupo.LogicSandbox.Forms {

    using System;
    using System.Windows.Forms;
    using Maxstupo.LogicSandbox.Logic.Components;

    public partial class FormRenameComponent : Form {

        private readonly DigitalComponent component;

        public FormRenameComponent(DigitalComponent component = null) {
            InitializeComponent();

            this.component = component;

            if (component != null)
                txtComponentLabel.Text = component.Label;
        }

        private void btnOkay_Click(object sender, EventArgs e) {
            if (component != null)
                component.Label = txtComponentLabel.Text;

            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
        }

    }

}