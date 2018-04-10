using System.Collections.Generic;
using NUnit.Framework;
using Registration;
using Registration.Error;
using UnityEngine;
using NUnit.Framework.Constraints;

namespace Tests.Registration.Error
{
    [TestFixture]
    public class WheelerIterativeErrorTests
    {
        WheelerIterativeError error;

        float precision = 0.000001f;

        [SetUp]
        public void Init()
        {
            error = new WheelerIterativeError();
        }

        [Test, TestCaseSource("TranslationGradientsCases")]
        public void Test_TranslatonalGradient(List<Vector4> XCs, List<Vector4> Ps, Vector4 translation, Vector4 expected)
        {
            Vector4 actual = error.TranslationalGradient(XCs, Ps, translation);
            Assert.That(actual.x, Is.EqualTo(expected.x).Within(precision));
            Assert.That(actual.y, Is.EqualTo(expected.y).Within(precision));
            Assert.That(actual.z, Is.EqualTo(expected.z).Within(precision));
            Assert.That(actual.w, Is.EqualTo(expected.w).Within(precision));
        }

        static object[] TranslationGradientsCases =
        {
            // M = eye(3) iteration 1
            new object[] {
                new List<Vector4> {
                    new Vector4(-1, +2, +3, +1),
                    new Vector4(+5, +3, +4, +1),
                    new Vector4(-2, +8, +2, +1),
                    new Vector4(+7, +9, -1, +1),
                },
                new List<Vector4> {
                    new Vector4(-0.999370552627214f, +2.000811583874151f, +2.999253973632587f, +1.000000000000000f),
                    new Vector4(+5.000826751712278f, +3.000264718492451f, +3.999195080809999f, +1.000000000000000f),
                    new Vector4(-2.000443003562266f, +8.000093763038411f, +2.000915013670868f, +1.000000000000000f),
                    new Vector4(+7.000929777070398f, +8.999315226163356f, -0.999058814436479f, +1.000000000000000f),
                },
                new Vector4(0, 0, 0, 0),
                new Vector4(-262.972138886736e-006f, 172.877165654395e-006f, 7.13718434064425e-006f, 0.00000000000000e+000f)
            },
            //M = TrMat, iteration 2
            new object[] {
                new List<Vector4> {
                    new Vector4(-999.297092153610e-003f, +2.00414635848319e+000f, +2.99746604574462e+000f, 1.00000000000000e+000f),
                    new Vector4(+5.00081247569012e+000f, +3.00787123351609e+000f, +3.99306714513648e+000f, 1.00000000000000e+000f),
                    new Vector4(-2.00200246547213e+000f, +8.00226292403415e+000f, +1.98891282435203e+000f, 1.00000000000000e+000f),
                    new Vector4(+6.99618105757846e+000f, +9.00107273367475e+000f, -1.01692686692670e+000f, 1.00000000000000e+000f),
                },
                new List<Vector4> {
                    new Vector4(1.99921330554036e+000f, 6.00092379616171e+000f, 9.99900926844827e+000f, 1.00000000000000e+000f),
                    new Vector4(8.00054982092942e+000f, 7.00063460644131e+000f, 11.0007373894107e+000f, 1.00000000000000e+000f),
                    new Vector4(999.168871691022e-003f, 11.9997995652982e+000f, 8.99951974080570e+000f, 1.00000000000000e+000f),
                    new Vector4(10.0006001369604e+000f, 12.9998628276549e+000f, 6.00082129518886e+000f, 1.00000000000000e+000f),
                },
                new Vector4(+333.320337086701e-006f, +444.478355432115e-006f, +777.780213718154e-006f, 0.00000000000000e+000f),
                new Vector4(-3.00062621953252e+000f, -3.99602240810656e+000f, -7.00861435617306e+000f, 0.00000000000000e+000f)
            },
            //M = RotMat, iteration 2
            new object[] {
                new List<Vector4> {
                    new Vector4(-997.713693521610e-003f, +2.00247305822741e+000f, +2.99911137452960e+000f, 1.00000000000000e+000f),
                    new Vector4(+5.00338988236606e+000f, +2.99674425416681e+000f, +3.99820129059996e+000f, 1.00000000000000e+000f),
                    new Vector4(-1.99157747707616e+000f, +8.00302393562880e+000f, +1.99630334332123e+000f, 1.00000000000000e+000f),
                    new Vector4(+7.00923583435866e+000f, +8.99227152823672e+000f, -1.00482126808981e+000f, 1.00000000000000e+000f),
                },
                new List<Vector4> {
                    new Vector4(-855.865892697126e-003f, 785.828607728723e-003f, 3.55601894052755e+000f, 1.00000000000000e+000f),
                    new Vector4(+4.38761238984696e+000f, 4.01728024699196e+000f, 3.82244444551875e+000f, 1.00000000000000e+000f),
                    new Vector4(-4.22421087597142e+000f, 5.87288134350375e+000f, 4.43422877593433e+000f, 1.00000000000000e+000f),
                    new Vector4(+2.66142447545864e+000f, 11.1232445533982e+000f, 430.047437945584e-003f, 1.00000000000000e+000f),
                },
                new Vector4(-195.306663982304e-006f, -5.57681245492593e-006f, +117.853877775728e-006f, 0.00000000000000e+000f),
                new Vector4(+1.76339830570849e+000f, +48.8139293468117e-003f, -1.06336836101354e+000f, 0.00000000000000e+000f)
            },
            //M = M = TrMat * RotMat;, iteration 2
            new object[] {
                new List<Vector4> {
                    new Vector4(-997.010668164106e-003f, 2.00661810798254e+000f, +2.99657362604084e+000f, 1.00000000000000e+000f),
                    new Vector4(+5.00420458561278e+000f, 3.00461411273726e+000f, +3.99126928418418e+000f, 1.00000000000000e+000f),
                    new Vector4(-1.99358309793502e+000f, 8.00528362129043e+000f, +1.98521046096555e+000f, 1.00000000000000e+000f),
                    new Vector4(+7.00541516386642e+000f, 8.99334192379706e+000f, -1.02174332566677e+000f, 1.00000000000000e+000f),
                },
                new List<Vector4> {
                    new Vector4(+2.14539986061905e+000f, 4.78711258576983e+000f, 10.5559818361994e+000f, 1.00000000000000e+000f),
                    new Vector4(+7.38916700444182e+000f, 8.01680638106913e+000f, 10.8214801171540e+000f, 1.00000000000000e+000f),
                    new Vector4(-1.22475359993736e+000f, 9.87368519694571e+000f, 11.4344377273698e+000f, 1.00000000000000e+000f),
                    new Vector4(+5.66211014226606e+000f, 15.1228578747998e+000f, 7.43096212442937e+000f, 1.00000000000000e+000f),
                },
                new Vector4(+138.108983538599e-006f, +438.901723294013e-006f, +895.635050143125e-006f, 0.00000000000000e+000f),
                new Vector4(-1.23808624701884e+000f, -3.94721216647100e+000f, -8.07199230485704e+000f, 0.00000000000000e+000f)
            },
        };
    }
}