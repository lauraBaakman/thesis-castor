using System.Collections.Generic;
using NUnit.Framework;
using Utils;
using Registration.Error;

namespace Tests.Registration.Error
{
    [TestFixture]
    public class IntersectionTermErrorTests
    {
        private double precision = 0.0001f;

        private static List<Vector4D> Xc_0 = new List<Vector4D>
        {
            new Vector4D(-1, +2, +3, 1),
            new Vector4D(+5, +3, +4, 1),
            new Vector4D(-2, +8, +2, 1),
            new Vector4D(+7, +9, -1, 1),
        };

        private static List<Vector4D> Xc_1 = new List<Vector4D>
        {
            new Vector4D(-2.168475505157833e+00, +2.889003750905794e+00, +9.753826483912550e-01, 1),
            new Vector4D(-2.453064591554630e+00, +4.348978350413981e+00, -5.006881406353799e+00, 1),
            new Vector4D(-8.223440589390870e+00, +1.746057524180481e+00, +1.151654459901695e+00, 1),
            new Vector4D(-8.060708180140827e+00, -5.440234750164819e-01, -8.107343713772211e+00, 1),
        };

        private static List<Vector4D> P = new List<Vector4D>
        {
            new Vector4D(-1.250919762305275e+00, +2.901428612819832e+00, +3.463987883622810e+00, 1),
            new Vector4D(+5.197316968394073e+00, +2.312037280884873e+00, +3.311989040672406e+00, 1),
            new Vector4D(-2.883832775663601e+00, +8.732352291549871e+00, +2.202230023486417e+00, 1),
            new Vector4D(+7.000000000000000e+00, +9.000000000000000e+00, -1.000000000000000e+00, 1),
        };

        private static Vector4D translation_0 = new Vector4D(+0.000000000000000e+00, +0.000000000000000e+00, +0.000000000000000e+00, 0);
        private static Vector4D translation_1 = new Vector4D(+2.080725777960455e+00, -4.794155057041976e+00, +4.699098521619943e+00, 0);

        private static QuaternionD quaternion_0 = new QuaternionD(+0.000000000000000e+00, +0.000000000000000e+00, +0.000000000000000e+00, +1.000000000000000e+00);
        private static QuaternionD quaternion_1 = new QuaternionD(-5.136895585373659e-01, +4.444929052673886e-01, +4.916432854084975e-01, +5.448265545376632e-01);

        private static double intersection_weight_0 = 1.0;
        private static double intersection_weight_1 = 0.2;

        private static double distance_weight_0 = 1.0;
        private static double distance_weight_1 = 0.3;

        private static int xi_0 = 0;
        private static int xi_1 = 1;

        [Test, TestCaseSource("ErrorCases")]
        public void Test_ComputeError(List<Vector4D> XCs, List<Vector4D> Ps, Vector4D translation, int xi, double intersection_weight, double distance_weight, double expected)
        {
            IntersectionTermError errorMetric = new IntersectionTermError(distance_weight, intersection_weight);
            double actual = errorMetric.ComputeError(XCs, Ps, translation, xi);

            Assert.That(actual, Is.EqualTo(expected).Within(precision));
        }

