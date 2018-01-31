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
        public IPointSelector Selector { get; set; }

        /// <summary>
        /// The method used to find correspondecs within the points selected by
        /// the Selector.
        /// </summary>
        /// <value>The correspondence finder.</value>
        public ICorrespondenceFinder CorrespondenceFinder { get; set; }

        public Settings(
            Transform referenceTransform,
            float errorThreshold = 0.001f, int maxNumIterations = 50
        )
        {
            ReferenceTransform = referenceTransform;

            ErrorThreshold = errorThreshold;

            MaxNumIterations = maxNumIterations;

            Selector = new SelectAllPointsSelector(ReferenceTransform);

            CorrespondenceFinder = new NearstPointCorrespondenceFinder();
        }
    }
}

