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

        protected override Matrix4x4 FindTransformImplementation(CorrespondenceCollection correspondences)
        {
            List<Vector4D> modelCoordinates = new List<Vector4D>(correspondences.Count);
            List<Vector4D> staticCoordinates = new List<Vector4D>(correspondences.Count);

            foreach (Correspondence correspondence in correspondences)
            {
                modelCoordinates.Add(Vector4D.HomogeneousCoordinate(correspondence.ModelPoint.Position));
                staticCoordinates.Add(Vector4D.HomogeneousCoordinate(correspondence.StaticPoint.Position));
            }

            return new _IGDTransformFinder(
                modelPoints: modelCoordinates, staticPoints: staticCoordinates,
                configuration: configuration
            ).FindTransform();
        }

        public class Configuration
        {
            public readonly double learningRate;

            /// <summary>
            /// After this many iterations the algorithm terminates.
            /// </summary>
            public readonly int maxNumIterations;

            /// <summary>
            /// If the current error is smaller than or equal to this error the algorithm terminates.
            /// </summary>
            public readonly double convergenceError;

            public readonly IIterativeErrorMetric errorMetric;

            public Configuration(double learningRate, double convergenceError, int maxNumIterations, IIterativeErrorMetric errorMetric)
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

        private readonly List<Vector4D> modelPoints;
        private List<Vector4D> preMultipliedModelPoints;
        private readonly List<Vector4D> staticPoints;

        private Vector4D translation;
        private QuaternionD rotation;

        private double error;
        private int iteration;
        private double scale;

        internal _IGDTransformFinder(
            List<Vector4D> modelPoints, List<Vector4D> staticPoints,
            IGDTransformFinder.Configuration configuration)
        {
            this.modelPoints = modelPoints;
            this.staticPoints = staticPoints;

            this.configuration = configuration;

            initIGD();
        }

        private double determineScale()
        {
            double[] ranges = computeXYZRange(this.modelPoints);

            double range = Double.NegativeInfinity;
            for (int i = 0; i < ranges.Length; i++) range = Math.Max(range, ranges[i]);

            return range;
        }

        private double[] computeXYZRange(List<Vector4D> points)
        {
            double[] minima = { Double.PositiveInfinity, Double.PositiveInfinity, Double.PositiveInfinity };
            double[] maxima = { Double.NegativeInfinity, Double.NegativeInfinity, Double.NegativeInfinity };

            foreach (Vector4D modelPoint in points)
            {
                for (int i = 0; i < 3; i++)
                {
                    minima[i] = Math.Min(minima[i], modelPoint[i]);
                    maxima[i] = Math.Max(maxima[i], modelPoint[i]);
                }
            }

            double[] ranges = new double[3];
            for (int i = 0; i < 3; i++) ranges[i] = maxima[i] - minima[i];

            return ranges;
        }

        private void initIGD()
        {
            this.scale = determineScale();

            iteration = 0;

            translation = new Vector4D(0, 0, 0, 0);
            rotation = QuaternionD.identity;

            preMultipliedModelPoints = new List<Vector4D>(this.modelPoints.Count);

            error = 0;
        }

        internal Matrix4x4 FindTransform()
        {
            while (true)
            {
                preMultiplyModelPoints();

                error = configuration.errorMetric.ComputeError(preMultipliedModelPoints, staticPoints, translation);

                if (convergence()) return buildTransformationMatrix().ToUnityMatrix4x4();

                step();

                iteration++;
            }
        }

        private void preMultiplyModelPoints()
        {
            preMultipliedModelPoints.Clear();

            Matrix4x4D rotationMatrix = Matrix4x4D.TransformationMatrixFromQuaternion(rotation);

            Vector4D xc;
            foreach (Vector4D x in modelPoints)
            {
                xc = rotationMatrix * x;
                preMultipliedModelPoints.Add(xc);
            }
        }

        private Matrix4x4D buildTransformationMatrix()
        {
            Matrix4x4D translationMatrix = Matrix4x4D.TransformationMatrixFromTranslation(translation);
            Matrix4x4D rotationMatrix = Matrix4x4D.TransformationMatrixFromQuaternion(rotation);

            return translationMatrix * rotationMatrix;
        }

        private void step()
        {
            Vector4D translationalGradient = configuration.errorMetric.TranslationalGradient(modelPoints, staticPoints, translation);
            QuaternionD rotationalGradient = configuration.errorMetric.RotationalGradient(modelPoints, staticPoints, translation);

            updateTranslation(translationalGradient);
            updateRotation(rotationalGradient);
        }

        private void updateTranslation(Vector4D gradient)
        {
            //Normalize gradient
            gradient /= this.scale;

            translation -= configuration.learningRate * gradient;
        }

        private void updateRotation(QuaternionD gradient)
        {
            gradient /= (this.scale * this.scale);

            rotation = new QuaternionD(-1 * configuration.learningRate * gradient.xyz, 1);
        }

        private bool convergence()
        {
            return
                (this.error < configuration.convergenceError) ||
                (this.iteration > configuration.maxNumIterations);
        }
    }
}