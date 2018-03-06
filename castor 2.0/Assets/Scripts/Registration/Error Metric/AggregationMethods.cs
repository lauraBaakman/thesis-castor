using System.Collections.Generic;
using System.Linq;

namespace Registration
{
    namespace Error
    {
        public static class AggregationMethods
        {
            public delegate float AggregationMethod(IEnumerable<float> errors);

            public static float Sum(IEnumerable<float> errors)
            {
                return errors.Sum();
            }

            public static float Mean(IEnumerable<float> errors)
            {
                return errors.Average();
            }
        }
    }

}
