using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

public static class EnumUtility
{
    public static List<T> GetAllEnumValuesAsCollection<T>() where T : Enum
    {
        var values = Enum.GetValues(typeof(T));
        var valueList = values.Cast<T>().ToList();
        return valueList;
    }

    public static string GetDescription<T>(this T value) where T : Enum
    {
        var attribute = value.GetAttribute<DescriptionAttribute>();
        return attribute == null ? value.ToString() : attribute.Description;
    }

    private static T GetAttribute<T>(this Enum value) where T : Attribute
    {
        var type = value.GetType();
        var memberInfo = type.GetMember(value.ToString());

        if (memberInfo.Length == 0)
        {
            return null;
        }

        var attributes = memberInfo[0].GetCustomAttributes(typeof(T), false);
        var attribute = attributes.Length > 0 ? (T)attributes[0] : null;
        return attribute;
    }
}