        static object[] ErrorCases =
        {
            new object[] {
                Xc_0, P,  translation_0, xi_0, intersection_weight_0, distance_weight_0, +8.587005082531676e-01
            },
            new object[] {
                Xc_0, P,  translation_0, xi_0, intersection_weight_0, distance_weight_1, +2.576101524759503e-01
            },
            new object[] {
                Xc_0, P,  translation_0, xi_0, intersection_weight_1, distance_weight_0, +8.587005082531676e-01
            },
            new object[] {
                Xc_0, P,  translation_0, xi_0, intersection_weight_1, distance_weight_1, +2.576101524759503e-01
            },
            new object[] {
                Xc_0, P,  translation_0, xi_1, intersection_weight_0, distance_weight_0, +1.717401016506335e+00
            },
            new object[] {
                Xc_0, P,  translation_0, xi_1, intersection_weight_0, distance_weight_1, +1.116310660729118e+00
            },
            new object[] {
                Xc_0, P,  translation_0, xi_1, intersection_weight_1, distance_weight_0, +1.030440609903801e+00
            },
            new object[] {
                Xc_0, P,  translation_0, xi_1, intersection_weight_1, distance_weight_1, +4.293502541265838e-01
            },
            new object[] {
                Xc_1, P,  translation_0, xi_0, intersection_weight_0, distance_weight_0, +1.464418440559290e+02
            },
            new object[] {
                Xc_1, P,  translation_0, xi_0, intersection_weight_0, distance_weight_1, +4.393255321677871e+01
            },
            new object[] {
                Xc_1, P,  translation_0, xi_0, intersection_weight_1, distance_weight_0, +1.464418440559290e+02
            },
            new object[] {
                Xc_1, P,  translation_0, xi_0, intersection_weight_1, distance_weight_1, +4.393255321677871e+01
            },
            new object[] {
                Xc_1, P,  translation_0, xi_1, intersection_weight_0, distance_weight_0, +2.928836881118581e+02
            },
            new object[] {
                Xc_1, P,  translation_0, xi_1, intersection_weight_0, distance_weight_1, +1.903743972727078e+02
            },
            new object[] {
                Xc_1, P,  translation_0, xi_1, intersection_weight_1, distance_weight_0, +1.757302128671149e+02
            },
            new object[] {
                Xc_1, P,  translation_0, xi_1, intersection_weight_1, distance_weight_1, +7.322092202796452e+01
            },
            new object[] {
                Xc_0, P,  translation_1, xi_0, intersection_weight_0, distance_weight_0, +5.354724644324055e+01
            },
            new object[] {
                Xc_0, P,  translation_1, xi_0, intersection_weight_0, distance_weight_1, +1.606417393297217e+01
            },
            new object[] {
                Xc_0, P,  translation_1, xi_0, intersection_weight_1, distance_weight_0, +5.354724644324055e+01
            },
            new object[] {
                Xc_0, P,  translation_1, xi_0, intersection_weight_1, distance_weight_1, +1.606417393297217e+01
            },
            new object[] {
                Xc_0, P,  translation_1, xi_1, intersection_weight_0, distance_weight_0, +1.070944928864811e+02
            },
            new object[] {
                Xc_0, P,  translation_1, xi_1, intersection_weight_0, distance_weight_1, +6.961142037621272e+01
            },
            new object[] {
                Xc_0, P,  translation_1, xi_1, intersection_weight_1, distance_weight_0, +6.425669573188867e+01
            },
            new object[] {
                Xc_0, P,  translation_1, xi_1, intersection_weight_1, distance_weight_1, +2.677362322162028e+01
            },
            new object[] {
                Xc_1, P,  translation_1, xi_0, intersection_weight_0, distance_weight_0, +1.559106201398074e+02
            },
            new object[] {
                Xc_1, P,  translation_1, xi_0, intersection_weight_0, distance_weight_1, +4.677318604194222e+01
            },
            new object[] {
                Xc_1, P,  translation_1, xi_0, intersection_weight_1, distance_weight_0, +1.559106201398074e+02
            },
            new object[] {
                Xc_1, P,  translation_1, xi_0, intersection_weight_1, distance_weight_1, +4.677318604194222e+01
            },
            new object[] {
                Xc_1, P,  translation_1, xi_1, intersection_weight_0, distance_weight_0, +3.118212402796148e+02
            },
            new object[] {
                Xc_1, P,  translation_1, xi_1, intersection_weight_0, distance_weight_1, +2.026838061817496e+02
            },
            new object[] {
                Xc_1, P,  translation_1, xi_1, intersection_weight_1, distance_weight_0, +1.870927441677689e+02
            },
            new object[] {
                Xc_1, P,  translation_1, xi_1, intersection_weight_1, distance_weight_1, +7.795531006990370e+01
            }
        };


