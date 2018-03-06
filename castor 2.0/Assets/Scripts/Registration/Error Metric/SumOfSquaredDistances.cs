using UnityEngine;

namespace Registration
{
    namespace Error
    {
        public class SumOfSquaredDistances : AbstractErrorMetric
        {
            public SumOfSquaredDistances(Configuration configuration)
                : base(configuration) { }

            public override float ComputeError(CorrespondenceCollection correspondences, Transform orignalTransform, Transform newTransform)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}

