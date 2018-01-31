using UnityEngine;
using System.Collections;

namespace Registration
{
    public static class PointToPointDistanceMetrics
    {
        public static float SquaredEuclidean(Vector3 staticPoint, Vector3 modelPoint)
        {
            return Vector3.SqrMagnitude(staticPoint - modelPoint);
        }
    }
}