        [Test, TestCaseSource("TranslationalGradientCases")]
        public void Test_TranslationalGradient(List<Vector4D> XCs, List<Vector4D> Ps, Vector4D translation, int xi, double intersection_weight, double distance_weight, Vector4D expected)
        {
            IntersectionTermError errorMetric = new IntersectionTermError(distance_weight, intersection_weight);
            Vector4D actual = errorMetric.TranslationalGradient(XCs, Ps, translation, xi);

            Assert.AreEqual(expected, actual);
        }

        static object[] TranslationalGradientCases =
        {
            new object[]{
                Xc_0, P, translation_0, xi_0, intersection_weight_0, distance_weight_0,
                new Vector4D(+1.171794461968503e-01, -1.182272731568220e-01, +2.724131527295892e-03, 1)
            },
            new object[]{
                Xc_0, P, translation_0, xi_0, intersection_weight_0, distance_weight_1,
                new Vector4D(+3.515383385905510e-02, -3.546818194704661e-02, +8.172394581887682e-04, 1)
            },
            new object[]{
                Xc_0, P, translation_0, xi_0, intersection_weight_1, distance_weight_0,
                new Vector4D(+1.171794461968503e-01, -1.182272731568220e-01, +2.724131527295892e-03, 1)
            },
            new object[]{
                Xc_0, P, translation_0, xi_0, intersection_weight_1, distance_weight_1,
                new Vector4D(+3.515383385905510e-02, -3.546818194704661e-02, +8.172394581887682e-04, 1)
            },
            new object[]{
                Xc_0, P, translation_0, xi_1, intersection_weight_0, distance_weight_0,
                new Vector4D(+2.343588923937007e-01, -2.364545463136440e-01, +5.448263054591784e-03, 1)
            },
            new object[]{
                Xc_0, P, translation_0, xi_1, intersection_weight_0, distance_weight_1,
                new Vector4D(+1.523332800559054e-01, -1.536954551038686e-01, +3.541370985484661e-03, 1)
            },
            new object[]{
                Xc_0, P, translation_0, xi_1, intersection_weight_1, distance_weight_0,
                new Vector4D(+1.406153354362204e-01, -1.418727277881864e-01, +3.268957832755073e-03, 1)
            },
            new object[]{
                Xc_0, P, translation_0, xi_1, intersection_weight_1, distance_weight_1,
                new Vector4D(+5.858972309842517e-02, -5.911363657841101e-02, +1.362065763647946e-03, 1)
            },
            new object[]{
                Xc_1, P, translation_0, xi_0, intersection_weight_0, distance_weight_0,
                new Vector4D(-3.621031662083670e+00, -1.813225254346350e+00, -2.370674369951836e+00, 1)
            },
            new object[]{
                Xc_1, P, translation_0, xi_0, intersection_weight_0, distance_weight_1,
                new Vector4D(-1.086309498625101e+00, -5.439675763039051e-01, -7.112023109855510e-01, 1)
            },
            new object[]{
                Xc_1, P, translation_0, xi_0, intersection_weight_1, distance_weight_0,
                new Vector4D(-3.621031662083670e+00, -1.813225254346350e+00, -2.370674369951836e+00, 1)
            },
            new object[]{
                Xc_1, P, translation_0, xi_0, intersection_weight_1, distance_weight_1,
                new Vector4D(-1.086309498625101e+00, -5.439675763039051e-01, -7.112023109855510e-01, 1)
            },
            new object[]{
                Xc_1, P, translation_0, xi_1, intersection_weight_0, distance_weight_0,
                new Vector4D(-7.242063324167340e+00, -3.626450508692701e+00, -4.741348739903673e+00, 1)
            },
            new object[]{
                Xc_1, P, translation_0, xi_1, intersection_weight_0, distance_weight_1,
                new Vector4D(-4.707341160708770e+00, -2.357192830650256e+00, -3.081876680937388e+00, 1)
            },
            new object[]{
                Xc_1, P, translation_0, xi_1, intersection_weight_1, distance_weight_0,
                new Vector4D(-4.345237994500403e+00, -2.175870305215621e+00, -2.844809243942204e+00, 1)
            },
            new object[]{
                Xc_1, P, translation_0, xi_1, intersection_weight_1, distance_weight_1,
                new Vector4D(-1.810515831041835e+00, -9.066126271731751e-01, -1.185337184975918e+00, 1)
            },
            new object[]{
                Xc_0, P, translation_1, xi_0, intersection_weight_0, distance_weight_0,
                new Vector4D(+1.157542335177078e+00, -2.515304801677810e+00, +2.352273392337268e+00, 1)
            },
            new object[]{
                Xc_0, P, translation_1, xi_0, intersection_weight_0, distance_weight_1,
                new Vector4D(+3.472627005531233e-01, -7.545914405033429e-01, +7.056820177011802e-01, 1)
            },
            new object[]{
                Xc_0, P, translation_1, xi_0, intersection_weight_1, distance_weight_0,
                new Vector4D(+1.157542335177078e+00, -2.515304801677810e+00, +2.352273392337268e+00, 1)
            },
            new object[]{
                Xc_0, P, translation_1, xi_0, intersection_weight_1, distance_weight_1,
                new Vector4D(+3.472627005531233e-01, -7.545914405033429e-01, +7.056820177011802e-01, 1)
            },
            new object[]{
                Xc_0, P, translation_1, xi_1, intersection_weight_0, distance_weight_0,
                new Vector4D(+2.315084670354155e+00, -5.030609603355620e+00, +4.704546784674536e+00, 1)
            },
            new object[]{
                Xc_0, P, translation_1, xi_1, intersection_weight_0, distance_weight_1,
                new Vector4D(+1.504805035730201e+00, -3.269896242181153e+00, +3.057955410038448e+00, 1)
            },
            new object[]{
                Xc_0, P, translation_1, xi_1, intersection_weight_1, distance_weight_0,
                new Vector4D(+1.389050802212493e+00, -3.018365762013372e+00, +2.822728070804721e+00, 1)
            },
            new object[]{
                Xc_0, P, translation_1, xi_1, intersection_weight_1, distance_weight_1,
                new Vector4D(+5.787711675885389e-01, -1.257652400838905e+00, +1.176136696168634e+00, 1)
            },
            new object[]{
                Xc_1, P, translation_1, xi_0, intersection_weight_0, distance_weight_0,
                new Vector4D(-2.580668773103442e+00, -4.210302782867338e+00, -2.112510914186516e-02, 1)
            },
            new object[]{
                Xc_1, P, translation_1, xi_0, intersection_weight_0, distance_weight_1,
                new Vector4D(-7.742006319310327e-01, -1.263090834860201e+00, -6.337532742559535e-03, 1)
            },
            new object[]{
                Xc_1, P, translation_1, xi_0, intersection_weight_1, distance_weight_0,
                new Vector4D(-2.580668773103442e+00, -4.210302782867338e+00, -2.112510914186516e-02, 1)
            },
            new object[]{
                Xc_1, P, translation_1, xi_0, intersection_weight_1, distance_weight_1,
                new Vector4D(-7.742006319310327e-01, -1.263090834860201e+00, -6.337532742559535e-03, 1)
            },
            new object[]{
                Xc_1, P, translation_1, xi_1, intersection_weight_0, distance_weight_0,
                new Vector4D(-5.161337546206885e+00, -8.420605565734675e+00, -4.225021828373032e-02, 1)
            },
            new object[]{
                Xc_1, P, translation_1, xi_1, intersection_weight_0, distance_weight_1,
                new Vector4D(-3.354869405034475e+00, -5.473393617727540e+00, -2.746264188442477e-02, 1)
            },
            new object[]{
                Xc_1, P, translation_1, xi_1, intersection_weight_1, distance_weight_0,
                new Vector4D(-3.096802527724131e+00, -5.052363339440806e+00, -2.535013097023814e-02, 1)
            },
            new object[]{
                Xc_1, P, translation_1, xi_1, intersection_weight_1, distance_weight_1,
                new Vector4D(-1.290334386551721e+00, -2.105151391433669e+00, -1.056255457093258e-02, 1)
            },
        };

