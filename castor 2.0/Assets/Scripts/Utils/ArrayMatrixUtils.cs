using System;
namespace Utils
{
    public static class ArrayMatrixUtils
    {
        /// <summary>
        /// Represent a column vector with N elements as a N x 1 matrix.
        /// </summary>
        /// <returns>The matrix representing the vector.</returns>
        /// <param name="vector">Vector.</param>
        public static double[,] ColumnVectorToMatrix(double[] vector)
        {
            int numRows = vector.Length;

            double[,] matrix = new double[numRows, 1];

            for (int i = 0; i < numRows; i++) matrix[i, 0] = vector[i];

            return matrix;
        }

        /// <summary>
        /// Represent a row vector with N elements as a 1 x N matrix.
        /// </summary>
        /// <returns>The matrix representing the vector.</returns>
        /// <param name="vector">Vector.</param>
        public static double[,] RowVectorToMatrix(double[] vector)
        {
            int numCols = vector.Length;

            double[,] matrix = new double[1, numCols];

            for (int i = 0; i < numCols; i++) matrix[0, i] = vector[i];

            return matrix;
        }

        /// <summary>
        /// Represent a 1 X N or N x 1 matrix as a vector.
        /// </summary>
        /// <returns>The vector.</returns>
        /// <param name="matrix">Matrix.</param>
        public static double[] ToVector(double[,] matrix)
        {
            int numRows = matrix.GetLength(0);
            int numCols = matrix.GetLength(1);

            if ((numRows != 1) && (numCols != 1)) throw new System.ArgumentException("One of the dimension of the matrix needs to be zero.");

            double[] vector = new double[Math.Max(numRows, numCols)];

            int idx = 0;
            foreach (double value in matrix)
            {
                vector[idx++] = value;
            }

            return vector;
        }

        /// <summary>
        /// Generate a diagonal matrix from a vector with the values for the diagonal.
        /// </summary>
        /// <returns>The diagonal matrix.</returns>
        /// <param name="diagonal">Diagonal.</param>
        public static double[,] ToDiagonalMatrix(double[] diagonal)
        {
            return ToDiagonalMatrix(diagonal, diagonal.Length, diagonal.Length);
        }

        /// <summary>
        /// Generate a N X M diagonal matrix from a vector with at least MAX(N,M) values for the diagonal.
        /// </summary>
        /// <returns>The diagonal matrix.</returns>
        /// <param name="diagonal">Values to be placed on the diagonal of the matrix</param>
        /// <param name="numRows">Number rows of the output matrix.</param>
        /// <param name="numCols">Number cols of the output matrix.</param>
        public static double[,] ToDiagonalMatrix(double[] diagonal, int numRows, int numCols)
        {
            if (diagonal.Length > numRows) throw new System.ArgumentException("Cannot fit " + diagonal.Length + "elements in a matrix with " + numRows + "rows.");
            if (diagonal.Length > numCols) throw new System.ArgumentException("Cannot fit " + diagonal.Length + "elements in a matrix with " + numCols + "columns.");

            double[,] matrix = new double[numRows, numCols];

            for (int i = 0; i < diagonal.Length; i++) matrix[i, i] = diagonal[i];

            return matrix;
        }

        /// <summary>
        /// Compute the transpose of the input matrix.
        /// </summary>
        /// <returns>The transpose.</returns>
        /// <param name="matrix">Matrix.</param>
        public static double[,] Transpose(double[,] matrix)
        {
            int sourceNumRows = matrix.GetLength(0);
            int sourceNumCols = matrix.GetLength(1);

            double[,] transpose = new double[sourceNumCols, sourceNumRows];

            alglib.rmatrixtranspose(
                sourceNumRows, sourceNumCols, matrix, 0, 0,
                ref transpose, 0, 0
            );

            return transpose;
        }

        /// <summary>
        /// Multiply the specified lhs and rhs.
        /// </summary>
        /// <returns>The result of the multiplication.</returns>
        /// <param name="lhs">Lhs.</param>
        /// <param name="rhs">Rhs.</param>
        public static double[,] Multiply(double[,] lhs, double[,] rhs)
        {
            int lhs_num_rows = lhs.GetLength(0); int rhs_num_rows = rhs.GetLength(0);
            int lhs_num_cols = lhs.GetLength(1); int rhs_num_cols = rhs.GetLength(1);

            if (lhs_num_cols != rhs_num_rows) throw new System.ArgumentException("Incompatible matrix dimensions");

            double[,] result = new double[lhs_num_rows, rhs_num_cols];

            //C = alpha * optypea(A) * optypeb(B) + beta * C
            // A: M X K matrix
            // B: K X N matrix
            alglib.rmatrixgemm(
                m: lhs_num_rows, n: rhs_num_cols, k: lhs_num_cols,
                alpha: 1.0d, a: lhs, ia: 0, ja: 0, optypea: 0,
                b: rhs, ib: 0, jb: 0, optypeb: 0,
                beta: 1.0d, c: ref result, ic: 0, jc: 0
            );
            return result;
        }


