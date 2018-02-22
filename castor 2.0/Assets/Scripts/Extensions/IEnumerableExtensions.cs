using System.Collections.Generic;
using System;
using System.Linq;

public static class IEnumerable
{
    public static bool UnorderedElementsAreEqual<T>(this IEnumerable<T> thisList, IEnumerable<T> otherList) where T : IEquatable<T>
    {
        IEnumerable<T> inThisButNotInOther = thisList.Except(otherList);
        IEnumerable<T> inOtherButNotInThis = otherList.Except(thisList);

        return (
            !inThisButNotInOther.Any() &&
            !inOtherButNotInThis.Any()
        );
    }

    public static bool UnorderedElementsAreEqual<T>(this IEnumerable<T> thisList, IEnumerable<T> otherList, IEqualityComparer<T> comparer)
    {
        IEnumerable<T> inThisButNotInOther = thisList.Except(otherList, comparer);
        IEnumerable<T> inOtherButNotInThis = otherList.Except(thisList, comparer);

        return (
            !inThisButNotInOther.Any() &&
            !inOtherButNotInThis.Any()
        );
    }

    public static int UnorderedElementsGetHashCode<T>(this IEnumerable<T> thisList) where T : IEquatable<T>
    {
        int hash = 17;
        foreach (T element in thisList)
        {
            hash *= (31 + element.GetHashCode());
        }
        return hash;
    }

    public static int UnorderedElementsGetHashCode<T>(this IEnumerable<T> thisList, IEqualityComparer<T> comparer)
    {
        int hash = 17;
        foreach (T element in thisList)
        {
            hash *= (31 + comparer.GetHashCode(element));
        }
        return hash;
    }
}
