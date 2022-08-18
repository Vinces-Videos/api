namespace Helpers;

public static class AttributeHelper
{
    public static string GetDbCollectionName(Type classType)
    {
        foreach(var attr in Attribute.GetCustomAttributes(classType, true))
        {
            if (attr is DbCollectionName a)
                return a.GetName;
        }

        throw new KeyNotFoundException($"The type {classType.Name} does not have the DbCollectionName attribute");
    }
}