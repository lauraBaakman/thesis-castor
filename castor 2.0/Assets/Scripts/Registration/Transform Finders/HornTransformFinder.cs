using UnityEngine;
using System.Collections.Generic;

using ICPLib;
using OpenTK;

namespace Registration
{
    /// <summary>
    /// Finds the transform that minimizes the sum of squares of residual error.
    /// 
    /// Horn, Berthold KP. "Closed-form solution of absolute orientation using unit quaternions." JOSA A 4.4 (1987): 629-642.
    /// </summary>
    public class HornTransformFinder : ITransformFinder
    {
        /// <summary>
        /// Finds the transform that should be applied to the model points to 
        /// reduce the sum of squard distances error.
        /// </summary>
        /// <returns>The transform.</returns>
        /// <param name="correspondences">Correspondences.</param>
        public Matrix4x4 FindTransform(CorrespondenceCollection correspondences)
        {
            ValidateCorrespondences(correspondences);

            List<Vector3d> modelPoints = new List<Vector3d>();
            List<Vector3d> staticPoints = new List<Vector3d>();

            CorrespondecesToVector3dLists(correspondences, ref modelPoints, ref staticPoints);

            LandmarkTransform transformComputer = new LandmarkTransform(modelPoints, staticPoints);

            bool computationSucceed = transformComputer.ComputeTransform();

            if (!computationSucceed)
            {
                Debug.LogError(
                    "Could not compute the transform, should not happen, since " +
                    "ValidateCorrespondences should extract these issues");
            }

            return transformComputer.TransformMatrix.ToUnityMatrix();
        }

        private void CorrespondecesToVector3dLists(
            CorrespondenceCollection correspondences,
            ref List<Vector3d> modelPoints, ref List<Vector3d> staticPoints)
        {
            foreach (Correspondence correspondence in correspondences)
            {
                modelPoints.Add(new Vector3d(correspondence.ModelPoint.Position));
                staticPoints.Add(new Vector3d(correspondence.StaticPoint.Position));
            }
        }

        private void ValidateCorrespondences(CorrespondenceCollection correspondences)
        {
            if (correspondences == null) throw new System.NullReferenceException();
            if (correspondences.IsEmpty())
            {
                throw new System.NotSupportedException("Cannot compute the transform if no correspondences are given.");
            }
        }
    }
}

