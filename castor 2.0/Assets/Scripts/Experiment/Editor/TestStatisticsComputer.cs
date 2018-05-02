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

        private float precision = 0.00001f;

        private Vector3 translation = new Vector3(+0.5f, +07f, -0.9f);

        private static List<Point> oldPositions = new List<Point>
        {
            //V1
            new Point(new Vector3(+1.000000f, +1.000000f, -1.000000f)),
            new Point(new Vector3(+1.000000f, +1.000000f, -1.000000f)),
            new Point(new Vector3(+1.000000f, +1.000000f, -1.000000f)),
            new Point(new Vector3(+1.000000f, +1.000000f, -1.000000f)),
            //V2
            new Point(new Vector3(+1.000000f, -1.000000f, -1.000000f)),
            new Point(new Vector3(+1.000000f, -1.000000f, -1.000000f)),
            new Point(new Vector3(+1.000000f, -1.000000f, -1.000000f)),
            new Point(new Vector3(+1.000000f, -1.000000f, -1.000000f)),
            new Point(new Vector3(+1.000000f, -1.000000f, -1.000000f)),
            //V3
            new Point(new Vector3(-1.000000f, -1.000000f, -1.000000f)),
            new Point(new Vector3(-1.000000f, -1.000000f, -1.000000f)),
            new Point(new Vector3(-1.000000f, -1.000000f, -1.000000f)),
            new Point(new Vector3(-1.000000f, -1.000000f, -1.000000f)),
            new Point(new Vector3(-1.000000f, -1.000000f, -1.000000f)),
            //V4
            new Point(new Vector3(-1.000000f, +1.000000f, -1.000000f)),
            new Point(new Vector3(-1.000000f, +1.000000f, -1.000000f)),
            new Point(new Vector3(-1.000000f, +1.000000f, -1.000000f)),
            new Point(new Vector3(-1.000000f, +1.000000f, -1.000000f)),
            //V5
            new Point(new Vector3(+1.000000f, +0.999999f, +1.000000f)),
            new Point(new Vector3(+1.000000f, +0.999999f, +1.000000f)),
            new Point(new Vector3(+1.000000f, +0.999999f, +1.000000f)),
            new Point(new Vector3(+1.000000f, +0.999999f, +1.000000f)),
            //V6
            new Point(new Vector3(+0.999999f, -1.000001f, +1.000000f)),
            new Point(new Vector3(+0.999999f, -1.000001f, +1.000000f)),
            new Point(new Vector3(+0.999999f, -1.000001f, +1.000000f)),
            new Point(new Vector3(+0.999999f, -1.000001f, +1.000000f)),
            new Point(new Vector3(+0.999999f, -1.000001f, +1.000000f)),
            //V7
            new Point(new Vector3(-1.000000f, -1.000000f, +1.000000f)),
            new Point(new Vector3(-1.000000f, -1.000000f, +1.000000f)),
            new Point(new Vector3(-1.000000f, -1.000000f, +1.000000f)),
            //V8
            new Point(new Vector3(-1.000000f, +1.000000f, +1.000000f)),
            new Point(new Vector3(-1.000000f, +1.000000f, +1.000000f)),
            new Point(new Vector3(-1.000000f, +1.000000f, +1.000000f)),
            new Point(new Vector3(-1.000000f, +1.000000f, +1.000000f)),
            new Point(new Vector3(-1.000000f, +1.000000f, +1.000000f)),
            new Point(new Vector3(-1.000000f, +1.000000f, +1.000000f)),
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
            computer.CollectCorrespondences();

            CorrespondenceCollection actual = computer.Correspondences;

            Assert.AreEqual(expected, actual);
        }

        #region CorrespondenceCases
        static object[] CorrespondencesCases = {
            new object[] {
                inputPath_no_change,
                new CorrespondenceCollection(
                    modelpoints: oldPositions,
                    staticpoints: new List<Point>{
                    //V1
                    new Point(new Vector3(+1.000000f, +1.000000f, -1.000000f)),
                    new Point(new Vector3(+1.000000f, +1.000000f, -1.000000f)),
                    new Point(new Vector3(+1.000000f, +1.000000f, -1.000000f)),
                    new Point(new Vector3(+1.000000f, +1.000000f, -1.000000f)),
                    //V2
                    new Point(new Vector3(+1.000000f, -1.000000f, -1.000000f)),
                    new Point(new Vector3(+1.000000f, -1.000000f, -1.000000f)),
                    new Point(new Vector3(+1.000000f, -1.000000f, -1.000000f)),
                    new Point(new Vector3(+1.000000f, -1.000000f, -1.000000f)),
                    new Point(new Vector3(+1.000000f, -1.000000f, -1.000000f)),
                    //V3
                    new Point(new Vector3(-1.000000f, -1.000000f, -1.000000f)),
                    new Point(new Vector3(-1.000000f, -1.000000f, -1.000000f)),
                    new Point(new Vector3(-1.000000f, -1.000000f, -1.000000f)),
                    new Point(new Vector3(-1.000000f, -1.000000f, -1.000000f)),
                    new Point(new Vector3(-1.000000f, -1.000000f, -1.000000f)),
                    //V4
                    new Point(new Vector3(-1.000000f, +1.000000f, -1.000000f)),
                    new Point(new Vector3(-1.000000f, +1.000000f, -1.000000f)),
                    new Point(new Vector3(-1.000000f, +1.000000f, -1.000000f)),
                    new Point(new Vector3(-1.000000f, +1.000000f, -1.000000f)),
                    //V5
                    new Point(new Vector3(+1.000000f, +0.999999f, +1.000000f)),
                    new Point(new Vector3(+1.000000f, +0.999999f, +1.000000f)),
                    new Point(new Vector3(+1.000000f, +0.999999f, +1.000000f)),
                    new Point(new Vector3(+1.000000f, +0.999999f, +1.000000f)),
                    //V6
                    new Point(new Vector3(+0.999999f, -1.000001f, +1.000000f)),
                    new Point(new Vector3(+0.999999f, -1.000001f, +1.000000f)),
                    new Point(new Vector3(+0.999999f, -1.000001f, +1.000000f)),
                    new Point(new Vector3(+0.999999f, -1.000001f, +1.000000f)),
                    new Point(new Vector3(+0.999999f, -1.000001f, +1.000000f)),
                    //V7
                    new Point(new Vector3(-1.000000f, -1.000000f, +1.000000f)),
                    new Point(new Vector3(-1.000000f, -1.000000f, +1.000000f)),
                    new Point(new Vector3(-1.000000f, -1.000000f, +1.000000f)),
                    //V8
                    new Point(new Vector3(-1.000000f, +1.000000f, +1.000000f)),
                    new Point(new Vector3(-1.000000f, +1.000000f, +1.000000f)),
                    new Point(new Vector3(-1.000000f, +1.000000f, +1.000000f)),
                    new Point(new Vector3(-1.000000f, +1.000000f, +1.000000f)),
                    new Point(new Vector3(-1.000000f, +1.000000f, +1.000000f)),
                    new Point(new Vector3(-1.000000f, +1.000000f, +1.000000f)),
                    }
                )
            },
            new object[] {
                inputPath_only_translation,
                new CorrespondenceCollection(
                    modelpoints: oldPositions,
                    staticpoints: new List<Point>{
                        //V1
                        new Point(new Vector3(+1.500000f, +1.700000f, -1.900000f)),
                        new Point(new Vector3(+1.500000f, +1.700000f, -1.900000f)),
                        new Point(new Vector3(+1.500000f, +1.700000f, -1.900000f)),
                        new Point(new Vector3(+1.500000f, +1.700000f, -1.900000f)),
                        //V2
                        new Point(new Vector3(+1.500000f, -0.300000f, -1.900000f)),
                        new Point(new Vector3(+1.500000f, -0.300000f, -1.900000f)),
                        new Point(new Vector3(+1.500000f, -0.300000f, -1.900000f)),
                        new Point(new Vector3(+1.500000f, -0.300000f, -1.900000f)),
                        new Point(new Vector3(+1.500000f, -0.300000f, -1.900000f)),
                        //V3
                        new Point(new Vector3(-0.500000f, -0.300000f, -1.900000f)),
                        new Point(new Vector3(-0.500000f, -0.300000f, -1.900000f)),
                        new Point(new Vector3(-0.500000f, -0.300000f, -1.900000f)),
                        new Point(new Vector3(-0.500000f, -0.300000f, -1.900000f)),
                        new Point(new Vector3(-0.500000f, -0.300000f, -1.900000f)),
                        //V4
                        new Point(new Vector3(-0.500000f, +1.700000f, -1.900000f)),
                        new Point(new Vector3(-0.500000f, +1.700000f, -1.900000f)),
                        new Point(new Vector3(-0.500000f, +1.700000f, -1.900000f)),
                        new Point(new Vector3(-0.500000f, +1.700000f, -1.900000f)),
                        //V5
                        new Point(new Vector3(+1.500000f, +1.699999f, +0.100000f)),
                        new Point(new Vector3(+1.500000f, +1.699999f, +0.100000f)),
                        new Point(new Vector3(+1.500000f, +1.699999f, +0.100000f)),
                        new Point(new Vector3(+1.500000f, +1.699999f, +0.100000f)),
                        //V6
                        new Point(new Vector3(+1.499999f, -0.300001f, +0.100000f)),
                        new Point(new Vector3(+1.499999f, -0.300001f, +0.100000f)),
                        new Point(new Vector3(+1.499999f, -0.300001f, +0.100000f)),
                        new Point(new Vector3(+1.499999f, -0.300001f, +0.100000f)),
                        new Point(new Vector3(+1.499999f, -0.300001f, +0.100000f)),
                        //V7
                        new Point(new Vector3(-0.500000f, -0.300000f, +0.100000f)),
                        new Point(new Vector3(-0.500000f, -0.300000f, +0.100000f)),
                        new Point(new Vector3(-0.500000f, -0.300000f, +0.100000f)),
                        //V8
                        new Point(new Vector3(-0.500000f, +1.700000f, +0.100000f)),
                        new Point(new Vector3(-0.500000f, +1.700000f, +0.100000f)),
                        new Point(new Vector3(-0.500000f, +1.700000f, +0.100000f)),
                        new Point(new Vector3(-0.500000f, +1.700000f, +0.100000f)),
                        new Point(new Vector3(-0.500000f, +1.700000f, +0.100000f)),
                        new Point(new Vector3(-0.500000f, +1.700000f, +0.100000f)),
                    }
                )
            },
            new object[] {
                inputPath_only_rotation,
                new CorrespondenceCollection(
                    modelpoints: oldPositions,
                    staticpoints: new List<Point>{
                        //V1
                        new Point(new Vector3(+1.724745f, +0.158919f, -0.000000f)),
                        new Point(new Vector3(+1.724745f, +0.158919f, -0.000000f)),
                        new Point(new Vector3(+1.724745f, +0.158919f, -0.000000f)),
                        new Point(new Vector3(+1.724745f, +0.158919f, -0.000000f)),
                        //V2
                        new Point(new Vector3(+0.500000f, +0.866025f, -1.414214f)),
                        new Point(new Vector3(+0.500000f, +0.866025f, -1.414214f)),
                        new Point(new Vector3(+0.500000f, +0.866025f, -1.414214f)),
                        new Point(new Vector3(+0.500000f, +0.866025f, -1.414214f)),
                        new Point(new Vector3(+0.500000f, +0.866025f, -1.414214f)),
                        //V3
                        new Point(new Vector3(-0.724745f, +1.573132f, +0.000000f)),
                        new Point(new Vector3(-0.724745f, +1.573132f, +0.000000f)),
                        new Point(new Vector3(-0.724745f, +1.573132f, +0.000000f)),
                        new Point(new Vector3(-0.724745f, +1.573132f, +0.000000f)),
                        new Point(new Vector3(-0.724745f, +1.573132f, +0.000000f)),
                        //V4
                        new Point(new Vector3(+0.500001f, +0.866025f, +1.414214f)),
                        new Point(new Vector3(+0.500001f, +0.866025f, +1.414214f)),
                        new Point(new Vector3(+0.500001f, +0.866025f, +1.414214f)),
                        new Point(new Vector3(+0.500001f, +0.866025f, +1.414214f)),
                        //V5
                        new Point(new Vector3(+0.724745f, -1.573132f, -0.000001f)),
                        new Point(new Vector3(+0.724745f, -1.573132f, -0.000001f)),
                        new Point(new Vector3(+0.724745f, -1.573132f, -0.000001f)),
                        new Point(new Vector3(+0.724745f, -1.573132f, -0.000001f)),
                        //V6
                        new Point(new Vector3(-0.500001f, -0.866025f, -1.414213f)),
                        new Point(new Vector3(-0.500001f, -0.866025f, -1.414213f)),
                        new Point(new Vector3(-0.500001f, -0.866025f, -1.414213f)),
                        new Point(new Vector3(-0.500001f, -0.866025f, -1.414213f)),
                        new Point(new Vector3(-0.500001f, -0.866025f, -1.414213f)),
                        //V7
                        new Point(new Vector3(-1.724745f, -0.158919f, +0.000000f)),
                        new Point(new Vector3(-1.724745f, -0.158919f, +0.000000f)),
                        new Point(new Vector3(-1.724745f, -0.158919f, +0.000000f)),
                        //V8
                        new Point(new Vector3(-0.500000f, -0.866025f, +1.414213f)),
                        new Point(new Vector3(-0.500000f, -0.866025f, +1.414213f)),
                        new Point(new Vector3(-0.500000f, -0.866025f, +1.414213f)),
                        new Point(new Vector3(-0.500000f, -0.866025f, +1.414213f)),
                        new Point(new Vector3(-0.500000f, -0.866025f, +1.414213f)),
                        new Point(new Vector3(-0.500000f, -0.866025f, +1.414213f)),
                    }
                )
            },
            new object[] {
                inputPath_tranlsation_rotation,
                new CorrespondenceCollection(
                    modelpoints: oldPositions,
                    staticpoints: new List<Point>{
                        //V1
                        new Point(new Vector3(+2.224745f, +0.858919f, -0.900000f)),
                        new Point(new Vector3(+2.224745f, +0.858919f, -0.900000f)),
                        new Point(new Vector3(+2.224745f, +0.858919f, -0.900000f)),
                        new Point(new Vector3(+2.224745f, +0.858919f, -0.900000f)),
                        //V2
                        new Point(new Vector3(+1.000000f, +1.566025f, -2.314214f)),
                        new Point(new Vector3(+1.000000f, +1.566025f, -2.314214f)),
                        new Point(new Vector3(+1.000000f, +1.566025f, -2.314214f)),
                        new Point(new Vector3(+1.000000f, +1.566025f, -2.314214f)),
                        new Point(new Vector3(+1.000000f, +1.566025f, -2.314214f)),
                        //V3
                        new Point(new Vector3(-0.224745f, +2.273132f, -0.900000f)),
                        new Point(new Vector3(-0.224745f, +2.273132f, -0.900000f)),
                        new Point(new Vector3(-0.224745f, +2.273132f, -0.900000f)),
                        new Point(new Vector3(-0.224745f, +2.273132f, -0.900000f)),
                        new Point(new Vector3(-0.224745f, +2.273132f, -0.900000f)),
                        //V4
                        new Point(new Vector3(+1.000000f, +1.566025f, +0.514214f)),
                        new Point(new Vector3(+1.000000f, +1.566025f, +0.514214f)),
                        new Point(new Vector3(+1.000000f, +1.566025f, +0.514214f)),
                        new Point(new Vector3(+1.000000f, +1.566025f, +0.514214f)),
                        //V5
                        new Point(new Vector3(+1.224745f, -0.873132f, -0.900001f)),
                        new Point(new Vector3(+1.224745f, -0.873132f, -0.900001f)),
                        new Point(new Vector3(+1.224745f, -0.873132f, -0.900001f)),
                        new Point(new Vector3(+1.224745f, -0.873132f, -0.900001f)),
                        //V6
                        new Point(new Vector3(-0.000001f, -0.166025f, -2.314213f)),
                        new Point(new Vector3(-0.000001f, -0.166025f, -2.314213f)),
                        new Point(new Vector3(-0.000001f, -0.166025f, -2.314213f)),
                        new Point(new Vector3(-0.000001f, -0.166025f, -2.314213f)),
                        new Point(new Vector3(-0.000001f, -0.166025f, -2.314213f)),
                        //V7
                        new Point(new Vector3(-1.224745f, +0.541081f, -0.900000f)),
                        new Point(new Vector3(-1.224745f, +0.541081f, -0.900000f)),
                        new Point(new Vector3(-1.224745f, +0.541081f, -0.900000f)),
                        //V8
                        new Point(new Vector3(+0.000000f, -0.166025f, +0.514213f)),
                        new Point(new Vector3(+0.000000f, -0.166025f, +0.514213f)),
                        new Point(new Vector3(+0.000000f, -0.166025f, +0.514213f)),
                        new Point(new Vector3(+0.000000f, -0.166025f, +0.514213f)),
                        new Point(new Vector3(+0.000000f, -0.166025f, +0.514213f)),
                        new Point(new Vector3(+0.000000f, -0.166025f, +0.514213f)),
                    }
                )
            },
        };
        #endregion

        [Test, TestCaseSource("TransformCases")]
        public void Test_ComputeTransformationMatrix(string file, Matrix4x4 expected)
        {
            _StatisticsComputer computer = new _StatisticsComputer(InputPath(file));
            computer.ReadObjFile();
            computer.CollectCorrespondences();
            computer.ComputeTransformationMatrix();

            Matrix4x4 actual = computer.TransformationMatrix;
            for (int i = 0; i < 16; i++)
            {
                Assert.That(expected[i], Is.EqualTo(actual[i]).Within(precision));
            }
        }

        #region TransformCases
        static object[] TransformCases = {
            new object[] {
                inputPath_no_change,
                new Matrix4x4(
                    column0: new Vector4(1, 0, 0, 0),
                    column1: new Vector4(0, 1, 0, 0),
                    column2: new Vector4(0, 0, 1, 0),
                    column3: new Vector4(0, 0, 0, 1)
                )
            },
            new object[] {
                inputPath_only_translation,
                new Matrix4x4(
                    column0: new Vector4(+1.0f, +0.0f, +0.0f, 0),
                    column1: new Vector4(+0.0f, +1.0f, +0.0f, 0),
                    column2: new Vector4(+0.0f, +0.0f, +1.0f, 0),
                    column3: new Vector4(+0.5f, +0.7f, -0.9f, 1)
                )
            },
            new object[] {
                inputPath_only_rotation,
                new Matrix4x4(
                    column0: new Vector4(+0.612372435695795f, -0.353553390593274f, -0.707106781186547f, 0),
                    column1: new Vector4(+0.612372435695794f, -0.353553390593274f, +0.707106781186548f, 0),
                    column2: new Vector4(-0.500000000000000f, -0.866025403784439f, +0.000000000000000f, 0),
                    column3: new Vector4(+0.000000000000000f, +0.000000000000000f,  +0.00000000000000f, 1)
                )
            },
            new object[] {
                inputPath_tranlsation_rotation,
                new Matrix4x4(
                    column0: new Vector4(+0.612372435695795f, -0.353553390593274f, -0.707106781186547f, 0),
                    column1: new Vector4(+0.612372435695794f, -0.353553390593274f, +0.707106781186548f, 0),
                    column2: new Vector4(-0.500000000000000f, -0.866025403784439f, +0.000000000000000f, 0),
                    column3: new Vector4(+0.500000000000000f, +0.700000000000000f, -0.900000000000000f, 1)
                )
            },
        };
        #endregion
    }
}