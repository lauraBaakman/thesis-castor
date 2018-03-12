using System;
using NUnit.Framework;
using UnityEngine;
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

        [Test]
        public void Test_SVD_Square_Matrix()
        {
            double[,] A = {
                {-29.809999999999999, -84.807000000000002, -63.218000000000004},
                {+02.650000000000000, -52.017000000000003, -52.009000000000000},
                {-19.638000000000002, -75.335999999999999, -16.547000000000001},
            };

            double[,] expected_U = {
                {-0.729908087739305, -0.123505763730895, -0.672294957424195},
                {-0.465764528149259, -0.630004873055934, +0.621415532670109},
                {-0.500297499264836, +0.766707366764547, +0.402321048390866},
            };
            double[,] expected_S = {
                {149.7913869700865, 000.0000000000000, 000.0000000000000},
                {000.0000000000000, 034.0373717936407, 000.0000000000000},
                {000.0000000000000, 000.0000000000000, 015.2993660966482},
            };
            double[,] expected_Vt = {
                {+0.202609288827379, +0.826612287707770, +0.525034857786977},
                {-0.383237737754462, -0.426459172115321, +0.819305444190112},
                {+0.901153878336938, -0.367212064520276, +0.230384433564968},
            };

            double[,] actual_U, actual_S, actual_Vt;

            ArrayMatrixUtils.SVD(A, out actual_U, out actual_S, out actual_Vt);

            Assert.That(actual_U, Is.EqualTo(expected_U).Within(precision));
            Assert.That(actual_S, Is.EqualTo(expected_S).Within(precision));
            Assert.That(actual_Vt, Is.EqualTo(expected_Vt).Within(precision));
        }

        [Test]
        public void Test_SVD_Rectangular_Matrix()
        {
            double[,] A = {
                {79.2200, 03.5700},
                {95.9500, 84.9100},
                {65.5700, 93.4000},
            };

            double[,] expected_U = {
                {-0.343225213778742, +0.878072041895140, +0.333445560577190},
                {-0.710225885367531, -0.010313122653119, -0.703898310308420},
                {-0.614634561654683, -0.478417316516910, +0.627169217098794},
            };
            double[,] expected_S = {
                {180.3990135626532, 000.0000000000000},
                {000.0000000000000, 056.4255678360587},
                {000.0000000000000, 000.0000000000000},
            };
            double[,] expected_Vt = {
                {-0.751878076634585, -0.659302174936711},
                {+0.659302174936711, -0.751878076634585},
            };

            double[,] actual_U, actual_S, actual_Vt;

            ArrayMatrixUtils.SVD(A, out actual_U, out actual_S, out actual_Vt);

            Assert.That(actual_U, Is.EqualTo(expected_U).Within(precision));
            Assert.That(actual_S, Is.EqualTo(expected_S).Within(precision));
            Assert.That(actual_Vt, Is.EqualTo(expected_Vt).Within(precision));
        }
    }
}