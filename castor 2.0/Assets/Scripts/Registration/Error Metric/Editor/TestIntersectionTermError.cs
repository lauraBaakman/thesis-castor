using System.Collections.Generic;
using NUnit.Framework;
using Utils;
using Registration.Error;
using UnityEngine;

namespace Tests.Registration.Error
{
    [TestFixture]
    public class IntersectionTermErrorTests
    {
        private readonly double precision = 0.0001f;

        private static readonly List<Vector4> Xc_0 = new List<Vector4>
        {
            new Vector4(-1, +2, +3, 1),
            new Vector4(+5, +3, +4, 1),
            new Vector4(-2, +8, +2, 1),
            new Vector4(+7, +9, -1, 1),
        };

        private static readonly List<Vector4> Xc_1 = new List<Vector4>
        {
            new Vector4(-2.168475505157833e+00f, +2.889003750905794e+00f, +9.753826483912550e-01f, 1),
            new Vector4(-2.453064591554630e+00f, +4.348978350413981e+00f, -5.006881406353799e+00f, 1),
            new Vector4(-8.223440589390870e+00f, +1.746057524180481e+00f, +1.151654459901695e+00f, 1),
            new Vector4(-8.060708180140827e+00f, -5.440234750164819e-01f, -8.107343713772211e+00f, 1),
        };

        private static readonly List<Vector4> P = new List<Vector4>
        {
            new Vector4(-1.250919762305275e+00f, +2.901428612819832e+00f, +3.463987883622810e+00f, 1),
            new Vector4(+5.197316968394073e+00f, +2.312037280884873e+00f, +3.311989040672406e+00f, 1),
            new Vector4(-2.883832775663601e+00f, +8.732352291549871e+00f, +2.202230023486417e+00f, 1),
            new Vector4(+7.000000000000000e+00f, +9.000000000000000e+00f, -1.000000000000000e+00f, 1),
        };

        private static readonly Vector4 translation_0 = new Vector4(+0.000000000000000e+00f, +0.000000000000000e+00f, +0.000000000000000e+00f, 0);
        private static readonly Vector4 translation_1 = new Vector4(+2.080725777960455e+00f, -4.794155057041976e+00f, +4.699098521619943e+00f, 0);

        private static readonly Quaternion quaternion_0 = new Quaternion(+0.000000000000000e+00f, +0.000000000000000e+00f, +0.000000000000000e+00f, +1.000000000000000e+00f);
        private static readonly Quaternion quaternion_1 = new Quaternion(-5.136895585373659e-01f, +4.444929052673886e-01f, +4.916432854084975e-01f, +5.448265545376632e-01f);

        private static readonly float intersection_weight_0 = 1.0f;
        private static readonly float intersection_weight_1 = 0.2f;

        private static readonly float distance_weight_0 = 1.0f;
        private static readonly float distance_weight_1 = 0.3f;

        private static readonly int[] Xis_0 = { 0, 0, 0, 0 };
        private static readonly int[] Xis_1 = { 1, 1, 1, 1 };

        [Test, TestCaseSource("ErrorCases")]
        public void Test_ComputeError(List<Vector4> XCs, List<Vector4> Ps, Vector4 translation, int[] xis, float intersection_weight, float distance_weight, float expected)
        {
            IntersectionTermError errorMetric = new IntersectionTermError(distance_weight, intersection_weight);
            double actual = errorMetric.ComputeError(XCs, Ps, translation, xis);

            Assert.That(actual, Is.EqualTo(expected).Within(precision));
        }

