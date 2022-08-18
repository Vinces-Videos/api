using Database;

namespace Helpers;

public static class AttributeHelper
{
    //Locate the DbCollectionName attribute on the class or one of its base classes.
    public static string GetDbCollectionName(Type classType)
    {
        var DbCollectionAttributes = Attribute.GetCustomAttributes(classType, true).Where(attr => attr is DbCollectionName).ToList();
        if (DbCollectionAttributes.Count > 1) 
            throw new Exception($"Multiple DbCollectionName attributes have been found on {classType.Name}. This is not okay.");

        if (!DbCollectionAttributes.Any())
            throw new Exception($"The type {classType.Name} does not have the DbCollectionName attribute");

        return (DbCollectionAttributes.First() as DbCollectionName).GetName;
    }
}