using NUnit.Framework;
using UnityEngine;
using Utils;

namespace Tests.Utils
{
    [TestFixture]
    public class NumericalMathTests
    {
        [Test]
        public void NewellsMethodTest_OneVector()
        {
            Vector3 a = Auxilaries.RandomPosition();

            Assert.Throws(
                typeof(System.ArgumentException),
                delegate
                {
                    NumericalMath.NewellsMethod(new Vector3[] { a });
                }
            );
        }

        [Test]
        public void NewellsMethodTest_TwoVectors()
        {
            Vector3 a = Auxilaries.RandomPosition();
            Vector3 b = Auxilaries.RandomPosition();

            Assert.Throws(
                typeof(System.ArgumentException),
                delegate
                {
                    NumericalMath.NewellsMethod(new Vector3[] { a, b });
                }
            );
        }

        [Test]
        public void NewellsMethodTest_ThreePlanarVectors()
        {
            Vector3 a = new Vector3(1, 2, 3);
            Vector3 b = new Vector3(2, 4, 3);
            Vector3 c = new Vector3(3, 9, 3);

            Vector3 expected = new Vector3(0, 0, 1);
            Vector3 actual1 = NumericalMath.NewellsMethod(a, b, c);
            Vector3 actual2 = NumericalMath.NewellsMethod(new Vector3[] { a, b, c });

            Assert.AreEqual(expected, actual1);
            Assert.AreEqual(expected, actual2);
        }

        [Test]
        public void NewellsMethodTest_ThreeVectors()
        {
            Vector3 a = new Vector3(1, +2, 3);
            Vector3 b = new Vector3(3, +5, 9);
            Vector3 c = new Vector3(2, -2, 5);

            Vector3 expected = new Vector3(0.937042571331636f, 0.062469504755442f, -0.343582276154933f);

            Vector3 actual1 = NumericalMath.NewellsMethod(a, b, c);
            Vector3 actual2 = NumericalMath.NewellsMethod(new Vector3[] { a, b, c });

            Assert.AreEqual(expected, actual1);
            Assert.AreEqual(expected, actual2);
        }

        [Test]
        public void NewellsMethodTest_FiveVectors()
        {
            Vector3 a = new Vector3(1, +2, 3);
            Vector3 b = new Vector3(3, +5, 9);
            Vector3 c = new Vector3(2, -2, 5);
            Vector3 d = new Vector3(3, -4, 6);
            Vector3 e = new Vector3(3, -3, 7);

            Vector3 expected = new Vector3(+0.947716733053701f, +0.045129368240652f, -0.315905577684567f);

            Vector3 actual = NumericalMath.NewellsMethod(new Vector3[] { a, b, c, d, e });
            Assert.AreEqual(expected, actual);
        }
    }
}