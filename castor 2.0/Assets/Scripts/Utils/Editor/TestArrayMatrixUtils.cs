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
    }
}