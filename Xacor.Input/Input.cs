namespace Xacor.Input
{
    public class Input
    {
        public string Name { get; }

        public InputType InputType { get; }

        public Axis Axis { get; }

        public string PositiveButton { get; }

        public string NegativeButton { get; }

        public float Sensitivity { get; set; }

        public float DeadZone { get; set; }

        public static Input CreateKeyboardInput(string name, string positiveButtonName, string negativeButtonName)
        {
            return new Input(name, InputType.Button, Axis.Horizontal, positiveButtonName, negativeButtonName);
        }

        public static Input CreateMouseMovement(string name, Axis axis)
        {
            return new Input(name, InputType.MouseMovement, axis, null, null);
        }

        private Input(string name, InputType inputType, Axis axis, string positiveButtonName, string negativeButtonName)
        {
            Name = name;
            InputType = inputType;
            Axis = axis;
            PositiveButton = positiveButtonName;
            NegativeButton = negativeButtonName;
        }
    }
}