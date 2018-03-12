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
        public void Test_ToDiagonalMatrix_NonSquareOutput()
        {
            double[] diagonal = new double[] { 1, 2, 3 };
            int numRows = 4;
            int numCols = 3;

            double[,] expected = new double[,] {
                {1, 0, 0},
                {0, 2, 0},
                {0, 0, 3},
                {0, 0, 0},
            };

            double[,] actual = ArrayMatrixUtils.ToDiagonalMatrix(diagonal, numRows, numCols);

            Assert.That(actual, Is.EqualTo(expected).Within(precision));
        }

        [Test]
        public void Test_ToDiagonalMatrix_NonSquareOutput_InvalidNumRows()
        {
            Assert.Throws(typeof(System.ArgumentException), new TestDelegate(Test_ToDiagonalMatrix_NonSquareOutput_InvalidNumRows_Helper));
        }

        public void Test_ToDiagonalMatrix_NonSquareOutput_InvalidNumRows_Helper()
        {
            double[] diagonal = new double[] { 1, 2, 3 };
            int numRows = 2;
            int numCols = 3;

            ArrayMatrixUtils.ToDiagonalMatrix(diagonal, numRows, numCols);
        }

        [Test]
        public void Test_ToDiagonalMatrix_NonSquareOutput_InvalidNumCols()
        {
            Assert.Throws(typeof(System.ArgumentException), new TestDelegate(Test_ToDiagonalMatrix_NonSquareOutput_InvalidNumCols_Helper));
        }

        public void Test_ToDiagonalMatrix_NonSquareOutput_InvalidNumCols_Helper()
        {
            double[] diagonal = new double[] { 1, 2, 3 };
            int numRows = 3;
            int numCols = 2;

            ArrayMatrixUtils.ToDiagonalMatrix(diagonal, numRows, numCols);
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