using NUnit.Framework;

using DoubleConnectedEdgeList;
using UnityEngine;
using System.Collections.Generic;

namespace Tests.DoubleConnectedEdgeList
{
    [TestFixture]
    public class FaceTest
    {
        [Test, MaxTime(2000)]
        public void TestEquals_NotEqualFaceIdxNotEqual()
        {
            HalfEdge a = Auxilaries.RandomHalfEdge();
            HalfEdge b = Auxilaries.RandomHalfEdge();
            HalfEdge c = Auxilaries.RandomHalfEdge();
            HalfEdge d = Auxilaries.RandomHalfEdge();

            Vector3 normal = Auxilaries.RandomNormal();

            Face thisFace = new Face(0, normal);
            thisFace.AddOuterComponent(a);
            thisFace.AddOuterComponent(b);
            thisFace.AddOuterComponent(c);
            thisFace.AddOuterComponent(d);

            Face otherFace = new Face(1, normal);
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
            HalfEdge a = Auxilaries.RandomHalfEdge();
            HalfEdge b = Auxilaries.RandomHalfEdge();
            HalfEdge c = Auxilaries.RandomHalfEdge();
            HalfEdge d = Auxilaries.RandomHalfEdge();

            Vector3 normal = Auxilaries.RandomNormal();

            Face thisFace = new Face(0, normal);
            thisFace.AddOuterComponent(a);
            thisFace.AddOuterComponent(b);
            thisFace.AddOuterComponent(c);
            thisFace.AddOuterComponent(d);

            Face otherFace = new Face(0, normal);
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
            HalfEdge a = Auxilaries.RandomHalfEdge();
            HalfEdge b = Auxilaries.RandomHalfEdge();
            HalfEdge c = Auxilaries.RandomHalfEdge();
            HalfEdge d = Auxilaries.RandomHalfEdge();

            Vector3 normal = Auxilaries.RandomNormal();

            Face thisFace = new Face(0, normal);
            thisFace.AddOuterComponent(a);
            thisFace.AddOuterComponent(b);
            thisFace.AddOuterComponent(c);
            thisFace.AddOuterComponent(d);

            Face otherFace = new Face(0, normal);
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
            HalfEdge a = Auxilaries.RandomHalfEdge();
            HalfEdge b = Auxilaries.RandomHalfEdge();
            HalfEdge c = Auxilaries.RandomHalfEdge();
            HalfEdge d = Auxilaries.RandomHalfEdge();

            Vector3 normal = Auxilaries.RandomNormal();

            Face thisFace = new Face(0, normal);
            thisFace.AddOuterComponent(a);
            thisFace.AddOuterComponent(b);
            thisFace.AddOuterComponent(c);
            thisFace.AddOuterComponent(d);
            thisFace.AddOuterComponent(d);
            thisFace.AddOuterComponent(d);

            Face otherFace = new Face(0, normal);
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
            HalfEdge a = Auxilaries.RandomHalfEdge();
            HalfEdge b = Auxilaries.RandomHalfEdge();
            HalfEdge c = Auxilaries.RandomHalfEdge();
            HalfEdge d = Auxilaries.RandomHalfEdge();

            Vector3 normal = Auxilaries.RandomNormal();

            Face thisFace = new Face(0, normal);
            thisFace.AddOuterComponent(a);
            thisFace.AddOuterComponent(b);
            thisFace.AddOuterComponent(c);
            thisFace.AddOuterComponent(d);

            Face otherFace = new Face(0, normal);
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
            HalfEdge a = Auxilaries.RandomHalfEdge();
            HalfEdge b = Auxilaries.RandomHalfEdge();
            HalfEdge c = Auxilaries.RandomHalfEdge();
            HalfEdge d = Auxilaries.RandomHalfEdge();

            Vector3 normal = Auxilaries.RandomNormal();

            Face thisFace = new Face(0, normal);
            thisFace.AddOuterComponent(a);
            thisFace.AddOuterComponent(c);
            thisFace.AddOuterComponent(d);

            Face otherFace = new Face(0, normal);
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
            HalfEdge a = Auxilaries.RandomHalfEdge();
            HalfEdge b = Auxilaries.RandomHalfEdge();
            HalfEdge c = Auxilaries.RandomHalfEdge();
            HalfEdge d = Auxilaries.RandomHalfEdge();
            HalfEdge e = Auxilaries.RandomHalfEdge();

            Vector3 normal = Auxilaries.RandomNormal();

            Face thisFace = new Face(0, normal);
            thisFace.AddOuterComponent(a);
            thisFace.AddOuterComponent(b);
            thisFace.AddOuterComponent(c);
            thisFace.AddOuterComponent(d);

            Face otherFace = new Face(0, normal);
            otherFace.AddOuterComponent(a);
            otherFace.AddOuterComponent(b);
            otherFace.AddOuterComponent(e);
            otherFace.AddOuterComponent(d);

            Assert.IsFalse(thisFace.Equals(otherFace));
            Assert.IsFalse(otherFace.Equals(thisFace));
            Assert.AreNotEqual(thisFace.GetHashCode(), otherFace.GetHashCode());
        }

