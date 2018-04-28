using UnityEngine;
using Registration.Error;
using System;

namespace Registration
{
    public interface ITransformFinder
    {
        Matrix4x4 FindTransform(CorrespondenceCollection correspondences);

        IErrorMetric GetErrorMetric();

        SerializableTransformFinder Serialize();
    }
}