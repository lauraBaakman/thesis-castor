using UnityEngine;

namespace Registration
{
    namespace Error
    {
        public static class DistanceMetrics
        {
            public delegate float Metric(Point staticPoint, Point modelPoint);

            public static float SquaredEuclidean(Point staticPoint, Point modelPoint)
            {
                return Vector3.SqrMagnitude(staticPoint.Position - modelPoint.Position);
            }
        }
    }
}