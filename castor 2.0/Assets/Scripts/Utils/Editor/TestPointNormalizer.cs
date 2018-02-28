using UnityEngine;
using NUnit.Framework;
using Registration;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using System.Collections.ObjectModel;

namespace Tests
{
    [TestFixture]
    public class PointNormalizerTests
    {
        private List<Point> basePoints;
        private PointNormalizer normalizer;

        [SetUp]
        public void Init()
        {
            basePoints = new List<Point>
            {
                new Point(new Vector3(+0.0f, +0.0f, +0.9f)),
                new Point(new Vector3(-0.2f, -0.8f, +0.1f)),
                new Point(new Vector3(-0.3f, -0.1f, +0.0f)),
                new Point(new Vector3(+0.9f, +0.4f, -0.1f)),
                new Point(new Vector3(-0.3f, -0.4f, -0.3f)),
            };
            normalizer = new PointNormalizer();
        }

        [Test]
        public void TestComputeNormalizationMatrix_AllInUnitSphere()
        {
            Matrix4x4 expected = new Matrix4x4();
            expected.SetTRS(
                pos: new Vector3(),
                q: Quaternion.identity,
                s: new Vector3(1, 1, 1)
            );

            Matrix4x4 actual = normalizer.ComputeNormalizationMatrix(basePoints);
        }

        [Test]
        public void TestComputeNormalizationMatrix_AllInUnitSphereAndOneOn()
        {
            basePoints.Add(new Point(new Vector3(0, 0, +1)));
            basePoints.Add(new Point(new Vector3(0, 0, -1)));
            basePoints.Add(new Point(new Vector3(0, +1, 0)));
            basePoints.Add(new Point(new Vector3(0, -1, 0)));
            basePoints.Add(new Point(new Vector3(+1, 0, 0)));
            basePoints.Add(new Point(new Vector3(-1, 0, 0)));

            Matrix4x4 expected = new Matrix4x4();
            expected.SetTRS(
                pos: new Vector3(),
                q: Quaternion.identity,
                s: new Vector3(1, 1, 1)
            );

            Matrix4x4 actual = normalizer.ComputeNormalizationMatrix(basePoints);
        }

        [Test]
        public void TestComputeNormalizationMatrix_OnlyNeedsScaling()
        {
            Vector3 translation = new Vector3();
            Vector3 scale = new Vector3(2, 3, 5);
            List<Point> points = ScaleAndTranslate(translation, scale, basePoints.AsReadOnly());

            Matrix4x4 expected = new Matrix4x4();
            expected.SetTRS(
                pos: translation,
                q: Quaternion.identity,
                s: scale
            );

            Matrix4x4 actual = normalizer.ComputeNormalizationMatrix(points);
        }

        [Test]
        public void TestComputeNormalizationMatrix_OnlyNeedsTranslation()
        {
            Vector3 translation = new Vector3(2, 3, 4);
            Vector3 scale = new Vector3(1, 1, 1);
            List<Point> points = ScaleAndTranslate(translation, scale, basePoints.AsReadOnly());

            Matrix4x4 expected = new Matrix4x4();
            expected.SetTRS(
                pos: translation,
                q: Quaternion.identity,
                s: scale
            );

            Matrix4x4 actual = normalizer.ComputeNormalizationMatrix(points);
        }

        [Test]
        public void TestComputeNormalizationMatrix_NeedsScalingAndTranslation()
        {
            Vector3 translation = new Vector3(2, 3, 4);
            Vector3 scale = new Vector3(5, 6, 7);
            List<Point> points = ScaleAndTranslate(translation, scale, basePoints.AsReadOnly());

            Matrix4x4 expected = new Matrix4x4();
            expected.SetTRS(
                pos: translation,
                q: Quaternion.identity,
                s: scale
            );

            Matrix4x4 actual = normalizer.ComputeNormalizationMatrix(points);
        }

        [Test]
        public void TestNormalize_AllInUnitSphere()
        {
            List<Point> points = basePoints;
            IEnumerable<Point> normalizedPoints = normalizer.Normalize(basePoints);
            foreach (Point point in normalizedPoints)
            {
                Assert.That(point, new IsInUnitCircleInclusiveConstraint());
            }
            Assert.That(normalizedPoints, Is.EquivalentTo(basePoints));
        }