        [Test, TestCaseSource("RotationalGradientCases")]
        public void Test_RotationalGradient(List<Vector4D> XCs, List<Vector4D> Ps, Vector4D translation, int xi, double intersection_weight, double distance_weight, QuaternionD expected)
        {
            IntersectionTermError errorMetric = new IntersectionTermError(distance_weight, intersection_weight);
            QuaternionD actual = errorMetric.RotationalGradient(XCs, Ps, translation, xi);

            Assert.AreEqual(expected, actual);
        }

        static object[] RotationalGradientCases =
        {
            new object[]{
                Xc_0, P, translation_0, xi_0, intersection_weight_0, distance_weight_0,
                new QuaternionD(+2.338391169861387e-01, -6.443364406417210e-01, -2.936510083104817e-01, 0)
            },

            new object[]{
                Xc_0, P, translation_0, xi_0, intersection_weight_0, distance_weight_1,
                new QuaternionD(+7.015173509584161e-02, -1.933009321925163e-01, -8.809530249314457e-02, 0)
            },

            new object[]{
                Xc_0, P, translation_0, xi_0, intersection_weight_1, distance_weight_0,
                new QuaternionD(+2.338391169861387e-01, -6.443364406417210e-01, -2.936510083104817e-01, 0)
            },

            new object[]{
                Xc_0, P, translation_0, xi_0, intersection_weight_1, distance_weight_1,
                new QuaternionD(+7.015173509584161e-02, -1.933009321925163e-01, -8.809530249314457e-02, 0)
            },

