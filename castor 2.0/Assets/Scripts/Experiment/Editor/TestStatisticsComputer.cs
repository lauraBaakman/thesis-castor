using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using Registration;
using UnityEngine;

namespace Tests.Experiment
{
    [TestFixture]
    public class Test_StatisticsComputer
    {
        const string inputPath_no_change = "no_rotation_no_translation.obj";
        const string inputPath_only_translation = "no_rotation_translation.obj";
        const string inputPath_only_rotation = "rotation_no_translation.obj";
        const string inputPath_tranlsation_rotation = "rotation_translation.obj";

        private Vector3 translation = new Vector3(+0.5f, +07f, -0.9f);

        private static List<Point> oldPositions = new List<Point>
        {
            new Point(new Vector3(-1.000000f, -1.000000f, +1.000000f)),
            new Point(new Vector3(-1.000000f, -1.000000f, +1.000000f)),
            new Point(new Vector3(-1.000000f, -1.000000f, +1.000000f)),
            new Point(new Vector3(-1.000000f, -1.000000f, -1.000000f)),
            new Point(new Vector3(-1.000000f, -1.000000f, -1.000000f)),
            new Point(new Vector3(-1.000000f, -1.000000f, -1.000000f)),
            new Point(new Vector3(+1.000000f, -1.000000f, -1.000000f)),
            new Point(new Vector3(+1.000000f, -1.000000f, -1.000000f)),
            new Point(new Vector3(+1.000000f, -1.000000f, -1.000000f)),
            new Point(new Vector3(+1.000000f, -1.000000f, +1.000000f)),
            new Point(new Vector3(+1.000000f, -1.000000f, +1.000000f)),
            new Point(new Vector3(+1.000000f, -1.000000f, +1.000000f)),
            new Point(new Vector3(-1.000000f, +1.000000f, +0.999999f)),
            new Point(new Vector3(-1.000000f, +1.000000f, +0.999999f)),
            new Point(new Vector3(-1.000000f, +1.000000f, +0.999999f)),
            new Point(new Vector3(-0.999999f, +1.000000f, -1.000001f)),
            new Point(new Vector3(-0.999999f, +1.000000f, -1.000001f)),
            new Point(new Vector3(-0.999999f, +1.000000f, -1.000001f)),
            new Point(new Vector3(+1.000000f, +1.000000f, -1.000000f)),
            new Point(new Vector3(+1.000000f, +1.000000f, -1.000000f)),
            new Point(new Vector3(+1.000000f, +1.000000f, -1.000000f)),
            new Point(new Vector3(+1.000000f, +1.000000f, +1.000000f)),
            new Point(new Vector3(+1.000000f, +1.000000f, +1.000000f)),
            new Point(new Vector3(+1.000000f, +1.000000f, +1.000000f)),
        };

        [SetUp]
        public void SetUp()
        { }

        private string InputPath(string file)
        {
            return Path.Combine(
                Application.dataPath,
                Path.Combine(
                    "Scripts/Experiment/Editor",
                    file
                )
            );
        }

        [Test, TestCaseSource("CorrespondencesCases")]
        public void Test_CollectTrueCorrespondences(string file, CorrespondenceCollection expected)
        {
            _StatisticsComputer computer = new _StatisticsComputer(InputPath(file));
            computer.ReadObjFile();
            computer.CollectTrueCorrespondences();

            CorrespondenceCollection actual = computer.Correspondences;
            Assert.AreEqual(expected, actual);
        }

