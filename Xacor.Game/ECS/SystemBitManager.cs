using System.Collections.Generic;
using System.Numerics;

namespace Xacor.Game.ECS
{
    internal class SystemBitManager
    {
        private readonly Dictionary<EntitySystem, BigInteger> _systemBits = new Dictionary<EntitySystem, BigInteger>();

        private int _position;

        public BigInteger GetBitFor(EntitySystem entitySystem)
        {
            if (_systemBits.TryGetValue(entitySystem, out var bit) != false)
            {
                return bit;
            }

            bit = new BigInteger(1) << _position;

            _position++;
            _systemBits.Add(entitySystem, bit);

            return bit;
        }
    }
}