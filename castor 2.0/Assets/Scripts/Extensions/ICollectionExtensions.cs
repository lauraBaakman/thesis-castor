using System.Collections.Generic;

public static class ICollectionExtension
{
	public static bool IsEmpty<T>(this ICollection<T> collection)
	{
		return collection.Count == 0;
	}
}