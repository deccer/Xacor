using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Reflection;

namespace Xacor.Game.ECS
{
    public static class ComponentTypeManager
    {
        private static readonly Dictionary<Type, ComponentType> ComponentTypes = new Dictionary<Type, ComponentType>();

        public static BigInteger GetBit<T>() where T : IComponent
        {
            return GetTypeFor<T>().Bit;
        }

        public static int GetId<T>() where T : IComponent
        {
            return GetTypeFor<T>().Id;
        }

        public static ComponentType GetTypeFor<T>() where T : IComponent
        {
            return GetTypeFor(typeof(T));
        }

        public static ComponentType GetTypeFor(Type component)
        {
            Debug.Assert(component != null, "Component must not be null.");

            if (!ComponentTypes.TryGetValue(component, out var result))
            {
                result = new ComponentType();
                ComponentTypes.Add(component, result);
            }

            return result;
        }

        public static void Initialize(params Assembly[] assembliesToScan)
        {
            if (assembliesToScan.Length == 0)
            {
                assembliesToScan = AppDomain.CurrentDomain.GetAssemblies().ToArray();
            }

            foreach (var assembly in assembliesToScan)
            {
                IEnumerable<Type> types = assembly.GetTypes();
                Initialize(types, ignoreInvalidTypes: true);
            }
        }

        public static void Initialize(IEnumerable<Type> types, bool ignoreInvalidTypes = false)
        {
            foreach (var type in types)
            {
                if (typeof(IComponent).IsAssignableFrom(type))
                {
                    if (type.IsInterface)
                    {
                        continue;
                    }

                    if (type == typeof(ComponentPoolable))
                    {
                        continue;
                    }

                    GetTypeFor(type);
                }
                else if (!ignoreInvalidTypes)
                {
                    throw new ArgumentException($"Type {type} does not implement {typeof(IComponent)} interface");
                }
            }
        }

        internal static IEnumerable<Type> GetTypesFromBits(BigInteger bits)
        {
            foreach (var (key, value) in ComponentTypes)
            {
                if ((value.Bit & bits) != 0)
                {
                    yield return key;
                }
            }
        }

        internal static void SetTypeFor<T>(ComponentType type)
        {
            ComponentTypes.Add(typeof(T), type);
        }
    }
}