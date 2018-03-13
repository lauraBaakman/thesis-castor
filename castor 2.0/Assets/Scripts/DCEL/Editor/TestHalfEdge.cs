using UnityEngine;
using NUnit.Framework;

using DoubleConnectedEdgeList;

namespace Tests
{
    [TestFixture]
    public class HalfEdgeTest
    {
        [Test, MaxTime(2000)]
        public void TestDestination_TwinIsNull()
        {
            Vertex origin = Auxilaries.RandomVertex();

            HalfEdge edge = new HalfEdge(origin);

            Vertex actual = null;
            actual = edge.Destination;

            Assert.IsNull(actual);
        }

        [Test, MaxTime(2000)]
        public void TestDestination_TwinIsNotNull()
        {
            Vertex origin = Auxilaries.RandomVertex();
            Vertex destination = Auxilaries.RandomVertex();

            HalfEdge edge = new HalfEdge(origin);
            edge.Twin = new HalfEdge(destination);

            Vertex expected = destination;
            Vertex actual = edge.Destination;

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test, MaxTime(2000)]
        public void TestEquals_EqualsNoTwins()
        {
            Vertex origin = Auxilaries.RandomVertex();

            HalfEdge thisEdge = new HalfEdge(origin);
            HalfEdge otherEdge = new HalfEdge(origin);

            Assert.IsTrue(thisEdge.Equals(otherEdge));
            Assert.IsTrue(otherEdge.Equals(thisEdge));
            Assert.AreEqual(thisEdge.GetHashCode(), otherEdge.GetHashCode());
        }

        [Test, MaxTime(2000)]
        public void TestEquals_EqualsAllPropertiesSet()
        {
            Vertex a = Auxilaries.RandomVertex();
            Vertex b = Auxilaries.RandomVertex();
            Vertex c = Auxilaries.RandomVertex();

            HalfEdge abOther = new HalfEdge(a);
            HalfEdge abThis = new HalfEdge(a);
            HalfEdge ac = new HalfEdge(a);

            HalfEdge ba = new HalfEdge(b);
            HalfEdge bc = new HalfEdge(b);

            HalfEdge ca = new HalfEdge(c);
            HalfEdge cb = new HalfEdge(c);

            abThis.Twin = ba;
            abOther.Twin = ba;
            ac.Twin = ca;

            ba.Twin = abThis;
            bc.Twin = bc;

            ca.Twin = ac;
            cb.Twin = bc;

            abThis.Next = bc;
            abOther.Next = bc;
            ac.Next = cb;

            ba.Next = ac;
            bc.Next = ca;

            ca.Next = abThis;
            cb.Next = ba;

            abThis.Previous = ca;
            abOther.Previous = ca;
            ac.Previous = ba;

            ba.Previous = cb;
            bc.Previous = abThis;

            ca.Previous = bc;
            cb.Previous = ac;

            Assert.IsTrue(abThis.Equals(abOther));
            Assert.IsTrue(abOther.Equals(abThis));

            Assert.AreEqual(abThis.GetHashCode(), abOther.GetHashCode());
        }

        [Test, MaxTime(2000)]
        public void TestEquals_EqualsWithTwins()
        {
            Vertex origin = Auxilaries.RandomVertex();
            Vertex destination = Auxilaries.RandomVertex();

            HalfEdge thisEdge = new HalfEdge(origin);
            HalfEdge otherEdge = new HalfEdge(origin);
            HalfEdge twin = new HalfEdge(destination);

            thisEdge.Twin = twin;
            otherEdge.Twin = twin;

            twin.Twin = thisEdge;

            Assert.IsTrue(thisEdge.Equals(otherEdge));
            Assert.IsTrue(otherEdge.Equals(thisEdge));

            Assert.AreEqual(thisEdge.GetHashCode(), otherEdge.GetHashCode());
        }

        [Test, MaxTime(2000)]
        public void TestEquals_NotEqualOriginDifferent()
        {
            HalfEdge thisEdge = Auxilaries.RandomHalfEdge();
            HalfEdge otherEdge = Auxilaries.RandomHalfEdge();

            Assert.IsFalse(thisEdge.Equals(otherEdge));
            Assert.IsFalse(otherEdge.Equals(thisEdge));

            Assert.AreNotEqual(thisEdge.GetHashCode(), otherEdge.GetHashCode());
        }

