using UnityEngine;
using Registration.Error;
using Registration;
using System.Collections.Generic;

public class WheelerIterativeError : IIterativeErrorMetric
{
    public WheelerIterativeError()
    {

    }

    public float ComputeError(CorrespondenceCollection correspondences, Transform originalTransform, Transform newTransform)
    {
        throw new System.NotImplementedException();
    }

    public Vector4 RotationalGradient(List<Vector4> XCs, List<Vector4> Ps, Vector4 translation)
    {
        throw new System.NotImplementedException();
    }

    public Vector4 TranslationalGradient(List<Vector4> XCs, List<Vector4> Ps, Vector4 translation)
    {
        return new Vector4();
    }
}
