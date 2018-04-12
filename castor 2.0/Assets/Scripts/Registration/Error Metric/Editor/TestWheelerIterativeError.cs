using System.Collections.Generic;
using NUnit.Framework;
using Registration.Error;
using Utils;

namespace Tests.Registration.Error
{
    [TestFixture]
    public class WheelerIterativeErrorTests
    {
        WheelerIterativeError error;

        float precision = 0.0001f;

        [SetUp]
        public void Init()
        {
            error = new WheelerIterativeError();
        }

        [Test, TestCaseSource("TranslationGradientsCases")]
        public void Test_TranslatonalGradient(List<Vector4D> XCs, List<Vector4D> Ps, Vector4D translation, Vector4D expected)
        {
            Vector4D actual = error.TranslationalGradient(XCs, Ps, translation);
            Assert.That(actual.x, Is.EqualTo(expected.x).Within(precision));
            Assert.That(actual.y, Is.EqualTo(expected.y).Within(precision));
            Assert.That(actual.z, Is.EqualTo(expected.z).Within(precision));
            Assert.That(actual.w, Is.EqualTo(expected.w).Within(precision));
        }

        static object[] TranslationGradientsCases =
        {
            // M = eye(3) iteration 1
            new object[] {
                new List<Vector4D> {
                    new Vector4D(-1, +2, +3, +1),
                    new Vector4D(+5, +3, +4, +1),
                    new Vector4D(-2, +8, +2, +1),
                    new Vector4D(+7, +9, -1, +1),
                },
                new List<Vector4D> {
                    new Vector4D(-1.00025091976231e+000f, +2.00090142861282e+000f, +3.00046398788362e+000f, 1),
                    new Vector4D(+5.00019731696839e+000f, +2.99931203728088e+000f, +3.99931198904067e+000f, 1),
                    new Vector4D(-2.00088383277566e+000f, +8.00073235229155e+000f, +2.00020223002349e+000f, 1),
                    new Vector4D(+7.00041614515559e+000f, +8.99904116898859e+000f, -999.060180295676e-003f, 1),
                },
                new Vector4D(0, 0, 0, 0),
                new Vector4D(+130.322603495769e-006f, +3.25320653837835e-006f, -229.506663026402e-006f, 0),
            },
            //M = TrMat, iteration 2
            new object[] {
               new List<Vector4D> {
                   new Vector4D(-999.297740159048e-003f, +2.00414649813341e+000f, +2.99746573634005e+000f, 1),
                   new Vector4D(+5.00081156173036e+000f, +3.00787236959591e+000f, +3.99306743397672e+000f, 1),
                   new Vector4D(-2.00200389656587e+000f, +8.00226281580464e+000f, +1.98891181929351e+000f, 1),
                   new Vector4D(+6.99617982490228e+000f, +9.00107368512413e+000f, -1.01692692588808e+000f, 1),
               },
               new List<Vector4D> {
                   new Vector4D(+1.99974908023769e+000f, +6.00090142861282e+000f, +10.0004639878836e+000f, 1),
                   new Vector4D(+8.00019731696839e+000f, +6.99931203728088e+000f, +10.9993119890407e+000f, 1),
                   new Vector4D(+999.116167224336e-003f, +12.0007323522915e+000f, +9.00020223002349e+000f, 1),
                   new Vector4D(+10.0004161451556e+000f, +12.9990411689886e+000f, +6.00093981970432e+000f, 1),
               },
               new Vector4D(+333.318853044056e-006f, +444.444082977051e-006f, +777.803278518114e-006f, 0),
               new Vector4D(-3.00061392106653e+000f, -3.99571346054596e+000f, -7.00882218745396e+000f, 0),
            },
            //M = RotMat, iteration 2
            new object[] {
               new List<Vector4D> {
                   new Vector4D(-997.714047669903e-003f, +2.00247297033591e+000f, +2.99911131539932e+000f, 1),
                   new Vector4D(+5.00338935674703e+000f, +2.99674517248244e+000f, +3.99820126006604e+000f, 1),
                   new Vector4D(-1.99157880333413e+000f, +8.00302366009195e+000f, +1.99630312480813e+000f, 1),
                   new Vector4D(+7.00923437313759e+000f, +8.99227264900197e+000f, -1.00482143111982e+000f, 1),
               },
               new List<Vector4D> {
                   new Vector4D(-855.480506516037e-003f, +787.202430508499e-003f, +3.55719185045041e+000f, 1),
                   new Vector4D(+4.38853756969794e+000f, +4.01585369985757e+000f, +3.82159702538470e+000f, 1),
                   new Vector4D(-4.22519442915076e+000f, +5.87432378619885e+000f, +4.43372494372237e+000f, 1),
                   new Vector4D(+2.66159651035126e+000f, +11.1225838176250e+000f, +430.960758570174e-003f, 1),
               },
               new Vector4D(-195.292801544933e-006f, -5.55656293916782e-006f, +117.874293836879e-006f, 0),
               new Vector4D(+1.76327264082300e+000f, +48.6321228676377e-003f, -1.06355220294966e+000f, 0),
            },
            //M = M = TrMat * RotMat, iteration 2
            new object[] {
               new List<Vector4D> {
                   new Vector4D(-997.010341554109e-003f, +2.00661811888831e+000f, +2.99657372740657e+000f, 1),
                   new Vector4D(+5.00420503603016e+000f, +3.00461383363327e+000f, +3.99126892956487e+000f, 1),
                   new Vector4D(-1.99358257198830e+000f, +8.00528369033141e+000f, +1.98521071072530e+000f, 1),
                   new Vector4D(+7.00541550126063e+000f, +8.99334161099329e+000f, -1.02174376566426e+000f, 1),
               },
               new List<Vector4D> {
                   new Vector4D(+2.14451949348396e+000f, +4.78720243050850e+000f, +10.5571918504504e+000f, 1),
                   new Vector4D(+7.38853756969794e+000f, +8.01585369985757e+000f, +10.8215970253847e+000f, 1),
                   new Vector4D(-1.22519442915076e+000f, +9.87432378619885e+000f, +11.4337249437224e+000f, 1),
                   new Vector4D(+5.66159651035126e+000f, +15.1225838176250e+000f, +7.43096075857017e+000f, 1),
               },
               new Vector4D(+138.040531788400e-006f, +438.887881505277e-006f, +895.652071614657e-006f, 0),
               new Vector4D(-1.23746983962671e+000f, -3.94708773220442e+000f, -8.07214559195218e+000f, 0),
            },
        };

