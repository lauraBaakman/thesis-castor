using System.Collections;
using System.Collections.Generic;
using Registration;
using UnityEngine;

namespace Utils
{
    /// <summary>
    /// Normalize points to ensure that they all fall within the unit sphere. This
    /// ensures that the magnitude of the angles and the distances are comparable.
    /// </summary>
    public class PointNormalizer
    {
        public Matrix4x4 ComputeNormalizationMatrix(IEnumerable<Point> points)
        {
            return new _PointNormalizer(points).NormalizationMatrix;
        }

        public IEnumerable<Point> Normalize(IEnumerable<Point> points)
        {
            throw new System.NotImplementedException();
        }
    }

    internal class _PointNormalizer
    {
        private static Vector3 origin = new Vector3(0, 0, 0);
        private static Vector3 unitSphereAxes = new Vector3(2, 2, 2);

        IEnumerable<Point> Points;

        RangeF xRange = new RangeF();
        RangeF yRange = new RangeF();
        RangeF zRange = new RangeF();

        public readonly Matrix4x4 NormalizationMatrix;

        public _PointNormalizer(IEnumerable<Point> points)
        {
            this.Points = points;

            NormalizationMatrix = new Matrix4x4();
            NormalizationMatrix.SetTRS(
                pos: ComputeTranslation(),
                q: Quaternion.identity,
                s: ComputeScale()
            );
        }

        private Vector3 ComputeTranslation()
        {
            Vector3 center = ComputeCenter();
            return origin - center;
        }

        private Vector3 ComputeCenter()
        {
            ComputeRange(Points, 0, ref xRange);
            ComputeRange(Points, 1, ref yRange);
            ComputeRange(Points, 2, ref zRange);

            return new Vector3(
                xRange.Center, yRange.Center, zRange.Center
            );
        }

        private void ComputeRange(IEnumerable<Point> points, int idx, ref RangeF range)
        {
            foreach (Point point in points)
            {
                range.UpdateMin(point.Position[idx]);
                range.UpdateMax(point.Position[idx]);
            }
        }

        private Vector3 ComputeScale()
        {
            return new Vector3(
                x: ComputeScale(unitSphereAxes.x, xRange),
                y: ComputeScale(unitSphereAxes.y, yRange),
                z: ComputeScale(unitSphereAxes.z, zRange)
            );
        }

        private float ComputeScale(float requestedWidth, RangeF range)
        {
            return requestedWidth / range.Length;
        }
    }
}
