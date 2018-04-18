using UnityEngine;
using Registration;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using System.Collections.Generic;
using System;

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
            EditorSceneManager.OpenScene(sceneName);

            //The 'default' child of a mesh contains the stuff we are interested in
            GameObject gameObject = GameObject.Find(gameObjectName).transform.GetChild(0).gameObject;
            Transform referenceTransform = gameObject.transform.root;

            SamplingInformation samplingInformation = new SamplingInformation(gameObject);

            NormalBinner binner = new NormalBinner(6, referenceTransform);
            Dictionary<int, List<Point>> actual = binner.Bin(samplingInformation);

            Assert.That(actual.Keys, Is.EquivalentTo(expected.Keys));
            for (int i = 0; i < actual.Count; i++) Assert.That(actual[i], Is.EquivalentTo(expected[i]));
        }

        #region cases
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
            new object[] {"transformedCube", new Dictionary<int, List<Point>>{
                    {0,
                        new List<Point>{
                            new Point(new Vector3(+6, -2, +3), new Vector3(+0, +0, +1)),
                            new Point(new Vector3(+6, -4, +3), new Vector3(+0, +0, +1)),
                            new Point(new Vector3(+4, -2, +3), new Vector3(+0, +0, +1)),
                            new Point(new Vector3(+4, -4, +3), new Vector3(+0, +0, +1)),
                        }
                    },
                    {1, new List<Point>{
                            new Point(new Vector3(+6, -2, +3), new Vector3(+1, +0, +0)),
                            new Point(new Vector3(+6, -2, +1), new Vector3(+1, +0, +0)),
                            new Point(new Vector3(+6, -4, +3), new Vector3(+1, +0, +0)),
                            new Point(new Vector3(+6, -4, +1), new Vector3(+1, +0, +0)),
                        }
                    },
                    {2, new List<Point>{
                            new Point(new Vector3(+6, -2, +3), new Vector3(+0, +1, +0)),
                            new Point(new Vector3(+6, -2, +1), new Vector3(+0, +1, +0)),
                            new Point(new Vector3(+4, -2, +3), new Vector3(+0, +1, +0)),
                            new Point(new Vector3(+4, -2, +1), new Vector3(+0, +1, +0)),
                        }
                    },
                    {3, new List<Point>{
                            new Point(new Vector3(+4, -2, +3), new Vector3(-1, +0, +0)),
                            new Point(new Vector3(+4, -2, +1), new Vector3(-1, +0, +0)),
                            new Point(new Vector3(+4, -4, +3), new Vector3(-1, +0, +0)),
                            new Point(new Vector3(+4, -4, +1), new Vector3(-1, +0, +0)),
                        }
                    },
                    {4, new List<Point>{
                            new Point(new Vector3(+6, -2, +1), new Vector3(+0, +0, -1)),
                            new Point(new Vector3(+6, -4, +1), new Vector3(+0, +0, -1)),
                            new Point(new Vector3(+4, -2, +1), new Vector3(+0, +0, -1)),
                            new Point(new Vector3(+4, -4, +1), new Vector3(+0, +0, -1)),
                        }
                    },
                    {5, new List<Point>{
                            new Point(new Vector3(+6, -4, +3), new Vector3(+0, -1, +0)),
                            new Point(new Vector3(+6, -4, +1), new Vector3(+0, -1, +0)),
                            new Point(new Vector3(+4, -4, +3), new Vector3(+0, -1, +0)),
                            new Point(new Vector3(+4, -4, +1), new Vector3(+0, -1, +0)),
                        }
                    },
                }
            },
            new object[] {"pyramid", new Dictionary<int, List<Point>>{
                    {0, new List<Point>{
                            new Point(new Vector3(+1, -1, +1), new Vector3(+0, +0.1644f, +0.9864f)),
                            new Point(new Vector3(-1, -1, +1), new Vector3(+0, +0.1644f, +0.9864f)),
                            new Point(new Vector3(+0, +5, +0), new Vector3(+0, +0.1644f, +0.9864f)),
                        }
                    },
                    {1, new List<Point>{
                            new Point(new Vector3(+1, -1, -1), new Vector3(+0.9864f, +0.1644f, +0)),
                            new Point(new Vector3(+1, -1, +1), new Vector3(+0.9864f, +0.1644f, +0)),
                            new Point(new Vector3(+0, +5, +0), new Vector3(+0.9864f, +0.1644f, +0)),
                        }
                    },
                    {2, new List<Point>{}
                    },
                    {3, new List<Point>{
                            new Point(new Vector3(-1, -1, -1), new Vector3(-0.9864f, +0.1644f, +0)),
                            new Point(new Vector3(-1, -1, +1), new Vector3(-0.9864f, +0.1644f, +0)),
                            new Point(new Vector3(+0, +5, +0), new Vector3(-0.9864f, +0.1644f, +0)),
                        }
                    },
                    {4,
                        new List<Point>{
                            new Point(new Vector3(+1, -1, -1), new Vector3(+0, +0.1644f, -0.9864f)),
                            new Point(new Vector3(-1, -1, -1), new Vector3(+0, +0.1644f, -0.9864f)),
                            new Point(new Vector3(+0, +5, +0), new Vector3(+0, +0.1644f, -0.9864f)),
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
        #endregion
    }

    [TestFixture]
    public class SamplingConfigurationTests
    {
        [TestCase(-005f)]
        [TestCase(+200f)]
        public void SetInvalidPercentage(float percentage)
        {
            Assert.Throws(
                typeof(ArgumentException),
                delegate
                {
                    new NormalDistributionOptimizationSubsampling.SamplingConfiguration(null, percentage);
                }
            );

            NormalDistributionOptimizationSubsampling.SamplingConfiguration x = new NormalDistributionOptimizationSubsampling.SamplingConfiguration(null, 10);
            Assert.Throws(
                typeof(ArgumentException),
                delegate
                {
                    x.Percentage = percentage;
                }
            );
        }

        [TestCase(+05f)]
        [TestCase(+20f)]
        public void SetValidPercentage(float percentage)
        {
            Assert.DoesNotThrow(
                delegate
                {
                    new NormalDistributionOptimizationSubsampling.SamplingConfiguration(null, percentage);
                }
            );

            NormalDistributionOptimizationSubsampling.SamplingConfiguration x = new NormalDistributionOptimizationSubsampling.SamplingConfiguration(null, 10);
            Assert.DoesNotThrow(
                delegate
                {
                    x.Percentage = percentage;
                }
            );
        }
    }
}