        [Test, MaxTime(2000)]
        public void TestEquals_NotEqualTwinDifferent()
        {
            Vertex origin = Auxilaries.RandomVertex();
            Vertex thisDestination = Auxilaries.RandomVertex();
            Vertex otherDestination = Auxilaries.RandomVertex();

            HalfEdge thisEdge = new HalfEdge(origin);
            HalfEdge otherEdge = new HalfEdge(origin);

            HalfEdge thisEdgeTwin = new HalfEdge(thisDestination);
            thisEdge.Twin = thisEdgeTwin;
            thisEdgeTwin.Twin = thisEdge;

            HalfEdge otherEdgeTwin = new HalfEdge(otherDestination);
            otherEdge.Twin = new HalfEdge(otherDestination);
            otherEdgeTwin.Twin = otherEdge;

            Assert.IsFalse(thisEdge.Equals(otherEdge));
            Assert.IsFalse(otherEdge.Equals(thisEdge));

            Assert.AreNotEqual(thisEdge.GetHashCode(), otherEdge.GetHashCode());
        }

        [Test, MaxTime(2000)]
        public void TestEquals_NotEqualNextDifferent()
        {
            Vertex origin = Auxilaries.RandomVertex();
            Vertex destination = Auxilaries.RandomVertex();
            Vertex thisNextDestination = Auxilaries.RandomVertex();
            Vertex otherNextDestination = Auxilaries.RandomVertex();

            HalfEdge thisEdge = new HalfEdge(origin);
            thisEdge.Twin = new HalfEdge(destination);
            thisEdge.Next = new HalfEdge(destination);
            thisEdge.Next.Twin = new HalfEdge(thisNextDestination);

            HalfEdge otherEdge = new HalfEdge(origin);
            otherEdge.Twin = new HalfEdge(destination);
            otherEdge.Next = new HalfEdge(destination);
            otherEdge.Next.Twin = new HalfEdge(otherNextDestination);

            Assert.IsFalse(thisEdge.Equals(otherEdge));
            Assert.IsFalse(otherEdge.Equals(thisEdge));
            Assert.AreNotEqual(thisEdge.GetHashCode(), otherEdge.GetHashCode());
        }

        [Test, MaxTime(2000)]
        public void TestEquals_NotEqualPreviousDifferent()
        {
            Vertex origin = Auxilaries.RandomVertex();
            Vertex destination = Auxilaries.RandomVertex();
            Vertex thisPreviousOrigin = Auxilaries.RandomVertex();
            Vertex otherPreviousOrigin = Auxilaries.RandomVertex();

            HalfEdge thisEdge = new HalfEdge(origin);
            thisEdge.Twin = new HalfEdge(destination);
            thisEdge.Previous = new HalfEdge(thisPreviousOrigin);
            thisEdge.Previous.Twin = new HalfEdge(origin);

            HalfEdge otherEdge = new HalfEdge(origin);
            otherEdge.Twin = new HalfEdge(destination);
            otherEdge.Previous = new HalfEdge(otherPreviousOrigin);
            otherEdge.Previous.Twin = new HalfEdge(origin);

            Assert.IsFalse(thisEdge.Equals(otherEdge));
            Assert.IsFalse(otherEdge.Equals(thisEdge));
            Assert.AreNotEqual(thisEdge.GetHashCode(), otherEdge.GetHashCode());
        }

        [Test, MaxTime(2000)]
        public void TestEquals_NotEqualOneTwinNotSet()
        {
            Vertex a = Auxilaries.RandomVertex();
            Vertex b = Auxilaries.RandomVertex();
            Vertex c = Auxilaries.RandomVertex();

            HalfEdge abOther = new HalfEdge(a);
            HalfEdge abThis = new HalfEdge(a);
            HalfEdge ac = new HalfEdge(a);

            HalfEdge ba = new HalfEdge(b);
            HalfEdge bc = new HalfEdge(b);

            HalfEdge ca = new HalfEdge(c);
            HalfEdge cb = new HalfEdge(c);

            abThis.Twin = ba;
            ac.Twin = ca;

            ba.Twin = abThis;
            bc.Twin = bc;

            ca.Twin = ac;
            cb.Twin = bc;

            abThis.Next = bc;
            abOther.Next = bc;
            ac.Next = cb;

            ba.Next = ac;
            bc.Next = ca;

            ca.Next = abThis;
            cb.Next = ba;

            abThis.Previous = ca;
            abOther.Previous = ca;
            ac.Previous = ba;

            ba.Previous = cb;
            bc.Previous = abThis;

            ca.Previous = bc;
            cb.Previous = ac;

            Assert.IsFalse(abThis.Equals(abOther));
            Assert.IsFalse(abOther.Equals(abThis));
            Assert.AreNotEqual(abThis.GetHashCode(), abOther.GetHashCode());
        }

