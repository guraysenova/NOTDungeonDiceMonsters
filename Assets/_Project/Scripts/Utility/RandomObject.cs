using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class RandomObject
{
    public static T GetRandom<T>(IEnumerable<T> collection, IEnumerable<T> exception = null)
    {
        if (collection == null || collection.Count() == 0)
        {
            Debug.LogError("LIST IS EMPTY!!");
        }

        int max = collection.Count();
        T obj = collection.ElementAt(Random.Range(0, max));

        if(exception != null)
        {
            foreach (var item in exception)
            {
                if (item.Equals(obj))
                {
                    return GetRandom(collection, exception);
                }
                else
                {
                    continue;
                }
            }

            return obj;
        }
        else
        {
            return obj;
        }
    }
}

static class IEnumerableExtensions
{
    public static IEnumerable<T> Yield<T>(this T item)
    {
        yield return item;
    }
}
