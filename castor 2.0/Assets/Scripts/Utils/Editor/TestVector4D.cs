using Utils;
using NUnit.Framework;
using UnityEngine;
using System;

namespace Tests.Utils
{
    [TestFixture]
    public class Vector4DTest
    {
        double precision = 0.00000001;

        [Test]
        public void Test_Constructor_Four_Doubles()
        {
            double x = 1;
            double y = 2;
            double z = 3;
            double w = 4;

            Vector4D actual = new Vector4D(x, y, z, w);

            Assert.AreEqual(x, actual.x);
            Assert.AreEqual(y, actual.y);
            Assert.AreEqual(z, actual.z);
            Assert.AreEqual(w, actual.w);
        }

        [Test]
        public void Test_Constructor_Vector4()
        {
            Vector4 input = new Vector4(1, 2, 3, 4);

            Vector4D actual = new Vector4D(input);

            Assert.AreEqual(input.x, actual.x);
            Assert.AreEqual(input.y, actual.y);
            Assert.AreEqual(input.z, actual.z);
            Assert.AreEqual(input.w, actual.w);
        }

        [Test]
        public void Test_Constructor_NoArguments()
        {
            Vector4D actual = new Vector4D();

            Assert.AreEqual(0, actual.x);
            Assert.AreEqual(0, actual.y);
            Assert.AreEqual(0, actual.z);
            Assert.AreEqual(0, actual.w);
        }

        [Test]
        public void Test_Constructor_CopyConstructor()
        {
            Vector4D input = new Vector4D(1, 2, 3, 4);
            Vector4D actual = new Vector4D(input);

            Assert.AreEqual(input.x, actual.x);
            Assert.AreEqual(input.y, actual.y);
            Assert.AreEqual(input.z, actual.z);
            Assert.AreEqual(input.w, actual.w);
        }

        [Test, TestCaseSource("EqualsCases")]
        public void Test_GenericEquals(Vector4D self, Vector4D other, bool expected)
        {
            bool actual = self.Equals(other);

            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expected, self.GetHashCode().Equals(other.GetHashCode()));
        }

        static object[] EqualsCases =
        {
            //Equals
            new object[]{
                new Vector4D(1, 2, 3, 4), new Vector4D(1, 2, 3, 4), true
            },
            //Not equal x different
            new object[]{
                new Vector4D(1, 2, 3, 4), new Vector4D(2, 2, 3, 4), false
            },
            //Not equal y different
            new object[]{
                new Vector4D(1, 2, 3, 4), new Vector4D(1, 3, 3, 4), false
            },
            //Not equal z different
            new object[]{
                new Vector4D(1, 2, 3, 4), new Vector4D(1, 2, 4, 4), false
            },
            //Not equal z different
            new object[]{
                new Vector4D(1, 2, 3, 4), new Vector4D(1, 2, 3, 5), false
            },
        };

        [Test, TestCaseSource("SqrMagnitudeCases")]
        public void Test_SqrMagnitude_Method(Vector4D vector, double expected)
        {
            double actual = vector.SqrMagnitude();

            Assert.That(expected, Is.EqualTo(actual).Within(precision));
        }

        [Test, TestCaseSource("SqrMagnitudeCases")]
        public void Test_Magnitude_Method(Vector4D vector, double expectedSqrt)
        {
            double expected = Math.Sqrt(expectedSqrt);

            double actual = vector.Magnitude();

            Assert.That(expected, Is.EqualTo(actual).Within(precision));
        }

        [Test, TestCaseSource("DotCases")]
        public void Test_Dot_Method(Vector4D lhs, Vector4D rhs, double expected)
        {
            double actual = lhs.Dot(rhs);

            Assert.That(expected, Is.EqualTo(actual).Within(precision));
        }

