using System.Collections.Generic;
using UnityEngine;
using Utils;
using System;

namespace Registration.Error
{
    public interface IErrorMetric
    {
        float ComputeError(CorrespondenceCollection correspondences, Transform originalTransform, Transform newTransform);

        void Set(GameObject staticModel, Transform referenceTransform);

        SerializableErrorMetric Serialize();
    }

    public interface IIterativeErrorMetric : IErrorMetric
    {
        double ComputeError(List<Vector4D> XCs, List<Vector4D> Ps, Vector4D translation, object sharedParameters);

        /// <summary>
        /// Computes the rotational gradient of the error function.
        /// </summary>
        /// <returns>The rotational gradient.</returns>
        /// <param name="XCs">The model points premultiplied with the rotation matrix.</param>
        /// <param name="Ps">The static points in the same order as the model points.</param>
        /// <param name="translation">The translation vector</param>/// 
        QuaternionD RotationalGradient(List<Vector4D> XCs, List<Vector4D> Ps, Vector4D translation, object sharedParameters);

        /// <summary>
        /// Computes the rotation gradient of the error function.
        /// </summary>
        /// <returns>The translational gradient.</returns>
        /// <param name="XCs">The model points premultiplied with the rotation matrix.</param>
        /// <param name="Ps">The static points in the same order as the model points.</param>
        /// <param name="translation">The translation vector</param>/// 
        Vector4D TranslationalGradient(List<Vector4D> XCs, List<Vector4D> Ps, Vector4D translation, object sharedParameters);

        /// <summary>
        /// Computes the paramters that are shared between the error function and the gradients.
        /// </summary>
        /// <returns>The shared paramters.</returns>
        /// <param name="modelPoints">Model points.</param>
        /// <param name="staticPOints">Static Points.</param>
        /// <param name="translation">Translation.</param>
        object ComputeSharedParameters(List<Vector4D> rotatedModelPoints, List<Vector4D> staticPoints, Vector4D translation);
    }

    [System.Serializable]
    public class SerializableErrorMetric
    {
        public float distanceWeight;
        public float intersectionWeight;

        public string aggregationMethod;
        public string distanceMethod;
        public int normalizePoints;

        public SerializableErrorMetric(ErrorMetric.Configuration configuration)
            : this(
                AggregationMethodToString(configuration.AggregationMethod),
                DistanceMethodToString(configuration.DistanceMetric),
                NormalizePointsToInt(configuration.NormalizePoints))
        { }

        private static int NormalizePointsToInt(bool normalizePoints)
        {
            return normalizePoints ? 1 : 0;
        }

        private static string DistanceMethodToString(DistanceMetrics.Metric distanceMetric)
        {
            DistanceMetrics.Metric squaredEuclidean = DistanceMetrics.SquaredEuclidean;
            if (distanceMetric.Equals(squaredEuclidean)) return "squared Euclidean";

            DistanceMetrics.Metric PointToPlane = DistanceMetrics.PointToPlane;
            if (distanceMetric.Equals(PointToPlane)) return "point to plane";

            DistanceMetrics.Metric SquaredPointToPlane = DistanceMetrics.SquaredPointToPlane;
            if (distanceMetric.Equals(SquaredPointToPlane)) return "squared point to plane";

            return "unkown distance metric";
        }

        private static string AggregationMethodToString(AggregationMethods.AggregationMethod aggregationMethod)
        {
            AggregationMethods.AggregationMethod mean = AggregationMethods.Mean;
            if (aggregationMethod.Equals(mean)) return "mean";

            AggregationMethods.AggregationMethod sum = AggregationMethods.Sum;
            if (aggregationMethod.Equals(sum)) return "sum";

            return "unkown aggregatin method";
        }

        private SerializableErrorMetric(
            string aggregationMethod = "", string distanceMethod = "",
            int normalizePoints = -1
        )
            : this(1, 0, aggregationMethod, distanceMethod, normalizePoints)
        { }

        public SerializableErrorMetric(
            float distanceWeight = -1, float intersectionWeight = -1,
            string aggregationMethod = "", string distanceMethod = "",
            int normalizePoints = -1)
        {
            this.distanceWeight = distanceWeight;
            this.intersectionWeight = intersectionWeight;

            this.aggregationMethod = aggregationMethod;
            this.distanceMethod = distanceMethod;
            this.normalizePoints = normalizePoints;
        }
    }
}