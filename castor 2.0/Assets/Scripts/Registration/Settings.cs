using UnityEngine;

namespace Registration {

    public class Settings {

        public Transform ReferenceTransform { get; internal set; }

        public Settings(Transform referenceTransform)
        {
            ReferenceTransform = referenceTransform;
        }
    }
}

