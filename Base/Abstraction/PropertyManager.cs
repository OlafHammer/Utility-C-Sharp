using System.Reflection;
using Utility.Base.Attributes;

namespace Utility.Base.Abstraction;

public static class PropertyManager
{
    // returns a List of names of all relevant Properties of Object t while ignoring all Properties tagged with the Attribute "HideProperty"
    public static List<string> PropertyNames(object t, params AvalibleAttributes[] attributes)
    {
        return EnumProperties(t, attributes).Select(e => e.Name).ToList();
    }

    // returns a List of values as T of all relevant Properties of Object t while ignoring all Properties tagged with the Attribute "HideProperty"
    public static List<T> PropertyValues<T>(object t, params AvalibleAttributes[] attributes) where T : class
    {
        List<T> list = new();
        EnumProperties(t, attributes).ToList().ForEach(e =>
        {
            if (e.GetValue(t, null) is T val)
                list.Add((T)val);
        });
        return list;
    }

    // returns a List of all relevant Properties of Object t while ignoring all Properties tagged with the Attribute "HideProperty"
    public static List<PropertyInfo> Properties(object t, params AvalibleAttributes[] attributes)
    {
        return EnumProperties(t, attributes).ToList();
    }

    // returns the amount of all relevant Properties of Object t while ignoring all Properties tagged with the Attribute "HideProperty"
    public static int PropertyCount(object t, params AvalibleAttributes[] attributes)
    {
        return EnumProperties(t, attributes).Count();
    }

    private static IEnumerable<PropertyInfo> EnumProperties(object t, IEnumerable<AvalibleAttributes> attributes)
    {
        return t.GetType().GetProperties().Where(prop =>
            !prop.CustomAttributes.Any(e =>
                attributes.Select(attribute => attribute.ToString()).Contains(e.AttributeType.Name)));
    }
}