        //Forgot the  1/4 while computing the values for the tests.
        static object[] ErrorCases =
        {
            new object[] {
                Xc_0, P,  translation_0, Xis_0, intersection_weight_0, distance_weight_0, +8.587005082531676e-01f / 4.0f
            },
            new object[] {
                Xc_0, P,  translation_0, Xis_0, intersection_weight_0, distance_weight_1, +2.576101524759503e-01f / 4.0f
            },
            new object[] {
                Xc_0, P,  translation_0, Xis_0, intersection_weight_1, distance_weight_0, +8.587005082531676e-01f / 4.0f
            },
            new object[] {
                Xc_0, P,  translation_0, Xis_0, intersection_weight_1, distance_weight_1, +2.576101524759503e-01f / 4.0f
            },
            new object[] {
                Xc_0, P,  translation_0, Xis_1, intersection_weight_0, distance_weight_0, +1.717401016506335e+00f / 4.0f
            },
            new object[] {
                Xc_0, P,  translation_0, Xis_1, intersection_weight_0, distance_weight_1, +1.116310660729118e+00f / 4.0f
            },
            new object[] {
                Xc_0, P,  translation_0, Xis_1, intersection_weight_1, distance_weight_0, +1.030440609903801e+00f / 4.0f
            },
            new object[] {
                Xc_0, P,  translation_0, Xis_1, intersection_weight_1, distance_weight_1, +4.293502541265838e-01f / 4.0f
            },
            new object[] {
                Xc_1, P,  translation_0, Xis_0, intersection_weight_0, distance_weight_0, +1.464418440559290e+02f / 4.0f
            },
            new object[] {
                Xc_1, P,  translation_0, Xis_0, intersection_weight_0, distance_weight_1, +4.393255321677871e+01f / 4.0f
            },
            new object[] {
                Xc_1, P,  translation_0, Xis_0, intersection_weight_1, distance_weight_0, +1.464418440559290e+02f / 4.0f
            },
            new object[] {
                Xc_1, P,  translation_0, Xis_0, intersection_weight_1, distance_weight_1, +4.393255321677871e+01f / 4.0f
            },
            new object[] {
                Xc_1, P,  translation_0, Xis_1, intersection_weight_0, distance_weight_0, +2.928836881118581e+02f / 4.0f
            },
            new object[] {
                Xc_1, P,  translation_0, Xis_1, intersection_weight_0, distance_weight_1, +1.903743972727078e+02f / 4.0f
            },
            new object[] {
                Xc_1, P,  translation_0, Xis_1, intersection_weight_1, distance_weight_0, +1.757302128671149e+02f / 4.0f
            },
            new object[] {
                Xc_1, P,  translation_0, Xis_1, intersection_weight_1, distance_weight_1, +7.322092202796452e+01f / 4.0f
            },
            new object[] {
                Xc_0, P,  translation_1, Xis_0, intersection_weight_0, distance_weight_0, +5.354724644324055e+01f / 4.0f
            },
            new object[] {
                Xc_0, P,  translation_1, Xis_0, intersection_weight_0, distance_weight_1, +1.606417393297217e+01f / 4.0f
            },
            new object[] {
                Xc_0, P,  translation_1, Xis_0, intersection_weight_1, distance_weight_0, +5.354724644324055e+01f / 4.0f
            },
            new object[] {
                Xc_0, P,  translation_1, Xis_0, intersection_weight_1, distance_weight_1, +1.606417393297217e+01f / 4.0f
            },
            new object[] {
                Xc_0, P,  translation_1, Xis_1, intersection_weight_0, distance_weight_0, +1.070944928864811e+02f / 4.0f
            },
            new object[] {
                Xc_0, P,  translation_1, Xis_1, intersection_weight_0, distance_weight_1, +6.961142037621272e+01f / 4.0f
            },
            new object[] {
                Xc_0, P,  translation_1, Xis_1, intersection_weight_1, distance_weight_0, +6.425669573188867e+01f / 4.0f
            },
            new object[] {
                Xc_0, P,  translation_1, Xis_1, intersection_weight_1, distance_weight_1, +2.677362322162028e+01f / 4.0f
            },
            new object[] {
                Xc_1, P,  translation_1, Xis_0, intersection_weight_0, distance_weight_0, +1.559106201398074e+02f / 4.0f
            },
            new object[] {
                Xc_1, P,  translation_1, Xis_0, intersection_weight_0, distance_weight_1, +4.677318604194222e+01f / 4.0f
            },
            new object[] {
                Xc_1, P,  translation_1, Xis_0, intersection_weight_1, distance_weight_0, +1.559106201398074e+02f / 4.0f
            },
            new object[] {
                Xc_1, P,  translation_1, Xis_0, intersection_weight_1, distance_weight_1, +4.677318604194222e+01f / 4.0f
            },
            new object[] {
                Xc_1, P,  translation_1, Xis_1, intersection_weight_0, distance_weight_0, +3.118212402796148e+02f / 4.0f
            },
            new object[] {
                Xc_1, P,  translation_1, Xis_1, intersection_weight_0, distance_weight_1, +2.026838061817496e+02f / 4.0f
            },
            new object[] {
                Xc_1, P,  translation_1, Xis_1, intersection_weight_1, distance_weight_0, +1.870927441677689e+02f / 4.0f
            },
            new object[] {
                Xc_1, P,  translation_1, Xis_1, intersection_weight_1, distance_weight_1, +7.795531006990370e+01f / 4.0f
            }
        };


