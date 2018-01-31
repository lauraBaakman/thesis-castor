using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Registration
{
    public class PointToPointMeanSquaredDistance : IPointToPointErrorMetric
    {
        public float ComputeError(List<Correspondence> correspondences)
        {
            float error = 0;
            foreach (Correspondence correspondence in correspondences)
            {
                error += CorrespondenceError(correspondence);
            }
            error /= correspondences.Count;
            return error;
        }

        private float CorrespondenceError(Correspondence correspondence)
        {
            return SquaredDistance(correspondence.StaticPoint, correspondence.ModelPoint);
        }

        private float SquaredDistance(Vector3 staticPoint, Vector3 modelPoint)
        {
            return Vector3.SqrMagnitude(staticPoint - modelPoint);
        }
    }
}