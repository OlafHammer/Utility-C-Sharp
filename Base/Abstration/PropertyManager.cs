using Utility.Base.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Immutable;
using Utility.SQL_Server;
using System.Xml.Linq;
using System.Net.NetworkInformation;

namespace Utility.Base.Abstration
{
    public static class PropertyManager
    {
        // returns a List of names of all relevant Propertys of Object t while ignoring all Propertys tagged with the Attribute "HideProperty"
        public static List<string> PropertyNames(object t, params AvalibleAttributes[] attributes) => EnumProperties(t, attributes).Select(e => e.Name).ToList();

        // returns a List of values as T of all relevant Propertys of Object t while ignoring all Propertys tagged with the Attribute "HideProperty"
        public static List<T> PropertyValues<T>(object t, params AvalibleAttributes[] attributes) where T : class
        {
            List<T> list = new();
            EnumProperties(t, attributes).ToList().ForEach(e =>
            {
                if (e.GetValue(t, null) is T val)
                    list.Add((T) val);
            });
            return list;
        }

        // returns a List of all relevant Propertys of Object t while ignoring all Propertys tagged with the Attribute "HideProperty"
        public static List<PropertyInfo> Propertys(object t, params AvalibleAttributes[] attributes) => EnumProperties(t, attributes).ToList();

        // returns the amout of all relevant Propertys of Object t while ignoring all Propertys tagged with the Attribute "HideProperty"
        public static int PropertyCount(object t, params AvalibleAttributes[] attributes) => EnumProperties(t, attributes).Count();

        private static IEnumerable<PropertyInfo> EnumProperties(object t, AvalibleAttributes[] attributes) => t.GetType().GetProperties().Where(prop => !prop.CustomAttributes.Where(e => attributes.Select(attribute => attribute.ToString()).Contains(e.AttributeType.Name)).Any());

    }
}