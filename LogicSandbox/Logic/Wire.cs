namespace Maxstupo.LogicSandbox.Logic {

    using System.Drawing;

    public class Wire {

        public Pin P1 { get; }

        public Pin P2 { get; }

        public Wire(Pin p1, Pin p2) {
            this.P1 = p1;
            this.P2 = p2;
        }

        public void Draw(Graphics g) {
            Pen pen = (P1.Value || P2.Value) ? Pens.Red : Pens.Blue;

            g.DrawLine(pen, P1.GlobalX, P1.GlobalY, P2.GlobalX, P2.GlobalY);
        }

        public bool Step(float stepRate) {
            bool drivingValue = P1.Polarity == Polarity.Output ? P1.Value : P2.Value;
            bool otherValue = P1.Polarity == Polarity.Output ? P2.Value : P1.Value;

            P1.Value = P2.Value = drivingValue;

            return otherValue != drivingValue;
        }

    }

}