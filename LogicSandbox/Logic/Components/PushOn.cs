namespace Maxstupo.LogicSandbox.Logic.Components {
  
    public class PushOn : ButtonComponent {

        public PushOn(string id, float x, float y) : base(id, "Push On", x, y, 34, 34) { }

        protected override void OnButtonPressed() {
            ButtonState = true;
        }

        protected override void OnButtonReleased() {
            ButtonState = false;
        }

    }

}