        [Test, MaxTime(2000)]
        public void TestEquals_EqualBothTwinsNotSet()
        {
            Vertex a = Auxilaries.RandomVertex();
            Vertex b = Auxilaries.RandomVertex();
            Vertex c = Auxilaries.RandomVertex();

            HalfEdge abOther = new HalfEdge(a);
            HalfEdge abThis = new HalfEdge(a);
            HalfEdge ac = new HalfEdge(a);

            HalfEdge ba = new HalfEdge(b);
            HalfEdge bc = new HalfEdge(b);

            HalfEdge ca = new HalfEdge(c);
            HalfEdge cb = new HalfEdge(c);

            ac.Twin = ca;

            ba.Twin = abThis;
            bc.Twin = bc;

            ca.Twin = ac;
            cb.Twin = bc;

            abThis.Next = bc;
            abOther.Next = bc;
            ac.Next = cb;

            ba.Next = ac;
            bc.Next = ca;

            ca.Next = abThis;
            cb.Next = ba;

            abThis.Previous = ca;
            abOther.Previous = ca;
            ac.Previous = ba;

            ba.Previous = cb;
            bc.Previous = abThis;

            ca.Previous = bc;
            cb.Previous = ac;

            Assert.IsTrue(abThis.Equals(abOther));
            Assert.IsTrue(abOther.Equals(abThis));
            Assert.AreEqual(abThis.GetHashCode(), abOther.GetHashCode());
        }

        [Test, MaxTime(2000)]
        public void TestEquals_NotEqualOnePreviousNotSet()
        {
            Vertex a = Auxilaries.RandomVertex();
            Vertex b = Auxilaries.RandomVertex();
            Vertex c = Auxilaries.RandomVertex();

            HalfEdge abOther = new HalfEdge(a);
            HalfEdge abThis = new HalfEdge(a);
            HalfEdge ac = new HalfEdge(a);

            HalfEdge ba = new HalfEdge(b);
            HalfEdge bc = new HalfEdge(b);

            HalfEdge ca = new HalfEdge(c);
            HalfEdge cb = new HalfEdge(c);

            abThis.Twin = ba;
            abOther.Twin = ba;
            ac.Twin = ca;

            ba.Twin = abThis;
            bc.Twin = bc;

            ca.Twin = ac;
            cb.Twin = bc;

            abThis.Next = bc;
            abOther.Next = bc;
            ac.Next = cb;

            ba.Next = ac;
            bc.Next = ca;

            ca.Next = abThis;
            cb.Next = ba;

            abThis.Previous = ca;
            ac.Previous = ba;

            ba.Previous = cb;
            bc.Previous = abThis;

            ca.Previous = bc;
            cb.Previous = ac;

            Assert.IsFalse(abThis.Equals(abOther));
            Assert.IsFalse(abOther.Equals(abThis));
            Assert.AreNotEqual(abThis.GetHashCode(), abOther.GetHashCode());
        }

        [Test, MaxTime(2000)]
        public void TestEquals_EqualBothPreviousNotSet()
        {
            Vertex a = Auxilaries.RandomVertex();
            Vertex b = Auxilaries.RandomVertex();
            Vertex c = Auxilaries.RandomVertex();

            HalfEdge abOther = new HalfEdge(a);
            HalfEdge abThis = new HalfEdge(a);
            HalfEdge ac = new HalfEdge(a);

            HalfEdge ba = new HalfEdge(b);
            HalfEdge bc = new HalfEdge(b);

            HalfEdge ca = new HalfEdge(c);
            HalfEdge cb = new HalfEdge(c);

            abThis.Twin = ba;
            abOther.Twin = ba;
            ac.Twin = ca;

            ba.Twin = abThis;
            bc.Twin = bc;

            ca.Twin = ac;
            cb.Twin = bc;

            abThis.Next = bc;
            abOther.Next = bc;
            ac.Next = cb;

            ba.Next = ac;
            bc.Next = ca;

            ca.Next = abThis;
            cb.Next = ba;

            ac.Previous = ba;

            ba.Previous = cb;
            bc.Previous = abThis;

            ca.Previous = bc;
            cb.Previous = ac;

            Assert.IsTrue(abThis.Equals(abOther));
            Assert.IsTrue(abOther.Equals(abThis));
            Assert.AreEqual(abThis.GetHashCode(), abOther.GetHashCode());
        }