        static object[] SqrMagnitudeCases =
        {
            new object[]{
                new Vector4D(+957.506835434298e-003, +964.888535199276e-003, +157.613081677548e-003, +970.592781760616e-003),
                2.81472145678411e+000
            },
            new object[]{
                new Vector4D(+957.166948242946e-003, +485.375648722841e-003, +800.280468888800e-003, +141.886338627215e-003),
                1.81233864915575e+000
            },
            new object[]{
                new Vector4D(+421.761282626275e+000,  +915.735525189067e+000, +792.207329559554e+000, +959.492426392903e+000),
                2.56467230092908e+006
            },
            new object[]{
                new Vector4D(-344.259300843413e-003, -964.288321425810e-003, -150.870694131223e-003, -66.0067522424495e-003),
                1.07548529074464e+000
            },
            new object[]{
               new Vector4D(-49.3212648451422e+000, -49.2422598694217e+000, -49.2568675318751e+000, -49.6077729804658e+000),
               9.74455746210644e+003
            }
        };

        [Test, TestCaseSource("IndexerGetCases")]
        public void Test_Indexer_Get(Vector4D vector, int idx, double expected)
        {
            double actual = vector[idx];

            Assert.AreEqual(expected, actual);
        }

        static object[] IndexerGetCases = {
            new object[]{new Vector4D(1, 2, 3, 4), 0, 1},
            new object[]{new Vector4D(1, 2, 3, 4), 1, 2},
            new object[]{new Vector4D(1, 2, 3, 4), 2, 3},
            new object[]{new Vector4D(1, 2, 3, 4), 3, 4},
        };

        [Test, TestCaseSource("IndexerSetCases")]
        public void Test_Indexer_Set(Vector4D vector, int idx, double newvalue)
        {
            vector[idx] = newvalue;

            double actual = vector[idx];

            Assert.AreEqual(newvalue, actual);
        }

        static object[] IndexerSetCases = {
            new object[]{new Vector4D(1, 2, 3, 4), 0, 6},
            new object[]{new Vector4D(1, 2, 3, 4), 1, 7},
            new object[]{new Vector4D(1, 2, 3, 4), 2, 8},
            new object[]{new Vector4D(1, 2, 3, 4), 3, 9},
        };

        [Test, TestCaseSource("DotCases")]
        public void Test_Dot_Static(Vector4D lhs, Vector4D rhs, double expected)
        {
            double actual = Vector4D.Dot(lhs, rhs);

            Assert.That(expected, Is.EqualTo(actual).Within(precision));
        }

        static object[] DotCases = {
            new object[]{
                new Vector4D(+276.922984960890e-003, +46.1713906311539e-003, +97.1317812358475e-003, +823.457828327293e-003),
                new Vector4D(+694.828622975817e-003, +317.099480060861e-003, +950.222048838355e-003, +34.4460805029088e-003),
                327.716595092112e-003
            },
            new object[]{
                new Vector4D(+43.8744359656398e+000, +38.1558457093008e+000, +76.5516788149002e+000, +79.5199901137063e+000),
                new Vector4D(+18.6872604554379e+000, +48.9764395788231e+000, +44.5586200710899e+000, +64.6313010111265e+000),
                11.2391480737438e+003
            },
            new object[]{
                new Vector4D(+43.8744359656398e+000, +38.1558457093008e+000, +76.5516788149002e+000, +79.5199901137063e+000),
                new Vector4D(+0.00000000000000e+000, +0.00000000000000e+000, +0.00000000000000e+000, +0.00000000000000e+000),
                0.00000000000000e+000
            },
            new object[]{
                new Vector4D(+15.5098003973841e+000, -33.7388264805369e+000, -38.1002318441623e+000, -163.594801785705e-003),
                new Vector4D(+45.9743958516081e+000, -15.9614273333867e+000, +8.52677509797774e+000, -27.6188060508863e+000),
                931.219715217067e+000
            },
        };

        [Test, TestCaseSource("SqrMagnitudeCases")]
        public void Test_Magnitude_Static(Vector4D vector, double expectedSqrt)
        {
            double expected = Math.Sqrt(expectedSqrt);

            double actual = Vector4D.Magnitude(vector);

            Assert.That(expected, Is.EqualTo(actual).Within(precision));
        }

