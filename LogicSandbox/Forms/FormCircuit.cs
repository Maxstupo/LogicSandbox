namespace Maxstupo.LogicSandbox.Forms {
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using Maxstupo.LogicSandbox.Logic;

    public partial class FormCircuit : Form {
        public FormCircuit(Circuit circuit) {
            InitializeComponent();
            circuitCanvas1.Circuit = circuit;
        }
    }
}