        #region CorrespondenceCases
        static object[] CorrespondencesCases = {
            new object[] {
                inputPath_no_change,
                new CorrespondenceCollection(
                    staticpoints: oldPositions,
                    modelpoints: new List<Point>{
                        new Point(new Vector3(-1.000000f, -1.000000f, +1.000000f)),
                        new Point(new Vector3(-1.000000f, -1.000000f, +1.000000f)),
                        new Point(new Vector3(-1.000000f, -1.000000f, +1.000000f)),
                        new Point(new Vector3(-1.000000f, -1.000000f, -1.000000f)),
                        new Point(new Vector3(-1.000000f, -1.000000f, -1.000000f)),
                        new Point(new Vector3(-1.000000f, -1.000000f, -1.000000f)),
                        new Point(new Vector3(+1.000000f, -1.000000f, -1.000000f)),
                        new Point(new Vector3(+1.000000f, -1.000000f, -1.000000f)),
                        new Point(new Vector3(+1.000000f, -1.000000f, -1.000000f)),
                        new Point(new Vector3(+1.000000f, -1.000000f, +1.000000f)),
                        new Point(new Vector3(+1.000000f, -1.000000f, +1.000000f)),
                        new Point(new Vector3(+1.000000f, -1.000000f, +1.000000f)),
                        new Point(new Vector3(-1.000000f, +1.000000f, +0.999999f)),
                        new Point(new Vector3(-1.000000f, +1.000000f, +0.999999f)),
                        new Point(new Vector3(-1.000000f, +1.000000f, +0.999999f)),
                        new Point(new Vector3(-0.999999f, +1.000000f, -1.000001f)),
                        new Point(new Vector3(-0.999999f, +1.000000f, -1.000001f)),
                        new Point(new Vector3(-0.999999f, +1.000000f, -1.000001f)),
                        new Point(new Vector3(+1.000000f, +1.000000f, -1.000000f)),
                        new Point(new Vector3(+1.000000f, +1.000000f, -1.000000f)),
                        new Point(new Vector3(+1.000000f, +1.000000f, -1.000000f)),
                        new Point(new Vector3(+1.000000f, +1.000000f, +1.000000f)),
                        new Point(new Vector3(+1.000000f, +1.000000f, +1.000000f)),
                        new Point(new Vector3(+1.000000f, +1.000000f, +1.000000f)),
                    }
                )
            },
            new object[] {
                inputPath_only_translation,
                new CorrespondenceCollection(
                    staticpoints: oldPositions,
                    modelpoints: new List<Point>{
                        new Point(new Vector3(-1.500000f, -1.900000f, +1.700000f)),
                        new Point(new Vector3(-1.500000f, -1.900000f, +1.700000f)),
                        new Point(new Vector3(-1.500000f, -1.900000f, +1.700000f)),
                        new Point(new Vector3(-1.500000f, -1.900000f, -0.300000f)),
                        new Point(new Vector3(-1.500000f, -1.900000f, -0.300000f)),
                        new Point(new Vector3(-1.500000f, -1.900000f, -0.300000f)),
                        new Point(new Vector3(+0.500000f, -1.900000f, -0.300000f)),
                        new Point(new Vector3(+0.500000f, -1.900000f, -0.300000f)),
                        new Point(new Vector3(+0.500000f, -1.900000f, -0.300000f)),
                        new Point(new Vector3(+0.500000f, -1.900000f, +1.700000f)),
                        new Point(new Vector3(+0.500000f, -1.900000f, +1.700000f)),
                        new Point(new Vector3(+0.500000f, -1.900000f, +1.700000f)),
                        new Point(new Vector3(-1.500000f, +0.100000f, +1.699999f)),
                        new Point(new Vector3(-1.500000f, +0.100000f, +1.699999f)),
                        new Point(new Vector3(-1.500000f, +0.100000f, +1.699999f)),
                        new Point(new Vector3(-1.499999f, +0.100000f, -0.300001f)),
                        new Point(new Vector3(-1.499999f, +0.100000f, -0.300001f)),
                        new Point(new Vector3(-1.499999f, +0.100000f, -0.300001f)),
                        new Point(new Vector3(+0.500000f, +0.100000f, -0.300000f)),
                        new Point(new Vector3(+0.500000f, +0.100000f, -0.300000f)),
                        new Point(new Vector3(+0.500000f, +0.100000f, -0.300000f)),
                        new Point(new Vector3(+0.500000f, +0.100000f, +1.700000f)),
                        new Point(new Vector3(+0.500000f, +0.100000f, +1.700000f)),
                        new Point(new Vector3(+0.500000f, +0.100000f, +1.700000f)),
                    }
                )
            },
            new object[] {
                inputPath_only_rotation,
                new CorrespondenceCollection(
                    staticpoints: oldPositions,
                    modelpoints: new List<Point>{
                        new Point(new Vector3(-1.724745f, -0.000000f, +0.158919f)),
                        new Point(new Vector3(-1.724745f, -0.000000f, +0.158919f)),
                        new Point(new Vector3(-1.724745f, -0.000000f, +0.158919f)),
                        new Point(new Vector3(-0.500000f, -1.414214f, +0.866025f)),
                        new Point(new Vector3(-0.500000f, -1.414214f, +0.866025f)),
                        new Point(new Vector3(-0.500000f, -1.414214f, +0.866025f)),
                        new Point(new Vector3(+0.724745f, +0.000000f, +1.573132f)),
                        new Point(new Vector3(+0.724745f, +0.000000f, +1.573132f)),
                        new Point(new Vector3(+0.724745f, +0.000000f, +1.573132f)),
                        new Point(new Vector3(-0.500001f, +1.414214f, +0.866025f)),
                        new Point(new Vector3(-0.500001f, +1.414214f, +0.866025f)),
                        new Point(new Vector3(-0.500001f, +1.414214f, +0.866025f)),
                        new Point(new Vector3(-0.724745f, -0.000001f, -1.573132f)),
                        new Point(new Vector3(-0.724745f, -0.000001f, -1.573132f)),
                        new Point(new Vector3(-0.724745f, -0.000001f, -1.573132f)),
                        new Point(new Vector3(+0.500001f, -1.414213f, -0.866025f)),
                        new Point(new Vector3(+0.500001f, -1.414213f, -0.866025f)),
                        new Point(new Vector3(+0.500001f, -1.414213f, -0.866025f)),
                        new Point(new Vector3(+1.724745f, +0.000000f, -0.158919f)),
                        new Point(new Vector3(+1.724745f, +0.000000f, -0.158919f)),
                        new Point(new Vector3(+1.724745f, +0.000000f, -0.158919f)),
                        new Point(new Vector3(+0.500000f, +1.414213f, -0.866025f)),
                        new Point(new Vector3(+0.500000f, +1.414213f, -0.866025f)),
                        new Point(new Vector3(+0.500000f, +1.414213f, -0.866025f)),
                    }
                )
            },
            new object[] {
                inputPath_tranlsation_rotation,
                new CorrespondenceCollection(
                    staticpoints: oldPositions,
                    modelpoints: new List<Point>{
                        new Point(new Vector3(-2.224745f, -0.900000f, +0.858919f)),
                        new Point(new Vector3(-2.224745f, -0.900000f, +0.858919f)),
                        new Point(new Vector3(-2.224745f, -0.900000f, +0.858919f)),
                        new Point(new Vector3(-1.000000f, -2.314214f, +1.566025f)),
                        new Point(new Vector3(-1.000000f, -2.314214f, +1.566025f)),
                        new Point(new Vector3(-1.000000f, -2.314214f, +1.566025f)),
                        new Point(new Vector3(+0.224745f, -0.900000f, +2.273132f)),
                        new Point(new Vector3(+0.224745f, -0.900000f, +2.273132f)),
                        new Point(new Vector3(+0.224745f, -0.900000f, +2.273132f)),
                        new Point(new Vector3(-1.000000f, +0.514214f, +1.566025f)),
                        new Point(new Vector3(-1.000000f, +0.514214f, +1.566025f)),
                        new Point(new Vector3(-1.000000f, +0.514214f, +1.566025f)),
                        new Point(new Vector3(-1.224745f, -0.900001f, -0.873132f)),
                        new Point(new Vector3(-1.224745f, -0.900001f, -0.873132f)),
                        new Point(new Vector3(-1.224745f, -0.900001f, -0.873132f)),
                        new Point(new Vector3(+0.000001f, -2.314213f, -0.166025f)),
                        new Point(new Vector3(+0.000001f, -2.314213f, -0.166025f)),
                        new Point(new Vector3(+0.000001f, -2.314213f, -0.166025f)),
                        new Point(new Vector3(+1.224745f, -0.900000f, +0.541081f)),
                        new Point(new Vector3(+1.224745f, -0.900000f, +0.541081f)),
                        new Point(new Vector3(+1.224745f, -0.900000f, +0.541081f)),
                        new Point(new Vector3(+0.000000f, +0.514213f, -0.166025f)),
                        new Point(new Vector3(+0.000000f, +0.514213f, -0.166025f)),
                        new Point(new Vector3(+0.000000f, +0.514213f, -0.166025f)),
                    }
                )
            },
        };
        #endregion
    }
}