        [Test, MaxTime(2000)]
        public void TestEquals_NotEqualOneNextNotSet()
        {
            Vertex a = Auxilaries.RandomVertex();
            Vertex b = Auxilaries.RandomVertex();
            Vertex c = Auxilaries.RandomVertex();

            HalfEdge abOther = new HalfEdge(a);
            HalfEdge abThis = new HalfEdge(a);
            HalfEdge ac = new HalfEdge(a);

            HalfEdge ba = new HalfEdge(b);
            HalfEdge bc = new HalfEdge(b);

            HalfEdge ca = new HalfEdge(c);
            HalfEdge cb = new HalfEdge(c);

            abThis.Twin = ba;
            abOther.Twin = ba;
            ac.Twin = ca;

            ba.Twin = abThis;
            bc.Twin = bc;

            ca.Twin = ac;
            cb.Twin = bc;

            abThis.Next = bc;
            ac.Next = cb;

            ba.Next = ac;
            bc.Next = ca;

            ca.Next = abThis;
            cb.Next = ba;

            abThis.Previous = ca;
            abOther.Previous = ca;
            ac.Previous = ba;

            ba.Previous = cb;
            bc.Previous = abThis;

            ca.Previous = bc;
            cb.Previous = ac;

            Assert.IsFalse(abThis.Equals(abOther));
            Assert.IsFalse(abOther.Equals(abThis));
            Assert.AreNotEqual(abThis.GetHashCode(), abOther.GetHashCode());
        }

        [Test, MaxTime(2000)]
        public void TestEquals_EqualBothNextNotSet()
        {
            Vertex a = Auxilaries.RandomVertex();
            Vertex b = Auxilaries.RandomVertex();
            Vertex c = Auxilaries.RandomVertex();

            HalfEdge abOther = new HalfEdge(a);
            HalfEdge abThis = new HalfEdge(a);
            HalfEdge ac = new HalfEdge(a);

            HalfEdge ba = new HalfEdge(b);
            HalfEdge bc = new HalfEdge(b);

            HalfEdge ca = new HalfEdge(c);
            HalfEdge cb = new HalfEdge(c);

            abThis.Twin = ba;
            abOther.Twin = ba;
            ac.Twin = ca;

            ba.Twin = abThis;
            bc.Twin = bc;

            ca.Twin = ac;
            cb.Twin = bc;

            ac.Next = cb;

            ba.Next = ac;
            bc.Next = ca;

            ca.Next = abThis;
            cb.Next = ba;

            abThis.Previous = ca;
            abOther.Previous = ca;
            ac.Previous = ba;

            ba.Previous = cb;
            bc.Previous = abThis;

            ca.Previous = bc;
            cb.Previous = ac;

            Assert.IsTrue(abThis.Equals(abOther));
            Assert.IsTrue(abOther.Equals(abThis));
            Assert.AreEqual(abThis.GetHashCode(), abOther.GetHashCode());
        }

        [Test, MaxTime(2000)]
        public void TestEquals_EqualBothIncidentFacesSet()
        {
            Vertex a = Auxilaries.RandomVertex();
            Vertex b = Auxilaries.RandomVertex();
            Vertex c = Auxilaries.RandomVertex();

            HalfEdge abOther = new HalfEdge(a);
            HalfEdge abThis = new HalfEdge(a);
            HalfEdge ac = new HalfEdge(a);

            HalfEdge ba = new HalfEdge(b);
            HalfEdge bc = new HalfEdge(b);

            HalfEdge ca = new HalfEdge(c);
            HalfEdge cb = new HalfEdge(c);

            Face face = new Face(2);
            face.AddOuterComponent(abThis);
            face.AddOuterComponent(bc);
            face.AddOuterComponent(ca);

            abThis.IncidentFace = face;
            abOther.IncidentFace = face;

            abThis.Twin = ba;
            abOther.Twin = ba;
            ac.Twin = ca;

            ba.Twin = abThis;
            bc.Twin = bc;

            ca.Twin = ac;
            cb.Twin = bc;

            abThis.Next = bc;
            abOther.Next = bc;
            ac.Next = cb;

            ba.Next = ac;
            bc.Next = ca;

            ca.Next = abThis;
            cb.Next = ba;

            abThis.Previous = ca;
            abOther.Previous = ca;
            ac.Previous = ba;

            ba.Previous = cb;
            bc.Previous = abThis;

            ca.Previous = bc;
            cb.Previous = ac;

            Assert.AreEqual(abThis.GetHashCode(), abOther.GetHashCode());

            Assert.IsTrue(abThis.Equals(abOther));
            Assert.IsTrue(abOther.Equals(abThis));
        }

