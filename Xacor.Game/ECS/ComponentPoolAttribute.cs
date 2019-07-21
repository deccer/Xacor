using System;

namespace Xacor.Game.ECS
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class ComponentPoolAttribute : Attribute
    {
        public ComponentPoolAttribute()
        {
            InitialSize = 10;
            ResizeSize = 10;
            IsResizable = true;
            IsSupportMultiThread = false;
        }

        public int InitialSize { get; set; }

        public int ResizeSize { get; set; }

        public bool IsResizable { get; set; }

        public bool IsSupportMultiThread { get; set; }
    }
}