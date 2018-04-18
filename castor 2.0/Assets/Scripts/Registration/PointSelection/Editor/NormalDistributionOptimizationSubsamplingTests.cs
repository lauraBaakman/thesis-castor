using UnityEngine;
using Registration;
using NUnit;
using NUnit.Framework;
using System.Collections.Generic;

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
            new object[] {new Vector3(+0, +0, -1), 4},
            new object[] {new Vector3(+0, +0, +1), 0},
            new object[] {new Vector3(+0, +1, +0), 2},
            new object[] {new Vector3(+0, -1, +0), 5},
            new object[] {new Vector3(-1, +0, +0), 3},
            new object[] {new Vector3(+1, +0, +0), 1},
            new object[] {new Vector3(0.5f, 0.5f, 0.5f).normalized, 0},
            new object[] {new Vector3(0.9f, 0.1f, 0.2f).normalized, 1},
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
        private static string sceneName = "Assets/Scenes/TestSceneNormalBinner.unity";

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

        [Test, TestCaseSource("BinCases")]
        public void Test_Bin(string gameObjectName, Dictionary<int, List<Point>> expected)
        {
            GameObject gameObject = GameObject.Find(gameObjectName);
            Transform referenceTransform = gameObject.transform.parent;

            SamplingInformation samplingInformation = new SamplingInformation(gameObject);

            NormalBinner binner = new NormalBinner(6, referenceTransform);
            Dictionary<int, List<Point>> actual = binner.Bin(samplingInformation);

            Assert.That(actual, Is.EquivalentTo(expected));

            Assert.Fail("Not implemented, use TestIntersection form");
        }

        //new object[] {new Vector3(+0, -1, +0), 5},

        static object[] BinCases = {
            new object[] {"cube", new Dictionary<int, List<Point>>{
                    {0,
                        new List<Point>{
                            new Point(new Vector3(+1, +1, +1), new Vector3(+0, +0, +1)),
                            new Point(new Vector3(+1, -1, +1), new Vector3(+0, +0, +1)),
                            new Point(new Vector3(-1, +1, +1), new Vector3(+0, +0, +1)),
                            new Point(new Vector3(-1, -1, +1), new Vector3(+0, +0, +1)),
                        }
                    },
                    {1, new List<Point>{
                            new Point(new Vector3(+1, +1, +1), new Vector3(+1, +0, +0)),
                            new Point(new Vector3(+1, +1, -1), new Vector3(+1, +0, +0)),
                            new Point(new Vector3(+1, -1, +1), new Vector3(+1, +0, +0)),
                            new Point(new Vector3(+1, -1, -1), new Vector3(+1, +0, +0)),
                        }
                    },
                    {2, new List<Point>{
                            new Point(new Vector3(+1, +1, +1), new Vector3(+0, +1, +0)),
                            new Point(new Vector3(+1, +1, -1), new Vector3(+0, +1, +0)),
                            new Point(new Vector3(-1, +1, +1), new Vector3(+0, +1, +0)),
                            new Point(new Vector3(-1, +1, -1), new Vector3(+0, +1, +0)),
                        }
                    },
                    {3, new List<Point>{
                            new Point(new Vector3(-1, +1, +1), new Vector3(-1, +0, +0)),
                            new Point(new Vector3(-1, +1, -1), new Vector3(-1, +0, +0)),
                            new Point(new Vector3(-1, -1, +1), new Vector3(-1, +0, +0)),
                            new Point(new Vector3(-1, -1, -1), new Vector3(-1, +0, +0)),
                        }
                    },
                    {4, new List<Point>{
                            new Point(new Vector3(+1, +1, -1), new Vector3(+0, +0, -1)),
                            new Point(new Vector3(+1, -1, -1), new Vector3(+0, +0, -1)),
                            new Point(new Vector3(-1, +1, -1), new Vector3(+0, +0, -1)),
                            new Point(new Vector3(-1, -1, -1), new Vector3(+0, +0, -1)),
                        }
                    },
                    {5, new List<Point>{
                            new Point(new Vector3(+1, -1, +1), new Vector3(+0, -1, +0)),
                            new Point(new Vector3(+1, -1, -1), new Vector3(+0, -1, +0)),
                            new Point(new Vector3(-1, -1, +1), new Vector3(+0, -1, +0)),
                            new Point(new Vector3(-1, -1, -1), new Vector3(+0, -1, +0)),
                        }
                    },
                }
            },
            new object[] {"pyramid", new Dictionary<int, List<Point>>{
                    {0,
                        new List<Point>{
                            new Point(new Vector3(+1, -1, -1), new Vector3(+0, +0.1644, -0.9864)),
                            new Point(new Vector3(-1, -1, -1), new Vector3(+0, +0.1644, -0.9864)),
                            new Point(new Vector3(+0, +5, +0), new Vector3(+0, +0.1644, -0.9864)),
                        }
                    },
                    {1, new List<Point>{
                            new Point(new Vector3(+1, -1, -1), new Vector3(+0.9864, +0.1644, +0)),
                            new Point(new Vector3(+1, -1, +1), new Vector3(+0.9864, +0.1644, +0)),
                            new Point(new Vector3(+0, +5, +0), new Vector3(+0.9864, +0.1644, +0)),
                        }
                    },
                    {2, new List<Point>{}
                    },
                    {3, new List<Point>{
                            new Point(new Vector3(-1, -1, -1), new Vector3(-0.9864, +0.1644, +0)),
                            new Point(new Vector3(-1, -1, +1), new Vector3(-0.9864, +0.1644, +0)),
                            new Point(new Vector3(+0, +5, +0), new Vector3(-0.9864, +0.1644, +0)),
                        }
                    },
                    {4, new List<Point>{
                            new Point(new Vector3(+1, -1, +1), new Vector3(+0, +0.1644, +0.9864)),
                            new Point(new Vector3(-1, -1, +1), new Vector3(+0, +0.1644, +0.9864)),
                            new Point(new Vector3(+0, +5, +0), new Vector3(+0, +0.1644, +0.9864)),
                        }
                    },
                    {5, new List<Point>{
                            new Point(new Vector3(-1, -1, -1), new Vector3(+0, -1, +0)),
                            new Point(new Vector3(-1, -1, +1), new Vector3(+0, -1, +0)),
                            new Point(new Vector3(+1, -1, -1), new Vector3(+0, -1, +0)),
                            new Point(new Vector3(+1, -1, +1), new Vector3(+0, -1, +0)),
                        }
                    },
                }
            },
        };
    }
}