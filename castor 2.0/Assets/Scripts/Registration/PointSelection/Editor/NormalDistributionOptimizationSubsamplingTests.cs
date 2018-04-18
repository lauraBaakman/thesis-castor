using UnityEngine;
using Registration;
using NUnit;
using NUnit.Framework;

namespace Tests.Registration
{
    [TestFixture]
    public class UniformPolyhedronTests
    {
        [Test]
        public void Test_UniformPolyhedron_Normals()
        {
            Vector3[] expected = {
                new Vector3(+0, +0, +1),
                new Vector3(+0, +0, -1),
                new Vector3(+0, +1, +0),
                new Vector3(+0, -1, +0),
                new Vector3(+1, +0, +0),
                new Vector3(-1, +0, +0),
            };

            Vector3[] actual = new TestCube().normals;

            Assert.That(actual, Is.EquivalentTo(expected));
        }

        [Test]
        public void Test_FaceCount()
        {
            int expected = 6;

            NormalBinner.UniformPolyhedron polyhedron = new TestCube();
            int actual = polyhedron.NumberOfFaces;

            Assert.AreEqual(expected, actual);
        }

        [Test, TestCaseSource("DetermineFaceIdxCases")]
        public void Test_DetermineFaceIdx(Vector3 vector, int expected)
        {
            NormalBinner.UniformPolyhedron polyhedron = new TestCube();
            int actual = polyhedron.DetermineFaceIdx(vector);

            Assert.AreEqual(expected, actual);
        }

        static object[] DetermineFaceIdxCases = {
            new object[] {
                new Vector3(+0, +0, -1), 4
            },
            new object[] {
                new Vector3(+0, +0, +1), 0
            },
            new object[] {
                new Vector3(+0, +1, +0), 2
            },
            new object[] {
                new Vector3(+0, -1, +0), 5
            },
            new object[] {
                new Vector3(-1, +0, +0), 3
            },
            new object[] {
                new Vector3(+1, +0, +0), 1
            },
            new object[] {
                new Vector3(0.5f, 0.5f, 0.5f).normalized, 0
            },
            new object[] {
                new Vector3(0.9f, 0.1f, 0.2f).normalized, 1
            },
        };
    }

    internal class TestCube : NormalBinner.UniformPolyhedron
    {
        //Mathematica: PolyhedronData["Cube", "Faces", "Polygon"]
        private static readonly Vector3[][] faces = {
               new Vector3[] { //normal: 0, 0, +1
                        new Vector3(+0.5f, +0.5f, +0.5f),
                        new Vector3(-0.5f, +0.5f, +0.5f),
                        new Vector3(-0.5f, -0.5f, +0.5f),
                        new Vector3(+0.5f, -0.5f, +0.5f)
            }, new Vector3[] { //normal: +1, 0, 0
                        new Vector3(+0.5f, +0.5f, +0.5f),
                        new Vector3(+0.5f, -0.5f, +0.5f),
                        new Vector3(+0.5f, -0.5f, -0.5f),
                        new Vector3(+0.5f, +0.5f, -0.5f)
            }, new Vector3[] { //normal: 0, +1, 0
                        new Vector3(+0.5f, +0.5f, +0.5f),
                        new Vector3(+0.5f, +0.5f, -0.5f),
                        new Vector3(-0.5f, +0.5f, -0.5f),
                        new Vector3(-0.5f, +0.5f, +0.5f)
            }, new Vector3[] {//normal: -1, 0, 0
                        new Vector3(-0.5f, +0.5f, +0.5f),
                        new Vector3(-0.5f, +0.5f, -0.5f),
                        new Vector3(-0.5f, -0.5f, -0.5f),
                        new Vector3(-0.5f, -0.5f, +0.5f)
            }, new Vector3[] { //normal: 0, 0, -1
                        new Vector3(-0.5f, -0.5f, -0.5f),
                        new Vector3(-0.5f, +0.5f, -0.5f),
                        new Vector3(+0.5f, +0.5f, -0.5f),
                        new Vector3(+0.5f, -0.5f, -0.5f)
            }, new Vector3[] { //normal: 0, -1, 0
                        new Vector3(-0.5f, -0.5f, +0.5f),
                        new Vector3(-0.5f, -0.5f, -0.5f),
                        new Vector3(+0.5f, -0.5f, -0.5f),
                        new Vector3(+0.5f, -0.5f, +0.5f)
            }
        };

        public TestCube()
            : base(faces)
        { }
    }

    [TestFixture]
    public class NormalBinnerTest
    {
        [Test]
        public void Test_Constructor_With_Invalid_Bin_Count()
        {
            Assert.Throws(typeof(System.ArgumentException), new TestDelegate(Test_Constructor_With_Invalid_Bin_Count_Helper));
        }

        public void Test_Constructor_With_Invalid_Bin_Count_Helper()
        {
            int numBins = 7;
            NormalBinner binner = new NormalBinner(numBins, null);
        }

        [Test]
        public void Test_Bin_Cube()
        {
            Assert.Fail("Not implemented, use TestIntersection form");
        }

        [Test]
        public void Test_Bin_Pyramid()
        {
            Assert.Fail("Not implemented, use TestIntersection form");
        }
    }
}