        [Test, TestCaseSource("SqrMagnitudeCases")]
        public void Test_SqrMagnitude_Static(Vector4D vector, double expected)
        {
            double actual = Vector4D.SqrMagnitude(vector);

            Assert.That(expected, Is.EqualTo(actual).Within(precision));
        }

        [Test, TestCaseSource("AdditionCases")]
        public void Test_Addition_Operator(Vector4D lhs, Vector4D rhs, Vector4D expected)
        {
            Vector4D actual = lhs + rhs;

            Assert.IsTrue(expected.Equals(actual));
        }

        [Test, TestCaseSource("AdditionCases")]
        public void Test_Addition_ShortHandOperator(Vector4D lhs, Vector4D rhs, Vector4D expected)
        {
            Vector4D actual = new Vector4D(lhs);
            actual += rhs;

            Assert.IsTrue(expected.Equals(actual));
        }

        static object[] AdditionCases = {
            new object[] {
                new Vector4D(01, 02, 03, 04),
                new Vector4D(04, 05, 06, 07),
                new Vector4D(05, 07, 09, 11),
            },
            new object[] {
                new Vector4D(751.267059305653e-003, 255.095115459269e-003, 505.957051665142e-003, 699.076722656686e-003),
                new Vector4D(890.903252535799e-003, 959.291425205444e-003, 547.215529963803e-003, 138.624442828679e-003),
                new Vector4D(1.64217031184145e+000, 1.21438654066471e+000, 1.05317258162895e+000, 837.701165485365e-003),
            },
            new object[] {
                new Vector4D(14.9294005559057e+000, 25.7508254123736e+000, 84.0717255983663e+000, 25.4282178971531e+000),
                new Vector4D(81.4284826068816e+000, 24.3524968724989e+000, 92.9263623187228e+000, 34.9983765984809e+000),
                new Vector4D(96.3578831627874e+000, 50.1033222848726e+000, 176.998087917089e+000, 60.4265944956340e+000),
            },
            new object[] {
                new Vector4D(-303.404749568792e-003, -248.916142023969e-003, +116.044676146639e-003, -26.7111510972707e-003),
                new Vector4D(-148.340492937003e-003, +330.828627896291e-003, +85.2640911527243e-003, +49.7236082911395e-003),
                new Vector4D(-451.745242505795e-003, +81.9124858723219e-003, +201.308767299363e-003, +23.0124571938688e-003),
            },
            new object[] {
                new Vector4D(+41.7193663829810e+000, -21.4160981179626e+000, +25.7200229110721e+000, +25.3729094278495e+000),
                new Vector4D(-11.9554153024643e+000, +6.78216407252211e+000, -42.4145710436936e+000, -44.6049881333393e+000),
                new Vector4D(+29.7639510805167e+000, -14.6339340454405e+000, -16.6945481326215e+000, -19.2320787054898e+000),
            },
        };

        [Test, TestCaseSource("SubtractionCases")]
        public void Test_Subtraction_Operator(Vector4D lhs, Vector4D rhs, Vector4D expected)
        {
            Vector4D actual = lhs - rhs;

            Assert.IsTrue(expected.Equals(actual));
        }

        [Test, TestCaseSource("SubtractionCases")]
        public void Test_Subtraction_ShortHandOperator(Vector4D lhs, Vector4D rhs, Vector4D expected)
        {
            Vector4D actual = new Vector4D(lhs);
            actual -= rhs;

            Assert.IsTrue(expected.Equals(actual));
        }

