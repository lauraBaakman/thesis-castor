using System.Collections.Generic;
using Registration;
using UnityEngine;

namespace Utils
{
    /// <summary>
    /// Normalize points to ensure that they all fall within the unit cube. This
    /// ensures that the magnitude of the angles and the distances are comparable.
    /// </summary>
    public class PointNormalizer
    {
        public Matrix4x4 ComputeNormalizationMatrix(IEnumerable<Point> points)
        {
            return new _PointNormalizer(points).NormalizationMatrix;
        }

        public Matrix4x4 ComputeNormalizationMatrix(params IEnumerable<Point>[] args)
        {
            List<Point> points = new List<Point>();
            for (int i = 0; i < args.Length; i++)
            {
                points.AddRange(args[i]);
            }
            return ComputeNormalizationMatrix(points);
        }

        public IEnumerable<Point> Normalize(IEnumerable<Point> points)
        {
            Matrix4x4 normalizationmatrix = ComputeNormalizationMatrix(points);
            List<Point> normalizedPoints = new List<Point>();

            foreach (Point point in points)
            {
                normalizedPoints.Add(
                    point.ApplyTransform(normalizationmatrix)
                );
            }
            return normalizedPoints;
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

            //The translation matrix needs to be compute before the scale matrix
            Matrix4x4 translation = ComputeTranslationMatrix();
            Matrix4x4 scale = ComputeScaleMatrix();

            //First apply the translation, then the scaling, not the other way around as the result of SetTRS does
            NormalizationMatrix = scale * translation;
        }

        private Matrix4x4 ComputeTranslationMatrix()
        {
            return new Matrix4x4().SetTranslation(ComputeTranslation());
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

        private Matrix4x4 ComputeScaleMatrix()
        {
            float xScale = ComputeScaleForDimension(xRange);
            float yScale = ComputeScaleForDimension(yRange);
            float zScale = ComputeScaleForDimension(zRange);

            float scale = Mathf.Min(xScale, yScale, zScale);

            return new Matrix4x4().SetScale(scale); ;
        }

        private float ComputeScaleForDimension(RangeF range)
        {
            return 1.0f / range.Length;
        }
    }
}
