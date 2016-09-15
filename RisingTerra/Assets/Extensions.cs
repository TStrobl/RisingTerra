
using System;

public static class Extensions
{
    public static T GetMemberAttribute<T>(this Enum enumValue) where T : Attribute
    {
        var member = enumValue.GetType().GetMember(enumValue.ToString());
        if (member != null && member.Length > 0)
        {
            var attributes = member[0].GetCustomAttributes(typeof(T), false);
            if (attributes != null && attributes.Length > 0)
            {
                return attributes[0] as T;
            }
        }

        return null;
    }
}
