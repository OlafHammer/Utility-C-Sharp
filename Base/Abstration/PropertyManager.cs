using Utility.Base.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Immutable;

namespace Utility.Base.Abstration
{
    public static class PropertyManager<T>
    {
        // returns a List of names of all relevant Propertys of Class T while ignoring all Propertys tagged with the Attribute "HideProperty"
        public static List<string> PropertyNames => EnumProperties.Select(e => e.Name).ToList();

        // returns a List of all relevant Propertys of Class T while ignoring all Propertys tagged with the Attribute "HideProperty"
        public static List<PropertyInfo> Propertys => EnumProperties.ToList();

        // returns the amout of all relevant Propertys of Class T while ignoring all Propertys tagged with the Attribute "HideProperty"
        public static int PropertyCount => EnumProperties.Count();

        private static IEnumerable<PropertyInfo> EnumProperties => typeof(T).GetProperties().Where(prop => !prop.CustomAttributes.Where(e => e.AttributeType.Name == nameof(AvalibleAttributes.HideProperty)).Any());

    }
}
