namespace Xacor
{
    public class MouseInputMapping : InputMapping
    {
        public Axis Axis { get; }

        public MouseInputMapping(string name, Axis axis)
            : base(name)
        {
            Axis = axis;
        }
    }
}