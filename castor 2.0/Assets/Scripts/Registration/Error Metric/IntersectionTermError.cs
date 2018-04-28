using System.Collections.Generic;
using Utils;
using System;
using UnityEngine;

namespace Registration.Error
{
    public class IntersectionTermError : IIterativeErrorMetric
    {
        private readonly double distanceWeight;
        private readonly double intersectionWeight;

        private Transform referenceTransform;
        private ContainmentDetector staticModelContainmentDetector;

        public IntersectionTermError(double distanceWeight, double intersectionWeight)
        {
            this.distanceWeight = distanceWeight;
            this.intersectionWeight = intersectionWeight;
        }

        #region sharedParameters
        public object ComputeSharedParameters(List<Vector4D> rotatedModelPoints, List<Vector4D> staticPoints, Vector4D translation)
        {
            List<Vector4D> transformedPoints = new List<Vector4D>();
            int[] XIs = new int[rotatedModelPoints.Count];

            bool xi;
            Vector4D transformedModelPoint;
            int idx = 0;
            foreach (Vector4D rotatedModelPoint in rotatedModelPoints)
            {
                transformedModelPoint = rotatedModelPoint + translation;
                transformedPoints.Add(transformedModelPoint);

                xi = staticModelContainmentDetector.GameObjectContains(
                    point: referenceTransform.TransformPoint(transformedModelPoint)
                );
                XIs[idx++] = xi ? 1 : 0;
            }

            return new SharedParameters(XIs, transformedPoints);
        }

        private class SharedParameters
        {
            public readonly int[] XIs;
            public readonly List<Vector4D> TransformedPoints;

            public SharedParameters(int[] XIs, List<Vector4D> transformedPoints)
            {
                this.XIs = XIs;
                this.TransformedPoints = transformedPoints;
            }
        }
        #endregion

        #region error
        public double ComputeError(List<Vector4D> rotatedModelPoints, List<Vector4D> staticPoints, Vector4D translation, object sharedParametersObj)
        {
            SharedParameters sharedParameters = sharedParametersObj as SharedParameters;

            return ComputeError(sharedParameters.TransformedPoints, staticPoints, sharedParameters.XIs);
        }

        public double ComputeError(List<Vector4D> transformedModelPoints, List<Vector4D> staticPoints, int[] XIs)
        {
            int N = transformedModelPoints.Count;

            double error = 0;
            for (int i = 0; i < N; i++) error += ComputeError(transformedModelPoints[i], staticPoints[i], XIs[i]);
            error /= (4 * N);

            return error;
        }

        public double ComputeError(List<Vector4D> rotatedModelPoints, List<Vector4D> staticPoints, Vector4D translation, int[] XIs)
        {
            int N = rotatedModelPoints.Count;

            double error = 0;
            for (int i = 0; i < N; i++) error += ComputeError(rotatedModelPoints[i] + translation, staticPoints[i], XIs[i]);
            error /= (4 * N);

            return error;
        }

        private double ComputeError(Vector4D xc_translated, Vector4D p, int xi)
        {
            return (this.distanceWeight + this.intersectionWeight * xi) * (xc_translated - p).SqrMagnitude();
        }
        #endregion

        #region rotationalGradient
        public QuaternionD RotationalGradient(List<Vector4D> rotatedModelPoints, List<Vector4D> staticPoints, Vector4D translation, object sharedParametersObj)
        {
            SharedParameters sharedParameters = sharedParametersObj as SharedParameters;
            return RotationalGradient(rotatedModelPoints, staticPoints, translation, sharedParameters.XIs);
        }

        public QuaternionD RotationalGradient(List<Vector4D> rotatedModelPoints, List<Vector4D> staticPoints, Vector4D translation, int[] XIs)
        {
            int N = rotatedModelPoints.Count;
            Vector4D gradient = new Vector4D();

            for (int i = 0; i < N; i++) gradient += RotationalGradient(rotatedModelPoints[i], staticPoints[i], translation, XIs[i]);

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
        public Vector4D TranslationalGradient(List<Vector4D> rotatedModelPoints, List<Vector4D> staticPoints, Vector4D translation, object sharedParametersObj)
        {
            SharedParameters sharedParameters = sharedParametersObj as SharedParameters;

            int N = rotatedModelPoints.Count;
            Vector4D gradient = new Vector4D();

            for (int i = 0; i < N; i++) gradient += TranslationalGradient(sharedParameters.TransformedPoints[i], staticPoints[i], sharedParameters.XIs[i]);

            gradient /= (2 * N);
            return gradient;
        }

        public Vector4D TranslationalGradient(List<Vector4D> rotatedModelPoints, List<Vector4D> staticPoints, Vector4D translation, int[] XIs)
        {
            int N = rotatedModelPoints.Count;
            Vector4D gradient = new Vector4D();

            for (int i = 0; i < N; i++) gradient += TranslationalGradient(rotatedModelPoints[i] + translation, staticPoints[i], XIs[i]);

            gradient /= (2 * N);
            return gradient;
        }

        public Vector4D TranslationalGradient(Vector4D xc_translated, Vector4D p, int xi)
        {
            return (this.distanceWeight + xi * this.intersectionWeight) * (xc_translated - p);
        }
        #endregion

        #region IErrorMetric
        public float ComputeError(CorrespondenceCollection correspondences, Transform originalTransform, Transform newTransform)
        {
            //Apply the newTransform to the model points
            List<Point> modelPoints = TransformPoints(correspondences.ModelPoints, originalTransform, newTransform);

            bool xi;

            float error = 0;
            for (int i = 0; i < modelPoints.Count; i++)
            {
                xi = staticModelContainmentDetector.GameObjectContains(
                    newTransform.TransformPoint(modelPoints[i].Position)
                );
                error += ComputeError(
                    modelPoint: modelPoints[i].Position,
                    staticPoint: correspondences[i].StaticPoint.Position,
                    xi: xi ? 1 : 0
                );
            }
            return error / (4 * modelPoints.Count);
        }

        private float ComputeError(Vector3 modelPoint, Vector3 staticPoint, int xi)
        {
            float weight = (float)(this.distanceWeight + this.intersectionWeight * xi);
            return weight * (modelPoint - staticPoint).sqrMagnitude;
        }

        private List<Point> TransformPoints(List<Point> points, Transform orignalTransform, Transform newTransform)
        {
            List<Point> transformedPoints = new List<Point>(points.Count);
            Matrix4x4 transformation = orignalTransform.LocalToOther(newTransform);

            foreach (Point point in points)
            {
                transformedPoints.Add(point.ChangeTransform(transformation));
            }
            return transformedPoints;
        }

        public void Set(GameObject staticModel, Transform referenceTransform)
        {
            this.staticModelContainmentDetector = staticModel.GetComponent<ContainmentDetector>();
            this.referenceTransform = referenceTransform;
        }

        public SerializableErrorMetric Serialize()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}