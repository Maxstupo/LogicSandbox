namespace Maxstupo.LogicSandbox.Logic.Components {
   
    public class PushOff : ButtonComponent {

        public PushOff(string id) : this(id, 0, 0) {

        }

        public PushOff(string id, float x, float y) : base(id, "Push Off", x, y) {
            ButtonState = true;
        }

        protected override void OnButtonPressed() {
            ButtonState = false;
        }

        protected override void OnButtonReleased() {
            ButtonState = true;
        }

    }

}