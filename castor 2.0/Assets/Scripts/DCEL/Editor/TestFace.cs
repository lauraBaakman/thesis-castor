using UnityEngine;
using NUnit.Framework;

using DoubleConnectedEdgeList;
using System;

namespace Tests
{
    [TestFixture]
    public class FaceTest
    {
        [Test, MaxTime(2000)]
        public void TestEquals_NotEqualFaceIdxNotEqual()
        {
            HalfEdge a = TestAux.RandomHalfEdge();
            HalfEdge b = TestAux.RandomHalfEdge();
            HalfEdge c = TestAux.RandomHalfEdge();
            HalfEdge d = TestAux.RandomHalfEdge();

            Face thisFace = new Face(0);
            thisFace.AddOuterComponent(a);
            thisFace.AddOuterComponent(b);
            thisFace.AddOuterComponent(c);
            thisFace.AddOuterComponent(d);

            Face otherFace = new Face(1);
            otherFace.AddOuterComponent(a);
            otherFace.AddOuterComponent(b);
            otherFace.AddOuterComponent(c);
            otherFace.AddOuterComponent(d);

            Assert.IsFalse(thisFace.Equals(otherFace));
            Assert.IsFalse(otherFace.Equals(thisFace));
            Assert.AreNotEqual(thisFace.GetHashCode(), otherFace.GetHashCode());
        }

        [Test, MaxTime(2000)]
        public void TestEquals_EqualsSameOrder()
        {
            HalfEdge a = TestAux.RandomHalfEdge();
            HalfEdge b = TestAux.RandomHalfEdge();
            HalfEdge c = TestAux.RandomHalfEdge();
            HalfEdge d = TestAux.RandomHalfEdge();

            Face thisFace = new Face(0);
            thisFace.AddOuterComponent(a);
            thisFace.AddOuterComponent(b);
            thisFace.AddOuterComponent(c);
            thisFace.AddOuterComponent(d);

            Face otherFace = new Face(0);
            otherFace.AddOuterComponent(a);
            otherFace.AddOuterComponent(b);
            otherFace.AddOuterComponent(c);
            otherFace.AddOuterComponent(d);

            Assert.IsTrue(thisFace.Equals(otherFace));
            Assert.IsTrue(otherFace.Equals(thisFace));
            Assert.AreEqual(thisFace.GetHashCode(), otherFace.GetHashCode());
        }

        [Test, MaxTime(2000)]
        public void TestEquals_EqualsDifferentOrder()
        {
            HalfEdge a = TestAux.RandomHalfEdge();
            HalfEdge b = TestAux.RandomHalfEdge();
            HalfEdge c = TestAux.RandomHalfEdge();
            HalfEdge d = TestAux.RandomHalfEdge();

            Face thisFace = new Face(0);
            thisFace.AddOuterComponent(a);
            thisFace.AddOuterComponent(b);
            thisFace.AddOuterComponent(c);
            thisFace.AddOuterComponent(d);

            Face otherFace = new Face(0);
            otherFace.AddOuterComponent(b);
            otherFace.AddOuterComponent(a);
            otherFace.AddOuterComponent(d);
            otherFace.AddOuterComponent(c);

            Assert.IsTrue(thisFace.Equals(otherFace));
            Assert.IsTrue(otherFace.Equals(thisFace));
            Assert.AreEqual(thisFace.GetHashCode(), otherFace.GetHashCode());
        }

        [Test, MaxTime(2000)]
        public void TestEquals_EqualsAddSameEdgeTwice()
        {
            HalfEdge a = TestAux.RandomHalfEdge();
            HalfEdge b = TestAux.RandomHalfEdge();
            HalfEdge c = TestAux.RandomHalfEdge();
            HalfEdge d = TestAux.RandomHalfEdge();

            Face thisFace = new Face(0);
            thisFace.AddOuterComponent(a);
            thisFace.AddOuterComponent(b);
            thisFace.AddOuterComponent(c);
            thisFace.AddOuterComponent(d);
            thisFace.AddOuterComponent(d);
            thisFace.AddOuterComponent(d);

            Face otherFace = new Face(0);
            otherFace.AddOuterComponent(b);
            otherFace.AddOuterComponent(a);
            otherFace.AddOuterComponent(d);
            otherFace.AddOuterComponent(c);

            Assert.IsTrue(thisFace.Equals(otherFace));
            Assert.IsTrue(otherFace.Equals(thisFace));
            Assert.AreEqual(thisFace.GetHashCode(), otherFace.GetHashCode());
        }

        [Test, MaxTime(2000)]
        public void TestEquals_IdxEqualNotEqualThisGreater()
        {
            HalfEdge a = TestAux.RandomHalfEdge();
            HalfEdge b = TestAux.RandomHalfEdge();
            HalfEdge c = TestAux.RandomHalfEdge();
            HalfEdge d = TestAux.RandomHalfEdge();

            Face thisFace = new Face(0);
            thisFace.AddOuterComponent(a);
            thisFace.AddOuterComponent(b);
            thisFace.AddOuterComponent(c);
            thisFace.AddOuterComponent(d);

            Face otherFace = new Face(0);
            otherFace.AddOuterComponent(a);
            otherFace.AddOuterComponent(b);
            otherFace.AddOuterComponent(d);

            Assert.IsFalse(thisFace.Equals(otherFace));
            Assert.IsFalse(otherFace.Equals(thisFace));
            Assert.AreNotEqual(thisFace.GetHashCode(), otherFace.GetHashCode());
        }

