using System.Collections.Generic;
using System.Linq;

namespace Registration
{
    namespace Error
    {
        public static class AggregationMethods
        {
            public delegate float AggregationMethod(IEnumerable<float> errors);

            /// <summary>
            /// Computes the sum of the specified errors.
            /// 
            /// If an empty list is input the resulting error is 0.0.
            /// </summary>
            /// <returns>The sum of the errors</returns>
            /// <param name="errors">The errors.</param>
            public static float Sum(IEnumerable<float> errors)
            {
                return errors.Sum();
            }

            /// <summary>
            /// Computes the mean the specified errors.
            /// 
            /// If an empty list is input the resulting error is 0.0.
            /// </summary>
            /// <returns>The mean of the erros.</returns>
            /// <param name="errors">The errors</param>
            public static float Mean(IEnumerable<float> errors)
            {
                return errors.Average();
            }
        }
    }

}
