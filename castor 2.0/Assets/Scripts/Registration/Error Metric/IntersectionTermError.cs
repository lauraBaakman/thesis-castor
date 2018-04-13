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

        public double ComputeError(List<Vector4D> XCs, List<Vector4D> Ps, Vector4D translation, int xi)
        {
            int N = XCs.Count;

            double error = 0;
            for (int i = 0; i < N; i++)
            {
                error += ComputeError(XCs[i], Ps[i], translation, xi);
            }
            error /= (4 * N);

            return error;
        }

        private double ComputeError(Vector4D xc, Vector4D p, Vector4D translation, int xi)
        {
            throw new NotImplementedException();
        }

        private int Xi(Vector4D modelPoint)
        {
            throw new NotImplementedException();
        }

        public QuaternionD RotationalGradient(List<Vector4D> XCs, List<Vector4D> Ps, Vector4D translation)
        {
            throw new System.NotImplementedException();
        }

        public Vector4D TranslationalGradient(List<Vector4D> XCs, List<Vector4D> Ps, Vector4D translation)
        {
            throw new System.NotImplementedException();
        }
    }
}