        static object[] SubtractionCases = {
            new object[] {
                new Vector4D(+530.797553008973e-003, +779.167230102011e-003, +934.010684229183e-003, +129.906208473730e-003),
                new Vector4D(+568.823660872193e-003, +469.390641058206e-003, +11.9020695012414e-003, +337.122644398882e-003),
                new Vector4D(-38.0261078632200e-003, +309.776589043805e-003, +922.108614727942e-003, -207.216435925151e-003),
            },
            new object[] {
                new Vector4D(+16.2182308193243e+000, +79.4284540683907e+000, +31.1215042044805e+000, +52.8533135506213e+000),
                new Vector4D(+16.5648729499781e+000, +60.1981941401637e+000, +26.2971284540144e+000, +65.4079098476782e+000),
                new Vector4D(-346.642130653816e-003, +19.2302599282270e+000, +4.82437575046606e+000, -12.5545962970570e+000),
            },
            new object[] {
                new Vector4D(+189.214503140008e-003, +248.151592823709e-003, -49.4584014975022e-003, -416.178622003067e-003),
                new Vector4D(-271.023031283181e-003, +413.337361501670e-003, -347.621981030777e-003, +325.816977489547e-003),
                new Vector4D(+460.237534423189e-003, -165.185768677960e-003, +298.163579533275e-003, -741.995599492615e-003),
            },
            new object[] {
                new Vector4D(+3.83424352600571e+000, +49.6134716626885e+000, -42.1824471246816e+000, -5.73217302245537e+000),
                new Vector4D(-39.3347229819416e+000, +46.1898080855054e+000, -49.5365775865933e+000, +27.4910464711502e+000),
                new Vector4D(+43.1689665079473e+000, +3.42366357718318e+000, +7.35413046191162e+000, -33.2232194936056e+000),
            },
        };

        [Test, TestCaseSource("ScalarMultiplicationCases")]
        public void Test_Scaling_Operator(Vector4D vector, double scale, Vector4D expected)
        {
            Vector4D actual = vector * scale;
            Assert.IsTrue(expected.Equals(actual));

            actual = scale * vector;
            Assert.IsTrue(expected.Equals(actual));
        }

        [Test, TestCaseSource("ScalarMultiplicationCases")]
        public void Test_Scaling_ShortHandOperator(Vector4D lhs, double scale, Vector4D expected)
        {
            Vector4D actual = new Vector4D(lhs);
            actual *= scale;
            Assert.IsTrue(expected.Equals(actual));
        }

        static object[] ScalarMultiplicationCases = {
            new object[] {
                new Vector4D(+530.797553008973e-003, +779.167230102011e-003, +934.010684229183e-003, +129.906208473730e-003),
                +817.303220653433e-003,
                new Vector4D(+433.822549589195e-003, +636.815886589988e-003, +763.369940345228e-003, +106.172762568456e-003),
            },
            new object[] {
                new Vector4D(+16.2182308193243e+000, +79.4284540683907e+000, +31.1215042044805e+000, +52.8533135506213e+000),
                +86.8694705363510e+000,
                new Vector4D(+1.40886912431103e+003, +6.89990775044197e+003, +2.70350859253804e+003, +4.59133936423421e+003),
            },
            new object[] {
                new Vector4D(+189.214503140008e-003, +248.151592823709e-003, -49.4584014975022e-003, -416.178622003067e-003),
                -415.564154489090e-003,
                new Vector4D(-78.6307650144505e-003, -103.122906856906e-003, +20.5531388006914e-003, +172.948917169139e-003),
            },
            new object[] {
                new Vector4D(+3.83424352600571e+000, +49.6134716626885e+000, -42.1824471246816e+000, -5.73217302245537e+000),
                -10.0217350901104e+000,
                new Vector4D(-38.4257728885999e+000, -497.213069904161e+000, +422.741310536146e+000, +57.4463195217249e+000),
            },
        };

        [Test, TestCaseSource("ScalarDivisionCases")]
        public void Test_Division_Operator(Vector4D vector, double scale, Vector4D expected)
        {
            Vector4D actual = vector / scale;
            Assert.IsTrue(expected.Equals(actual));
        }

        [Test, TestCaseSource("ScalarDivisionCases")]
        public void Test_Division_ShortHandOperator(Vector4D lhs, double scale, Vector4D expected)
        {
            Vector4D actual = new Vector4D(lhs);
            actual /= scale;
            Assert.IsTrue(expected.Equals(actual));
        }

