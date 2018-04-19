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
    public class NDOSubSamplingConfigurationTests
    {
        private static float validPercentage = 50.0f;
        private static int validBinCount = 6;
        private static Transform validTransform = null;

        [TestCase(-005f)]
        [TestCase(+200f)]
        public void SetInvalidPercentageConstructor(float percentage)
        {
            Assert.Throws<ArgumentException>(
                delegate
                {
                    NDOSubsampling.Configuration y = new NDOSubsampling.Configuration(validTransform, percentage, validBinCount);
                }
            );
        }

        [TestCase(-005f)]
        [TestCase(+200f)]
        public void SetInvalidPercentageSetter(float percentage)
        {
            NDOSubsampling.Configuration config = new NDOSubsampling.Configuration(validTransform, validPercentage, validBinCount);
            Assert.Throws<ArgumentException>(
                delegate
                {
                    config.Percentage = percentage;
                }
            );
        }

        [TestCase(0)]
        [TestCase(5)]
        [TestCase(20)]
        [TestCase(100)]
        public void SetValidPercentageConstructor(float percentage)
        {
            Assert.DoesNotThrow(
                delegate
                {
                    NDOSubsampling.Configuration config = new NDOSubsampling.Configuration(validTransform, percentage, validBinCount);
                }
            );
        }

        [TestCase(0)]
        [TestCase(5)]
        [TestCase(20)]
        [TestCase(100)]
        public void SetValidPercentageSetter(float percentage)
        {
            NDOSubsampling.Configuration config = new NDOSubsampling.Configuration(validTransform, validPercentage, validBinCount);
            Assert.DoesNotThrow(
                delegate
                {
                    config.Percentage = percentage;
                }
            );
        }

        [TestCase(0)]
        [TestCase(200)]
        [TestCase(3)]
        public void SetInvalidBinCountConstructor(int binCount)
        {
            Assert.Throws(
                typeof(ArgumentException),
                delegate
                {
                    NDOSubsampling.Configuration config = new NDOSubsampling.Configuration(validTransform, validPercentage, binCount);
                }
            );
        }

        [TestCase(0)]
        [TestCase(200)]
        [TestCase(3)]
        public void SetInvalidBinCountSetter(int binCount)
        {
            NDOSubsampling.Configuration config = new NDOSubsampling.Configuration(validTransform, validPercentage, validBinCount);
            Assert.Throws(
                typeof(ArgumentException),
                delegate
                {
                    config.BinCount = binCount;
                }
            );
        }

        [TestCase(4)]
        [TestCase(6)]
        [TestCase(8)]
        [TestCase(12)]
        [TestCase(20)]
        public void SetValidBinCountConstructor(int binCount)
        {
            Assert.DoesNotThrow(
                delegate
                {
                    object config = new NDOSubsampling.Configuration(validTransform, validPercentage, binCount);
                }
            );
        }

        [TestCase(4)]
        [TestCase(6)]
        [TestCase(8)]
        [TestCase(12)]
        [TestCase(20)]
        public void SetValidBinCountSetter(int binCount)
        {
            NDOSubsampling.Configuration config = new NDOSubsampling.Configuration(validTransform, validPercentage, validBinCount);
            Assert.DoesNotThrow(
                delegate
                {
                    config.BinCount = binCount;
                }
            );
        }

        [TestCase(0, 0)]
        [TestCase(20, 0.2f)]
        [TestCase(50, 0.5f)]
        [TestCase(45.5f, 0.455f)]
        [TestCase(100, 1)]
        public void ValidProbability_WithConstructor(float percentage, float expected)
        {
            NDOSubsampling.Configuration config = new NDOSubsampling.Configuration(validTransform, percentage, validBinCount);

            float actual = config.Probability;
            Assert.AreEqual(expected, actual);
        }

        [TestCase(0, 0)]
        [TestCase(20, 0.2f)]
        [TestCase(50, 0.5f)]
        [TestCase(45.5f, 0.455f)]
        [TestCase(100, 1)]
        public void ValidProbability_WithSetter(float percentage, float expected)
        {
            NDOSubsampling.Configuration config = new NDOSubsampling.Configuration(validTransform, validPercentage, validBinCount);
            config.Percentage = percentage;

            float actual = config.Probability;
            Assert.AreEqual(expected, actual);
        }
    }

    [TestFixture]
    public class NDOSubsamplingTests
    {
        private static string sceneName = "Assets/Scenes/TestSceneNormalBinner.unity";

        [TestCase("cube")]
        [TestCase("pyramid")]
        [TestCase("transformedCube")]
        public void TestSample_100(string gameObjectName)
        {
            EditorSceneManager.OpenScene(sceneName);

            //The 'default' child of a mesh contains the stuff we are interested in
            GameObject gameObject = GameObject.Find(gameObjectName).transform.GetChild(0).gameObject;

            SamplingInformation samplingInformation = new SamplingInformation(gameObject);

            NDOSubsampling.Configuration ndosConfig = new NDOSubsampling.Configuration(
                referenceTransform: gameObject.transform.root,
                percentage: 100,
                binCount: 6
            );

            AllPointsSampler.Configuration allPointsConfig = new AllPointsSampler.Configuration(
                referenceTransform: gameObject.transform.root,
                normalProcessing: AllPointsSampler.Configuration.NormalProcessing.VertexNormals
            );

            SamplingInformation samplingInfo = new SamplingInformation(gameObject);

            List<Point> expected = new AllPointsSampler(allPointsConfig).Sample(samplingInfo);
            List<Point> actual = new NDOSubsampling(ndosConfig).Sample(samplingInfo);

            Assert.That(actual, Is.EquivalentTo(expected));
        }

        [TestCase("cube")]
        [TestCase("pyramid")]
        [TestCase("transformedCube")]
        public void TestSample_0(string gameObjectName)
        {
            EditorSceneManager.OpenScene(sceneName);

            //The 'default' child of a mesh contains the stuff we are interested in
            GameObject gameObject = GameObject.Find(gameObjectName).transform.GetChild(0).gameObject;

            SamplingInformation samplingInformation = new SamplingInformation(gameObject);

            NDOSubsampling.Configuration ndosConfig = new NDOSubsampling.Configuration(
                referenceTransform: gameObject.transform.root,
                percentage: 100,
                binCount: 6
            );

            SamplingInformation samplingInfo = new SamplingInformation(gameObject);

            List<Point> expected = new List<Point>();
            List<Point> actual = new NDOSubsampling(ndosConfig).Sample(samplingInfo);

            Assert.That(actual, Is.EquivalentTo(expected));
        }

        [Test]
        public void TestSample_Cube_50()
        {
            EditorSceneManager.OpenScene(sceneName);

            //The 'default' child of a mesh contains the stuff we are interested in
            GameObject gameObject = GameObject.Find("cube").transform.GetChild(0).gameObject;

            SamplingInformation samplingInformation = new SamplingInformation(gameObject);

            NDOSubsampling.Configuration ndosConfig = new NDOSubsampling.Configuration(
                referenceTransform: gameObject.transform.root,
                percentage: 50,
                binCount: 6
            );

            SamplingInformation samplingInfo = new SamplingInformation(gameObject);

            List<Point> actual = new NDOSubsampling(ndosConfig).Sample(samplingInfo);


            NormalBinner binner = new NormalBinner(ndosConfig.BinCount, ndosConfig.referenceTransform);
            Dictionary<int, List<Point>> bins = binner.Bin(samplingInformation);

            for (int i = 0; i < bins.Count; i++)
            {
                Assert.That(CountPointsSampledFromBin(actual, bins[i]), Is.EqualTo(2));
            }
        }

        [Test]
        public void TestSample_Pyramid_45(string gameObjectName, int expectedSampledBinSize)
        {
            EditorSceneManager.OpenScene(sceneName);

            //The 'default' child of a mesh contains the stuff we are interested in
            GameObject gameObject = GameObject.Find(gameObjectName).transform.GetChild(0).gameObject;

            SamplingInformation samplingInformation = new SamplingInformation(gameObject);

            NDOSubsampling.Configuration ndosConfig = new NDOSubsampling.Configuration(
                referenceTransform: gameObject.transform.root,
                percentage: 0.45f,
                binCount: 6
            );

            SamplingInformation samplingInfo = new SamplingInformation(gameObject);

            List<Point> actual = new NDOSubsampling(ndosConfig).Sample(samplingInfo);


            NormalBinner binner = new NormalBinner(ndosConfig.BinCount, ndosConfig.referenceTransform);
            Dictionary<int, List<Point>> bins = binner.Bin(samplingInformation);

            Assert.That(CountPointsSampledFromBin(actual, bins[0]), Is.EqualTo(1));
            Assert.That(CountPointsSampledFromBin(actual, bins[1]), Is.EqualTo(1));
            Assert.That(CountPointsSampledFromBin(actual, bins[2]), Is.EqualTo(0));
            Assert.That(CountPointsSampledFromBin(actual, bins[3]), Is.EqualTo(1));
            Assert.That(CountPointsSampledFromBin(actual, bins[4]), Is.EqualTo(1));
            Assert.That(CountPointsSampledFromBin(actual, bins[5]), Is.EqualTo(2));
        }

        private int CountPointsSampledFromBin(List<Point> actual, List<Point> bin)
        {
            int count = 0;
            foreach (Point point in bin) count += actual.Contains(point) ? 1 : 0;
            return count;
        }
    }
}