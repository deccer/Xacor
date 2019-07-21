using System;

namespace Xacor.Game.ECS
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class EntitySystemAttribute : Attribute
    {
        public EntitySystemAttribute()
        {
            GameLoopType = GameLoopType.Update;
            Layer = 0;
            ExecutionType = ExecutionType.Synchronous;
        }

        public GameLoopType GameLoopType { get; set; }

        public int Layer { get; set; }

        public ExecutionType ExecutionType { get; set; }
    }
}