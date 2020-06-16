namespace Maxstupo.LogicSandbox.Logic {

    using System;
    using System.Drawing;

    public class Wire {

        public Pin P1 { get; }

        public Pin P2 { get; }

        public Wire(Pin p1, Pin p2) {
            this.P1 = p1 ?? throw new ArgumentNullException(nameof(p1));
            this.P2 = p2 ?? throw new ArgumentNullException(nameof(p2));
        }

        public void Draw(Graphics g) {
            g.DrawLine(!P1.Value ? Pens.Blue : Pens.Red, P1.GlobalX, P1.GlobalY, P2.GlobalX, P2.GlobalY);
        }

    }

}