        [Test, MaxTime(2000)]
        public void TestEquals_NotEqualsDifferentIncidentFaces()
        {
            Vertex a = Auxilaries.RandomVertex();
            Vertex b = Auxilaries.RandomVertex();
            Vertex c = Auxilaries.RandomVertex();

            HalfEdge abOther = new HalfEdge(a);
            HalfEdge abThis = new HalfEdge(a);
            HalfEdge ac = new HalfEdge(a);

            HalfEdge ba = new HalfEdge(b);
            HalfEdge bc = new HalfEdge(b);

            HalfEdge ca = new HalfEdge(c);
            HalfEdge cb = new HalfEdge(c);

            Face thisFace = new Face(0);
            thisFace.AddOuterComponent(abThis);
            thisFace.AddOuterComponent(bc);
            thisFace.AddOuterComponent(ca);

            Face otherFace = new Face(1);
            otherFace.AddOuterComponent(abOther);

            abThis.IncidentFace = thisFace;
            abOther.IncidentFace = otherFace;

            abThis.Twin = ba;
            abOther.Twin = ba;
            ac.Twin = ca;

            ba.Twin = abThis;
            bc.Twin = bc;

            ca.Twin = ac;
            cb.Twin = bc;

            abThis.Next = bc;
            abOther.Next = bc;
            ac.Next = cb;

            ba.Next = ac;
            bc.Next = ca;

            ca.Next = abThis;
            cb.Next = ba;

            abThis.Previous = ca;
            abOther.Previous = ca;
            ac.Previous = ba;

            ba.Previous = cb;
            bc.Previous = abThis;

            ca.Previous = bc;
            cb.Previous = ac;

            Assert.AreNotEqual(abThis.GetHashCode(), abOther.GetHashCode());

            Assert.IsFalse(abThis.Equals(abOther));
            Assert.IsFalse(abOther.Equals(abThis));
        }

        [Test, MaxTime(2000)]
        public void TestHasTwin_HasTwin()
        {
            HalfEdge edge = Auxilaries.RandomHalfEdge();
            HalfEdge twin = Auxilaries.RandomHalfEdge();

            edge.Twin = twin;

            Assert.IsTrue(edge.HasTwin);
        }

        [Test, MaxTime(2000)]
        public void TestHasTwin_HasNoTwin()
        {
            HalfEdge edge = Auxilaries.RandomHalfEdge();

            Assert.IsFalse(edge.HasTwin);
        }

        [Test, MaxTime(2000)]
        public void TestHasPrevious_HasPrevious()
        {
            HalfEdge edge = Auxilaries.RandomHalfEdge();
            HalfEdge previous = Auxilaries.RandomHalfEdge();

            edge.Previous = previous;

            Assert.IsTrue(edge.HasPrevious);
        }

        [Test, MaxTime(2000)]
        public void TestHasPrevious_HasNoPrevious()
        {
            HalfEdge edge = Auxilaries.RandomHalfEdge();

            Assert.IsFalse(edge.HasPrevious);
        }

        [Test, MaxTime(2000)]
        public void TestHasNext_HasNext()
        {
            HalfEdge edge = Auxilaries.RandomHalfEdge();
            HalfEdge next = Auxilaries.RandomHalfEdge();

            edge.Next = next;

            Assert.IsTrue(edge.HasNext);
        }

        [Test, MaxTime(2000)]
        public void TestHasNext_HasNoNext()
        {
            HalfEdge edge = Auxilaries.RandomHalfEdge();

            Assert.IsFalse(edge.HasNext);
        }

        [Test, MaxTime(2000)]
        public void TestHasDestination_HasDestination()
        {
            HalfEdge edge = Auxilaries.RandomHalfEdge();
            HalfEdge twin = Auxilaries.RandomHalfEdge();

            edge.Twin = twin;

            Assert.IsTrue(edge.HasDestination);
        }

        [Test, MaxTime(2000)]
        public void TestHasDestination_HasNoDestination()
        {
            HalfEdge edge = Auxilaries.RandomHalfEdge();

            Assert.IsFalse(edge.HasDestination);
        }

        [Test, MaxTime(2000)]
        public void TestHasIncidentFace_HasIncidentFace()
        {
            HalfEdge edge = Auxilaries.RandomHalfEdge();

            edge.IncidentFace = new Face(0);

            Assert.IsTrue(edge.HasIncidentFace);
        }

