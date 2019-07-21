namespace Xacor
{
    public abstract class InputMapping
    {
        public string Name { get; set; }

        protected InputMapping(string name)
        {
            Name = name;
        }
    }
}