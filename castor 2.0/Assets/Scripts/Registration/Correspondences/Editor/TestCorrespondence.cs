using UnityEngine;
using NUnit.Framework;

using Registration;

namespace Tests
{
    [TestFixture]
    public class CorrespondenceTests
    {
        [Test]
        public void TestEquals_Equals()
        {
            Correspondence thisCorrespondence = new Correspondence(
                new Point(new Vector3(1.0f, 2.0f, 2.0f)),
                new Point(new Vector3(3.0f, 4.0f, 5.0f))
            );
            Correspondence otherCorrespondence = new Correspondence(
                new Point(new Vector3(1.0f, 2.0f, 2.0f)),
                new Point(new Vector3(3.0f, 4.0f, 5.0f))
            );

            bool expected = true;
            bool actual = thisCorrespondence.Equals(otherCorrespondence);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void TestEquals_StaticPointNotEqual()
        {
            Correspondence thisCorrespondence = new Correspondence(
                new Point(new Vector3(1.0f, 2.0f, 2.0f)),
                new Point(new Vector3(3.0f, 4.0f, 5.0f))
            );
            Correspondence otherCorrespondence = new Correspondence(
                new Point(new Vector3(1.0f, 3.0f, 2.0f)),
                new Point(new Vector3(3.0f, 4.0f, 5.0f))
            );

            bool expected = false;
            bool actual = thisCorrespondence.Equals(otherCorrespondence);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void TestEquals_ModelPointNotEqual()
        {
            Correspondence thisCorrespondence = new Correspondence(
                new Point(new Vector3(1.0f, 2.0f, 2.0f)),
                new Point(new Vector3(3.0f, 4.0f, 5.0f))
            );
            Correspondence otherCorrespondence = new Correspondence(
                new Point(new Vector3(1.0f, 2.0f, 2.0f)),
                new Point(new Vector3(3.0f, 7.0f, 5.0f))
            );

            bool expected = false;
            bool actual = thisCorrespondence.Equals(otherCorrespondence);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void TestGetPoint_ModelPoint()
        {
            Point staticPoint = Auxilaries.RandomPoint();
            Point modelPoint = Auxilaries.RandomPoint();

            Correspondence correspondence = new Correspondence(
                staticPoint: staticPoint,
                modelPoint: modelPoint
            );

            Fragment.ICPFragmentType type = Fragment.ICPFragmentType.Model;

            Point actual = correspondence.GetPoint(type);

            Assert.AreEqual(modelPoint, actual);
        }

        [Test]
        public void TestGetPoint_StaticPoint()
        {
            Point staticPoint = Auxilaries.RandomPoint();
            Point modelPoint = Auxilaries.RandomPoint();

            Correspondence correspondence = new Correspondence(
                staticPoint: staticPoint,
                modelPoint: modelPoint
            );

            Fragment.ICPFragmentType type = Fragment.ICPFragmentType.Static;

            Point actual = correspondence.GetPoint(type);

            Assert.AreEqual(staticPoint, actual);
        }

        [Test]
        public void TestGetPoint_InvalidEnum()
        {
            Assert.Throws(typeof(System.ArgumentException), new TestDelegate(TestGetPoint_InvalidEnum_Helper));
        }

        private void TestGetPoint_InvalidEnum_Helper()
        {
            Point staticPoint = Auxilaries.RandomPoint();
            Point modelPoint = Auxilaries.RandomPoint();

            Correspondence correspondence = new Correspondence(
                staticPoint: staticPoint,
                modelPoint: modelPoint
            );

            Fragment.ICPFragmentType type = (Fragment.ICPFragmentType)99;

            correspondence.GetPoint(type);
        }
    }
}