        [Test, TestCaseSource("TranslationalGradientCases")]
        public void Test_TranslationalGradient(List<Vector4> XCs, List<Vector4> Ps, Vector4 translation, int[] xis, float intersection_weight, float distance_weight, Vector4 expected)
        {
            IntersectionTermError errorMetric = new IntersectionTermError(distance_weight, intersection_weight);
            Vector4 actual = errorMetric.TranslationalGradient(XCs, Ps, translation, xis);

            Assert.AreEqual(expected, actual);
        }

        static object[] TranslationalGradientCases =
        {
            new object[]{
                Xc_0, P, translation_0, Xis_0, intersection_weight_0, distance_weight_0,
                new Vector4(+1.171794461968503e-01f, -1.182272731568220e-01f, +2.724131527295892e-03f, 0)
            },
            new object[]{
                Xc_0, P, translation_0, Xis_0, intersection_weight_0, distance_weight_1,
                new Vector4(+3.515383385905510e-02f, -3.546818194704661e-02f, +8.172394581887682e-04f, 0)
            },
            new object[]{
                Xc_0, P, translation_0, Xis_0, intersection_weight_1, distance_weight_0,
                new Vector4(+1.171794461968503e-01f, -1.182272731568220e-01f, +2.724131527295892e-03f, 0)
            },
            new object[]{
                Xc_0, P, translation_0, Xis_0, intersection_weight_1, distance_weight_1,
                new Vector4(+3.515383385905510e-02f, -3.546818194704661e-02f, +8.172394581887682e-04f, 0)
            },
            new object[]{
                Xc_0, P, translation_0, Xis_1, intersection_weight_0, distance_weight_0,
                new Vector4(+2.343588923937007e-01f, -2.364545463136440e-01f, +5.448263054591784e-03f, 0)
            },
            new object[]{
                Xc_0, P, translation_0, Xis_1, intersection_weight_0, distance_weight_1,
                new Vector4(+1.523332800559054e-01f, -1.536954551038686e-01f, +3.541370985484661e-03f, 0)
            },
            new object[]{
                Xc_0, P, translation_0, Xis_1, intersection_weight_1, distance_weight_0,
                new Vector4(+1.406153354362204e-01f, -1.418727277881864e-01f, +3.268957832755073e-03f, 0)
            },
            new object[]{
                Xc_0, P, translation_0, Xis_1, intersection_weight_1, distance_weight_1,
                new Vector4(+5.858972309842517e-02f, -5.911363657841101e-02f, +1.362065763647946e-03f, 0)
            },
            new object[]{
                Xc_1, P, translation_0, Xis_0, intersection_weight_0, distance_weight_0,
                new Vector4(-3.621031662083670e+00f, -1.813225254346350e+00f, -2.370674369951836e+00f, 0)
            },
            new object[]{
                Xc_1, P, translation_0, Xis_0, intersection_weight_0, distance_weight_1,
                new Vector4(-1.086309498625101e+00f, -5.439675763039051e-01f, -7.112023109855510e-01f, 0)
            },
            new object[]{
                Xc_1, P, translation_0, Xis_0, intersection_weight_1, distance_weight_0,
                new Vector4(-3.621031662083670e+00f, -1.813225254346350e+00f, -2.370674369951836e+00f, 0)
            },
            new object[]{
                Xc_1, P, translation_0, Xis_0, intersection_weight_1, distance_weight_1,
                new Vector4(-1.086309498625101e+00f, -5.439675763039051e-01f, -7.112023109855510e-01f, 0)
            },
            new object[]{
                Xc_1, P, translation_0, Xis_1, intersection_weight_0, distance_weight_0,
                new Vector4(-7.242063324167340e+00f, -3.626450508692701e+00f, -4.741348739903673e+00f, 0)
            },
            new object[]{
                Xc_1, P, translation_0, Xis_1, intersection_weight_0, distance_weight_1,
                new Vector4(-4.707341160708770e+00f, -2.357192830650256e+00f, -3.081876680937388e+00f, 0)
            },
            new object[]{
                Xc_1, P, translation_0, Xis_1, intersection_weight_1, distance_weight_0,
                new Vector4(-4.345237994500403e+00f, -2.175870305215621e+00f, -2.844809243942204e+00f, 0)
            },
            new object[]{
                Xc_1, P, translation_0, Xis_1, intersection_weight_1, distance_weight_1,
                new Vector4(-1.810515831041835e+00f, -9.066126271731751e-01f, -1.185337184975918e+00f, 0)
            },
            new object[]{
                Xc_0, P, translation_1, Xis_0, intersection_weight_0, distance_weight_0,
                new Vector4(+1.157542335177078e+00f, -2.515304801677810e+00f, +2.352273392337268e+00f, 0)
            },
            new object[]{
                Xc_0, P, translation_1, Xis_0, intersection_weight_0, distance_weight_1,
                new Vector4(+3.472627005531233e-01f, -7.545914405033429e-01f, +7.056820177011802e-01f, 0)
            },
            new object[]{
                Xc_0, P, translation_1, Xis_0, intersection_weight_1, distance_weight_0,
                new Vector4(+1.157542335177078e+00f, -2.515304801677810e+00f, +2.352273392337268e+00f, 0)
            },
            new object[]{
                Xc_0, P, translation_1, Xis_0, intersection_weight_1, distance_weight_1,
                new Vector4(+3.472627005531233e-01f, -7.545914405033429e-01f, +7.056820177011802e-01f, 0)
            },
            new object[]{
                Xc_0, P, translation_1, Xis_1, intersection_weight_0, distance_weight_0,
                new Vector4(+2.315084670354155e+00f, -5.030609603355620e+00f, +4.704546784674536e+00f, 0)
            },
            new object[]{
                Xc_0, P, translation_1, Xis_1, intersection_weight_0, distance_weight_1,
                new Vector4(+1.504805035730201e+00f, -3.269896242181153e+00f, +3.057955410038448e+00f, 0)
            },
            new object[]{
                Xc_0, P, translation_1, Xis_1, intersection_weight_1, distance_weight_0,
                new Vector4(+1.389050802212493e+00f, -3.018365762013372e+00f, +2.822728070804721e+00f, 0)
            },
            new object[]{
                Xc_0, P, translation_1, Xis_1, intersection_weight_1, distance_weight_1,
                new Vector4(+5.787711675885389e-01f, -1.257652400838905e+00f, +1.176136696168634e+00f, 0)
            },
            new object[]{
                Xc_1, P, translation_1, Xis_0, intersection_weight_0, distance_weight_0,
                new Vector4(-2.580668773103442e+00f, -4.210302782867338e+00f, -2.112510914186516e-02f, 0)
            },
            new object[]{
                Xc_1, P, translation_1, Xis_0, intersection_weight_0, distance_weight_1,
                new Vector4(-7.742006319310327e-01f, -1.263090834860201e+00f, -6.337532742559535e-03f, 0)
            },
            new object[]{
                Xc_1, P, translation_1, Xis_0, intersection_weight_1, distance_weight_0,
                new Vector4(-2.580668773103442e+00f, -4.210302782867338e+00f, -2.112510914186516e-02f, 0)
            },
            new object[]{
                Xc_1, P, translation_1, Xis_0, intersection_weight_1, distance_weight_1,
                new Vector4(-7.742006319310327e-01f, -1.263090834860201e+00f, -6.337532742559535e-03f, 0)
            },
            new object[]{
                Xc_1, P, translation_1, Xis_1, intersection_weight_0, distance_weight_0,
                new Vector4(-5.161337546206885e+00f, -8.420605565734675e+00f, -4.225021828373032e-02f, 0)
            },
            new object[]{
                Xc_1, P, translation_1, Xis_1, intersection_weight_0, distance_weight_1,
                new Vector4(-3.354869405034475e+00f, -5.473393617727540e+00f, -2.746264188442477e-02f, 0)
            },
            new object[]{
                Xc_1, P, translation_1, Xis_1, intersection_weight_1, distance_weight_0,
                new Vector4(-3.096802527724131e+00f, -5.052363339440806e+00f, -2.535013097023814e-02f, 0)
            },
            new object[]{
                Xc_1, P, translation_1, Xis_1, intersection_weight_1, distance_weight_1,
                new Vector4(-1.290334386551721e+00f, -2.105151391433669e+00f, -1.056255457093258e-02f, 0)
            },
        };