        [Test, MaxTime(2000)]
        public void TestHasIncidentFace_HasNoIncidentFace()
        {
            HalfEdge edge = Auxilaries.RandomHalfEdge();

            Assert.IsFalse(edge.HasDestination);
        }

        [Test, MaxTime(2000)]
        public void TestCompareTo_ObjNull()
        {
            HalfEdge edge = Auxilaries.RandomHalfEdge();

            int expected = 1;
            int actual = edge.CompareTo(null);
            Assert.AreEqual(expected, actual);
        }

        [Test, MaxTime(2000)]
        public void TestCompareTo_ObjNotHalfEdge()
        {
            Assert.Throws(typeof(System.ArgumentException), new TestDelegate(TestCompareTo_ObjNotHalfedge_Helper));
        }

        void TestCompareTo_ObjNotHalfedge_Helper()
        {
            HalfEdge edge = Auxilaries.RandomHalfEdge();
            Vertex vertex = Auxilaries.RandomVertex();

            edge.CompareTo(vertex);
        }

        [Test, MaxTime(2000)]
        public void TestCompareTo_ObjSmallerInPosition()
        {
            HalfEdge thisHalfEdge = new HalfEdge(new Vertex(new Vector3(0, 1, 2)));
            HalfEdge otherHalfEdge = new HalfEdge(new Vertex(new Vector3(3, 4, 5)));

            int expected = -1;
            int actual = thisHalfEdge.CompareTo(otherHalfEdge);

            Assert.AreEqual(expected, actual);
        }

        [Test, MaxTime(2000)]
        public void TestCompareTo_ObjLargerInPosition()
        {
            HalfEdge thisHalfEdge = new HalfEdge(new Vertex(new Vector3(3, 4, 5)));
            HalfEdge otherHalfEdge = new HalfEdge(new Vertex(new Vector3(0, 1, 2)));

            int expected = 1;
            int actual = thisHalfEdge.CompareTo(otherHalfEdge);

            Assert.AreEqual(expected, actual);
        }

        [Test, MaxTime(2000)]
        public void TestCompareTo_ObjEqualInPositionEverythingElseNull()
        {
            Vertex vertex = Auxilaries.RandomVertex();

            HalfEdge thisHalfEdge = new HalfEdge(vertex);
            HalfEdge otherHalfEdge = new HalfEdge(vertex);

            int expected = 0;
            int actual = thisHalfEdge.CompareTo(otherHalfEdge);

            Assert.AreEqual(expected, actual);
        }

        [Test, MaxTime(2000)]
        public void TestCompareTo_ObjEqualInPositionSmallerInTwin()
        {
            Vertex vertex = Auxilaries.RandomVertex();

            HalfEdge thisEdge = new HalfEdge(vertex);
            HalfEdge otherEdge = new HalfEdge(vertex);

            thisEdge.Twin = new HalfEdge(new Vertex(new Vector3(0, 1, 2)));
            otherEdge.Twin = new HalfEdge(new Vertex(new Vector3(3, 4, 5)));

            int expected = -1;
            int actual = thisEdge.CompareTo(otherEdge);

            Assert.AreEqual(expected, actual);
        }

        [Test, MaxTime(2000)]
        public void TestCompareTo_ObjEqualInPositionLargerInTwin()
        {
            Vertex vertex = Auxilaries.RandomVertex();

            HalfEdge thisEdge = new HalfEdge(vertex);
            HalfEdge otherEdge = new HalfEdge(vertex);

            thisEdge.Twin = new HalfEdge(new Vertex(new Vector3(3, 4, 5)));
            otherEdge.Twin = new HalfEdge(new Vertex(new Vector3(0, 1, 2)));

            int expected = +1;
            int actual = thisEdge.CompareTo(otherEdge);

            Assert.AreEqual(expected, actual);
        }

        [Test, MaxTime(2000)]
        public void TestCompareTo_ObjEqualInPositionSmallerInNext()
        {
            Vertex vertex = Auxilaries.RandomVertex();

            HalfEdge thisEdge = new HalfEdge(vertex);
            HalfEdge otherEdge = new HalfEdge(vertex);

            thisEdge.Next = new HalfEdge(new Vertex(new Vector3(0, 1, 2)));
            otherEdge.Next = new HalfEdge(new Vertex(new Vector3(3, 4, 5)));

            int expected = -1;
            int actual = thisEdge.CompareTo(otherEdge);

            Assert.AreEqual(expected, actual);
        }

