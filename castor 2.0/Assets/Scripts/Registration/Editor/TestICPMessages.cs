using UnityEngine;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using NUnit.Framework;

using Registration;
using Registration.Messages;

namespace Tests
{

    [TestFixture]
    public class ICPPreparationStepCompletedMessageTests
    {
        private Transform transform = null;
        private List<Point> staticPoints = new List<Point>();
        private List<Point> modelPoints = new List<Point>();
        private List<Correspondence> correspondences = new List<Correspondence>();
        private int iterationIndex = 3;

        private ICPPreparationStepCompletedMessage message;

        [SetUp]
        public void SetUp()
        {
            int numCorrespondences = 5;

            Point staticPoint, modelPoint;

            for (int i = 0; i < numCorrespondences; i++)
            {
                staticPoint = Auxilaries.RandomPoint();
                modelPoint = Auxilaries.RandomPoint();

                staticPoints.Add(staticPoint);
                modelPoints.Add(modelPoint);

                correspondences.Add(
                    new Correspondence(
                        staticPoint: staticPoint,
                        modelPoint: modelPoint
                    )
                );
            }

            message = new ICPPreparationStepCompletedMessage(
                correspondences, transform, iterationIndex
            );
        }

        [TestCase]
        public void Test_GetPointsByType_ModelPoints()
        {
            Fragment.ICPFragmentType type = Fragment.ICPFragmentType.Model;

            List<Point> expected = modelPoints;
            ReadOnlyCollection<Point> actual = message.GetPointsByType(type);

            Assert.That(expected, Is.EquivalentTo(actual));
        }

        [TestCase]
        public void Test_GetPointsByType_StaticPoints()
        {
            Fragment.ICPFragmentType type = Fragment.ICPFragmentType.Static;

            List<Point> expected = staticPoints;
            ReadOnlyCollection<Point> actual = message.GetPointsByType(type);

            Assert.That(expected, Is.EquivalentTo(actual));
        }

        [TestCase]
        public void Test_GetPointsByType_UnknownType()
        {
            Assert.Throws(
                typeof(System.ArgumentException),
                new TestDelegate(Test_GetPointsByType_UnknownType_Helper)
            );
        }

        public void Test_GetPointsByType_UnknownType_Helper()
        {
            Fragment.ICPFragmentType type = (Fragment.ICPFragmentType)99;
            message.GetPointsByType(type);
        }

        [TestCase]
        public void Test_GetStaticPoints()
        {
            List<Point> expected = staticPoints;
            ReadOnlyCollection<Point> actual = message.StaticPoints;

            Assert.That(expected, Is.EquivalentTo(actual));
        }

        [TestCase]
        public void Test_GetModelPoints()
        {
            List<Point> expected = modelPoints;
            ReadOnlyCollection<Point> actual = message.ModelPoints;

            Assert.That(expected, Is.EquivalentTo(actual));
        }
    }
}