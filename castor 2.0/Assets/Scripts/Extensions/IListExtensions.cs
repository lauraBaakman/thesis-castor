using System.Collections.Generic;
using System;
using UnityEngine;

public static class IListExtension
{

    public static bool OrderedElementsAreEqual<T>(this IList<T> thisList, IList<T> otherList) where T : IEquatable<T>
    {
        Debug.Log("ILISTExtension OrderedElementsAreEqual");
        if (thisList.Count != otherList.Count) return false;

        for (int i = 0; i < thisList.Count; i++)
        {
            if (!thisList[i].Equals(otherList[i])) return false;
        }

        return true;
    }

    public static int OrderedElementsGetHashCode<T>(this IList<T> list) where T : IEquatable<T>
    {
        int hash = 17;
        for (int i = 0; i < list.Count; i++)
        {
            hash *= (31 + list[i].GetHashCode() + i);
        }
        return hash;
    }
}