        [Test, TestCaseSource("RotationGradientCases")]
        public void Test_RotationalGradient(List<Vector4D> XCs, List<Vector4D> Ps, Vector4D translation, QuaternionD expected)
        {
            QuaternionD actual = error.RotationalGradient(XCs, Ps, translation);
            Assert.That(actual.x, Is.EqualTo(expected.x).Within(precision));
            Assert.That(actual.y, Is.EqualTo(expected.y).Within(precision));
            Assert.That(actual.z, Is.EqualTo(expected.z).Within(precision));
            Assert.That(actual.w, Is.EqualTo(expected.w).Within(precision));
        }

        static object[] RotationGradientCases =
        {
            // M = eye(3) iteration 1
            new object[] {
                new List<Vector4D> {
                    new Vector4D(-1, +2, +3, +1),
                    new Vector4D(+5, +3, +4, +1),
                    new Vector4D(-2, +8, +2, +1),
                    new Vector4D(+7, +9, -1, +1),
                },
                new List<Vector4D> {
                    new Vector4D(-1.00025091976231e+000f, +2.00090142861282e+000f, +3.00046398788362e+000f, 1),
                    new Vector4D(+5.00019731696839e+000f, +2.99931203728088e+000f, +3.99931198904067e+000f, 1),
                    new Vector4D(-2.00088383277566e+000f, +8.00073235229155e+000f, +2.00020223002349e+000f, 1),
                    new Vector4D(+7.00041614515559e+000f, +8.99904116898859e+000f, -999.060180295676e-003f, 1),
                },
                new Vector4D(0, 0, 0, 0),
                new QuaternionD(3.28209492978226e-003, -2.20876866164743e-003, -4.64125972346974e-003, 0),
            },
            //M = TrMat, iteration 2
            new object[] {
               new List<Vector4D> {
                   new Vector4D(-999.297740159048e-003f, +2.00414649813341e+000f, +2.99746573634005e+000f, 1),
                   new Vector4D(+5.00081156173036e+000f, +3.00787236959591e+000f, +3.99306743397672e+000f, 1),
                   new Vector4D(-2.00200389656587e+000f, +8.00226281580464e+000f, +1.98891181929351e+000f, 1),
                   new Vector4D(+6.99617982490228e+000f, +9.00107368512413e+000f, -1.01692692588808e+000f, 1),
               },
               new List<Vector4D> {
                   new Vector4D(+1.99974908023769e+000f, +6.00090142861282e+000f, +10.0004639878836e+000f, 1),
                   new Vector4D(+8.00019731696839e+000f, +6.99931203728088e+000f, +10.9993119890407e+000f, 1),
                   new Vector4D(+999.116167224336e-003f, +12.0007323522915e+000f, +9.00020223002349e+000f, 1),
                   new Vector4D(+10.0004161451556e+000f, +12.9990411689886e+000f, +6.00093981970432e+000f, 1),
               },
                new Vector4D(+333.318853044056e-006, +444.444082977051e-006, +777.803278518114e-006, 0),
                new QuaternionD(+61.2823385485841e+000, -19.6079743092595e+000, -15.0770126606125e+000, 0),
            },
            //M = RotMat, iteration 2
            new object[] {
               new List<Vector4D> {
                   new Vector4D(-997.714047669903e-003f, +2.00247297033591e+000f, +2.99911131539932e+000f, 1),
                   new Vector4D(+5.00338935674703e+000f, +2.99674517248244e+000f, +3.99820126006604e+000f, 1),
                   new Vector4D(-1.99157880333413e+000f, +8.00302366009195e+000f, +1.99630312480813e+000f, 1),
                   new Vector4D(+7.00923437313759e+000f, +8.99227264900197e+000f, -1.00482143111982e+000f, 1),
               },
               new List<Vector4D> {
                   new Vector4D(-855.480506516037e-003f, +787.202430508499e-003f, +3.55719185045041e+000f, 1),
                   new Vector4D(+4.38853756969794e+000f, +4.01585369985757e+000f, +3.82159702538470e+000f, 1),
                   new Vector4D(-4.22519442915076e+000f, +5.87432378619885e+000f, +4.43372494372237e+000f, 1),
                   new Vector4D(+2.66159651035126e+000f, +11.1225838176250e+000f, +430.960758570174e-003f, 1),
               },
                new Vector4D(-195.292801544933e-006, -5.55656293916782e-006, +117.874293836879e-006, 0),
                new QuaternionD(+19.4817994388753e+000, -2.94423452884511e+000, +42.0035634391288e+000, 0),
            },
            //M = M = TrMat * RotMat, iteration 2
            new object[] {
               new List<Vector4D> {
                   new Vector4D(-997.010341554109e-003f, +2.00661811888831e+000f, +2.99657372740657e+000f, 1),
                   new Vector4D(+5.00420503603016e+000f, +3.00461383363327e+000f, +3.99126892956487e+000f, 1),
                   new Vector4D(-1.99358257198830e+000f, +8.00528369033141e+000f, +1.98521071072530e+000f, 1),
                   new Vector4D(+7.00541550126063e+000f, +8.99334161099329e+000f, -1.02174376566426e+000f, 1),
               },
               new List<Vector4D> {
                   new Vector4D(+2.14451949348396e+000f, +4.78720243050850e+000f, +10.5571918504504e+000f, 1),
                   new Vector4D(+7.38853756969794e+000f, +8.01585369985757e+000f, +10.8215970253847e+000f, 1),
                   new Vector4D(-1.22519442915076e+000f, +9.87432378619885e+000f, +11.4337249437224e+000f, 1),
                   new Vector4D(+5.66159651035126e+000f, +15.1225838176250e+000f, +7.43096075857017e+000f, 1),
               },
                new Vector4D(+138.040531788400e-006, +438.887881505277e-006, +895.652071614657e-006, 0),
                new QuaternionD(+80.7761445472091e+000, -22.5924461555999e+000, +26.9911459768104e+000, 0),
            },
        };
    }
}