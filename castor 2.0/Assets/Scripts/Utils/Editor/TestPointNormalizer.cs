using UnityEngine;
using NUnit.Framework;
using Registration;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using Utils;

namespace Tests
{
    [TestFixture]
    public class PointNormalizerTests
    {
        private PointNormalizer normalizer;

        [SetUp]
        public void Init()
        {
            normalizer = new PointNormalizer();
        }

        [Test]
        public void TestComputeNormalizationMatrix_PointsFormUnitCube()
        {
            List<Point> points = new List<Point>{
                new Point(new Vector3(-0.5f, -0.5f, -0.5f)),
                new Point(new Vector3(-0.5f, -0.5f, +0.5f)),
                new Point(new Vector3(-0.5f, +0.5f, -0.5f)),
                new Point(new Vector3(-0.5f, +0.5f, +0.5f)),
                new Point(new Vector3(+0.5f, -0.5f, -0.5f)),
                new Point(new Vector3(+0.5f, -0.5f, +0.5f)),
                new Point(new Vector3(+0.5f, +0.5f, -0.5f)),
                new Point(new Vector3(+0.5f, +0.5f, +0.5f))
            };

            Matrix4x4 actual = normalizer.ComputeNormalizationMatrix(points);
            Matrix4x4 expected = new Matrix4x4().SetScale(1.0f) * new Matrix4x4().SetTranslation(new Vector3(0, 0, 0));

            Assert.IsTrue(actual.Equals(expected));
        }

        [Test]
        public void TestComputeNormalizationMatrix_OnlyScaling()
        {
            List<Point> points = new List<Point>{
                new Point(new Vector3(-0.3f, -0.25f, -0.05f)),
                new Point(new Vector3(-0.2f, -0.20f, +0.05f)),
                new Point(new Vector3(-0.1f, +0.25f, -0.05f)),
                new Point(new Vector3(-0.1f, +0.15f, +0.05f)),
                new Point(new Vector3(+0.2f, -0.15f, -0.05f)),
                new Point(new Vector3(+0.1f, -0.05f, +0.05f)),
                new Point(new Vector3(+0.2f, +0.05f, -0.05f)),
                new Point(new Vector3(+0.3f, +0.05f, +0.05f))
            };

            Matrix4x4 actual = normalizer.ComputeNormalizationMatrix(points);
            Matrix4x4 expected = new Matrix4x4().SetScale(1.0f + 2.0f / 3.0f) * new Matrix4x4().SetTranslation(new Vector3(0, 0, 0));

            Assert.IsTrue(actual.Equals(expected));
        }

        [Test]
        public void TestComputeNormalizationMatrix_OnlyTranslation()
        {
            List<Point> points = new List<Point>{
                new Point(new Vector3(+1.5f, -3.5f, +0.0f)),
                new Point(new Vector3(+1.5f, -3.5f, +1.0f)),
                new Point(new Vector3(+1.5f, -2.5f, +0.0f)),
                new Point(new Vector3(+1.5f, -2.5f, +1.0f)),
                new Point(new Vector3(+2.5f, -3.5f, +0.0f)),
                new Point(new Vector3(+2.5f, -3.5f, +1.0f)),
                new Point(new Vector3(+2.5f, -2.5f, +0.0f)),
                new Point(new Vector3(+2.5f, -2.5f, +1.0f))
            };

            Matrix4x4 actual = normalizer.ComputeNormalizationMatrix(points);
            Matrix4x4 expected = new Matrix4x4().SetScale(1.0f) * new Matrix4x4().SetTranslation(new Vector3(-2, +3, -0.5f));

            Assert.IsTrue(actual.Equals(expected));
        }

        [Test]
        public void TestComputeNormalizationMatrix_TranslationAndScaling()
        {
            //Scaling applied on unit cube: [3, 5, 7]
            //Translation applied on scaled unit cube: [+2, +1, -5]
            List<Point> points = new List<Point>{
                new Point(new Vector3(+0.5f, -1.5f, -8.5f)),
                new Point(new Vector3(+0.5f, -1.5f, -1.5f)),
                new Point(new Vector3(+0.5f, +3.5f, -8.5f)),
                new Point(new Vector3(+0.5f, +3.5f, -1.5f)),
                new Point(new Vector3(+3.5f, -1.5f, -8.5f)),
                new Point(new Vector3(+3.5f, -1.5f, -1.5f)),
                new Point(new Vector3(+3.5f, +3.5f, -8.5f)),
                new Point(new Vector3(+3.5f, +3.5f, -1.5f))
            };

            Matrix4x4 actual = normalizer.ComputeNormalizationMatrix(points);
            Matrix4x4 expected = new Matrix4x4().SetScale(1f / 7f) * new Matrix4x4().SetTranslation(new Vector3(-2, -1, +5));

            Assert.IsTrue(actual.Equals(expected));
        }

