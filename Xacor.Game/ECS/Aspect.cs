using System;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Xacor.Game.ECS
{
    public class Aspect
    {
        protected Aspect()
        {
            OneTypesMap = 0;
            ExcludeTypesMap = 0;
            ContainsTypesMap = 0;
        }

        protected BigInteger ContainsTypesMap { get; set; }

        protected BigInteger ExcludeTypesMap { get; set; }

        protected BigInteger OneTypesMap { get; set; }

        public static Aspect All(params Type[] types)
        {
            return new Aspect().GetAll(types);
        }

        public static Aspect Empty()
        {
            return new Aspect();
        }

        public static Aspect Exclude(params Type[] types)
        {
            return new Aspect().GetExclude(types);
        }

        public static Aspect One(params Type[] types)
        {
            return new Aspect().GetOne(types);
        }

        public virtual bool Interests(Entity entity)
        {
            Debug.Assert(entity != null, "Entity must not be null.");

            if (!(ContainsTypesMap > 0 || ExcludeTypesMap > 0 || OneTypesMap > 0))
            {
                return false;
            }

            ////Little help
            ////10010 & 10000 = 10000
            ////10010 | 10000 = 10010
            ////10010 | 01000 = 11010

            ////1001 & 0000 = 0000 OK
            ////1001 & 0100 = 0000 NOK           
            ////0011 & 1001 = 0001 Ok

            return ((OneTypesMap & entity.TypeBits) != 0 || OneTypesMap == 0) &&
                   ((ContainsTypesMap & entity.TypeBits) == ContainsTypesMap || ContainsTypesMap == 0) &&
                   ((ExcludeTypesMap & entity.TypeBits) == 0 || ExcludeTypesMap == 0);
        }

        public Aspect GetAll(params Type[] types)
        {
            Debug.Assert(types != null, "Types must not be null.");

            foreach (var componentType in types.Select(ComponentTypeManager.GetTypeFor))
            {
                ContainsTypesMap |= componentType.Bit;
            }

            return this;
        }

        public Aspect GetExclude(params Type[] types)
        {
            Debug.Assert(types != null, "Types must not be null.");

            foreach (var componentType in types.Select(ComponentTypeManager.GetTypeFor))
            {
                ExcludeTypesMap |= componentType.Bit;
            }

            return this;
        }

        public Aspect GetOne(params Type[] types)
        {
            Debug.Assert(types != null, "Types must not be null.");

            foreach (var componentType in types.Select(ComponentTypeManager.GetTypeFor))
            {
                OneTypesMap |= componentType.Bit;
            }

            return this;
        }

        public override string ToString()
        {
            var builder = new StringBuilder(1024);

            builder.AppendLine("Aspect :");
            AppendTypes(builder, " Requires the components : ", ContainsTypesMap);
            AppendTypes(builder, " Has none of the components : ", ExcludeTypesMap);
            AppendTypes(builder, " Has atleast one of the components : ", OneTypesMap);

            return builder.ToString();
        }

        private static void AppendTypes(StringBuilder builder, string headerMessage, BigInteger typeBits)
        {
            if (typeBits != 0)
            {
                builder.AppendLine(headerMessage);
                foreach (var type in ComponentTypeManager.GetTypesFromBits(typeBits))
                {
                    builder.Append(", ");
                    builder.AppendLine(type.Name);
                }
            }
        }
    }
}