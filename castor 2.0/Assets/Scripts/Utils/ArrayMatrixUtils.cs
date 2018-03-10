namespace Utils
{
    public static class ArrayMatrixUtils
    {
        public static double[,] ToDiagonalMatrix(double[] diagonal)
        {
            int rank = diagonal.Length;

            double[,] matrix = new double[rank, rank];

            for (int i = 0; i < rank; i++) matrix[i, i] = diagonal[i];

            return matrix;
        }
    }
}