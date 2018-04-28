using Registration.Error;
using UnityEngine;
using System;

namespace Registration
{
    public abstract class AbstractTransformFinder : ITransformFinder
    {
        public virtual Matrix4x4 FindTransform(CorrespondenceCollection correspondences)
        {
            ValidateCorrespondences(correspondences);

            return FindTransformImplementation(correspondences);
        }

        public abstract IErrorMetric GetErrorMetric();

        public abstract SerializableTransformFinder Serialize();

        protected abstract Matrix4x4 FindTransformImplementation(CorrespondenceCollection correspondences);

        protected virtual void ValidateCorrespondences(CorrespondenceCollection correspondences)
        {
            if (correspondences == null) throw new System.NullReferenceException();
            if (correspondences.IsEmpty())
            {
                throw new System.NotSupportedException("Cannot compute the transform if no correspondences are given.");
            }
        }
    }

    [System.Serializable]
    public class SerializableTransformFinder
    {
        public string name;

        public SerializableTransformFinder(string name)
        {
            this.name = name;
        }
    }
}