        [Test, MaxTime(2000)]
        public void TestEquals_IdxEqualNotEqualThisSmaller()
        {
            HalfEdge a = TestAux.RandomHalfEdge();
            HalfEdge b = TestAux.RandomHalfEdge();
            HalfEdge c = TestAux.RandomHalfEdge();
            HalfEdge d = TestAux.RandomHalfEdge();

            Face thisFace = new Face(0);
            thisFace.AddOuterComponent(a);
            thisFace.AddOuterComponent(c);
            thisFace.AddOuterComponent(d);

            Face otherFace = new Face(0);
            otherFace.AddOuterComponent(a);
            otherFace.AddOuterComponent(b);
            otherFace.AddOuterComponent(c);
            otherFace.AddOuterComponent(d);

            Assert.IsFalse(thisFace.Equals(otherFace));
            Assert.IsFalse(otherFace.Equals(thisFace));
            Assert.AreNotEqual(thisFace.GetHashCode(), otherFace.GetHashCode());
        }

        [Test, MaxTime(2000)]
        public void TestEquals_IdxEqualNotEqualSameSizeDifferentEdges()
        {
            HalfEdge a = TestAux.RandomHalfEdge();
            HalfEdge b = TestAux.RandomHalfEdge();
            HalfEdge c = TestAux.RandomHalfEdge();
            HalfEdge d = TestAux.RandomHalfEdge();
            HalfEdge e = TestAux.RandomHalfEdge();

            Face thisFace = new Face(0);
            thisFace.AddOuterComponent(a);
            thisFace.AddOuterComponent(b);
            thisFace.AddOuterComponent(c);
            thisFace.AddOuterComponent(d);

            Face otherFace = new Face(0);
            otherFace.AddOuterComponent(a);
            otherFace.AddOuterComponent(b);
            otherFace.AddOuterComponent(e);
            otherFace.AddOuterComponent(d);

            Assert.IsFalse(thisFace.Equals(otherFace));
            Assert.IsFalse(otherFace.Equals(thisFace));
            Assert.AreNotEqual(thisFace.GetHashCode(), otherFace.GetHashCode());
        }

        [Test, MaxTime(2000)]
        public void TestCompareTo_ObjNull()
        {
            Face face = TestAux.RandomFace();

            int expected = 1;
            int actual = face.CompareTo(null);
            Assert.AreEqual(expected, actual);
        }

        [Test, MaxTime(2000)]
        public void TestCompareTo_ObjNotFace()
        {
            Assert.Throws(typeof(System.ArgumentException), new TestDelegate(TestCompareTo_ObjNotFace_Helper));
        }

        void TestCompareTo_ObjNotFace_Helper()
        {
            Face face = TestAux.RandomFace();
            HalfEdge edge = TestAux.RandomHalfEdge();

            face.CompareTo(edge);
        }

        [Test, MaxTime(2000)]
        public void TestCompareTo_MeshIdxSmaller()
        {
            Face thisFace = new Face(0);
            Face otherFace = new Face(1);

            int expected = -1;
            int actual = thisFace.CompareTo(otherFace);

            Assert.AreEqual(expected, actual);
        }

        [Test, MaxTime(2000)]
        public void TestCompareTo_MeshIdxEqual()
        {
            Face thisFace = new Face(1);
            Face otherFace = new Face(1);

            int expected = 0;
            int actual = thisFace.CompareTo(otherFace);

            Assert.AreEqual(expected, actual);
        }

        [Test, MaxTime(2000)]
        public void TestCompareTo_MeshIdxLarger()
        {
            Face thisFace = new Face(5);
            Face otherFace = new Face(1);

            int expected = 1;
            int actual = thisFace.CompareTo(otherFace);

            Assert.AreEqual(expected, actual);
        }
    }

    [TestFixture]
    public class FaceSimpleComparerTest
    {
        private Face.SimpleComparer comparer;

        [SetUp]
        public void Init()
        {
            comparer = new Face.SimpleComparer();
        }

        [Test, MaxTime(50)]
        public void XIsNull()
        {
            Face x = null;
            Face y = TestAux.RandomFace();

            Assert.IsFalse(comparer.Equals(x, y));
            Assert.AreNotEqual(comparer.GetHashCode(x), comparer.GetHashCode(y));
        }

        [Test, MaxTime(50)]
        public void YIsNull()
        {
            Face x = TestAux.RandomFace();
            Face y = null;

            Assert.IsFalse(comparer.Equals(x, y));
            Assert.AreNotEqual(comparer.GetHashCode(x), comparer.GetHashCode(y));
        }

        [Test, MaxTime(50)]
        public void XAndyAreNull()
        {
            Face x = null;
            Face y = null;

            Assert.IsTrue(comparer.Equals(x, y));
            Assert.AreEqual(comparer.GetHashCode(x), comparer.GetHashCode(y));
        }

        [Test, MaxTime(50)]
        public void XAndyAreEqual()
        {
            int idx = 3;
            Face x = new Face(idx);
            Face y = new Face(idx);

            Assert.IsTrue(comparer.Equals(x, y));
            Assert.AreEqual(comparer.GetHashCode(x), comparer.GetHashCode(y));
        }

        [Test, MaxTime(50)]
        public void XAndyAreNotEqual_IdxDifferent()
        {
            Face x = TestAux.RandomFace();
            Face y = TestAux.RandomFace();

            Assert.IsFalse(comparer.Equals(x, y));
            Assert.AreNotEqual(comparer.GetHashCode(x), comparer.GetHashCode(y));
        }


        [Test, MaxTime(50)]
        public void XAndyAreEqual_OuterComponentsDifferent()
        {
            int idx = 3;
            Face x = new Face(idx);
            Face y = new Face(idx);

            x.AddOuterComponent(TestAux.RandomHalfEdge());
            y.AddOuterComponent(TestAux.RandomHalfEdge());

            Assert.IsTrue(comparer.Equals(x, y));
            Assert.AreEqual(comparer.GetHashCode(x), comparer.GetHashCode(y));
        }
    }
}