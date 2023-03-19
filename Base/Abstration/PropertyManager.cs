using Utility.Base.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Immutable;
using Utility.SQL_Server;

namespace Utility.Base.Abstration
{
    public static class PropertyManager
    {
        // returns a List of names of all relevant Propertys of Object t while ignoring all Propertys tagged with the Attribute "HideProperty"
        public static List<string> PropertyNames(object t) => EnumProperties(t).Select(e => e.Name).ToList();

        // returns a List of values as T of all relevant Propertys of Object t while ignoring all Propertys tagged with the Attribute "HideProperty"
        public static List<T> PropertyValues<T>(object t) where T : class
        {
            List<T> list = new();
            EnumProperties(t).ToList().ForEach(e =>
            {
                if (e.GetValue(t, null) is T val)
                    list.Add((T) val);
            });
            return list;
        }

        // returns a List of all relevant Propertys of Object t while ignoring all Propertys tagged with the Attribute "HideProperty"
        public static List<PropertyInfo> Propertys(object t) => EnumProperties(t).ToList();

        // returns the amout of all relevant Propertys of Object t while ignoring all Propertys tagged with the Attribute "HideProperty"
        public static int PropertyCount(object t) => EnumProperties(t).Count();

        private static IEnumerable<PropertyInfo> EnumProperties(object t) => t.GetType().GetProperties().Where(prop => !prop.CustomAttributes.Where(e => e.AttributeType.Name == nameof(AvalibleAttributes.HideProperty)).Any());
    }
}