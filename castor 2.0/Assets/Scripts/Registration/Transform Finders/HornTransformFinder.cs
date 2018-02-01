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
            List<Vector3d> staticPoints = new List<Vector3d>();

            CorrespondecesToVector3dLists(correspondences, ref modelPoints, ref staticPoints);

            LandmarkTransform transformComputer = new LandmarkTransform(modelPoints, staticPoints);
            if (transformComputer.ComputeTransform())
            {
                Matrix4x4 transformMatrix = transformComputer.TransformMatrix.ToUnityMatrix();
                throw new System.NotImplementedException("Convert transformMatrix to Transform.");
            }
            return null;
        }

        private void CorrespondecesToVector3dLists(
            List<Correspondence> correspondences,
            ref List<Vector3d> modelPoints, ref List<Vector3d> staticPoints)
        {
            foreach (Correspondence correspondence in correspondences)
            {
                modelPoints.Add(new Vector3d(correspondence.ModelPoint));
                staticPoints.Add(new Vector3d(correspondence.StaticPoint));
            }
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

