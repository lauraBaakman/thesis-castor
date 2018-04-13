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

        #region error
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
            return (this.distanceWeight + this.intersectionWeight * xi) * (xc + translation - p).SqrMagnitude();
        }

        public QuaternionD RotationalGradient(List<Vector4D> XCs, List<Vector4D> Ps, Vector4D translation)
        {
            throw new System.NotImplementedException();
        }
        #endregion

        #region rotationalGradient
        public QuaternionD RotationalGradient(List<Vector4D> XCs, List<Vector4D> Ps, Vector4D translation, int[] XIs)
        {
            int N = XCs.Count;
            Vector4D gradient = new Vector4D();

            for (int i = 0; i < N; i++) gradient += RotationalGradient(XCs[i], Ps[i], translation, XIs[i]);

            gradient /= N;

            //Wheeler: The gradient w.r.t. to q will have no w component
            return new QuaternionD(x: gradient.x, y: gradient.y, z: gradient.z, w: 0);
        }

        public Vector4D RotationalGradient(Vector4D xc, Vector4D p, Vector4D translation, int xi)
        {
            return (this.distanceWeight + xi * this.intersectionWeight) * Vector4D.Cross(xc, translation - p);
        }
        #endregion

        #region translationalGradient
        public Vector4D TranslationalGradient(List<Vector4D> XCs, List<Vector4D> Ps, Vector4D translation)
        {
            throw new System.NotImplementedException();
        }

        public Vector4D TranslationalGradient(List<Vector4D> XCs, List<Vector4D> Ps, Vector4D translation, int[] XIs)
        {
            int N = XCs.Count;
            Vector4D gradient = new Vector4D();

            for (int i = 0; i < N; i++) gradient += TranslationalGradient(XCs[i], Ps[i], translation, XIs[i]);

            gradient /= (2 * N);
            return gradient;
        }

        public Vector4D TranslationalGradient(Vector4D xc, Vector4D p, Vector4D translation, int xi)
        {
            return (this.distanceWeight + xi * this.intersectionWeight) * (xc + translation - p);
        }
        #endregion
    }
}