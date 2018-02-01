using System.Collections.Generic;

namespace OpenTKLib
{
    public class KeyValueComparer : IComparer<KeyValuePair<int, double>>
    {

        public int Compare(KeyValuePair<int, double> a, KeyValuePair<int, double> b)
        {

            if (a.Value > b.Value)
                return 1;
            else
                return -1;


        }
    }
}
