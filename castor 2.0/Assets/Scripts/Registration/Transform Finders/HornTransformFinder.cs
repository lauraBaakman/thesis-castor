using UnityEngine;
using System.Collections.Generic;

using ICPLib;
using OpenTK;
using Registration.Error;

namespace Registration
{
    /// <summary>
    /// Finds the transform that minimizes the sum of squares of residual error.
    /// 
    /// Horn, Berthold KP. "Closed-form solution of absolute orientation using unit quaternions." JOSA A 4.4 (1987): 629-642.
    /// </summary>
    public class HornTransformFinder : AbstractTransformFinder
    {
        private IErrorMetric errorMetric;

        public HornTransformFinder()
        {
            this.errorMetric = ErrorMetric.Horn();
        }

        public override IErrorMetric GetErrorMetric()
        {
            return this.errorMetric;
        }

        /// <summary>
        /// Finds the transform that should be applied to the model points to 
        /// reduce the sum of squard distances error.
        /// </summary>
        /// <returns>The transform.</returns>
        /// <param name="correspondences">Correspondences.</param>
        protected override Matrix4x4 FindTransformImplementation(CorrespondenceCollection correspondences)
        {
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
    }
}