        /// <summary>
        /// Multiply the specified lhs and rhs.
        /// </summary>
        /// <returns>The result of the multiplication.</returns>
        /// <param name="lhs">Lhs.</param>
        /// <param name="rhs">Rhs.</param>
        public static double[] Multiply(double[,] lhs, double[] rhs)
        {
            double[,] rhs_as_matrix = ColumnVectorToMatrix(rhs);

            double[,] result_as_matrix = Multiply(lhs, rhs_as_matrix);

            return ToVector(result_as_matrix);
        }

        /// <summary>
        /// Compute the Moore-Penrose inverse of the matrix.
        /// </summary>
        /// <returns>The moore-penrose inverse.</returns>
        /// <param name="A">The matrix to invert.</param>
        public static double[,] MoorePenroseInverse(double[,] A)
        {
            /// A = U * S * V'
            double[,] U, S, Vt;
            SVD(A, out U, out S, out Vt);

            /// Compute the pseudo inverse of S
            double[,] Splus = PseudoInverseOfDiagonalMatrix(S);

            /// A+ = V * (S+)' * U'
            double[,] V = Transpose(Vt);
            double[,] Ut = Transpose(U);
            double[,] SplusT = Transpose(Splus);

            double[,] inverse = Multiply(Multiply(V, SplusT), Ut);
            return inverse;
        }

        /// <summary>
        /// Compute the infinity norm, the maximum absolute row sum, of the input matrix.
        /// </summary>
        /// <returns>The norm.</returns>
        /// <param name="matrix">Matrix.</param>
        public static double InfinityNorm(double[,] matrix)
        {
            int numRows = matrix.GetLength(0);
            double max = double.MinValue;

            for (int rowIdx = 0; rowIdx < numRows; rowIdx++)
                max = Math.Max(max, AbsRowSum(matrix, rowIdx));

            return max;
        }

        private static double AbsRowSum(double[,] matrix, int rowIdx)
        {
            int numCols = matrix.GetLength(1);
            double sum = 0;

            for (int colIdx = 0; colIdx < numCols; colIdx++)
                sum += Math.Abs(matrix[rowIdx, colIdx]);

            return sum;
        }

        /// <summary>
        /// returns the largest element of the absolute values of the vector.
        /// </summary>
        /// <returns>The norm.</returns>
        /// <param name="vector">Vector.</param>
        public static double InfinityNorm(double[] vector)
        {
            double norm = double.MinValue;

            for (int i = 0; i < vector.Length; i++) norm = Math.Max(Math.Abs(vector[i]), norm);

            return norm;
        }

        /// <summary>
        /// Compute the singular value decomposition of A: A = U * S * V^T
        /// </summary>
        public static void SVD(double[,] A, out double[,] U, out double[,] S, out double[,] Vt)
        {
            // numRowsA vector
            double[] singularValues = new double[A.GetLength(0)];

            // numRowsA x numRowsA matrix
            U = new double[A.GetLength(0), A.GetLength(0)];

            // numColsA x numColsA matrix
            Vt = new double[A.GetLength(1), A.GetLength(1)];

            bool succes = alglib.rmatrixsvd(
                A, A.GetLength(0), A.GetLength(1),
                uneeded: 2, vtneeded: 2, additionalmemory: 2,
                w: out singularValues, vt: out Vt, u: out U
            );

            S = ToDiagonalMatrix(singularValues, A.GetLength(0), A.GetLength(1));
        }

        /// <summary>
        /// Computes the pseudo inverse by taking the inverse of the non-zero 
        /// elements and leaving the zero elements unchanged.
        /// </summary>
        /// <returns>The inverse.</returns>
        public static double[,] PseudoInverseOfDiagonalMatrix(double[,] a, double tolerance)
        {
            int num_rows = a.GetLength(0);
            int num_cols = a.GetLength(1);

            double[,] inverse = new double[num_rows, num_cols];

            int maxIdx = Math.Min(num_rows, num_cols);

            for (int i = 0; i < maxIdx; i++)
                inverse[i, i] = (Math.Abs(a[i, i]) > tolerance) ? (1.0 / a[i, i]) : 0.0;

            return inverse;
        }

        /// <summary>
        /// Computes the pseudo inverse by taking the inverse of the non-zero 
        /// elements and leaving the zero elements unchanged.
        /// </summary>
        /// <returns>The inverse.</returns>
        public static double[,] PseudoInverseOfDiagonalMatrix(double[,] matrix)
        {
            double tolerance = ComputeTolerance(matrix);
            return PseudoInverseOfDiagonalMatrix(matrix, tolerance);
        }

        //Temporarily public
        public static double ComputeTolerance(double[,] matrix)
        {
            ///Tolerance used by matlab
            int maxSize = Math.Max(matrix.GetLength(0), matrix.GetLength(1));
            double normA = InfinityNorm(GetDiagonal(matrix));

            return maxSize * normA * double.Epsilon;
        }

        /// <summary>
        /// Gets the main diagonal of the input matrix.
        /// </summary>
        /// <returns>The diagonal.</returns>
        /// <param name="matrix">Matrix.</param>
        public static double[] GetDiagonal(double[,] matrix)
        {
            int numRows = matrix.GetLength(0);
            int numCols = matrix.GetLength(1);

            int maxIdx = Math.Min(numRows, numCols);

            double[] diagonal = new double[maxIdx];
            for (int i = 0; i < maxIdx; i++) diagonal[i] = matrix[i, i];

            return diagonal;
        }
    }
}