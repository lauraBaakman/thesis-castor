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

            /// <summary>
            /// Distance between the static point and the plane tangential to the
            ///  model point. As defined by
            /// 
            /// Low, Kok-Lim. "Linear least-squares optimization for 
            /// point-to-plane icp surface registration." Chapel Hill, 
            /// University of North Carolina 4 (2004).
            /// </summary>
            /// <returns>Distance between the static point and the plane tangential to the model point.</returns>
            /// <param name="staticPoint">Static point.</param>
            /// <param name="modelPoint">Model point.</param>
            public static float PointToPlane(Point staticPoint, Point modelPoint)
            {
                return Vector3.Dot(
                    (staticPoint.Position - modelPoint.Position),
                    modelPoint.Normal
                );
            }

            /// <summary>
            /// Squared Distance between the static point and the plane tangential to the
            ///  model point. As defined by
            /// 
            /// Low, Kok-Lim. "Linear least-squares optimization for 
            /// point-to-plane icp surface registration." Chapel Hill, 
            /// University of North Carolina 4 (2004).
            /// </summary>
            /// <returns>Squared distance between the static point and the plane tangential to the model point.</returns>
            /// <param name="staticPoint">Static point.</param>
            /// <param name="modelPoint">Model point.</param>
            public static float SquaredPointToPlane(Point staticPoint, Point modelPoint)
            {
                float pointToPlane = PointToPlane(staticPoint, modelPoint);
                return pointToPlane * pointToPlane;
            }
        }
    }
}