
using UnityEngine;
using NUnit.Framework;

using Registration;
using System.Collections.Generic;

namespace Tests
{
    [TestFixture]
    public class CorrespondenceCollectionTests
    {
        private List<Correspondence> correspondenceList;
        private List<Point> modelPointList;
        private List<Point> staticPointList;

        private CorrespondenceCollection correspondences;

        [SetUp]
        public void Init()
        {
            modelPointList = new List<Point>();
            staticPointList = new List<Point>();
            correspondenceList = new List<Correspondence>
            {
                Auxilaries.RandomCorrespondence(),
                Auxilaries.RandomCorrespondence(),
                Auxilaries.RandomCorrespondence(),
                Auxilaries.RandomCorrespondence()
            };

            correspondences = new CorrespondenceCollection();
            foreach (Correspondence correspondence in correspondenceList)
            {
                correspondences.Add(correspondence);

                modelPointList.Add(correspondence.ModelPoint);
                staticPointList.Add(correspondence.StaticPoint);
            }
        }

        [Test]
        public void Test_GetModelPoints()
        {
            IEnumerable<Point> expected = modelPointList;
            IEnumerable<Point> actual = correspondences.ModelPoints;

            Assert.That(actual, Is.EquivalentTo(expected));
        }

        [Test]
        public void Test_GetStaticPoints()
        {
            IEnumerable<Point> expected = staticPointList;
            IEnumerable<Point> actual = correspondences.StaticPoints;

            Assert.That(actual, Is.EquivalentTo(expected));
        }

        [Test]
        public void Test_GetCorrespondences()
        {
            IEnumerable<Correspondence> expected = correspondenceList;
            IEnumerable<Correspondence> actual = correspondences.Correspondences;

            Assert.That(actual, Is.EquivalentTo(expected));
        }

        [Test]
        public void Test_Add_DistanceNode()
        {
            Point staticPoint = Auxilaries.RandomPoint();
            Point modelPoint = Auxilaries.RandomPoint();
            float distance = 3;

            DistanceNode node = new DistanceNode(
                modelPoint: modelPoint,
                staticPoint: staticPoint,
                distance: distance
            );

            CorrespondenceCollection actual = correspondences;
            correspondences.Add(node);

            modelPointList.Add(modelPoint);
            staticPointList.Add(staticPoint);
            correspondenceList.Add(new Correspondence(staticPoint: staticPoint, modelPoint: modelPoint));
            CorrespondenceCollection expected = new CorrespondenceCollection(modelPointList, staticPointList, correspondenceList);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Test_Add_Correspondence()
        {
            Correspondence correspondence = Auxilaries.RandomCorrespondence();

            CorrespondenceCollection actual = new CorrespondenceCollection();
            actual.Add(correspondence);

            List<Correspondence> expected_correspondences = new List<Correspondence> { correspondence };
            List<Point> expected_modelpoints = new List<Point> { correspondence.ModelPoint };
            List<Point> expected_staticpoints = new List<Point> { correspondence.StaticPoint };

            Assert.That(actual.Correspondences, Is.EquivalentTo(expected_correspondences));
            Assert.That(actual.ModelPoints, Is.EquivalentTo(expected_modelpoints));
            Assert.That(actual.StaticPoints, Is.EquivalentTo(expected_staticpoints));
        }

        [Test]
        public void Test_Count()
        {
            int expected = this.correspondenceList.Count;
            int actual = this.correspondences.Count;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Test_Clear()
        {
            Assert.AreNotEqual(0, correspondences.ModelPoints.Count);
            Assert.AreNotEqual(0, correspondences.StaticPoints.Count);
            Assert.AreNotEqual(0, correspondences.Correspondences.Count);

            correspondences.Clear();

            Assert.AreEqual(0, correspondences.ModelPoints.Count);
            Assert.AreEqual(0, correspondences.StaticPoints.Count);
            Assert.AreEqual(0, correspondences.Correspondences.Count);
        }

        [Test]
        public void Test_GetEnumerator_ForEach()
        {
            int i = 0;
            foreach (Correspondence correspondence in correspondences)
            {
                Assert.AreEqual(correspondenceList[i++], correspondence);
            }
        }

        [Test]
        public void Test_GetStaticPointEnumerator_ForEach()
        {
            int i = 0;
            foreach (Point point in correspondences.StaticPointsEnumerator)
            {
                Assert.AreEqual(staticPointList[i++], point);
            }
        }

        [Test]
        public void Test_GetModelPointEnumerator_ForEach()
        {
            int i = 0;
            foreach (Point point in correspondences.ModelPointsEnumerator)
            {
                Assert.AreEqual(modelPointList[i++], point);
            }
        }

        [Test]
        public void Test_Equals_AreEqual()
        {
            CorrespondenceCollection actual = correspondences;
            CorrespondenceCollection other = new CorrespondenceCollection(modelPointList, staticPointList, correspondenceList);

            Assert.IsTrue(actual.Equals(other));
            Assert.IsTrue(other.Equals(actual));
            Assert.AreEqual(actual.GetHashCode(), other.GetHashCode());
        }

        [Test]
        public void Test_Equals_AreNotEqual()
        {
            CorrespondenceCollection actual = correspondences;

            Point modelPoint = Auxilaries.RandomPoint();
            Point staticPoint = Auxilaries.RandomPoint();

            modelPointList.Add(modelPoint);
            staticPointList.Add(staticPoint);
            correspondences.Add(new Correspondence(modelPoint: modelPoint, staticPoint: staticPoint));
            CorrespondenceCollection other = new CorrespondenceCollection(modelPointList, staticPointList, correspondenceList);

            Assert.IsFalse(actual.Equals(other));
            Assert.IsFalse(other.Equals(actual));
            Assert.AreNotEqual(actual.GetHashCode(), other.GetHashCode());
        }
    }
}