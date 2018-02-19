using UnityEngine;
using NUnit.Framework;

using DoubleConnectedEdgeList;
using System;

[TestFixture]
public class FaceTest
{
    [Test, MaxTime(2000)]
    public void TestEquals_EqualsSameOrder()
    {
        HalfEdge a = TestAux.RandomHalfEdge();
        HalfEdge b = TestAux.RandomHalfEdge();
        HalfEdge c = TestAux.RandomHalfEdge();
        HalfEdge d = TestAux.RandomHalfEdge();

        Face thisFace = new Face();
        thisFace.AddOuterComponent(a);
        thisFace.AddOuterComponent(b);
        thisFace.AddOuterComponent(c);
        thisFace.AddOuterComponent(d);

        Face otherFace = new Face();
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

        Face thisFace = new Face();
        thisFace.AddOuterComponent(a);
        thisFace.AddOuterComponent(b);
        thisFace.AddOuterComponent(c);
        thisFace.AddOuterComponent(d);

        Face otherFace = new Face();
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

        Face thisFace = new Face();
        thisFace.AddOuterComponent(a);
        thisFace.AddOuterComponent(b);
        thisFace.AddOuterComponent(c);
        thisFace.AddOuterComponent(d);
        thisFace.AddOuterComponent(d);
        thisFace.AddOuterComponent(d);

        Face otherFace = new Face();
        otherFace.AddOuterComponent(b);
        otherFace.AddOuterComponent(a);
        otherFace.AddOuterComponent(d);
        otherFace.AddOuterComponent(c);

        Assert.IsTrue(thisFace.Equals(otherFace));
        Assert.IsTrue(otherFace.Equals(thisFace));
        Assert.AreEqual(thisFace.GetHashCode(), otherFace.GetHashCode());
    }

    [Test, MaxTime(2000)]
    public void TestEquals_NotEqualThisGreater()
    {
        HalfEdge a = TestAux.RandomHalfEdge();
        HalfEdge b = TestAux.RandomHalfEdge();
        HalfEdge c = TestAux.RandomHalfEdge();
        HalfEdge d = TestAux.RandomHalfEdge();

        Face thisFace = new Face();
        thisFace.AddOuterComponent(a);
        thisFace.AddOuterComponent(b);
        thisFace.AddOuterComponent(c);
        thisFace.AddOuterComponent(d);

        Face otherFace = new Face();
        otherFace.AddOuterComponent(a);
        otherFace.AddOuterComponent(b);
        otherFace.AddOuterComponent(d);

        Assert.IsFalse(thisFace.Equals(otherFace));
        Assert.IsFalse(otherFace.Equals(thisFace));
        Assert.AreNotEqual(thisFace.GetHashCode(), otherFace.GetHashCode());
    }

    [Test, MaxTime(2000)]
    public void TestEquals_NotEqualThisSmaller()
    {
        HalfEdge a = TestAux.RandomHalfEdge();
        HalfEdge b = TestAux.RandomHalfEdge();
        HalfEdge c = TestAux.RandomHalfEdge();
        HalfEdge d = TestAux.RandomHalfEdge();

        Face thisFace = new Face();
        thisFace.AddOuterComponent(a);
        thisFace.AddOuterComponent(c);
        thisFace.AddOuterComponent(d);

        Face otherFace = new Face();
        otherFace.AddOuterComponent(a);
        otherFace.AddOuterComponent(b);
        otherFace.AddOuterComponent(c);
        otherFace.AddOuterComponent(d);

        Assert.IsFalse(thisFace.Equals(otherFace));
        Assert.IsFalse(otherFace.Equals(thisFace));
        Assert.AreNotEqual(thisFace.GetHashCode(), otherFace.GetHashCode());
    }

    [Test, MaxTime(2000)]
    public void TestEquals_NotEqualSameSizeDifferentEdges()
    {
        HalfEdge a = TestAux.RandomHalfEdge();
        HalfEdge b = TestAux.RandomHalfEdge();
        HalfEdge c = TestAux.RandomHalfEdge();
        HalfEdge d = TestAux.RandomHalfEdge();
        HalfEdge e = TestAux.RandomHalfEdge();

        Face thisFace = new Face();
        thisFace.AddOuterComponent(a);
        thisFace.AddOuterComponent(b);
        thisFace.AddOuterComponent(c);
        thisFace.AddOuterComponent(d);

        Face otherFace = new Face();
        otherFace.AddOuterComponent(a);
        otherFace.AddOuterComponent(b);
        otherFace.AddOuterComponent(e);
        otherFace.AddOuterComponent(d);

        Assert.IsFalse(thisFace.Equals(otherFace));
        Assert.IsFalse(otherFace.Equals(thisFace));
        Assert.AreNotEqual(thisFace.GetHashCode(), otherFace.GetHashCode());
    }
}