        [Test]
        public void TestComputeNormalizationMatrix_multiple_lists_TranslationAndScaling()
        {
            //Scaling applied on unit cube: [3, 5, 7]
            //Translation applied on scaled unit cube: [+2, +1, -5]
            List<Point> points_a = new List<Point> {
                new Point(new Vector3(+0.5f, -1.5f, -1.5f)),
                new Point(new Vector3(+0.5f, +3.5f, -1.5f)),

            };
            List<Point> points_b = new List<Point>
            {
                new Point(new Vector3(+3.5f, -1.5f, -1.5f)),
                new Point(new Vector3(+3.5f, +3.5f, -1.5f))
            };
            List<Point> points_c = new List<Point>
            {
                new Point(new Vector3(+0.5f, -1.5f, -8.5f)),
                new Point(new Vector3(+3.5f, -1.5f, -8.5f)),
                new Point(new Vector3(+3.5f, +3.5f, -8.5f)),
                new Point(new Vector3(+0.5f, +3.5f, -8.5f)),
            };

            Matrix4x4 actual = normalizer.ComputeNormalizationMatrix(points_a, points_b, points_c);
            Matrix4x4 expected = new Matrix4x4().SetScale(1f / 7f) * new Matrix4x4().SetTranslation(new Vector3(-2, -1, +5));

            Assert.IsTrue(actual.Equals(expected));
        }

        [Test]
        public void Normalize_NeutralTransformationMatrix()
        {
            List<Point> points = new List<Point>{
                new Point(new Vector3(-0.5f, -0.5f, -0.5f)),
                new Point(new Vector3(-0.5f, -0.5f, +0.5f)),
                new Point(new Vector3(-0.5f, +0.5f, -0.5f)),
                new Point(new Vector3(-0.5f, +0.5f, +0.5f)),
                new Point(new Vector3(+0.5f, -0.5f, -0.5f)),
                new Point(new Vector3(+0.5f, -0.5f, +0.5f)),
                new Point(new Vector3(+0.5f, +0.5f, -0.5f)),
                new Point(new Vector3(+0.5f, +0.5f, +0.5f))
            };

            List<Point> expected = new List<Point>
            {
                new Point(new Vector3(-0.5f, -0.5f, -0.5f)),
                new Point(new Vector3(-0.5f, -0.5f, +0.5f)),
                new Point(new Vector3(-0.5f, +0.5f, -0.5f)),
                new Point(new Vector3(-0.5f, +0.5f, +0.5f)),
                new Point(new Vector3(+0.5f, -0.5f, -0.5f)),
                new Point(new Vector3(+0.5f, -0.5f, +0.5f)),
                new Point(new Vector3(+0.5f, +0.5f, -0.5f)),
                new Point(new Vector3(+0.5f, +0.5f, +0.5f))
            };

            List<Point> actual = new List<Point>();
            actual.AddRange(normalizer.Normalize(points));

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
                Assert.That(expected[i], new IsInUnitCubeInclusiveConstraint());
            }
        }

        [Test]
        public void Normalize_TranslationAndScaling()
        {
            //Scaling applied on unit cube: [3, 5, 7]
            //Translation applied on scaled unit cube: [+2, +1, -5]
            List<Point> points = new List<Point>{
                new Point(new Vector3(+0.5f, -1.5f, -8.5f)),
                new Point(new Vector3(+0.5f, -1.5f, -1.5f)),
                new Point(new Vector3(+0.5f, +3.5f, -8.5f)),
                new Point(new Vector3(+0.5f, +3.5f, -1.5f)),
                new Point(new Vector3(+3.5f, -1.5f, -8.5f)),
                new Point(new Vector3(+3.5f, -1.5f, -1.5f)),
                new Point(new Vector3(+3.5f, +3.5f, -8.5f)),
                new Point(new Vector3(+3.5f, +3.5f, -1.5f))
            };

            List<Point> actual = new List<Point>();
            actual.AddRange(normalizer.Normalize(points));

            List<Point> expected = new List<Point>
            {
                new Point(new Vector3(-0.2142857143f, -0.3571428571f, -0.5f)),
                new Point(new Vector3(-0.2142857143f, -0.3571428571f, +0.5f)),
                new Point(new Vector3(-0.2142857143f, +0.3571428571f, -0.5f)),
                new Point(new Vector3(-0.2142857143f, +0.3571428571f, +0.5f)),
                new Point(new Vector3(+0.2142857143f, -0.3571428571f, -0.5f)),
                new Point(new Vector3(+0.2142857143f, -0.3571428571f, +0.5f)),
                new Point(new Vector3(+0.2142857143f, +0.3571428571f, -0.5f)),
                new Point(new Vector3(+0.2142857143f, +0.3571428571f, +0.5f))
            };

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
                Assert.That(expected[i], new IsInUnitCubeInclusiveConstraint());
            }
        }
    }

    public class IsInUnitCubeInclusiveConstraint : Constraint
    {
        public override ConstraintResult ApplyTo(object actual)
        {
            if ((actual is Point)) return ApplyTo(actual as Point);
            if ((actual is Vector3)) return ApplyTo((Vector3)actual);

            throw new System.ArgumentException("Objects of type object are not supported");
        }

        public ConstraintResult ApplyTo(Point actual)
        {
            return ApplyTo(actual.Position);
        }

        public ConstraintResult ApplyTo(Vector3 actual)
        {
            return new ConstraintResult(
                this, actual,
                OnAxis(actual.x) && OnAxis(actual.y) && OnAxis(actual.z)
            );
        }

        private bool OnAxis(float value)
        {
            return value <= 0.5 && value >= -0.5;
        }
    }
}


