using UnityEngine;
using System.Collections.Generic;

using ICPLib;
using OpenTK;

namespace Registration
{
    public class HornTransformFinder : ITransformFinder
    {
        /// <summary>
        /// Finds the transform that should be applied to the model points to 
        /// reduce the sum of squard distances error.
        /// </summary>
        /// <returns>The transform.</returns>
        /// <param name="correspondences">Correspondences.</param>
        public Transform FindTransform(List<Correspondence> correspondences)
        {
            ValidateCorrespondences(correspondences);

            List<Vector3d> modelPoints = new List<Vector3d>();
            List<Vector3d> targetPoints = new List<Vector3d>();

            CorrespondecesToVector3dLists(correspondences, out modelPoints, out targetPoints);

            LandmarkTransform transformComputer = new LandmarkTransform(modelPoints, targetPoints);
            if (transformComputer.ComputeTransform())
            {
                Matrix4d transformMatrix = transformComputer.TransformMatrix;
                throw new System.NotImplementedException("Convert transformMatrix to Transform.");
            }
            return null;
        }

        private void CorrespondecesToVector3dLists(
            List<Correspondence> correspondences,
            out List<Vector3d> sourcePoints, out List<Vector3d> targetPoints
        )
        {
            throw new System.NotImplementedException();
        }

        private void ValidateCorrespondences(List<Correspondence> correspondences)
        {
            if (correspondences == null) throw new System.NullReferenceException();
            if (correspondences.Count == 0)
            {
                throw new System.NotSupportedException("Cannot compute the transform if not correspondences are given.");
            }
        }

    }
}

