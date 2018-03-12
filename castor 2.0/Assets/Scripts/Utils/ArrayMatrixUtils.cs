namespace Utils
{
    public static class ArrayMatrixUtils
    {
        private static readonly double EPSILON = 10 * float.Epsilon;

        public static double[,] ToDiagonalMatrix(double[] diagonal)
        {
            return ToDiagonalMatrix(diagonal, diagonal.Length, diagonal.Length);
        }

        public static double[,] ToDiagonalMatrix(double[] diagonal, int numRows, int numCols)
        {
            if (diagonal.Length > numRows) throw new System.ArgumentException("Cannot fit " + diagonal.Length + "elements in a matrix with " + numRows + "rows.");
            if (diagonal.Length > numCols) throw new System.ArgumentException("Cannot fit " + diagonal.Length + "elements in a matrix with " + numCols + "columns.");

            double[,] matrix = new double[numRows, numCols];

            for (int i = 0; i < diagonal.Length; i++) matrix[i, i] = diagonal[i];

            return matrix;
        }

        public static double[,] Transpose(double[,] matrix)
        {
            double[,] transpose = new double[matrix.GetLength(0), matrix.GetLength(1)];

            return transpose;
        }

        public static double[,] Multiply(double[,] lhs, double[,] rhs)
        {
            double[,] result = new double[lhs.GetLength(0), rhs.GetLength(1)];

            return result;
        }

        public static double[] Multiply(double[,] lhs, double[] rhs)
        {
            double[] result = new double[lhs.GetLength(0)];

            return result;
        }

        public static double[,] MoorePenroseInverse(double[,] matrix)
        {
            double[,] inverse = new double[matrix.GetLength(0), matrix.GetLength(1)];

            return inverse;
        }

        /// <summary>
        /// Compute the singular value decomposition of A: A = U * S * V^T
        /// </summary>
        public static void SVD(double[,] A, out double[,] U, out double[,] S, out double[,] Vt)
        {
            // numUnknowns x 1 matrix
            double[] singularValues = new double[A.GetLength(1)];

            // Correspondences.Count x Correspondences.Count matrix
            U = new double[A.GetLength(0), A.GetLength(0)];

            // numUnknowns x numUnknowns matrix
            Vt = new double[A.GetLength(1), A.GetLength(1)];

            bool succes = alglib.rmatrixsvd(
                A, A.GetLength(0), A.GetLength(1),
                uneeded: 2, vtneeded: 2, additionalmemory: 2,
                w: out singularValues, vt: out Vt, u: out U
            );

            S = ToDiagonalMatrix(singularValues);
        }
    }
}