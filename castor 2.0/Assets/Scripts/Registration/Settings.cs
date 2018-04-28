using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using Registration.Error;
using System.IO;
using Utils;

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

        public float MaxWithinCorrespondenceDistance { get; set; }

        /// <summary>
        /// The method used to select points from a mesh, that can be used in a 
        /// correspondence.
        /// </summary>
        /// <value>The selector.</value>
        public IPointSampler PointSampler { get; set; }

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
        public DistanceMetrics.Metric DistanceMetric { get; set; }

        /// <summary>
        /// The error metric used to compute the error of a registration.
        /// </summary>
        /// <value>The error metric.</value>
        public IErrorMetric ErrorMetric
        {
            get { return TransFormFinder.GetErrorMetric(); }
        }

        /// <summary>
        /// The method used to find the transform between the static points and the model points.
        /// </summary>
        /// <value>The trans form finder.</value>
        public ITransformFinder TransFormFinder { get; set; }

        public Settings(
            Transform referenceTransform,
            float errorThreshold = 0.001f, int maxNumIterations = 100,
            float maxWithinCorrespondenceDistance = 1.0f
        )
        {
            ReferenceTransform = referenceTransform;

            ErrorThreshold = errorThreshold;

            MaxWithinCorrespondenceDistance = maxWithinCorrespondenceDistance;

            MaxNumIterations = maxNumIterations;

            PointSampler = new AllPointsSampler(
                new AllPointsSampler.Configuration(
                    referenceTransform,
                    AllPointsSampler.Configuration.NormalProcessing.AreaWeightedSmoothNormals
                )
            );

            correspondenceFilters = new List<ICorrespondenceFilter>();

            TransFormFinder = new IGDTransformFinder(
                new IGDTransformFinder.Configuration(
                    convergenceError: 0.001,
                    learningRate: 0.001,
                    maxNumIterations: 50,
                    errorMetric: new IntersectionTermError(0.5, 0.5)
                )
            );

            CorrespondenceFinder = new NearstPointCorrespondenceFinder(this);
        }

        public void ToJson(string outputPath)
        {
            new SerializableSettings(this).ToJson(outputPath);
        }

        [System.Serializable]
        public class SerializableSettings
        {
            [System.Serializable]
            public class SerializableCorrespondences
            {
                public List<string> correspondenceFilters;
                public float maxWithinCorrespondenceDistance;
                public SerializablePointSampler pointSampler;
                public SerializebleCorrespondenceFinder correspondenceFinder;

                private SerializableCorrespondences(
                    ReadOnlyCollection<ICorrespondenceFilter> correspondenceFilters,
                    float maxWithinCorrespondenceDistance,
                    IPointSampler pointSampler,
                    ICorrespondenceFinder correspondenceFinder
                )
                {
                    foreach (ICorrespondenceFilter filter in correspondenceFilters)
                    {
                        this.correspondenceFilters.Add(filter.ToJson());
                    }
                    this.maxWithinCorrespondenceDistance = maxWithinCorrespondenceDistance;

                    this.pointSampler = pointSampler.ToSerializableObject();
                    this.correspondenceFinder = correspondenceFinder.ToSerializableObject();
                }

                public SerializableCorrespondences(Settings settings)
                    : this(
                        settings.CorrespondenceFilters,
                        settings.MaxWithinCorrespondenceDistance,
                        settings.PointSampler,
                        settings.CorrespondenceFinder
                    )
                { }
            }

            [System.Serializable]
            public class SerializableError
            {
                public float errorThreshold;
                public SerializableErrorMetric errorMetric;

                private SerializableError(float errorThreshold, SerializableErrorMetric errorMetric)
                {
                    this.errorThreshold = errorThreshold;
                    this.errorMetric = errorMetric;
                }

                public SerializableError(Settings settings)
                    : this(settings.ErrorThreshold, new SerializableErrorMetric(settings.ErrorMetric))
                { }
            }

            public SerializableTransform referenceTransform;

            public SerializableCorrespondences correspondences;

            public SerializableError error;

            public int maxNumIterations;

            public SerializableSettings(Settings settings)
            {
                this.maxNumIterations = settings.MaxNumIterations;

                referenceTransform = new SerializableTransform(settings.ReferenceTransform);

                correspondences = new SerializableCorrespondences(settings);

                //find transform

                //error
                error = new SerializableError(settings);

                //Apply transform
            }

            public void ToJson(string outputPath)
            {
                string jsonString = JsonUtility.ToJson(this);

                Debug.Log("jsonString: " + jsonString);

                StreamWriter streamWriter = new StreamWriter(outputPath);
                streamWriter.Write(jsonString);
                streamWriter.Close();
            }
        }
    }

}

