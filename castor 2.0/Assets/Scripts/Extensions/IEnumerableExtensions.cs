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

	public static bool OrderedElementsAreEqual<T>(this IEnumerable<T> thisList, IEnumerable<T> otherList) where T : IEquatable<T>
	{
		return thisList.SequenceEqual(otherList);
	}

	public static int OrderedElementsGetHashCode<T>(this IEnumerable<T> list) where T : IEquatable<T>
	{
		int hash = 17, extra = 0;
		foreach (T element in list)
		{
			hash *= (31 + element.GetHashCode() + (extra++));
		}
		return hash;
	}

	public static string ElementsToString<T>(this IEnumerable<T> thisList, string prefix = "", string suffix = ", ")
	{
		string returnValue = "";
		int listLength = thisList.Count<T>();

		for (int i = 0; i < listLength - 1; i++)
		{
			returnValue += (prefix + thisList.ElementAt<T>(i) + suffix);
		}
		returnValue += (prefix + thisList.ElementAt<T>(listLength - 1));

		return returnValue;
	}
}
