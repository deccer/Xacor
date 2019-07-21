namespace Xacor
{
    public class KeyboardInputMapping : InputMapping
    {
        public InputButton Key1 { get; }

        public InputButton Key2 { get; }

        public KeyboardInputMapping(string name, InputButton key1 = InputButton.Unassigned, InputButton key2 = InputButton.Unassigned)
            : base(name)
        {
            Key1 = key1;
            Key2 = key2;
        }
    }
}