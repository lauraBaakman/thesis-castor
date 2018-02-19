﻿using UnityEngine;
using NUnit.Framework;

using DoubleConnectedEdgeList;

[TestFixture]
public class VertexTest
{
    [SetUp]
    public void Init()
    {
        Random.InitState(42);
    }

    [Test]
    public void TestEquals_Equals()
    {

        Vector3 position = TestAux.RandomPosition();

        Vertex thisVertex = new Vertex(position);
        Vertex otherVertex = new Vertex(position);

        //HalfEdge edge1 = new HalfEdge(new Vertex(new Vector3().FillRandomly()));
        //HalfEdge edge2 = new HalfEdge(new Vertex(new Vector3().FillRandomly()));

        //thisVertex.AddIncidentEdge(edge1);
        //thisVertex.AddIncidentEdge(edge2);

        //otherVertex.AddIncidentEdge(edge1);
        //otherVertex.AddIncidentEdge(edge2);

        Assert.IsTrue(thisVertex.Equals(otherVertex));
        Assert.IsTrue(otherVertex.Equals(thisVertex));
        Assert.AreEqual(thisVertex.GetHashCode(), otherVertex.GetHashCode());
    }

    [Test]
    public void TestEquals_PositionNotEqual()
    {
        Vertex thisVertex = TestAux.RandomVertex();
        Vertex otherVertex = TestAux.RandomVertex();

        Assert.IsFalse(thisVertex.Equals(otherVertex));
        Assert.IsFalse(otherVertex.Equals(thisVertex));
        Assert.AreNotEqual(thisVertex, otherVertex);
    }

    [Test]
    public void TestEquals_IncidentEdgesNotEqual()
    {
        Vector3 position = TestAux.RandomPosition();

        Vertex thisVertex = new Vertex(position);
        Vertex otherVertex = new Vertex(position);

        //HalfEdge edge1 = new HalfEdge(new Vertex(new Vector3().FillRandomly()));
        //HalfEdge edge2 = new HalfEdge(new Vertex(new Vector3().FillRandomly()));
        //HalfEdge edge3 = new HalfEdge(new Vertex(new Vector3().FillRandomly()));

        //thisVertex.AddIncidentEdge(edge1);
        //thisVertex.AddIncidentEdge(edge2);

        //otherVertex.AddIncidentEdge(edge3);

        Assert.IsFalse(thisVertex.Equals(otherVertex));
        Assert.IsFalse(otherVertex.Equals(thisVertex));
        Assert.AreNotEqual(thisVertex.GetHashCode(), otherVertex.GetHashCode());
    }

    [Test]
    public void TestNonRecursiveEquals_Equals()
    {

        Vector3 position = TestAux.RandomPosition();

        Vertex thisVertex = new Vertex(position);
        Vertex otherVertex = new Vertex(position);

        //HalfEdge edge1 = new HalfEdge(new Vertex(new Vector3().FillRandomly()));
        //HalfEdge edge2 = new HalfEdge(new Vertex(new Vector3().FillRandomly()));

        //thisVertex.AddIncidentEdge(edge1);
        //thisVertex.AddIncidentEdge(edge2);

        //otherVertex.AddIncidentEdge(edge1);
        //otherVertex.AddIncidentEdge(edge2);

        Assert.IsTrue(thisVertex.NonRecursiveEquals(otherVertex));
        Assert.IsTrue(otherVertex.NonRecursiveEquals(thisVertex));
    }

    [Test]
    public void TestNonRecursiveEquals_PositionNotEqual()
    {
        Vertex thisVertex = TestAux.RandomVertex();
        Vertex otherVertex = TestAux.RandomVertex();

        Assert.IsFalse(thisVertex.NonRecursiveEquals(otherVertex));
        Assert.IsFalse(otherVertex.NonRecursiveEquals(thisVertex));
    }
}