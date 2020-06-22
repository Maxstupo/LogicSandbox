namespace Maxstupo.LogicSandbox.Logic {

    using System.Drawing;

    /// <summary>
    /// Represents a bidirectional connection between two pins.
    /// </summary>
    public class Wire {

        public static Color WireHighColor { get; set; } = Color.Red;
        public static Color WireLowColor { get; set; } = Color.Blue;

        public Pin P1 { get; }

        public Pin P2 { get; }

        public Wire(Pin p1, Pin p2) {
            this.P1 = p1;
            this.P2 = p2;
        }

        /// <summary>
        /// Draws the wire, adjusting the wire color depending on if the wire is active or not.
        /// </summary>
        public void Draw(Graphics g) {
            Color color = (P1.Value || P2.Value) ? WireHighColor : WireLowColor;

            using (Pen pen = new Pen(color))
                g.DrawLine(pen, P1.GlobalX, P1.GlobalY, P2.GlobalX, P2.GlobalY);
        }

        /// <summary>
        /// Simulates the wire by stepping in time.
        /// </summary>
        /// <param name="deltaTime">Delta between steps, in seconds.</param>
        /// <returns>True if the state was changed, false otherwise.</returns>
        public bool Step(float deltaTime) {
            bool drivingValue = P1.Polarity == Polarity.Output ? P1.Value : P2.Value;
            bool otherValue = P1.Polarity == Polarity.Output ? P2.Value : P1.Value;

            P1.Value = P2.Value = drivingValue;

            return otherValue != drivingValue;
        }

    }

}