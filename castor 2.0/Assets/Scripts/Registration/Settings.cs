using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Registration
{
    public class Settings
    {
        /// <summary>
        /// The transform in which the registration is performed. 
        /// </summary>
        /// <value>The reference transform.</value>
        public Transform ReferenceTransform { get; set; }

        /// <summary>
        /// If the error of the registration is smaller than this threshold the
        /// registration process terminates.
        /// </summary>
        /// <value>The error threshold.</value>
        public float ErrorThreshold { get; set; }

        /// <summary>
        /// The maximum number of iterations to execute.
        /// </summary>
        /// <value>The max number iterations.</value>
        public int MaxNumIterations { get; set; }

        /// <summary>
        /// The method used to select points from a mesh, that can be used in a 
        /// correspondence.
        /// </summary>
        /// <value>The selector.</value>
        public IPointSelector PointSelector { get; set; }

        /// <summary>
        /// The method used to find correspondecs within the points selected by
        /// the Selector.
        /// </summary>
        /// <value>The correspondence finder.</value>
        public ICorrespondenceFinder CorrespondenceFinder { get; set; }

        /// <summary>
        /// The filters used to filter the correspondences.
        /// </summary>
        private List<ICorrespondenceFilter> correspondenceFilters;
        public ReadOnlyCollection<ICorrespondenceFilter> CorrespondenceFilters
        {
            get { return correspondenceFilters.AsReadOnly(); }
        }

        /// <summary>
        /// The method used to compute the distances between points.
        /// </summary>
        /// <value>The point to point distance metric.</value>
        public PointToPointDistanceMetrics.DistanceMetric DistanceMetric { get; set; }

        /// <summary>
        /// The error metric used to compute the error of a registration.
        /// </summary>
        /// <value>The error metric.</value>
        public PointToPointErrorMetric ErrorMetric { get; set; }

        /// <summary>
        /// The method used to find the transform between the static points and the model points.
        /// </summary>
        /// <value>The trans form finder.</value>
        public ITransformFinder TransFormFinder { get; set; }

        public Settings(
            Transform referenceTransform,
            float errorThreshold = 0.001f, int maxNumIterations = 50
        )
        {
            ReferenceTransform = referenceTransform;

            ErrorThreshold = errorThreshold;

            MaxNumIterations = maxNumIterations;

            PointSelector = new SelectAllPointsSelector(ReferenceTransform, includeNormals: true);

            correspondenceFilters = new List<ICorrespondenceFilter>();

            CorrespondenceFinder = new NormalShootingCorrespondenceFinder();

            DistanceMetric = PointToPointDistanceMetrics.SquaredEuclidean;

            ErrorMetric = new PointToPointSumOfDistances(DistanceMetric);

            TransFormFinder = new HornTransformFinder();
        }
    }
}