        [Test, MaxTime(2000)]
        public void TestEquals_NotEqualDifferentNormals()
        {
            HalfEdge a = Auxilaries.RandomHalfEdge();
            HalfEdge b = Auxilaries.RandomHalfEdge();

            Face thisFace = new Face(0, Auxilaries.RandomNormal());
            thisFace.AddOuterComponent(a);
            thisFace.AddOuterComponent(b);

            Face otherFace = new Face(0, Auxilaries.RandomNormal());
            thisFace.AddOuterComponent(a);
            thisFace.AddOuterComponent(b);

            Assert.IsFalse(thisFace.Equals(otherFace));
            Assert.IsFalse(otherFace.Equals(thisFace));
            Assert.AreNotEqual(thisFace.GetHashCode(), otherFace.GetHashCode());
        }

        [Test, MaxTime(2000)]
        public void TestCompareTo_ObjNull()
        {
            Face face = Auxilaries.RandomFace();

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
            Face face = Auxilaries.RandomFace();
            HalfEdge edge = Auxilaries.RandomHalfEdge();

            face.CompareTo(edge);
        }

        [Test, MaxTime(2000)]
        public void TestCompareTo_MeshIdxSmaller()
        {
            Vector3 normal = Auxilaries.RandomNormal();

            Face thisFace = new Face(0, normal);
            Face otherFace = new Face(1, normal);

            int expected = -1;
            int actual = thisFace.CompareTo(otherFace);

            Assert.AreEqual(expected, actual);
        }

        [Test, MaxTime(2000)]
        public void TestCompareTo_MeshIdxEqual()
        {
            Vector3 normal = Auxilaries.RandomNormal();

            Face thisFace = new Face(1, normal);
            Face otherFace = new Face(1, normal);

            int expected = 0;
            int actual = thisFace.CompareTo(otherFace);

            Assert.AreEqual(expected, actual);
        }