        [Test]
        public void TestNormalize_NeedsScalingAndTranslation()
        {
            List<Point> points = ScaleAndTranslate(new Vector3(1, 2, 3), new Vector3(3, 4, 5), basePoints.AsReadOnly());
            IEnumerable<Point> normalizedPoints = normalizer.Normalize(basePoints);
            foreach (Point point in normalizedPoints)
            {
                Assert.That(point, new IsInUnitCircleInclusiveConstraint());
            }
        }

        [Test]
        public void TestScaleAndTranslate_List_UnitTransform()
        {
            Vector3 translation = new Vector3();
            Vector3 scale = new Vector3(1, 1, 1);

            List<Point> actual = ScaleAndTranslate(translation, scale, basePoints.AsReadOnly());
            List<Point> expected = basePoints;

            Assert.That(actual, Is.EquivalentTo(expected));
        }

        [Test]
        public void TestScaleAndTranslate_List_NonUnitTransform()
        {
            Vector3 translation = new Vector3(1, 2, 3);
            Vector3 scale = new Vector3(3, 2, 1);

            List<Point> actual = ScaleAndTranslate(translation, scale, basePoints.AsReadOnly());
            List<Point> expected = new List<Point>
            {
                new Point(new Vector3(1.0000f, 2.0000f, 3.9000f)),
                new Point(new Vector3(0.4000f, 0.4000f, 3.1000f)),
                new Point(new Vector3(0.1000f, 1.8000f, 3.0000f)),
                new Point(new Vector3(3.7000f, 2.8000f, 2.9000f)),
                new Point(new Vector3(0.1000f, 1.2000f, 2.7000f))
            };

            Assert.That(actual, Is.EquivalentTo(expected));
        }

        [Test]
        public void TestScaleAndTranslate_UnitTransform()
        {
            Vector3 translation = new Vector3(0, 0, 0);
            Vector3 scale = new Vector3(1, 1, 1);

            Point point = new Point(new Vector3(0.0f, 0.0f, 0.9f));

            Point expected = new Point(new Vector3(0.0f, 0.0f, 0.9f));
            Point actual = ScaleAndTranslate(translation, scale, point);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void TestScaleAndTranslate_NonUnitTransform()
        {
            Vector3 translation = new Vector3(1, 2, 3);
            Vector3 scale = new Vector3(3, 2, 1);

            Point point = new Point(new Vector3(-0.2f, -0.8f, +0.1f));

            Point expected = new Point(new Vector3(0.4000f, 0.4000f, 3.1000f));
            Point actual = ScaleAndTranslate(translation, scale, point);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void TestScaleAndTranslate_NonUnitTransform_2()
        {
            Vector3 translation = new Vector3(1, 2, 3);
            Vector3 scale = new Vector3(3, 2, 1);

            Point point = new Point(new Vector3(-0.3f, -0.4f, -0.3f));

            Point expected = new Point(new Vector3(0.1000f, 1.2000f, 2.7000f));
            Point actual = ScaleAndTranslate(translation, scale, point);

            Assert.That(actual, Is.EqualTo(expected));
        }

        private List<Point> ScaleAndTranslate(Vector3 translation, Vector3 scale, ReadOnlyCollection<Point> points)
        {
            List<Point> transformedPoints = new List<Point>(points.Count);
            foreach (Point point in points)
            {
                transformedPoints.Add(ScaleAndTranslate(translation, scale, point));
            }
            return transformedPoints;
        }

        private Point ScaleAndTranslate(Vector3 translation, Vector3 scale, Point point)
        {
            Vector3 position = point.Position;

            position.Scale(scale);
            position = position + translation;

            return new Point(position);
        }
    }

    public class IsInUnitCircleInclusiveConstraint : Constraint
    {
        public override ConstraintResult ApplyTo(object actual)
        {
            throw new System.ArgumentException("Objects of type object are not supported");
        }

        public ConstraintResult ApplyTo(Point actual)
        {
            return ApplyTo(actual.Position);
        }

        public ConstraintResult ApplyTo(Vector3 actual)
        {
            float distance = DistanceFromOrigin(actual);
            return new ConstraintResult(this, actual, distance <= 1.0f);
        }

        private float DistanceFromOrigin(Vector3 position)
        {
            float distanceFromOrigin = (
                position.x * position.x +
                position.y * position.y +
                position.z * position.z
            );
            return distanceFromOrigin;
        }
    }
}


