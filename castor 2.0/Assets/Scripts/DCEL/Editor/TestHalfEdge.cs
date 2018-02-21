using UnityEngine;
using NUnit.Framework;

using DoubleConnectedEdgeList;

[TestFixture]
public class HalfEdgeTest
{
    [Test, MaxTime(2000)]
    public void TestDestination_TwinIsNull()
    {
        Vertex origin = TestAux.RandomVertex();
        Vertex destination = TestAux.RandomVertex();

        HalfEdge edge = new HalfEdge(origin);

        Vertex actual = null;
        actual = edge.Destination;

        Assert.IsNull(actual);
    }

    [Test, MaxTime(2000)]
    public void TestDestination_TwinIsNotNull()
    {
        Vertex origin = TestAux.RandomVertex();
        Vertex destination = TestAux.RandomVertex();

        HalfEdge edge = new HalfEdge(origin);
        edge.Twin = new HalfEdge(destination);

        Vertex expected = destination;
        Vertex actual = edge.Destination;

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test, MaxTime(2000)]
    public void TestEquals_EqualsNoTwins()
    {
        Vertex origin = TestAux.RandomVertex();

        HalfEdge thisEdge = new HalfEdge(origin);
        HalfEdge otherEdge = new HalfEdge(origin);

        Assert.IsTrue(thisEdge.Equals(otherEdge));
        Assert.IsTrue(otherEdge.Equals(thisEdge));
        Assert.AreEqual(thisEdge.GetHashCode(), otherEdge.GetHashCode());
    }

    [Test, MaxTime(2000)]
    public void TestEquals_EqualsAllPropertiesSet()
    {
        Vertex a = TestAux.RandomVertex();
        Vertex b = TestAux.RandomVertex();
        Vertex c = TestAux.RandomVertex();

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
        Vertex origin = TestAux.RandomVertex();
        Vertex destination = TestAux.RandomVertex();

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
        HalfEdge thisEdge = TestAux.RandomHalfEdge();
        HalfEdge otherEdge = TestAux.RandomHalfEdge();

        Assert.IsFalse(thisEdge.Equals(otherEdge));
        Assert.IsFalse(otherEdge.Equals(thisEdge));

        Assert.AreNotEqual(thisEdge.GetHashCode(), otherEdge.GetHashCode());
    }

