namespace Xacor.Input
{
    public class Input
    {
        public string Name { get; }

        public InputType InputType { get; }

        public Axis Axis { get; }

        public InputButton Button { get; }

        public InputButton AlternativeButton { get; }

        public float Sensitivity { get; set; }

        public float DeadZone { get; set; }

        public static Input CreateKeyboardInput(string name, InputButton button, InputButton alternativeButton)
        {
            return new Input(name, InputType.Keyboard, Axis.Horizontal, button, alternativeButton);
        }

        public static Input CreateMouseMovement(string name, Axis axis)
        {
            return new Input(name, InputType.MouseMovement, axis, InputButton.Unassigned, InputButton.Unassigned);
        }

        private Input(string name, InputType inputType, Axis axis, InputButton button, InputButton alternativeButton)
        {
            Name = name;
            InputType = inputType;
            Axis = axis;
            Button = button;
            AlternativeButton = alternativeButton;
        }
    }
}