        [Test, TestCaseSource("RotationalGradientCases")]
        public void Test_RotationalGradient(List<Vector4> XCs, List<Vector4> Ps, Vector4 translation, int[] xis, float intersection_weight, float distance_weight, Quaternion expected)
        {
            IntersectionTermError errorMetric = new IntersectionTermError(distance_weight, intersection_weight);
            Quaternion actual = errorMetric.RotationalGradient(XCs, Ps, translation, xis);

            for (int i = 0; i < 4; i++) Assert.That(actual[i], Is.EqualTo(expected[i]).Within(precision));
        }

        static object[] RotationalGradientCases =
        {
            new object[]{
                Xc_0, P, translation_0, Xis_0, intersection_weight_0, distance_weight_0,
                new Quaternion(+2.338391169861387e-01f, -6.443364406417210e-01f, -2.936510083104817e-01f, 0)
            },
            new object[]{
                Xc_0, P, translation_0, Xis_0, intersection_weight_0, distance_weight_1,
                new Quaternion(+7.015173509584161e-02f, -1.933009321925163e-01f, -8.809530249314457e-02f, 0)
            },

            new object[]{
                Xc_0, P, translation_0, Xis_0, intersection_weight_1, distance_weight_0,
                new Quaternion(+2.338391169861387e-01f, -6.443364406417210e-01f, -2.936510083104817e-01f, 0)
            },

