using UnityEngine;
using Registration.Error;
using System.Collections.Generic;
using Utils;
using System;

namespace Registration
{
    public class IGDTransformFinder : AbstractTransformFinder
    {
        private readonly Configuration configuration;

        public IGDTransformFinder(Configuration configuration)
        {
            this.configuration = configuration;
        }

        public override IErrorMetric GetErrorMetric()
        {
            return configuration.errorMetric;
        }

        public override SerializableTransformFinder Serialize()
        {
            return new SerializableTransformFinder(this.configuration);
        }

        protected override Matrix4x4 FindTransformImplementation(CorrespondenceCollection correspondences)
        {
            List<Vector4> modelCoordinates = new List<Vector4>(correspondences.Count);
            List<Vector4> staticCoordinates = new List<Vector4>(correspondences.Count);

            foreach (Correspondence correspondence in correspondences)
            {
                modelCoordinates.Add(VectorUtils.HomogeneousCoordinate(correspondence.ModelPoint.Position));
                staticCoordinates.Add(VectorUtils.HomogeneousCoordinate(correspondence.StaticPoint.Position));
            }

            return new _IGDTransformFinder(
                modelPoints: modelCoordinates, staticPoints: staticCoordinates,
                configuration: configuration
            ).FindTransform();
        }

        public class Configuration
        {
            public readonly float learningRate;

            /// <summary>
            /// After this many iterations the algorithm terminates.
            /// </summary>
            public readonly int maxNumIterations;

            /// <summary>
            /// If the current error is smaller than or equal to this error the algorithm terminates.
            /// </summary>
            public readonly double convergenceError;

            public readonly IIterativeErrorMetric errorMetric;

            public Configuration(float learningRate, float convergenceError, int maxNumIterations, IIterativeErrorMetric errorMetric)
            {
                this.learningRate = learningRate;
                this.convergenceError = convergenceError;
                this.maxNumIterations = maxNumIterations;

                this.errorMetric = errorMetric;
            }
        }
    }

    internal class _IGDTransformFinder
    {
        private readonly IGDTransformFinder.Configuration configuration;

        private readonly List<Vector4> modelPoints;
        private List<Vector4> preRotatedModelPoints;
        private readonly List<Vector4> staticPoints;

        private Vector4 translation;
        private Quaternion rotation;

        private object sharedParameters;

        private double error;
        private Counter iterationCounter;

        private float squaredScale;

        private static float minimumScale = Mathf.Sqrt(float.Epsilon);

        private float scale;
        public float Scale
        {
            get { return scale; }
            set
            {
                scale = value;
                squaredScale = scale * scale;
            }
        }

        internal _IGDTransformFinder(
            List<Vector4> modelPoints, List<Vector4> staticPoints,
            IGDTransformFinder.Configuration configuration)
        {
            this.modelPoints = modelPoints;
            this.staticPoints = staticPoints;

            this.configuration = configuration;

            this.iterationCounter = new Counter(configuration.maxNumIterations);

            initIGD();
        }

        private float determineScale()
        {
            float[] ranges = computeXYZRange(this.modelPoints);

            float range = float.NegativeInfinity;
            for (int i = 0; i < ranges.Length; i++) range = Mathf.Max(range, ranges[i]);

            return range;
        }

        private float[] computeXYZRange(List<Vector4> points)
        {
            float[] minima = { float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity };
            float[] maxima = { float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity };

            foreach (Vector4 modelPoint in points)
            {
                for (int i = 0; i < 3; i++)
                {
                    minima[i] = Mathf.Min(minima[i], modelPoint[i]);
                    maxima[i] = Mathf.Max(maxima[i], modelPoint[i]);
                }
            }

            float[] ranges = new float[3];
            for (int i = 0; i < 3; i++)
            {
                //Avoid that range becomes 0
                ranges[i] = Mathf.Max(maxima[i] - minima[i], minimumScale);
            }

            return ranges;
        }

        private void initIGD()
        {
            this.Scale = determineScale();

            translation = new Vector4(0, 0, 0, 0);
            rotation = Quaternion.identity;

            preRotatedModelPoints = new List<Vector4>(this.modelPoints.Count);

            error = 0;
        }

        internal Matrix4x4 FindTransform()
        {
            while (!iterationCounter.IsCompleted())
            {
                iterationCounter.Increase();

                preRotateModelPoints();

                sharedParameters = configuration.errorMetric.ComputeSharedParameters(preRotatedModelPoints, staticPoints, translation);

                error = configuration.errorMetric.ComputeError(preRotatedModelPoints, staticPoints, translation, sharedParameters);

                if (convergence()) break;

                step();
            }
            return buildTransformationMatrix();
        }

        /// <summary>
        /// Apply the current rotation to the model points.
        /// </summary>
        private void preRotateModelPoints()
        {
            preRotatedModelPoints.Clear();

            Matrix4x4 rotationMatrix = MatrixUtils.TransformationMatrixFromQuaternion(rotation);

            Vector4 xc;
            foreach (Vector4 x in modelPoints)
            {
                xc = rotationMatrix * x;
                preRotatedModelPoints.Add(xc);
            }
        }

        private Matrix4x4 buildTransformationMatrix()
        {
            Matrix4x4 translationMatrix = MatrixUtils.TransformationMatrixFromTranslation(translation);
            Matrix4x4 rotationMatrix = MatrixUtils.TransformationMatrixFromQuaternion(rotation);

            return translationMatrix * rotationMatrix;
        }

        private void step()
        {
            Vector4 translationalGradient = configuration.errorMetric.TranslationalGradient(preRotatedModelPoints, staticPoints, translation, sharedParameters);
            Quaternion rotationalGradient = configuration.errorMetric.RotationalGradient(preRotatedModelPoints, staticPoints, translation, sharedParameters);

            updateTranslation(translationalGradient);
            updateRotation(rotationalGradient);
        }

        private void updateTranslation(Vector4 gradient)
        {
            //Normalize gradient
            gradient = new Vector4(
                x: (gradient.x / Scale) * configuration.learningRate,
                y: (gradient.y / Scale) * configuration.learningRate,
                z: (gradient.z / Scale) * configuration.learningRate,
                w: (gradient.w / Scale) * configuration.learningRate);

            translation -= gradient;
        }

        private void updateRotation(Quaternion gradient)
        {
            Quaternion scaledGradient = new Quaternion(
                x: (gradient.x / squaredScale) * configuration.learningRate * -1,
                y: (gradient.y / squaredScale) * configuration.learningRate * -1,
                z: (gradient.z / squaredScale) * configuration.learningRate * -1,
                w: (gradient.w / squaredScale) * configuration.learningRate * -1
            );
            rotation = new Quaternion(x: scaledGradient.x, y: scaledGradient.y, z: scaledGradient.z, w: 1);
        }

        private bool convergence()
        {
            return this.error < configuration.convergenceError;
        }
    }
}