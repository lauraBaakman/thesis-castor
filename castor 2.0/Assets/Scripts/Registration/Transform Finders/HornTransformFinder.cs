using UnityEngine;
using System.Collections.Generic;

using ICPLib;
using OpenTK;

namespace Registration
{
    public class HornTransformFinder : ITransformFinder
    {
        public Transform FindTransform(List<Correspondence> correspondences)
        {
            List<Vector3d> sourceLandmarks = new List<Vector3d>();
            List<Vector3d> targetLandmarks = new List<Vector3d>();

            LandmarkTransform transformComputer = new LandmarkTransform(sourceLandmarks, targetLandmarks);
            if (transformComputer.ComputeTransform())
            {
                Matrix4d transformMatrix = transformComputer.TransformMatrix;
            }
            return null;
        }
    }
}

