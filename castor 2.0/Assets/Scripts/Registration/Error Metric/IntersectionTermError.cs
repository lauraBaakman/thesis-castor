using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Utils;
using System;

namespace Registration.Error
{
    public class IntersectionTermError : IIterativeErrorMetric
    {
        double distanceWeight;
        double intersectionWeight;

        public IntersectionTermError(double distanceWeight, double intersectionWeight)
        {
            this.distanceWeight = distanceWeight;
            this.intersectionWeight = intersectionWeight;
        }

        public double ComputeError(List<Vector4D> XCs, List<Vector4D> Ps, Vector4D translation)
        {
            throw new NotImplementedException();
        }

        public double ComputeError(List<Vector4D> XCs, List<Vector4D> Ps, Vector4D translation, int[] XIs)
        {
            int N = XCs.Count;

            double error = 0;
            for (int i = 0; i < N; i++) error += ComputeError(XCs[i], Ps[i], translation, XIs[i]);
            error /= (4 * N);

            return error;
        }

        private double ComputeError(Vector4D xc, Vector4D p, Vector4D translation, int xi)
        {
            return 0;
        }

        public QuaternionD RotationalGradient(List<Vector4D> XCs, List<Vector4D> Ps, Vector4D translation)
        {
            throw new System.NotImplementedException();
        }

        public QuaternionD RotationalGradient(List<Vector4D> XCs, List<Vector4D> Ps, Vector4D translation, int[] XIs)
        {
            return QuaternionD.identity;
        }

        public Vector4D TranslationalGradient(List<Vector4D> XCs, List<Vector4D> Ps, Vector4D translation)
        {
            throw new System.NotImplementedException();
        }

        public Vector4D TranslationalGradient(List<Vector4D> XCs, List<Vector4D> Ps, Vector4D translation, int[] XIs)
        {
            return new Vector4D();
        }
    }
}