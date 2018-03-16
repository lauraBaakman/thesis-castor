using System.Collections.Generic;

public static class DictionaryExtensions
{
    public static int UnorderedElementsGetHashCode<K, V>(this Dictionary<K, V> dictionary)
    {
        int hash = 17;
        foreach (KeyValuePair<K, V> element in dictionary)
        {
            hash *= (31 + element.GetHashCode());
        }
        return hash;
    }
}