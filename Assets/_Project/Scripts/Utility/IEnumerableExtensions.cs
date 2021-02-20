using System.Collections.Generic;
using System.Linq;
using UnityEngine;

static class IEnumerableExtensions
{
    public static T GetRandom<T>(this IEnumerable<T> collection, IEnumerable<T> exception = null)
    {
        if (collection == null || collection.Count() == 0)
        {
            Debug.LogError("THE GIVEN LIST IS EMPTY!!");
        }

        if (collection.Count() == 1)
        {
            Debug.LogWarning("YOUR COLLECTION ONLY HAS ONE ITEM , RETURNING THE FIRST ITEM IN THE COLLECTION");
            return collection.ElementAt(0);
        }

        T obj = collection.ElementAt(Random.Range(0, collection.Count()));

        if (exception != null)
        {
            int exceptionMatchCount = 0;
            foreach (var item in collection)
            {
                if (exception.Contains(item))
                {
                    exceptionMatchCount++;
                }
                else
                {
                    break;
                }
            }
            if (exceptionMatchCount == collection.Count())
            {
                Debug.LogWarning("YOUR EXCEPTION INCLUDES ALL THE OBJECTS IN THE COLLECTION! , RETURNING THE FIRST ITEM IN THE COLLECTION");
                return collection.ElementAt(0);
            }

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

    public static IEnumerable<T> Yield<T>(this T item)
    {
        yield return item;
    }
}
