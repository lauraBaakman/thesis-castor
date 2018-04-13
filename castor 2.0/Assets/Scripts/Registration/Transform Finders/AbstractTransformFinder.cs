using Registration.Error;
using UnityEngine;

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
}