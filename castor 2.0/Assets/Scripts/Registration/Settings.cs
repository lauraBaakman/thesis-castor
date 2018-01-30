using UnityEngine;

namespace Registration
{

    public class Settings
    {
        /// <summary>
        /// The transform in which the registration is performed. 
        /// </summary>
        /// <value>The reference transform.</value>
        public Transform ReferenceTransform { get; internal set; }

        /// <summary>
        /// If the error of the registration is smaller than this threshold the
        /// registration process terminates.
        /// </summary>
        /// <value>The error threshold.</value>
        public float ErrorThreshold { get; internal set; }

        public Settings(Transform referenceTransform, float errorThreshold = 0.001f)
        {
            ReferenceTransform = referenceTransform;

            ErrorThreshold = errorThreshold;
        }
    }
}

