using System;
using NUnit.Framework;
using Utils;

namespace Tests.Utils
{
    [TestFixture]
    public class ArrayMatrixUtilsTests
    {
        float precision = 0.0001f;

        [Test]
        public void Test_ToDiagonalMatrix()
        {
            double[] diagonal = new double[] { 1, 2, 3 };

            double[,] expected = new double[,] {
                {1, 0, 0},
                {0, 2, 0},
                {0, 0, 3},
            };

            double[,] actual = ArrayMatrixUtils.ToDiagonalMatrix(diagonal);

            Assert.That(actual, Is.EqualTo(expected).Within(precision));
        }

        [Test]
        public void Test_PseudoInverse_ValidArray()
        {
            double[,] array = { { 2, 0 }, { 0, 2 * float.Epsilon } };

            double[,] expected = { { 0.5f, 0 }, { 0, 0 } };

            double[,] actual = ArrayMatrixUtils.PseudoInverseOfDiagonalMatrix(array);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Test_PseudoInverse_InValidArray()
        {
            Assert.Throws(typeof(System.ArgumentException), new TestDelegate(Test_PseudoInverse_InValidArray_Helper));
        }

        private void Test_PseudoInverse_InValidArray_Helper()
        {
            double[,] array = { { 2, 0, 0 }, { 0, 0, 0 } };

            ArrayMatrixUtils.PseudoInverseOfDiagonalMatrix(array);
        }
    }
}