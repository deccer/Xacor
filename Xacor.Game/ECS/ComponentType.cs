using System.Diagnostics;
using System.Numerics;

namespace Xacor.Game.ECS
{
    [DebuggerDisplay("Id:{Id}, Bit:{Bit}")]
    public sealed class ComponentType
    {
        private static BigInteger _nextBit;

        private static int _nextId;

        static ComponentType()
        {
            _nextBit = 1;
            _nextId = 0;
        }

        internal ComponentType()
        {
            Id = _nextId;
            Bit = _nextBit;

            _nextId++;
            _nextBit <<= 1;
        }

        public int Id { get; private set; }

        public BigInteger Bit { get; private set; }
    }

    internal static class ComponentType<T> where T : IComponent
    {
        static ComponentType()
        {
            CType = ComponentTypeManager.GetTypeFor<T>();
            if (CType == null)
            {
                CType = new ComponentType();
                ComponentTypeManager.SetTypeFor<T>(CType);
            }
        }

        public static ComponentType CType { get; private set; }
    }
}