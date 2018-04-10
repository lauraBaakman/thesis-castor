using UnityEngine;
using Registration.Error;
using Registration;
using System.Collections.Generic;

public class WheelerIterativeError : AbstractIterativeErrorMetric
{
    protected WheelerIterativeError(Configuration configuration)
        : base(configuration)
    { }

    public override float ComputeError(CorrespondenceCollection correspondences, Transform originalTransform, Transform newTransform)
    {
        throw new System.NotImplementedException();
    }

    public override Vector4 RotationalGradient(List<Vector4> XCs, List<Vector4> Ps, Vector4 translation)
    {
        throw new System.NotImplementedException();
    }

    public override Vector4 TranslationalGradient(List<Vector4> XCs, List<Vector4> Ps, Vector4 translation)
    {
        throw new System.NotImplementedException();
    }
}
