using System.Collections.Generic;
using NUnit.Framework;
using Registration;
using UnityEngine;

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
            expected[0, 0] = +0.999591940870855f; expected[1, 0] = -0.021613024656252f; expected[2, 0] = -0.018676962045425f; expected[3, 0] = +1.846045394336890f;
            expected[0, 1] = +0.018868670036331f; expected[1, 1] = +0.990482822730230f; expected[2, 1] = -0.136336903175245f; expected[3, 1] = -1.076449856474986f;
            expected[0, 2] = +0.021445862936662f; expected[1, 2] = +0.135928860223149f; expected[2, 2] = +0.990486456202879f; expected[3, 2] = +2.943459815683508f;
            expected[0, 3] = +0.000000000000000f; expected[1, 3] = +0.000000000000000f; expected[2, 3] = +0.000000000000000f; expected[3, 3] = +1.000000000000000f;

            Matrix4x4 actual = TransformFinder.FindTransform(correspondences);

            Assert.That(actual, Is.EqualTo(expected).Within(precision));
        }

        [Test]
        public void Test_BuildA()
        {
            CorrespondenceCollection correspondences = new CorrespondenceCollection(
                modelpoints: modelPoints,
                staticpoints: staticPoints
            );
            double[,] actual = TransformFinder.BuildA(correspondences);

            double[,] expected = new double[,]
            {
                { -30.927470532575377f, +15.716033098828387f, -11.160593171915908f, +00.389722943438479f, +00.901288540481228f, +00.189195650464976f },
                { -16.918505059116331f, +22.107475091040680f, +15.242425914394650f, +00.834205746984289f, +00.527449302081401f, +00.160928572454451f},
                { -00.487915488050234f, -02.417369238519484f, +06.799923736583882f, +00.688679610190335f, +00.666173746815904f, +00.286239294230103f},
                { -00.173481402528037f, +13.369670144528470f, -00.326535846011476f, +00.751746048334168f, +00.025848313608354f, +00.658945933667980f},
                { +04.018085563921582f, -15.007721997794109f, +32.247718057018083f, +00.145384286927125f, +00.903800032100591f, +00.402503305687869f},
                { -19.607588578018557f, -08.282042601765877f, +34.175844433166759f, +00.174177689724676f, +00.929474546224019f, +00.325175645342373f},
                { -01.626447053806614f, -16.736506312394237f, +05.962682889284890f, +00.089324994078233f, +00.326549763278991f, +00.940949678535127f}
            };

            Assert.That(actual, Is.EqualTo(expected).Within(precision));
        }

        [Test]
        public void Test_BuildB()
        {
            CorrespondenceCollection correspondences = new CorrespondenceCollection(
                modelpoints: modelPoints,
                staticpoints: staticPoints
            );
            double[] actual = TransformFinder.BuildB(correspondences);

            double[] expected = new double[]{
                +0.538810425414532f,
                -1.528182923017297f,
                -0.876810984054252f,
                -2.533014484619784f,
                +1.170195550575876f,
                +1.173828200602824f,
                -0.085350294944277f
            };

            Assert.That(actual, Is.EqualTo(expected).Within(precision));
        }

        [Test]
        public void Test_xOptToTransformationMatrix()
        {
            double[] xOpt = new double[] {
                +0.136382526761545f,
                -0.021447507191874f,
                +0.018874131198093f,
                +1.846045394336890f,
                -1.076449856474986f,
                +2.943459815683508f,
            };
            Matrix4x4 actual = TransformFinder.xOptToTransformationMatrix(xOpt);

            Matrix4x4 expected = new Matrix4x4();
            expected[0, 0] = +0.999591940870855f; expected[0, 1] = -0.021613024656252f; expected[0, 2] = -0.018676962045425f; expected[0, 3] = +1.846045394336890f;
            expected[1, 0] = +0.018868670036331f; expected[1, 1] = +0.990482822730230f; expected[1, 2] = -0.136336903175245f; expected[1, 3] = -1.076449856474986f;
            expected[2, 0] = +0.021445862936662f; expected[2, 1] = +0.135928860223149f; expected[2, 2] = +0.990486456202879f; expected[2, 3] = +2.943459815683508f;
            expected[3, 0] = +0.000000000000000f; expected[3, 1] = +0.000000000000000f; expected[3, 2] = +0.000000000000000f; expected[3, 3] = +1.000000000000000f;

            Assert.That(actual, Is.EqualTo(expected).Within(precision));
        }
    }
}