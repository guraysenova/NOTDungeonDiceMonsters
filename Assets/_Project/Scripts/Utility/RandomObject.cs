using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class RandomObject
{
    public static T GetRandomObject<T>(List<T> list)
    {
        if (list == null || list.Count == 0)
        {
            Debug.LogError("LIST IS EMPTY!!");
        }
        return list[Random.Range(0, list.Count)];
    }

    public static T GetRandomObject<T>(T[] array)
    {
        if (array == null || array.Length == 0)
        {
            Debug.LogError("ARRAY IS EMPTY!!");
        }
        return GetRandomObject(array.ToList());
    }

    public static T GetRandomExcept<T>(T[] array, T exception)
    {
        T obj = GetRandomObject(array);
        if (obj.Equals(exception))
        {
            return GetRandomExcept(array, exception);
        }
        else
        {
            return obj;
        }
    }

    public static T GetRandomExcept<T>(T[] array, T[] exceptions)
    {
        T obj = GetRandomObject(array);
        for (int i = 0; i < exceptions.Length; i++)
        {
            if (obj.Equals(exceptions[i]))
            {
                return GetRandomExcept(array, exceptions);
            }
        }
        return obj;
    }

    public static T GetRandomExcept<T>(T[] array, List<T> exceptions)
    {
        T obj = GetRandomObject(array);
        for (int i = 0; i < exceptions.Count; i++)
        {
            if (obj.Equals(exceptions[i]))
            {
                return GetRandomExcept(array, exceptions);
            }
        }
        return obj;
    }

    public static T GetRandomExcept<T>(List<T> list, List<T> exceptions)
    {
        T obj = GetRandomObject(list);
        for (int i = 0; i < exceptions.Count; i++)
        {
            if (obj.Equals(exceptions[i]))
            {
                return GetRandomExcept(list, exceptions);
            }
        }
        return obj;
    }

    public static T GetRandomExcept<T>(List<T> list, T[] exceptions)
    {
        T obj = GetRandomObject(list);
        for (int i = 0; i < exceptions.Length; i++)
        {
            if (obj.Equals(exceptions[i]))
            {
                return GetRandomExcept(list, exceptions);
            }
        }
        return obj;
    }

    public static T GetRandomExcept<T>(List<T> list, T exception)
    {
        T obj = GetRandomObject(list);
        if (obj.Equals(exception))
        {
            return GetRandomExcept(list, exception);
        }
        else
        {
            return obj;
        }
    }
}
