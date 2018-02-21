using UnityEngine;
using NUnit.Framework;

using DoubleConnectedEdgeList;
using System;

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
}