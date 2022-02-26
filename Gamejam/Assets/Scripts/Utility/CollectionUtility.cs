using System.Collections.Generic;

public static class CollectionUtility
{
    public static List<T> AsList<T>(this T item)
    {
        return new List<T>() { item };
    }
}
