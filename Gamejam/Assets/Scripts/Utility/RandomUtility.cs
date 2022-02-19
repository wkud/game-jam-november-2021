using System;
using System.Collections.Generic;
using System.Linq;

static class RandomUtility
{
    public static T RandomizeFrom<T>(IEnumerable<T> collection)
    {
        var index = UnityEngine.Random.Range(0, collection.Count());
        var randomItem = collection.ElementAt(index);
        return randomItem;
    }

    public static T GetRandomEnumValueOf<T>(params T[] itemsToRemove) where T:Enum
    {
        var enumValues = EnumUtility.GetAllEnumValuesAsCollection<T>().Except(itemsToRemove);
        var randomEnum = RandomizeFrom(enumValues);

        return randomEnum;
    }
}