        [Test, MaxTime(2000)]
        public void TestCompareTo_MeshIdxLarger()
        {
            Vector3 normal = Auxilaries.RandomNormal();

            Face thisFace = new Face(5, normal);
            Face otherFace = new Face(1, normal);

            int expected = 1;
            int actual = thisFace.CompareTo(otherFace);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestArea_Triangle()
        {
            Vertex a = new Vertex(new Vector3(1, 2, 3));
            Vertex b = new Vertex(new Vector3(3, 9, -1));
            Vertex c = new Vertex(new Vector3(4, 4, -7));

            HalfEdge ab = new HalfEdge(a);
            HalfEdge ac = new HalfEdge(a);

            HalfEdge ba = new HalfEdge(b);
            HalfEdge bc = new HalfEdge(b);

            HalfEdge ca = new HalfEdge(c);
            HalfEdge cb = new HalfEdge(c);

            ab.Twin = ba;
            ac.Twin = ca;

            ba.Twin = ab;
            bc.Twin = cb;

            ca.Twin = ac;
            cb.Twin = bc;

            Face triangle = new Face(0, Auxilaries.RandomNormal());
            triangle.AddOuterComponent(ac);
            triangle.AddOuterComponent(cb);
            triangle.AddOuterComponent(ba);

            float expected = 13.405389f;
            float actual = triangle.Area;

            Assert.That(actual, Is.EqualTo(expected).Within(0.0001f));

            //Check the lazy evaluation, sort of
            Assert.That(triangle.Area, Is.EqualTo(expected).Within(0.0001f));
        }

        [Test]
        public void TestArea_NotTriangle()
        {
            Assert.Throws(typeof(System.InvalidOperationException), new TestDelegate(TestArea_NotTriangle_Helper));
        }

        public void TestArea_NotTriangle_Helper()
        {
            Vertex a = Auxilaries.RandomVertex();
            Vertex b = Auxilaries.RandomVertex();
            Vertex c = Auxilaries.RandomVertex();
            Vertex d = Auxilaries.RandomVertex();

            HalfEdge ab = new HalfEdge(a);
            HalfEdge ba = new HalfEdge(b);

            HalfEdge bc = new HalfEdge(b);
            HalfEdge cb = new HalfEdge(c);

            HalfEdge cd = new HalfEdge(c);
            HalfEdge dc = new HalfEdge(d);

            HalfEdge da = new HalfEdge(d);
            HalfEdge ad = new HalfEdge(a);

            ab.Twin = ba;
            ab.Next = bc;
            ab.Previous = da;

            bc.Twin = cb;
            bc.Next = cd;
            bc.Previous = ab;

            cd.Twin = dc;
            cd.Next = da;
            cd.Previous = bc;

            da.Twin = ad;
            da.Next = ab;
            da.Previous = cd;

            Face rectangle = new Face(0, Auxilaries.RandomNormal());
            rectangle.AddOuterComponent(bc);
            rectangle.AddOuterComponent(cd);
            rectangle.AddOuterComponent(da);
            rectangle.AddOuterComponent(ab);

            float area = rectangle.Area;
        }

        [Test]
        public void TestIsTriangular_Triangle()
        {
            Vertex a = new Vertex(new Vector3(1, 2));
            Vertex b = new Vertex(new Vector3(3, 9, -1));
            Vertex c = new Vertex(new Vector3(4, 4, -7));

            HalfEdge ab = new HalfEdge(a);
            HalfEdge ac = new HalfEdge(a);

            HalfEdge ba = new HalfEdge(b);
            HalfEdge bc = new HalfEdge(b);

            HalfEdge ca = new HalfEdge(c);
            HalfEdge cb = new HalfEdge(c);

            ab.Twin = ba;
            ac.Twin = ca;

            ba.Twin = ab;
            bc.Twin = cb;

            ca.Twin = ac;
            cb.Twin = bc;

            Face triangle = new Face(0, Auxilaries.RandomNormal());
            triangle.AddOuterComponent(ac);
            triangle.AddOuterComponent(cb);
            triangle.AddOuterComponent(ba);

            Assert.IsTrue(triangle.IsTriangular());
        }


        [Test]
        public void TestIsTriangular_Rectangle()
        {
            Vertex a = Auxilaries.RandomVertex();
            Vertex b = Auxilaries.RandomVertex();
            Vertex c = Auxilaries.RandomVertex();
            Vertex d = Auxilaries.RandomVertex();

            HalfEdge ab = new HalfEdge(a);
            HalfEdge ba = new HalfEdge(b);

            HalfEdge bc = new HalfEdge(b);
            HalfEdge cb = new HalfEdge(c);

            HalfEdge cd = new HalfEdge(c);
            HalfEdge dc = new HalfEdge(d);

            HalfEdge da = new HalfEdge(d);
            HalfEdge ad = new HalfEdge(a);

            ab.Twin = ba;
            ab.Next = bc;
            ab.Previous = da;

            bc.Twin = cb;
            bc.Next = cd;
            bc.Previous = ab;

            cd.Twin = dc;
            cd.Next = da;
            cd.Previous = bc;

            da.Twin = ad;
            da.Next = ab;
            da.Previous = cd;

            Face rectangle = new Face(0, Auxilaries.RandomNormal());
            rectangle.AddOuterComponent(bc);
            rectangle.AddOuterComponent(cd);
            rectangle.AddOuterComponent(da);
            rectangle.AddOuterComponent(ab);

            Assert.IsFalse(rectangle.IsTriangular());
        }
    }

