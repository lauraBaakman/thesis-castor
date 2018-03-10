namespace Utils
{
    public static class ArrayMatrixUtils
    {
        private static readonly double EPSILON = 10 * float.Epsilon;

        public static double[,] ToDiagonalMatrix(double[] diagonal)
        {
            int rank = diagonal.Length;

            double[,] matrix = new double[rank, rank];

            for (int i = 0; i < rank; i++) matrix[i, i] = diagonal[i];

            return matrix;
        }

        /// <summary>
        /// Computes the pseudo inverse by taking the inverse of the non-zero 
        /// elements and leaving the zero elements unchanged.
        /// </summary>
        /// <returns>The inverse.</returns>
        public static double[,] PseudoInverseOfDiagonalMatrix(double[,] matrix)
        {
            if (matrix.GetLength(0) != matrix.GetLength(1)) throw new System.ArgumentException("Input matrix needs to be square.");

            double[,] pseudoInverse = new double[matrix.GetLength(0), matrix.GetLength(1)];

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                if (System.Math.Abs(matrix[i, i]) > EPSILON)
                {
                    pseudoInverse[i, i] = 1.0f / matrix[i, i];
                }
            }
            return pseudoInverse;
        }

        public static double[,] Transpose(double[,] matrix)
        {

        }

        public static double[,] Multiply(double[,] lhs, double[,] rhs)
        {

        }
    }
}