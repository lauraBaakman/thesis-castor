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
		public void Test_MoorePenroseInverse_SquareMatrix()
		{
			double[,] array = {
				{-6.040000000000000, +4.880000000000000},
				{-9.390000000000001, +0.000000000000001},
			};

			double[,] expected = {
				{+0.000000000000000, -0.106496272630458},
				{+0.204918032786885, -0.131810960386878},
			};

			double[,] actual = ArrayMatrixUtils.MoorePenroseInverse(array);

			Assert.That(actual, Is.EqualTo(expected).Within(precision));
		}

		[Test]
		public void Test_MoorePenroseInverse_RectangularMatrix()
		{
			double[,] array = {
				{+5.5800, +7.8200},
				{+4.3000, -3.3200},
				{+8.0700, +3.9700},
			};

			double[,] expected = {
				{+0.0017, +0.0921, +0.0737},
				{+0.0878, -0.1020, -0.0063},
			};

			double[,] actual = ArrayMatrixUtils.MoorePenroseInverse(array);

			Assert.That(actual, Is.EqualTo(expected).Within(precision));
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

		[Test]
		public void Test_Transpose_Square_Matrix()
		{
			double[,] matrix = {
				{-90.069000000000003, -01.827000000000000, +80.010999999999996},
				{+80.543000000000006, -02.149000000000000, -26.151000000000000},
				{+88.956999999999994, -32.456000000000003, -77.759000000000000},
			};

			double[,] expected = {
				{-90.069000000000003, +80.543000000000006, +88.956999999999994},
				{-01.827000000000000, -02.149000000000000, -32.456000000000003},
				{+80.010999999999996, -26.151000000000000, -77.759000000000000},
			};

			double[,] actual = ArrayMatrixUtils.Transpose(matrix);

			Assert.That(actual, Is.EqualTo(expected).Within(precision));
		}

		[Test]
		public void Test_Transpose_Rectangular_Matrix()
		{
			double[,] matrix = {
				{+56.049999999999997, -19.218000000000000},
				{-22.052000000000000, -80.709000000000003},
				{-51.661999999999999, -73.605000000000004},
			};

			double[,] expected = {
				{+56.049999999999997, -22.052000000000000, -51.661999999999999},
				{-19.218000000000000, -80.709000000000003, -73.605000000000004},
			};

			double[,] actual = ArrayMatrixUtils.Transpose(matrix);

			Assert.That(actual, Is.EqualTo(expected).Within(precision));
		}

		[Test]
		public void Test_Multiply_Matrix_SquareMatrix()
		{
			double[,] lhs = {
				{-26.303000000000001, +56.045000000000002},
				{+25.123999999999999, -83.775000000000006},
			};
			double[,] rhs = {
				{85.876999999999995, -02.642000000000000},
				{55.143000000000001, -12.827999999999999},
			};

			double[,] expected = {
				{+0831.666704000000, -0649.452734000000},
				{-2462.031077000001, +1008.288092000000},
			};

			double[,] actual = ArrayMatrixUtils.Multiply(lhs, rhs);

			Assert.That(actual, Is.EqualTo(expected).Within(precision));
		}

		[Test]
		public void Test_Multiply_Matrix_RectangularMatrix()
		{
			double[,] lhs = {
				{+88.409999999999997, -88.043999999999997},
				{+91.227000000000004, -53.043999999999997},
				{+15.042000000000000, -29.367999999999999},
			};
			double[,] rhs = {
				{+64.239000000000004, -91.394999999999996, +29.823000000000000, +29.548999999999999},
				{-96.918999999999997, -66.201999999999998, +46.344000000000001, -09.815000000000000},
			};

			double[,] expected = {
				{14212.50642600000, -02251.54306200000, -01443.65970600000, +03476.57895000000},
				{11001.30268900000, -04826.07277700000, +00262.39168500000, +03216.29348300000},
				{03812.60023000000, +00569.45674600000, -00912.43302600000, +00732.72297800000},
			};

			double[,] actual = ArrayMatrixUtils.Multiply(lhs, rhs);

			Assert.That(actual, Is.EqualTo(expected).Within(precision));
		}

		[Test]
		public void Test_Multiply_Matrix_IncompatibleSize()
		{
			Assert.Throws(typeof(System.ArgumentException), new TestDelegate(Test_Multiply_Matrix_IncompatibleSize_Helper));
		}

		public void Test_Multiply_Matrix_IncompatibleSize_Helper()
		{
			double[,] lhs = {
				{-26.303000000000001, +56.045000000000002},
				{+25.123999999999999, -83.775000000000006},
			};
			double[,] rhs = {
				{85.876999999999995, -02.642000000000000},
			};

			ArrayMatrixUtils.Multiply(lhs, rhs);
		}

		[Test]
		public void Test_Multiply_Matrix_Vector()
		{
			double[,] lhs = {
				{-1.064000000000000, +0.215000000000000},
				{-3.873000000000000, +6.353000000000000},
				{+0.170000000000000, +5.897000000000000},
			};
			double[] rhs = {
				+2.886000000000000, -2.428000000000000
			};

			double[] expected = {
				-03.592724000000000, -26.602561999999999, -13.827296000000000,
			};

			double[] actual = ArrayMatrixUtils.Multiply(lhs, rhs);

			Assert.That(actual, Is.EqualTo(expected).Within(precision));
		}

		[Test]
		public void Test_Multiply_Vector_IncompatibleSize()
		{
			Assert.Throws(typeof(System.ArgumentException), new TestDelegate(Test_Multiply_Vector_IncompatibleSize_Helper));
		}

		public void Test_Multiply_Vector_IncompatibleSize_Helper()
		{
			double[,] lhs = {
				{-1.064000000000000, +0.215000000000000},
				{-3.873000000000000, +6.353000000000000},
				{+0.170000000000000, +5.897000000000000},
			};
			double[] rhs = {
				+2.886000000000000, -2.428000000000000, -13.827296000000000
			};

			ArrayMatrixUtils.Multiply(lhs, rhs);
		}

		[Test]
		public void Test_ColumnVectorToMatrix()
		{
			double[] vector = { 1, 2, 3 };

			double[,] expected = {
				{1},
				{2},
				{3}
			};

			double[,] actual = ArrayMatrixUtils.ColumnVectorToMatrix(vector);

			Assert.That(actual, Is.EqualTo(expected).Within(precision));
		}

		[Test]
		public void Test_RowVectorToMatrix()
		{
			double[] vector = { 1, 2, 3 };

			double[,] expected = {
				{1, 2, 3}
			};

			double[,] actual = ArrayMatrixUtils.RowVectorToMatrix(vector);

			Assert.That(actual, Is.EqualTo(expected).Within(precision));
		}

		[Test]
		public void Test_MatrixToVector_RowMatrix()
		{
			double[,] matrix = {
				{1, 2, 3}
			};

			double[] expected = { 1, 2, 3 };

			double[] actual = ArrayMatrixUtils.ToVector(matrix);

			Assert.That(actual, Is.EqualTo(expected).Within(precision));
		}

		[Test]
		public void Test_MatrixToVector_ColumnMatrix()
		{
			double[,] matrix = {
				{1},
				{2},
				{3}
			};

			double[] expected = { 1, 2, 3 };

			double[] actual = ArrayMatrixUtils.ToVector(matrix);

			Assert.That(actual, Is.EqualTo(expected).Within(precision));
		}

		[Test]
		public void Test_MatrixToVector_InvalidSize()
		{
			Assert.Throws(typeof(System.ArgumentException), new TestDelegate(Test_MatrixToVector_InvalidSize_Helper));
		}

		public void Test_MatrixToVector_InvalidSize_Helper()
		{
			double[,] matrix = {
				{1, 2, 3},
				{1, 2, 3}
			};

			ArrayMatrixUtils.ToVector(matrix);
		}

		[Test]
		public void Test_InfinityNorm_Matrix()
		{
			double[,] matrix = {
				{+1, -7},
				{-2, -3}
			};

			double expected = 8;

			double actual = ArrayMatrixUtils.InfinityNorm(matrix);

			Assert.That(actual, Is.EqualTo(expected));
		}

		[Test]
		public void Test_InfinityNorm_Vector()
		{
			double[] vector = { -1, -7 };

			double expected = 7;

			double actual = ArrayMatrixUtils.InfinityNorm(vector);

			Assert.That(actual, Is.EqualTo(expected));
		}

		[Test]
		public void Test_GetDiagonal_NonSquareMatrix()
		{
			double[,] matrix = {
				{+1, -7},
				{-2, -3},
				{-2, -3}
			};

			double[] expected = { +1, -3 };

			double[] actual = ArrayMatrixUtils.GetDiagonal(matrix);

			Assert.That(actual, Is.EqualTo(expected));
		}

		[Test]
		public void Test_GetDiagonal_SquareMatrix()
		{
			double[,] matrix = {
				{+1, -7, +0},
				{-2, -3, -8},
				{-2, -3, +9}
			};

			double[] expected = { +1, -3, +9 };

			double[] actual = ArrayMatrixUtils.GetDiagonal(matrix);

			Assert.That(actual, Is.EqualTo(expected));
		}

		[Test]
		public void Test_PseudoInverseOfDiagonalMatrix_SquareMatrix_WithTolerance()
		{
			double[,] matrix = {
				{1.0, 0.0, 0.0},
				{0.0, 0.5, 0.0},
				{0.0, 0.0, 0.1},
			};
			double tolerance = 0.2;

			double[,] expected = {
				{1.0, 0.0, 0.0},
				{0.0, 2.0, 0.0},
				{0.0, 0.0, 0.0},
			};

			double[,] actual = ArrayMatrixUtils.PseudoInverseOfDiagonalMatrix(matrix, tolerance);

			Assert.That(actual, Is.EqualTo(expected));
		}

		[Test]
		public void Test_PseudoInverseOfDiagonalMatrix_RectangularMatrix1_WithTolerance()
		{
			double[,] matrix = {
				{1.0, 0.0, 0.0},
				{0.0, 0.5, 0.0},
			};
			double tolerance = 0.2;

			double[,] expected = {
				{1.0, 0.0, 0.0},
				{0.0, 2.0, 0.0},
			};

			double[,] actual = ArrayMatrixUtils.PseudoInverseOfDiagonalMatrix(matrix, tolerance);

			Assert.That(actual, Is.EqualTo(expected));
		}

		[Test]
		public void Test_PseudoInverseOfDiagonalMatrix_RectangularMatrix2_WithTolerance()
		{
			double[,] matrix = {
				{0.1, 0.0},
				{0.0, 5.0},
			};
			double tolerance = 0.2;

			double[,] expected = {
				{0.0, 0.0},
				{0.0, 0.2},
			};

			double[,] actual = ArrayMatrixUtils.PseudoInverseOfDiagonalMatrix(matrix, tolerance);

			Assert.That(actual, Is.EqualTo(expected));
		}
	}
}