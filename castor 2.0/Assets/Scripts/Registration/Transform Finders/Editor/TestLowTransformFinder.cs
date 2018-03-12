using System.Collections.Generic;
using NUnit.Framework;
using Registration;
using UnityEngine;
using NUnit.Framework.Internal.Commands;

namespace Tests.Registration.TransformFinders
{
    [TestFixture]
    public class LowTransformFinderTests
    {
        float precision = 0.0001f;

        LowTransformFinder TransformFinder;
        List<Point> modelPoints;
        List<Point> staticPoints;

        [SetUp]
        public void Init()
        {
            TransformFinder = new LowTransformFinder();

            modelPoints = new List<Point>
            {
                new Point(position: new Vector3(-01.200000000000000f, +34.799999999999997f, +40.700000000000003f), normal: new Vector3(0.389722943438479f, 0.901288540481228f, 0.189195650464976f)),
                new Point(position: new Vector3(+39.500000000000000f, +10.600000000000000f, +34.200000000000003f), normal: new Vector3(0.834205746984289f, 0.527449302081401f, 0.160928572454451f)),
                new Point(position: new Vector3(+43.700000000000003f, +37.299999999999997f, +15.400000000000000f), normal: new Vector3(0.688679610190335f, 0.666173746815904f, 0.286239294230103f)),
                new Point(position: new Vector3(+30.899999999999999f, +03.600000000000000f, +47.000000000000000f), normal: new Vector3(0.751746048334168f, 0.025848313608354f, 0.658945933667980f)),
                new Point(position: new Vector3(+34.899999999999999f, +15.800000000000001f, +01.200000000000000f), normal: new Vector3(0.145384286927125f, 0.903800032100591f, 0.402503305687869f)),
                new Point(position: new Vector3(+34.200000000000003f, +04.300000000000000f, +21.399999999999999f), normal: new Vector3(0.174177689724676f, 0.929474546224019f, 0.325175645342373f)),
                new Point(position: new Vector3(+16.600000000000001f, +06.900000000000000f, +18.600000000000001f), normal: new Vector3(0.089324994078233f, 0.326549763278991f, 0.940949678535127f)),
            };

            staticPoints = new List<Point>
            {
                new Point(new Vector3(01.800000000000000f, 32.799999999999997f, 41.200000000000003f)),
                new Point(new Vector3(42.500000000000000f, 08.600000000000000f, 34.700000000000003f)),
                new Point(new Vector3(46.700000000000003f, 35.299999999999997f, 15.900000000000000f)),
                new Point(new Vector3(33.899999999999999f, 01.600000000000000f, 47.500000000000000f)),
                new Point(new Vector3(37.899999999999999f, 13.800000000000001f, 01.700000000000000f)),
                new Point(new Vector3(37.200000000000003f, 02.300000000000000f, 21.899999999999999f)),
                new Point(new Vector3(19.600000000000001f, 04.900000000000000f, 19.100000000000001f))
            };

        }

        [Test]
        public void Test_NeutralTransform()
        {
            CorrespondenceCollection correspondences = new CorrespondenceCollection(
                modelpoints: modelPoints,
                staticpoints: modelPoints
            );
            Matrix4x4 expected = Matrix4x4.identity;

            Matrix4x4 actual = TransformFinder.FindTransform(correspondences);

            Assert.That(actual, Is.EqualTo(expected).Within(precision));
        }

        [Test]
        public void Test_NonNeutralTransform()
        {
            CorrespondenceCollection correspondences = new CorrespondenceCollection(
                modelpoints: modelPoints,
                staticpoints: staticPoints
            );

            Matrix4x4 expected = new Matrix4x4();
            expected[0, 0] = +1.000000000000000f; expected[0, 1] = -0.000000000000000f; expected[0, 2] = -0.000000000000001f; expected[0, 3] = +3.000000000000021f;
            expected[1, 0] = +0.000000000000000f; expected[1, 1] = +1.000000000000000f; expected[1, 2] = +0.000000000000000f; expected[1, 3] = -2.000000000000009f;
            expected[2, 0] = +0.000000000000001f; expected[2, 1] = -0.000000000000000f; expected[2, 2] = +1.000000000000000f; expected[2, 3] = +0.499999999999987f;
            expected[3, 0] = +0.000000000000000f; expected[3, 1] = +0.000000000000000f; expected[3, 2] = +0.000000000000000f; expected[3, 3] = +1.000000000000000f;

            Matrix4x4 actual = TransformFinder.FindTransform(correspondences);

            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    Assert.That(actual[i, j], Is.EqualTo(expected[i, j]).Within(precision));
        }
    }
}