        [Test, MaxTime(2000)]
        public void TestCompareTo_ObjEqualInPositionLargerInNext()
        {
            Vertex vertex = Auxilaries.RandomVertex();

            HalfEdge thisEdge = new HalfEdge(vertex);
            HalfEdge otherEdge = new HalfEdge(vertex);

            thisEdge.Next = new HalfEdge(new Vertex(new Vector3(3, 4, 5)));
            otherEdge.Next = new HalfEdge(new Vertex(new Vector3(0, 1, 2)));

            int expected = +1;
            int actual = thisEdge.CompareTo(otherEdge);

            Assert.AreEqual(expected, actual);
        }

        [Test, MaxTime(2000)]
        public void TestCompareTo_ObjEqualInPositionSmallerInNextTwinSet()
        {
            Vertex vertex = Auxilaries.RandomVertex();

            HalfEdge thisEdge = new HalfEdge(vertex);
            HalfEdge otherEdge = new HalfEdge(vertex);

            HalfEdge twin = Auxilaries.RandomHalfEdge();

            thisEdge.Twin = twin;
            otherEdge.Twin = twin;

            thisEdge.Next = new HalfEdge(new Vertex(new Vector3(0, 1, 2)));
            otherEdge.Next = new HalfEdge(new Vertex(new Vector3(3, 4, 5)));

            int expected = -1;
            int actual = thisEdge.CompareTo(otherEdge);

            Assert.AreEqual(expected, actual);
        }

        [Test, MaxTime(2000)]
        public void TestCompareTo_ObjEqualInPositionLargerInNextTwinSet()
        {
            Vertex vertex = Auxilaries.RandomVertex();

            HalfEdge thisEdge = new HalfEdge(vertex);
            HalfEdge otherEdge = new HalfEdge(vertex);

            HalfEdge twin = Auxilaries.RandomHalfEdge();

            thisEdge.Twin = twin;
            otherEdge.Twin = twin;

            thisEdge.Next = new HalfEdge(new Vertex(new Vector3(3, 4, 5)));
            otherEdge.Next = new HalfEdge(new Vertex(new Vector3(0, 1, 2)));

            int expected = +1;
            int actual = thisEdge.CompareTo(otherEdge);

            Assert.AreEqual(expected, actual);
        }

        [Test, MaxTime(2000)]
        public void TestCompareTo_ObjEqualInPositionSmallerInPrevious()
        {
            Vertex vertex = Auxilaries.RandomVertex();

            HalfEdge thisEdge = new HalfEdge(vertex);
            HalfEdge otherEdge = new HalfEdge(vertex);

            thisEdge.Previous = new HalfEdge(new Vertex(new Vector3(0, 1, 2)));
            otherEdge.Previous = new HalfEdge(new Vertex(new Vector3(3, 4, 5)));

            int expected = -1;
            int actual = thisEdge.CompareTo(otherEdge);

            Assert.AreEqual(expected, actual);
        }

        [Test, MaxTime(2000)]
        public void TestCompareTo_ObjEqualInPositionLargerInPrevious()
        {
            Vertex vertex = Auxilaries.RandomVertex();

            HalfEdge thisEdge = new HalfEdge(vertex);
            HalfEdge otherEdge = new HalfEdge(vertex);

            thisEdge.Previous = new HalfEdge(new Vertex(new Vector3(3, 4, 5)));
            otherEdge.Previous = new HalfEdge(new Vertex(new Vector3(0, 1, 2)));

            int expected = +1;
            int actual = thisEdge.CompareTo(otherEdge);

            Assert.AreEqual(expected, actual);
        }

        [Test, MaxTime(2000)]
        public void TestCompareTo_ObjEqualInEverything()
        {
            Vertex vertex = Auxilaries.RandomVertex();

            HalfEdge thisEdge = new HalfEdge(vertex);
            HalfEdge otherEdge = new HalfEdge(vertex);

            HalfEdge twin = Auxilaries.RandomHalfEdge();
            thisEdge.Twin = twin;
            otherEdge.Twin = twin;

            HalfEdge next = Auxilaries.RandomHalfEdge();
            thisEdge.Next = next;
            otherEdge.Next = next;

            HalfEdge previous = Auxilaries.RandomHalfEdge();
            thisEdge.Previous = previous;
            otherEdge.Previous = previous;

            int expected = 0;
            int actual = thisEdge.CompareTo(otherEdge);

            Assert.AreEqual(expected, actual);
        }

    }


    [TestFixture]
    public class HalfEdgeSimpleComparerTest
    {
        private HalfEdge.SimpleComparer comparer;

        [SetUp]
        public void Init()
        {
            comparer = new HalfEdge.SimpleComparer();
        }