            new object[]{
                Xc_0, P, translation_0, Xis_0, intersection_weight_1, distance_weight_1,
                new Quaternion(+7.015173509584161e-02f, -1.933009321925163e-01f, -8.809530249314457e-02f, 0)
            },

            new object[]{
                Xc_0, P, translation_0, Xis_1, intersection_weight_0, distance_weight_0,
                new Quaternion(+4.676782339722774e-01f, -1.288672881283442e+00f, -5.873020166209635e-01f, 0)
            },

            new object[]{
                Xc_0, P, translation_0, Xis_1, intersection_weight_0, distance_weight_1,
                new Quaternion(+3.039908520819803e-01f, -8.376373728342374e-01f, -3.817463108036261e-01f, 0)
            },

            new object[]{
                Xc_0, P, translation_0, Xis_1, intersection_weight_1, distance_weight_0,
                new Quaternion(+2.806069403833664e-01f, -7.732037287700653e-01f, -3.523812099725783e-01f, 0)
            },

            new object[]{
                Xc_0, P, translation_0, Xis_1, intersection_weight_1, distance_weight_1,
                new Quaternion(+1.169195584930693e-01f, -3.221682203208605e-01f, -1.468255041552409e-01f, 0)
            },

            new object[]{
                Xc_1, P, translation_0, Xis_0, intersection_weight_0, distance_weight_0,
                new Quaternion(-2.511400517774872e+01f, +1.540744112387704e+01f, +4.161630310256942e+01f, 0)
            },

            new object[]{
                Xc_1, P, translation_0, Xis_0, intersection_weight_0, distance_weight_1,
                new Quaternion(-7.534201553324616e+00f, +4.622232337163112e+00f, +1.248489093077082e+01f, 0)
            },

            new object[]{
                Xc_1, P, translation_0, Xis_0, intersection_weight_1, distance_weight_0,
                new Quaternion(-2.511400517774872e+01f, +1.540744112387704e+01f, +4.161630310256942e+01f, 0)
            },

            new object[]{
                Xc_1, P, translation_0, Xis_0, intersection_weight_1, distance_weight_1,
                new Quaternion(-7.534201553324616e+00f, +4.622232337163112e+00f, +1.248489093077082e+01f, 0)
            },

            new object[]{
                Xc_1, P, translation_0, Xis_1, intersection_weight_0, distance_weight_0,
                new Quaternion(-5.022801035549745e+01f, +3.081488224775408e+01f, +8.323260620513884e+01f, 0)
            },

            new object[]{
                Xc_1, P, translation_0, Xis_1, intersection_weight_0, distance_weight_1,
                new Quaternion(-3.264820673107334e+01f, +2.002967346104015e+01f, +5.410119403334025e+01f, 0)
            },

            new object[]{
                Xc_1, P, translation_0, Xis_1, intersection_weight_1, distance_weight_0,
                new Quaternion(-3.013680621329846e+01f, +1.848892934865245e+01f, +4.993956372308330e+01f, 0)
            },

