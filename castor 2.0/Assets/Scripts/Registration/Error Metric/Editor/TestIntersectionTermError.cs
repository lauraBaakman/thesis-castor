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

    }
}