using UnityEngine;
using Registration.Error;
using Registration;
using System.Collections.Generic;
using Utils;

namespace Registration.Error
{
    public class WheelerIterativeError : IIterativeErrorMetric
    {
        public float ComputeError(CorrespondenceCollection correspondences, Transform originalTransform, Transform newTransform)
        {
            throw new System.NotImplementedException();
        }

        public Vector4D RotationalGradient(List<Vector4D> XCs, List<Vector4D> Ps, Vector4D translation)
        {
            throw new System.NotImplementedException();
        }

        public Vector4D TranslationalGradient(List<Vector4D> XCs, List<Vector4D> Ps, Vector4D translation)
        {
            int N = XCs.Count;
            Vector4D gradient = new Vector4D();
            Vector4D localGradient;
            for (int i = 0; i < N; i++)
            {
                localGradient = TranslationalGradient(XCs[i], Ps[i], translation);
                gradient += localGradient;
            }
            gradient *= (1.0 / (2 * N));
            return gradient;
        }

        private Vector4D TranslationalGradient(Vector4D Xc, Vector4D p, Vector4D translation)
        {
            Vector4D gradient = 2 * (Xc + translation - p);
            return gradient;
        }
    }
}