            new object[]{
                Xc_1, P, translation_0, Xis_1, intersection_weight_1, distance_weight_1,
                new Quaternion(-1.255700258887436e+01f, +7.703720561938521e+00f, +2.080815155128471e+01f, 0)
            },

            new object[]{
                Xc_0, P, translation_1, Xis_0, intersection_weight_0, distance_weight_0,
                new Quaternion(+3.566719109997977e+01f, -7.055856558365682e+00f, -2.252449166543743e+01f, 0)
            },

            new object[]{
                Xc_0, P, translation_1, Xis_0, intersection_weight_0, distance_weight_1,
                new Quaternion(+1.070015732999393e+01f, -2.116756967509705e+00f, -6.757347499631228e+00f, 0)
            },

            new object[]{
                Xc_0, P, translation_1, Xis_0, intersection_weight_1, distance_weight_0,
                new Quaternion(+3.566719109997977e+01f, -7.055856558365682e+00f, -2.252449166543743e+01f, 0)
            },

            new object[]{
                Xc_0, P, translation_1, Xis_0, intersection_weight_1, distance_weight_1,
                new Quaternion(+1.070015732999393e+01f, -2.116756967509705e+00f, -6.757347499631228e+00f, 0)
            },

            new object[]{
                Xc_0, P, translation_1, Xis_1, intersection_weight_0, distance_weight_0,
                new Quaternion(+7.133438219995955e+01f, -1.411171311673136e+01f, -4.504898333087485e+01f, 0)
            },

            new object[]{
                Xc_0, P, translation_1, Xis_1, intersection_weight_0, distance_weight_1,
                new Quaternion(+4.636734842997372e+01f, -9.172613525875388e+00f, -2.928183916506866e+01f, 0)
            },

            new object[]{
                Xc_0, P, translation_1, Xis_1, intersection_weight_1, distance_weight_0,
                new Quaternion(+4.280062931997573e+01f, -8.467027870038820e+00f, -2.702938999852491e+01f, 0)
            },

            new object[]{
                Xc_0, P, translation_1, Xis_1, intersection_weight_1, distance_weight_1,
                new Quaternion(+1.783359554998989e+01f, -3.527928279182841e+00f, -1.126224583271871e+01f, 0)
            },

            new object[]{
                Xc_1, P, translation_1, Xis_0, intersection_weight_0, distance_weight_0,
                new Quaternion(-2.836745906635215e+01f, +3.425158270420087e+01f, +6.228224180965352e+01f, 0)
            },

            new object[]{
                Xc_1, P, translation_1, Xis_0, intersection_weight_0, distance_weight_1,
                new Quaternion(-8.510237719905643e+00f, +1.027547481126026e+01f, +1.868467254289606e+01f, 0)
            },

            new object[]{
                Xc_1, P, translation_1, Xis_0, intersection_weight_1, distance_weight_0,
                new Quaternion(-2.836745906635215e+01f, +3.425158270420087e+01f, +6.228224180965352e+01f, 0)
            },

            new object[]{
                Xc_1, P, translation_1, Xis_0, intersection_weight_1, distance_weight_1,
                new Quaternion(-8.510237719905643e+00f, +1.027547481126026e+01f, +1.868467254289606e+01f, 0)
            },

            new object[]{
                Xc_1, P, translation_1, Xis_1, intersection_weight_0, distance_weight_0,
                new Quaternion(-5.673491813270429e+01f, +6.850316540840174e+01f, +1.245644836193070e+02f, 0)
            },

            new object[]{
                Xc_1, P, translation_1, Xis_1, intersection_weight_0, distance_weight_1,
                new Quaternion(-3.687769678625779e+01f, +4.452705751546113e+01f, +8.096691435254958e+01f, 0)
            },

            new object[]{
                Xc_1, P, translation_1, Xis_1, intersection_weight_1, distance_weight_0,
                new Quaternion(-3.404095087962257e+01f, +4.110189924504104e+01f, +7.473869017158422e+01f, 0)
            },

            new object[]{
                Xc_1, P, translation_1, Xis_1, intersection_weight_1, distance_weight_1,
                new Quaternion(-1.418372953317607e+01f, +1.712579135210044e+01f, +3.114112090482676e+01f, 0)
            },
        };
    }
}