            new object[]{
                Xc_0, P, translation_0, xi_1, intersection_weight_0, distance_weight_0,
                new QuaternionD(+4.676782339722774e-01, -1.288672881283442e+00, -5.873020166209635e-01, 0)
            },

            new object[]{
                Xc_0, P, translation_0, xi_1, intersection_weight_0, distance_weight_1,
                new QuaternionD(+3.039908520819803e-01, -8.376373728342374e-01, -3.817463108036261e-01, 0)
            },

            new object[]{
                Xc_0, P, translation_0, xi_1, intersection_weight_1, distance_weight_0,
                new QuaternionD(+2.806069403833664e-01, -7.732037287700653e-01, -3.523812099725783e-01, 0)
            },

            new object[]{
                Xc_0, P, translation_0, xi_1, intersection_weight_1, distance_weight_1,
                new QuaternionD(+1.169195584930693e-01, -3.221682203208605e-01, -1.468255041552409e-01, 0)
            },

            new object[]{
                Xc_1, P, translation_0, xi_0, intersection_weight_0, distance_weight_0,
                new QuaternionD(-2.511400517774872e+01, +1.540744112387704e+01, +4.161630310256942e+01, 0)
            },

            new object[]{
                Xc_1, P, translation_0, xi_0, intersection_weight_0, distance_weight_1,
                new QuaternionD(-7.534201553324616e+00, +4.622232337163112e+00, +1.248489093077082e+01, 0)
            },

            new object[]{
                Xc_1, P, translation_0, xi_0, intersection_weight_1, distance_weight_0,
                new QuaternionD(-2.511400517774872e+01, +1.540744112387704e+01, +4.161630310256942e+01, 0)
            },

            new object[]{
                Xc_1, P, translation_0, xi_0, intersection_weight_1, distance_weight_1,
                new QuaternionD(-7.534201553324616e+00, +4.622232337163112e+00, +1.248489093077082e+01, 0)
            },

            new object[]{
                Xc_1, P, translation_0, xi_1, intersection_weight_0, distance_weight_0,
                new QuaternionD(-5.022801035549745e+01, +3.081488224775408e+01, +8.323260620513884e+01, 0)
            },

            new object[]{
                Xc_1, P, translation_0, xi_1, intersection_weight_0, distance_weight_1,
                new QuaternionD(-3.264820673107334e+01, +2.002967346104015e+01, +5.410119403334025e+01, 0)
            },

            new object[]{
                Xc_1, P, translation_0, xi_1, intersection_weight_1, distance_weight_0,
                new QuaternionD(-3.013680621329846e+01, +1.848892934865245e+01, +4.993956372308330e+01, 0)
            },