        static object[] ScalarDivisionCases = {
            new object[] {
                new Vector4D(+530.797553008973e-003, +779.167230102011e-003, +934.010684229183e-003, +129.906208473730e-003),
                +817.303220653433e-003,
                new Vector4D(+649.449971070224e-003, 953.339238623173e-003, 1.14279579552182e+000, 158.944936458063e-003),
            },
            new object[] {
                new Vector4D(+16.2182308193243e+000, +79.4284540683907e+000, +31.1215042044805e+000, +52.8533135506213e+000),
                +86.8694705363510e+000,
                new Vector4D(+186.696554257663e-003, 914.342559911810e-003, 358.255944376425e-003, 608.422190492165e-003),
            },
            new object[] {
                new Vector4D(+189.214503140008e-003, +248.151592823709e-003, -49.4584014975022e-003, -416.178622003067e-003),
                -415.564154489090e-003,
                new Vector4D(-455.319596495601e-003, -597.143882943408e-003, +119.015080976626e-003, +1.00147863454376e+000),
            },
            new object[] {
                new Vector4D(+3.83424352600571e+000, +49.6134716626885e+000, -42.1824471246816e+000, -5.73217302245537e+000),
                -10.0217350901104e+000,
                new Vector4D(-382.592783737560e-003, -4.95058702076930e+000, +4.20909620394059e+000, +571.974111360416e-003),
            },
        };

        [Test, TestCaseSource("EqualsCases")]
        public void Test_Equals_Operator(Vector4D lhs, Vector4D rhs, bool expected)
        {
            bool actual = (lhs == rhs);
            Assert.AreEqual(expected, actual);
        }

        [Test, TestCaseSource("EqualsCases")]
        public void Test_NotEquals_Operator(Vector4D lhs, Vector4D rhs, bool notExpected)
        {
            bool expected = !notExpected;

            bool actual = (lhs != rhs);
            Assert.AreEqual(expected, actual);
        }

        [Test, TestCaseSource("CrossCases")]
        public void Test_Cross(Vector4D lhs, Vector4D rhs, Vector4D expected)
        {
            Vector4D actual = Vector4D.Cross(lhs, rhs);

            Assert.IsTrue(expected.Equals(actual));
        }

        static object[] CrossCases = {
            new object[] {
                new Vector4D(+530.797553008973e-003, +779.167230102011e-003, +934.010684229183e-003, +129.906208473730e-003),
                new Vector4D(+568.823660872193e-003, +469.390641058206e-003, +11.9020695012414e-003, +337.122644398882e-003),
                new Vector4D(-429.142171299786e-003, +524.969787329984e-003, -194.057352579263e-003, +1.00000000000000e+000),
            },
            new object[] {
                new Vector4D(+16.2182308193243e+000, +79.4284540683907e+000, +31.1215042044805e+000, +52.8533135506213e+000),
                new Vector4D(+16.5648729499781e+000, +60.1981941401637e+000, +26.2971284540144e+000, +65.4079098476782e+000),
                new Vector4D(+215.281907505019e+000, +89.0308640068021e+000, -339.414042784394e+000, +1.00000000000000e+000),
            },
            new object[] {
                new Vector4D(+189.214503140008e-003, +248.151592823709e-003, -49.4584014975022e-003, -416.178622003067e-003),
                new Vector4D(-271.023031283181e-003, +413.337361501670e-003, -347.621981030777e-003, +325.816977489547e-003),
                new Vector4D(-65.8199431142528e-003, +79.1794863175574e-003, +145.464220390572e-003, +1.00000000000000e+000),
            },
            new object[] {
                new Vector4D(+3.83424352600571e+000, +49.6134716626885e+000, -42.1824471246816e+000, -5.73217302245537e+000),
                new Vector4D(-39.3347229819416e+000, +46.1898080855054e+000, -49.5365775865933e+000, +27.4910464711502e+000),
                new Vector4D(-509.282451092995e+000, +1.84917017426162e+003, +2.12863513664356e+003, +1.00000000000000e+000),
            },
        };
    }
}