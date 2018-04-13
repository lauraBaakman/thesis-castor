using UnityEngine;
using Registration.Error;

namespace Registration
{
    public interface ITransformFinder
    {
        Matrix4x4 FindTransform(CorrespondenceCollection correspondences);

        IErrorMetric GetErrorMetric();
    }
}
