using System;
using System.Collections.Generic;
using System.Reflection;

namespace Xacor.Game.ECS
{
    public class AttributesProcessor
    {
        public static readonly List<Type> SupportedAttributes = new List<Type>
                                                                    {
                                                                        typeof(EntitySystemAttribute),
                                                                        typeof(EntityTemplateAttribute),
                                                                        typeof(ComponentPoolAttribute),
                                                                        typeof(ComponentCreateAttribute)
                                                                    };

        public static IDictionary<Type, List<Attribute>> Process(List<Type> supportedAttributes)
        {
            return Process(supportedAttributes, AppDomain.CurrentDomain.GetAssemblies());
        }

        public static IDictionary<Type, List<Attribute>> Process(List<Type> supportedAttributes, IEnumerable<Assembly> assembliesToScan) // Do not double overload "= null)"
        {
            IDictionary<Type, List<Attribute>> attributeTypes = new Dictionary<Type, List<Attribute>>();

            if (assembliesToScan != null)
            {
                foreach (var item in assembliesToScan)
                {
                    IEnumerable<Type> types = item.GetTypes();

                    foreach (var type in types)
                    {
                        var attributes = type.GetCustomAttributes(false);
                        foreach (var attribute in attributes)
                        {
                            if (supportedAttributes.Contains(attribute.GetType()))
                            {
                                if (!attributeTypes.ContainsKey(type))
                                {
                                    attributeTypes[type] = new List<Attribute>();
                                }

                                attributeTypes[type].Add((Attribute)attribute);
                            }
                        }
                    }
                }
            }

            return attributeTypes;
        }
    }
}