    [Test, MaxTime(2000)]
    public void TestEquals_NotEqualTwinDifferent()
    {
        Vertex origin = TestAux.RandomVertex();
        Vertex thisDestination = TestAux.RandomVertex();
        Vertex otherDestination = TestAux.RandomVertex();

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
        Vertex origin = TestAux.RandomVertex();
        Vertex destination = TestAux.RandomVertex();
        Vertex thisNextDestination = TestAux.RandomVertex();
        Vertex otherNextDestination = TestAux.RandomVertex();

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
        Vertex origin = TestAux.RandomVertex();
        Vertex destination = TestAux.RandomVertex();
        Vertex thisPreviousOrigin = TestAux.RandomVertex();
        Vertex otherPreviousOrigin = TestAux.RandomVertex();

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
        Vertex a = TestAux.RandomVertex();
        Vertex b = TestAux.RandomVertex();
        Vertex c = TestAux.RandomVertex();

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
        Vertex a = TestAux.RandomVertex();
        Vertex b = TestAux.RandomVertex();
        Vertex c = TestAux.RandomVertex();

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
        Vertex a = TestAux.RandomVertex();
        Vertex b = TestAux.RandomVertex();
        Vertex c = TestAux.RandomVertex();

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
        Vertex a = TestAux.RandomVertex();
        Vertex b = TestAux.RandomVertex();
        Vertex c = TestAux.RandomVertex();

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
        Vertex a = TestAux.RandomVertex();
        Vertex b = TestAux.RandomVertex();
        Vertex c = TestAux.RandomVertex();

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
        Vertex a = TestAux.RandomVertex();
        Vertex b = TestAux.RandomVertex();
        Vertex c = TestAux.RandomVertex();

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
        Vertex a = TestAux.RandomVertex();
        Vertex b = TestAux.RandomVertex();
        Vertex c = TestAux.RandomVertex();

        HalfEdge abOther = new HalfEdge(a);
        HalfEdge abThis = new HalfEdge(a);
        HalfEdge ac = new HalfEdge(a);

        HalfEdge ba = new HalfEdge(b);
        HalfEdge bc = new HalfEdge(b);

        HalfEdge ca = new HalfEdge(c);
        HalfEdge cb = new HalfEdge(c);

        Face face = new Face();
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
        Vertex a = TestAux.RandomVertex();
        Vertex b = TestAux.RandomVertex();
        Vertex c = TestAux.RandomVertex();

        HalfEdge abOther = new HalfEdge(a);
        HalfEdge abThis = new HalfEdge(a);
        HalfEdge ac = new HalfEdge(a);

        HalfEdge ba = new HalfEdge(b);
        HalfEdge bc = new HalfEdge(b);

        HalfEdge ca = new HalfEdge(c);
        HalfEdge cb = new HalfEdge(c);

        Face thisFace = new Face();
        thisFace.AddOuterComponent(abThis);
        thisFace.AddOuterComponent(bc);
        thisFace.AddOuterComponent(ca);

        Face otherFace = new Face();
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
    public void TestNonRecursiveEquals_EqualsNoTwins()
    {
        Vertex origin = TestAux.RandomVertex();

        HalfEdge thisEdge = new HalfEdge(origin);
        HalfEdge otherEdge = new HalfEdge(origin);

        Assert.IsTrue(thisEdge.NonRecursiveEquals(otherEdge));
        Assert.IsTrue(otherEdge.NonRecursiveEquals(thisEdge));
    }

    [Test, MaxTime(2000)]
    public void TestNonRecursiveEquals_NotEqualNoTwins()
    {
        HalfEdge thisEdge = TestAux.RandomHalfEdge();
        HalfEdge otherEdge = TestAux.RandomHalfEdge();

        Assert.IsFalse(thisEdge.NonRecursiveEquals(otherEdge));
        Assert.IsFalse(otherEdge.NonRecursiveEquals(thisEdge));
    }

    [Test, MaxTime(2000)]
    public void TestHasTwin_HasTwin()
    {
        HalfEdge edge = TestAux.RandomHalfEdge();
        HalfEdge twin = TestAux.RandomHalfEdge();

        edge.Twin = twin;

        Assert.IsTrue(edge.HasTwin);
    }

    [Test, MaxTime(2000)]
    public void TestHasTwin_HasNoTwin()
    {
        HalfEdge edge = TestAux.RandomHalfEdge();

        Assert.IsFalse(edge.HasTwin);
    }

    [Test, MaxTime(2000)]
    public void TestHasPrevious_HasPrevious()
    {
        HalfEdge edge = TestAux.RandomHalfEdge();
        HalfEdge previous = TestAux.RandomHalfEdge();

        edge.Previous = previous;

        Assert.IsTrue(edge.HasPrevious);
    }

    [Test, MaxTime(2000)]
    public void TestHasPrevious_HasNoPrevious()
    {
        HalfEdge edge = TestAux.RandomHalfEdge();

        Assert.IsFalse(edge.HasPrevious);
    }

    [Test, MaxTime(2000)]
    public void TestHasNext_HasNext()
    {
        HalfEdge edge = TestAux.RandomHalfEdge();
        HalfEdge next = TestAux.RandomHalfEdge();

        edge.Next = next;

        Assert.IsTrue(edge.HasNext);
    }

    [Test, MaxTime(2000)]
    public void TestHasNext_HasNoNext()
    {
        HalfEdge edge = TestAux.RandomHalfEdge();

        Assert.IsFalse(edge.HasNext);
    }

    [Test, MaxTime(2000)]
    public void TestHasDestination_HasDestination()
    {
        HalfEdge edge = TestAux.RandomHalfEdge();
        HalfEdge twin = TestAux.RandomHalfEdge();

        edge.Twin = twin;

        Assert.IsTrue(edge.HasDestination);
    }

    [Test, MaxTime(2000)]
    public void TestHasDestination_HasNoDestination()
    {
        HalfEdge edge = TestAux.RandomHalfEdge();

        Assert.IsFalse(edge.HasDestination);
    }

    [Test, MaxTime(2000)]
    public void TestHasIncidentFace_HasIncidentFace()
    {
        HalfEdge edge = TestAux.RandomHalfEdge();

        edge.IncidentFace = new Face();

        Assert.IsTrue(edge.HasIncidentFace);
    }

    [Test, MaxTime(2000)]
    public void TestHasIncidentFace_HasNoIncidentFace()
    {
        HalfEdge edge = TestAux.RandomHalfEdge();

        Assert.IsFalse(edge.HasDestination);
    }

    [Test, MaxTime(2000)]
    public void TestCompareTo_ObjNull()
    {
        HalfEdge edge = TestAux.RandomHalfEdge();

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
        HalfEdge edge = TestAux.RandomHalfEdge();
        Vertex vertex = TestAux.RandomVertex();

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
        Vertex vertex = TestAux.RandomVertex();

        HalfEdge thisHalfEdge = new HalfEdge(vertex);
        HalfEdge otherHalfEdge = new HalfEdge(vertex);

        int expected = 0;
        int actual = thisHalfEdge.CompareTo(otherHalfEdge);

        Assert.AreEqual(expected, actual);
    }

    [Test, MaxTime(2000)]
    public void TestCompareTo_ObjEqualInPositionSmallerInTwin()
    {
        Vertex vertex = TestAux.RandomVertex();

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
        Vertex vertex = TestAux.RandomVertex();

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
        Vertex vertex = TestAux.RandomVertex();

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
        Vertex vertex = TestAux.RandomVertex();

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
        Vertex vertex = TestAux.RandomVertex();

        HalfEdge thisEdge = new HalfEdge(vertex);
        HalfEdge otherEdge = new HalfEdge(vertex);

        HalfEdge twin = TestAux.RandomHalfEdge();

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
        Vertex vertex = TestAux.RandomVertex();

        HalfEdge thisEdge = new HalfEdge(vertex);
        HalfEdge otherEdge = new HalfEdge(vertex);

        HalfEdge twin = TestAux.RandomHalfEdge();

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
        Vertex vertex = TestAux.RandomVertex();

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
        Vertex vertex = TestAux.RandomVertex();

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
        Vertex vertex = TestAux.RandomVertex();

        HalfEdge thisEdge = new HalfEdge(vertex);
        HalfEdge otherEdge = new HalfEdge(vertex);

        HalfEdge twin = TestAux.RandomHalfEdge();
        thisEdge.Twin = twin;
        otherEdge.Twin = twin;

        HalfEdge next = TestAux.RandomHalfEdge();
        thisEdge.Next = next;
        otherEdge.Next = next;

        HalfEdge previous = TestAux.RandomHalfEdge();
        thisEdge.Previous = previous;
        otherEdge.Previous = previous;

        int expected = 0;
        int actual = thisEdge.CompareTo(otherEdge);

        Assert.AreEqual(expected, actual);
    }

}