    [TestFixture]
    public class MeshIdxAndNormalComparerTest
    {
        private Face.MeshIdxAndNormalComparer comparer;

        [SetUp]
        public void Init()
        {
            comparer = new Face.MeshIdxAndNormalComparer();
        }

        [Test, MaxTime(50)]
        public void XIsNull()
        {
            Face x = null;
            Face y = Auxilaries.RandomFace();

            Assert.IsFalse(comparer.Equals(x, y));
            Assert.AreNotEqual(comparer.GetHashCode(x), comparer.GetHashCode(y));
        }

        [Test, MaxTime(50)]
        public void YIsNull()
        {
            Face x = Auxilaries.RandomFace();
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
            Vector3 normal = Auxilaries.RandomNormal();

            Face x = new Face(idx, normal);
            Face y = new Face(idx, normal);

            Assert.IsTrue(comparer.Equals(x, y));
            Assert.AreEqual(comparer.GetHashCode(x), comparer.GetHashCode(y));
        }

        [Test, MaxTime(50)]
        public void XAndyAreNotEqual_IdxDifferent()
        {
            Vector3 normal = Auxilaries.RandomNormal();

            Face x = new Face(1, normal);
            Face y = new Face(2, normal);

            Assert.IsFalse(comparer.Equals(x, y));
            Assert.AreNotEqual(comparer.GetHashCode(x), comparer.GetHashCode(y));
        }

        [Test, MaxTime(50)]
        public void XAndyAreNotEqual_NormalDifferent()
        {
            int idx = 0;

            Face x = new Face(idx, Auxilaries.RandomNormal());
            Face y = new Face(idx, Auxilaries.RandomNormal());

            Assert.IsFalse(comparer.Equals(x, y));
            Assert.AreNotEqual(comparer.GetHashCode(x), comparer.GetHashCode(y));
        }

        [Test, MaxTime(50)]
        public void XAndyAreNotEqual_OuterComponentsDifferent()
        {
            int idx = 3;
            Vector3 normal = Auxilaries.RandomNormal();
            Face x = new Face(idx, normal);
            Face y = new Face(idx, normal);

            x.AddOuterComponent(Auxilaries.RandomHalfEdge());
            y.AddOuterComponent(Auxilaries.RandomHalfEdge());

            Assert.IsTrue(comparer.Equals(x, y));
            Assert.AreEqual(comparer.GetHashCode(x), comparer.GetHashCode(y));
        }
    }

    [TestFixture]
    public class FaceKeyValueComparer
    {
        private Face.KeyValueComparer<int> comparer;

        [SetUp]
        public void Init()
        {
            comparer = new Face.KeyValueComparer<int>();
        }

        [Test, MaxTime(50)]
        public void XAndyAreEqual()
        {
            int idx = 3;
            Vector3 normal = Auxilaries.RandomNormal();
            KeyValuePair<int, Face> x = new KeyValuePair<int, Face>(idx, new Face(idx, normal));
            KeyValuePair<int, Face> y = new KeyValuePair<int, Face>(idx, new Face(idx, normal));

            Assert.IsTrue(comparer.Equals(x, y));
            Assert.AreEqual(comparer.GetHashCode(x), comparer.GetHashCode(y));
        }

        [Test, MaxTime(50)]
        public void XAndyAreNotEqual_KeyDifferent()
        {
            Face face = Auxilaries.RandomFace();
            KeyValuePair<int, Face> x = new KeyValuePair<int, Face>(0, face);
            KeyValuePair<int, Face> y = new KeyValuePair<int, Face>(1, face);

            Assert.IsFalse(comparer.Equals(x, y));
            Assert.AreNotEqual(comparer.GetHashCode(x), comparer.GetHashCode(y));
        }

        [Test, MaxTime(50)]
        public void XAndyAreNotEqual_ValueDifferent()
        {
            int idx = 2;
            KeyValuePair<int, Face> x = new KeyValuePair<int, Face>(idx, Auxilaries.RandomFace());
            KeyValuePair<int, Face> y = new KeyValuePair<int, Face>(idx, Auxilaries.RandomFace());

            Assert.IsFalse(comparer.Equals(x, y));
            Assert.AreNotEqual(comparer.GetHashCode(x), comparer.GetHashCode(y));
        }

    }
}