        [Test, MaxTime(50)]
        public void XIsNull()
        {
            HalfEdge x = null;
            HalfEdge y = Auxilaries.RandomHalfEdge();

            Assert.IsFalse(comparer.Equals(x, y));
            Assert.AreNotEqual(comparer.GetHashCode(x), comparer.GetHashCode(y));
        }

        [Test, MaxTime(50)]
        public void YIsNull()
        {
            HalfEdge x = Auxilaries.RandomHalfEdge();
            HalfEdge y = null;

            Assert.IsFalse(comparer.Equals(x, y));
            Assert.AreNotEqual(comparer.GetHashCode(x), comparer.GetHashCode(y));
        }

        [Test, MaxTime(50)]
        public void XAndyAreNull()
        {
            HalfEdge x = null;
            HalfEdge y = null;

            Assert.IsTrue(comparer.Equals(x, y));
            Assert.AreEqual(comparer.GetHashCode(x), comparer.GetHashCode(y));
        }

        [Test, MaxTime(50)]
        public void XAndyAreEqual()
        {
            Vertex positon = Auxilaries.RandomVertex();
            HalfEdge x = new HalfEdge(positon);
            HalfEdge y = new HalfEdge(positon);

            Assert.IsTrue(comparer.Equals(x, y));
            Assert.AreEqual(comparer.GetHashCode(x), comparer.GetHashCode(y));
        }

        [Test, MaxTime(50)]
        public void XAndyAreNotEqual_OriginDifferent()
        {
            HalfEdge x = Auxilaries.RandomHalfEdge();
            HalfEdge y = Auxilaries.RandomHalfEdge();

            Assert.IsFalse(comparer.Equals(x, y));
            Assert.AreNotEqual(comparer.GetHashCode(x), comparer.GetHashCode(y));
        }

        [Test, MaxTime(50)]
        public void XAndyAreNotEqual_DestinationDifferent()
        {
            Vertex origin = Auxilaries.RandomVertex();

            HalfEdge x = new HalfEdge(origin);
            HalfEdge y = new HalfEdge(origin);

            x.Twin = Auxilaries.RandomHalfEdge();
            y.Twin = Auxilaries.RandomHalfEdge();

            Assert.IsFalse(comparer.Equals(x, y));
            Assert.AreNotEqual(comparer.GetHashCode(x), comparer.GetHashCode(y));
        }

        [Test, MaxTime(50)]
        public void XAndyAreEqual_NextDifferent()
        {
            Vertex origin = Auxilaries.RandomVertex();
            Vertex destination = Auxilaries.RandomVertex();

            HalfEdge x = new HalfEdge(origin);
            HalfEdge y = new HalfEdge(origin);

            x.Twin = new HalfEdge(destination);
            y.Twin = new HalfEdge(destination);

            x.Next = Auxilaries.RandomHalfEdge();
            y.Next = Auxilaries.RandomHalfEdge();

            Assert.IsTrue(comparer.Equals(x, y));
            Assert.AreEqual(comparer.GetHashCode(x), comparer.GetHashCode(y));
        }

        [Test, MaxTime(50)]
        public void XAndyAreEqual_PreviousDifferent()
        {
            Vertex origin = Auxilaries.RandomVertex();
            Vertex destination = Auxilaries.RandomVertex();

            HalfEdge x = new HalfEdge(origin);
            HalfEdge y = new HalfEdge(origin);

            x.Twin = new HalfEdge(destination);
            y.Twin = new HalfEdge(destination);

            x.Previous = Auxilaries.RandomHalfEdge();
            y.Previous = Auxilaries.RandomHalfEdge();

            Assert.IsTrue(comparer.Equals(x, y));
            Assert.AreEqual(comparer.GetHashCode(x), comparer.GetHashCode(y));
        }

        [Test, MaxTime(50)]
        public void XAndyAreEqual_IncidentFaceDifferent()
        {
            Vertex origin = Auxilaries.RandomVertex();
            Vertex destination = Auxilaries.RandomVertex();

            HalfEdge x = new HalfEdge(origin);
            HalfEdge y = new HalfEdge(origin);

            x.Twin = new HalfEdge(destination);
            y.Twin = new HalfEdge(destination);

            x.IncidentFace = Auxilaries.RandomFace();
            y.IncidentFace = Auxilaries.RandomFace();

            Assert.IsTrue(comparer.Equals(x, y));
            Assert.AreEqual(comparer.GetHashCode(x), comparer.GetHashCode(y));
        }
    }
}