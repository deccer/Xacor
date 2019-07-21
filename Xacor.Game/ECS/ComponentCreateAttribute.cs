using System;

namespace Xacor.Game.ECS
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class ComponentCreateAttribute : Attribute
    {
    }
}