            new object[]{
                Xc_1, P, translation_0, xi_1, intersection_weight_1, distance_weight_1,
                new QuaternionD(-1.255700258887436e+01, +7.703720561938521e+00, +2.080815155128471e+01, 0)
            },

            new object[]{
                Xc_0, P, translation_1, xi_0, intersection_weight_0, distance_weight_0,
                new QuaternionD(+3.566719109997977e+01, -7.055856558365682e+00, -2.252449166543743e+01, 0)
            },

            new object[]{
                Xc_0, P, translation_1, xi_0, intersection_weight_0, distance_weight_1,
                new QuaternionD(+1.070015732999393e+01, -2.116756967509705e+00, -6.757347499631228e+00, 0)
            },

            new object[]{
                Xc_0, P, translation_1, xi_0, intersection_weight_1, distance_weight_0,
                new QuaternionD(+3.566719109997977e+01, -7.055856558365682e+00, -2.252449166543743e+01, 0)
            },

            new object[]{
                Xc_0, P, translation_1, xi_0, intersection_weight_1, distance_weight_1,
                new QuaternionD(+1.070015732999393e+01, -2.116756967509705e+00, -6.757347499631228e+00, 0)
            },

            new object[]{
                Xc_0, P, translation_1, xi_1, intersection_weight_0, distance_weight_0,
                new QuaternionD(+7.133438219995955e+01, -1.411171311673136e+01, -4.504898333087485e+01, 0)
            },

            new object[]{
                Xc_0, P, translation_1, xi_1, intersection_weight_0, distance_weight_1,
                new QuaternionD(+4.636734842997372e+01, -9.172613525875388e+00, -2.928183916506866e+01, 0)
            },

            new object[]{
                Xc_0, P, translation_1, xi_1, intersection_weight_1, distance_weight_0,
                new QuaternionD(+4.280062931997573e+01, -8.467027870038820e+00, -2.702938999852491e+01, 0)
            },

            new object[]{
                Xc_0, P, translation_1, xi_1, intersection_weight_1, distance_weight_1,
                new QuaternionD(+1.783359554998989e+01, -3.527928279182841e+00, -1.126224583271871e+01, 0)
            },

            new object[]{
                Xc_1, P, translation_1, xi_0, intersection_weight_0, distance_weight_0,
                new QuaternionD(-2.836745906635215e+01, +3.425158270420087e+01, +6.228224180965352e+01, 0)
            },

            new object[]{
                Xc_1, P, translation_1, xi_0, intersection_weight_0, distance_weight_1,
                new QuaternionD(-8.510237719905643e+00, +1.027547481126026e+01, +1.868467254289606e+01, 0)
            },

            new object[]{
                Xc_1, P, translation_1, xi_0, intersection_weight_1, distance_weight_0,
                new QuaternionD(-2.836745906635215e+01, +3.425158270420087e+01, +6.228224180965352e+01, 0)
            },

            new object[]{
                Xc_1, P, translation_1, xi_0, intersection_weight_1, distance_weight_1,
                new QuaternionD(-8.510237719905643e+00, +1.027547481126026e+01, +1.868467254289606e+01, 0)
            },

            new object[]{
                Xc_1, P, translation_1, xi_1, intersection_weight_0, distance_weight_0,
                new QuaternionD(-5.673491813270429e+01, +6.850316540840174e+01, +1.245644836193070e+02, 0)
            },

            new object[]{
                Xc_1, P, translation_1, xi_1, intersection_weight_0, distance_weight_1,
                new QuaternionD(-3.687769678625779e+01, +4.452705751546113e+01, +8.096691435254958e+01, 0)
            },

            new object[]{
                Xc_1, P, translation_1, xi_1, intersection_weight_1, distance_weight_0,
                new QuaternionD(-3.404095087962257e+01, +4.110189924504104e+01, +7.473869017158422e+01, 0)
            },

            new object[]{
                Xc_1, P, translation_1, xi_1, intersection_weight_1, distance_weight_1,
                new QuaternionD(-1.418372953317607e+01, +1.712579135210044e+01, +3.114112090482676e+01, 0)
            },
        };
    }
}