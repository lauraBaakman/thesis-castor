using UnityEngine;
using NUnit.Framework;

using DoubleConnectedEdgeList;
using System.Collections.Generic;
using System;

[TestFixture]
public class DCELTests
{
    [Test, MaxTime(5000)]
    public void Build_SingleTriangle()
    {
        UnityEngine.Mesh mesh = new UnityEngine.Mesh();
        Vector3[] meshVertices = {
            new Vector3(0, 4, 3),
            new Vector3(2, 4, 4),
            new Vector3(2, 2, 5),
        };

        int[] triangles = {
            0, 2, 1
        };
        mesh.vertices = meshVertices;
        mesh.triangles = triangles;

        Vertex v1 = new Vertex(meshVertices[0]);
        Vertex v2 = new Vertex(meshVertices[1]);
        Vertex v3 = new Vertex(meshVertices[2]);

        List<HalfEdge> halfEdges = new List<HalfEdge>();
        HalfEdge e11 = new HalfEdge(v1);
        HalfEdge e12 = new HalfEdge(v2);
        HalfEdge e31 = new HalfEdge(v3);
        HalfEdge e32 = new HalfEdge(v1);
        HalfEdge e41 = new HalfEdge(v3);
        HalfEdge e42 = new HalfEdge(v2);

        e11.Twin = e12;
        e12.Twin = e11;
        e31.Twin = e32;
        e32.Twin = e31;
        e41.Twin = e42;
        e42.Twin = e41;

        e12.Next = e32;
        e32.Next = e41;
        e41.Next = e12;

        e12.Previous = e41;
        e32.Previous = e12;
        e41.Previous = e32;

        halfEdges.Add(e11);
        halfEdges.Add(e12);
        halfEdges.Add(e31);
        halfEdges.Add(e32);
        halfEdges.Add(e41);
        halfEdges.Add(e42);

        List<Face> faces = new List<Face>();

        Face f2 = new Face(0);
        f2.AddOuterComponent(e32);
        f2.AddOuterComponent(e41);
        f2.AddOuterComponent(e12);

        e32.IncidentFace = f2;
        e41.IncidentFace = f2;
        e12.IncidentFace = f2;

        faces.Add(f2);

        v1.AddIncidentEdge(e11);
        v1.AddIncidentEdge(e32);

        v2.AddIncidentEdge(e42);
        v2.AddIncidentEdge(e12);

        v3.AddIncidentEdge(e31);
        v3.AddIncidentEdge(e41);

        List<Vertex> dcelVertices = new List<Vertex> { v1, v2, v3 };

        DCEL expected = new DCEL(
            dcelVertices.AsReadOnly(),
            halfEdges.AsReadOnly(),
            faces.AsReadOnly()
        );

        DCEL actual = DCEL.Build(mesh);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test, MaxTime(5000)]
    public void Build_Rectangle()
    {
        Mesh mesh = new Mesh();
        Vector3[] meshVertices = {
            new Vector3(5, 7, 2),
            new Vector3(5, 3, 3),
            new Vector3(2, 3, 2.5f),
            new Vector3(2, 3, 2.5f),
            new Vector3(2, 5, 3f),
            new Vector3(5, 7, 2),
        };

        int[] triangles = {
            0, 2, 1,
            3, 5, 4
        };

        mesh.vertices = meshVertices;
        mesh.triangles = triangles;

        Vertex v1 = new Vertex(meshVertices[0]);
        Vertex v2 = new Vertex(meshVertices[1]);
        Vertex v3 = new Vertex(meshVertices[2]);
        Vertex v4 = new Vertex(meshVertices[4]);

        List<HalfEdge> halfEdges = new List<HalfEdge>();
        HalfEdge e12 = new HalfEdge(v1);
        HalfEdge e13 = new HalfEdge(v1);
        HalfEdge e14 = new HalfEdge(v1);

        HalfEdge e21 = new HalfEdge(v2);
        HalfEdge e23 = new HalfEdge(v2);

        HalfEdge e31 = new HalfEdge(v3);
        HalfEdge e32 = new HalfEdge(v3);
        HalfEdge e34 = new HalfEdge(v3);

        HalfEdge e41 = new HalfEdge(v4);
        HalfEdge e43 = new HalfEdge(v4);

        e12.Twin = e21;
        e13.Twin = e31;
        e14.Twin = e41;
        e21.Twin = e12;
        e23.Twin = e32;
        e31.Twin = e13;
        e32.Twin = e23;
        e34.Twin = e43;
        e41.Twin = e14;
        e43.Twin = e34;

        e13.Next = e32;
        e14.Next = e43;
        e21.Next = e13;
        e31.Next = e14;
        e32.Next = e21;
        e43.Next = e31;

        e13.Previous = e21;
        e14.Previous = e31;
        e21.Previous = e32;
        e31.Previous = e43;
        e32.Previous = e13;
        e43.Previous = e14;

        halfEdges.Add(e12);
        halfEdges.Add(e13);
        halfEdges.Add(e14);

        halfEdges.Add(e21);
        halfEdges.Add(e23);

        halfEdges.Add(e31);
        halfEdges.Add(e32);
        halfEdges.Add(e34);

        halfEdges.Add(e41);
        halfEdges.Add(e43);

        List<Face> faces = new List<Face>();

        Face f1 = new Face(0);
        f1.AddOuterComponent(e31);
        f1.AddOuterComponent(e43);
        f1.AddOuterComponent(e14);
        e31.IncidentFace = f1;
        e43.IncidentFace = f1;
        e14.IncidentFace = f1;
        faces.Add(f1);

        Face f2 = new Face(1);
        f2.AddOuterComponent(e21);
        f2.AddOuterComponent(e32);
        f2.AddOuterComponent(e13);
        e21.IncidentFace = f2;
        e32.IncidentFace = f2;
        e13.IncidentFace = f2;
        faces.Add(f2);

        v1.AddIncidentEdge(e12);
        v1.AddIncidentEdge(e13);
        v1.AddIncidentEdge(e14);

        v2.AddIncidentEdge(e21);
        v2.AddIncidentEdge(e23);

        v3.AddIncidentEdge(e31);
        v3.AddIncidentEdge(e32);
        v3.AddIncidentEdge(e34);

        v4.AddIncidentEdge(e41);
        v4.AddIncidentEdge(e43);

        List<Vertex> dcelVertices = new List<Vertex> {
            v1, v2, v3, v4
        };

        DCEL expected = new DCEL(
            dcelVertices.AsReadOnly(),
            halfEdges.AsReadOnly(),
            faces.AsReadOnly()
        );

        DCEL actual = DCEL.Build(mesh);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test, MaxTime(5000)]
    public void Build_ClosedMesh()
    {
        Mesh mesh = new Mesh();

        Vector3 p0 = new Vector3(2, 3, 4);
        Vector3 p1 = new Vector3(7, 3, 5);
        Vector3 p2 = new Vector3(6, 4, 9);
        Vector3 p3 = new Vector3(4, 6, 7);

        Vector3[] meshVertices = {
            p0, p1, p3,
            p0, p2, p1,
            p0, p3, p2,
            p1, p2, p3
        };

        int[] triangles = {
            00, 01, 02,
            03, 04, 05,
            06, 07, 08,
            09, 10, 11
        };

        mesh.vertices = meshVertices;
        mesh.triangles = triangles;

        Vertex v0 = new Vertex(p0);
        Vertex v1 = new Vertex(p1);
        Vertex v2 = new Vertex(p2);
        Vertex v3 = new Vertex(p3);

        List<HalfEdge> halfEdges = new List<HalfEdge>();
        HalfEdge e01 = new HalfEdge(v0);
        HalfEdge e02 = new HalfEdge(v0);
        HalfEdge e03 = new HalfEdge(v0);

        HalfEdge e10 = new HalfEdge(v1);
        HalfEdge e12 = new HalfEdge(v1);
        HalfEdge e13 = new HalfEdge(v1);

        HalfEdge e20 = new HalfEdge(v2);
        HalfEdge e21 = new HalfEdge(v2);
        HalfEdge e23 = new HalfEdge(v2);

        HalfEdge e30 = new HalfEdge(v3);
        HalfEdge e31 = new HalfEdge(v3);
        HalfEdge e32 = new HalfEdge(v3);

        e01.Twin = e10;
        e02.Twin = e20;
        e03.Twin = e30;

        e10.Twin = e01;
        e12.Twin = e21;
        e13.Twin = e31;

        e20.Twin = e02;
        e21.Twin = e12;
        e23.Twin = e32;

        e30.Twin = e03;
        e31.Twin = e13;
        e32.Twin = e23;

        e01.Next = e13;
        e02.Next = e21;
        e03.Next = e32;

        e10.Next = e02;
        e12.Next = e23;
        e13.Next = e30;

        e20.Next = e03;
        e21.Next = e10;
        e23.Next = e31;

        e30.Next = e01;
        e31.Next = e12;
        e32.Next = e20;

        e01.Previous = e30;
        e02.Previous = e10;
        e03.Previous = e20;

        e10.Previous = e21;
        e12.Previous = e31;
        e13.Previous = e01;

        e20.Previous = e32;
        e21.Previous = e02;
        e23.Previous = e12;

        e30.Previous = e13;
        e31.Previous = e23;
        e32.Previous = e03;

        halfEdges.Add(e01);
        halfEdges.Add(e02);
        halfEdges.Add(e03);

        halfEdges.Add(e10);
        halfEdges.Add(e12);
        halfEdges.Add(e13);

        halfEdges.Add(e20);
        halfEdges.Add(e21);
        halfEdges.Add(e23);

        halfEdges.Add(e30);
        halfEdges.Add(e31);
        halfEdges.Add(e32);

        List<Face> faces = new List<Face>();

        Face f1 = new Face(0);
        f1.AddOuterComponent(e01);
        f1.AddOuterComponent(e13);
        f1.AddOuterComponent(e30);
        e01.IncidentFace = f1;
        e13.IncidentFace = f1;
        e30.IncidentFace = f1;
        faces.Add(f1);

        Face f2 = new Face(1);
        f2.AddOuterComponent(e21);
        f2.AddOuterComponent(e02);
        f2.AddOuterComponent(e10);
        e21.IncidentFace = f2;
        e02.IncidentFace = f2;
        e10.IncidentFace = f2;
        faces.Add(f2);

        Face f3 = new Face(2);
        f3.AddOuterComponent(e32);
        f3.AddOuterComponent(e03);
        f3.AddOuterComponent(e20);
        e32.IncidentFace = f3;
        e03.IncidentFace = f3;
        e20.IncidentFace = f3;
        faces.Add(f3);

        Face f4 = new Face(3);
        f4.AddOuterComponent(e12);
        f4.AddOuterComponent(e23);
        f4.AddOuterComponent(e31);
        e12.IncidentFace = f4;
        e23.IncidentFace = f4;
        e31.IncidentFace = f4;
        faces.Add(f4);

        v0.AddIncidentEdge(e01);
        v0.AddIncidentEdge(e02);
        v0.AddIncidentEdge(e03);

        v1.AddIncidentEdge(e10);
        v1.AddIncidentEdge(e12);
        v1.AddIncidentEdge(e13);

        v2.AddIncidentEdge(e20);
        v2.AddIncidentEdge(e21);
        v2.AddIncidentEdge(e23);

        v3.AddIncidentEdge(e30);
        v3.AddIncidentEdge(e31);
        v3.AddIncidentEdge(e32);

        List<Vertex> dcelVertices = new List<Vertex> {
            v0, v1, v2, v3
        };

        DCEL expected = new DCEL(
            dcelVertices.AsReadOnly(),
            halfEdges.AsReadOnly(),
            faces.AsReadOnly()
        );

        DCEL actual = DCEL.Build(mesh);

        Assert.AreEqual(actual, expected);
        Assert.AreEqual(actual.GetHashCode(), expected.GetHashCode());
    }

    [Test, MaxTime(5000)]
    public void Equals_Equal()
    {
        List<Vertex> thisVertices = new List<Vertex> {
            new Vertex(new Vector3(0, 4, 3)),
            new Vertex(new Vector3(2, 4, 4)),
            new Vertex(new Vector3(2, 2, 5))
        };
        List<Vertex> otherVertices = new List<Vertex> {
            new Vertex(new Vector3(0, 4, 3)),
            new Vertex(new Vector3(2, 4, 4)),
            new Vertex(new Vector3(2, 2, 5))
        };

        List<HalfEdge> thisEdges = new List<HalfEdge>{
            new HalfEdge(new Vertex(new Vector3(0, 4, 3))),
            new HalfEdge(new Vertex(new Vector3(2, 4, 4))),
            new HalfEdge(new Vertex(new Vector3(2, 2, 5)))
        };
        List<HalfEdge> otherEdges = new List<HalfEdge>{
            new HalfEdge(new Vertex(new Vector3(0, 4, 3))),
            new HalfEdge(new Vertex(new Vector3(2, 4, 4))),
            new HalfEdge(new Vertex(new Vector3(2, 2, 5)))
        };

        List<Face> faces = new List<Face>();

        DCEL thisDcel = new DCEL(
            thisVertices.AsReadOnly(),
            thisEdges.AsReadOnly(),
            faces.AsReadOnly()
        );
        DCEL otherDcel = new DCEL(
            otherVertices.AsReadOnly(),
            otherEdges.AsReadOnly(),
            faces.AsReadOnly()
        );

        Assert.IsTrue(thisDcel.Equals(otherDcel));
        Assert.IsTrue(otherDcel.Equals(thisDcel));

        Assert.AreEqual(thisDcel.GetHashCode(), otherDcel.GetHashCode());
    }

    [Test, MaxTime(5000)]
    public void Equals_NotEqualDifferentVerticesInOther()
    {
        List<Vertex> thisVertices = new List<Vertex> {
            new Vertex(new Vector3(0, 4, 3)),
            new Vertex(new Vector3(2, 4, 4)),
            new Vertex(new Vector3(2, 2, 5))
        };
        List<Vertex> otherVertices = new List<Vertex> {
            new Vertex(new Vector3(1, 4, 3)),
            new Vertex(new Vector3(2, 5, 4)),
            new Vertex(new Vector3(3, 2, 6))
        };

        List<HalfEdge> halfEdges = new List<HalfEdge>();
        List<Face> faces = new List<Face>();

        DCEL thisDcel = new DCEL(
            thisVertices.AsReadOnly(),
            halfEdges.AsReadOnly(),
            faces.AsReadOnly()
        );
        DCEL otherDcel = new DCEL(
            otherVertices.AsReadOnly(),
            halfEdges.AsReadOnly(),
            faces.AsReadOnly()
        );

        Assert.IsFalse(thisDcel.Equals(otherDcel));
        Assert.IsFalse(otherDcel.Equals(thisDcel));
        Assert.AreNotEqual(thisDcel.GetHashCode(), otherDcel.GetHashCode());
    }

    [Test, MaxTime(5000)]
    public void Equals_NotEqualMoreVerticesInThis()
    {
        List<Vertex> thisVertices = new List<Vertex> {
            new Vertex(new Vector3(0, 4, 3)),
            new Vertex(new Vector3(2, 4, 4)),
            new Vertex(new Vector3(2, 2, 5))
        };
        List<Vertex> otherVertices = new List<Vertex> {
            new Vertex(new Vector3(0, 4, 3)),
            new Vertex(new Vector3(2, 4, 4)),
        };

        List<HalfEdge> halfEdges = new List<HalfEdge>();
        List<Face> faces = new List<Face>();

        DCEL thisDcel = new DCEL(
            thisVertices.AsReadOnly(),
            halfEdges.AsReadOnly(),
            faces.AsReadOnly()
        );
        DCEL otherDcel = new DCEL(
            otherVertices.AsReadOnly(),
            halfEdges.AsReadOnly(),
            faces.AsReadOnly()
        );

        Assert.IsFalse(thisDcel.Equals(otherDcel));
        Assert.IsFalse(otherDcel.Equals(thisDcel));
        Assert.AreNotEqual(thisDcel.GetHashCode(), otherDcel.GetHashCode());
    }

    [Test, MaxTime(5000)]
    public void Equals_NotEqualMoreVerticesInOther()
    {
        List<Vertex> thisVertices = new List<Vertex> {
            new Vertex(new Vector3(0, 4, 3)),
            new Vertex(new Vector3(2, 2, 5))
        };
        List<Vertex> otherVertices = new List<Vertex> {
            new Vertex(new Vector3(0, 4, 3)),
            new Vertex(new Vector3(2, 4, 4)),
            new Vertex(new Vector3(2, 2, 5))
        };

        List<HalfEdge> halfEdges = new List<HalfEdge>();
        List<Face> faces = new List<Face>();

        DCEL thisDcel = new DCEL(
            thisVertices.AsReadOnly(),
            halfEdges.AsReadOnly(),
            faces.AsReadOnly()
        );
        DCEL otherDcel = new DCEL(
            otherVertices.AsReadOnly(),
            halfEdges.AsReadOnly(),
            faces.AsReadOnly()
        );

        Assert.IsFalse(thisDcel.Equals(otherDcel));
        Assert.IsFalse(otherDcel.Equals(thisDcel));
        Assert.AreNotEqual(thisDcel.GetHashCode(), otherDcel.GetHashCode());
    }

    [Test, MaxTime(5000)]
    public void Equals_NotEqualDifferentEdgesInOther()
    {
        List<Vertex> thisVertices = new List<Vertex> {
            new Vertex(new Vector3(0, 4, 3)),
            new Vertex(new Vector3(2, 4, 4)),
            new Vertex(new Vector3(2, 2, 5))
        };
        List<Vertex> otherVertices = new List<Vertex> {
            new Vertex(new Vector3(0, 4, 3)),
            new Vertex(new Vector3(2, 4, 4)),
            new Vertex(new Vector3(2, 2, 5))
        };

        List<HalfEdge> thisEdges = new List<HalfEdge>{
            new HalfEdge(new Vertex(new Vector3(0, 4, 3))),
            new HalfEdge(new Vertex(new Vector3(2, 4, 4))),
            new HalfEdge(new Vertex(new Vector3(2, 2, 5)))
        };
        List<HalfEdge> otherEdges = new List<HalfEdge>{
            new HalfEdge(new Vertex(new Vector3(0, 4, 3))),
            new HalfEdge(new Vertex(new Vector3(2, 4, 4))),
            new HalfEdge(new Vertex(new Vector3(2, 4, 5)))
        };

        List<Face> faces = new List<Face>();

        DCEL thisDcel = new DCEL(
            thisVertices.AsReadOnly(),
            thisEdges.AsReadOnly(),
            faces.AsReadOnly()
        );
        DCEL otherDcel = new DCEL(
            otherVertices.AsReadOnly(),
            otherEdges.AsReadOnly(),
            faces.AsReadOnly()
        );

        Assert.IsFalse(thisDcel.Equals(otherDcel));
        Assert.IsFalse(otherDcel.Equals(thisDcel));

        Assert.AreNotEqual(thisDcel.GetHashCode(), otherDcel.GetHashCode());
    }

    [Test, MaxTime(5000)]
    public void Equals_NotEqual_EdgesDifferent()
    {
        List<Vertex> thisVertices = new List<Vertex> {
            new Vertex(new Vector3(0, 4, 3)),
            new Vertex(new Vector3(2, 4, 4)),
            new Vertex(new Vector3(2, 2, 5))
        };
        List<Vertex> otherVertices = new List<Vertex> {
            new Vertex(new Vector3(0, 4, 3)),
            new Vertex(new Vector3(2, 4, 4)),
            new Vertex(new Vector3(2, 2, 5))
        };

        List<HalfEdge> thisEdges = new List<HalfEdge>{
            TestAux.RandomHalfEdge(),
            TestAux.RandomHalfEdge(),
            TestAux.RandomHalfEdge()
        };
        List<HalfEdge> otherEdges = new List<HalfEdge>{
            TestAux.RandomHalfEdge(),
            TestAux.RandomHalfEdge(),
            TestAux.RandomHalfEdge()
        };

        List<Face> faces = new List<Face>();

        DCEL thisDcel = new DCEL(
            thisVertices.AsReadOnly(),
            thisEdges.AsReadOnly(),
            faces.AsReadOnly()
        );
        DCEL otherDcel = new DCEL(
            otherVertices.AsReadOnly(),
            otherEdges.AsReadOnly(),
            faces.AsReadOnly()
        );

        Assert.IsFalse(thisDcel.Equals(otherDcel));
        Assert.IsFalse(otherDcel.Equals(thisDcel));

        Assert.AreNotEqual(thisDcel.GetHashCode(), otherDcel.GetHashCode());
    }

    [Test, MaxTime(1000)]
    public void Equals_EqualWithFullDCEL()
    {
        Vertex v1 = TestAux.RandomVertex();
        Vertex v2 = TestAux.RandomVertex();
        Vertex v3 = TestAux.RandomVertex();

        HalfEdge e11 = new HalfEdge(v1);
        HalfEdge e12 = new HalfEdge(v2);
        HalfEdge e31 = new HalfEdge(v3);
        HalfEdge e32 = new HalfEdge(v1);
        HalfEdge e41 = new HalfEdge(v3);
        HalfEdge e42 = new HalfEdge(v2);

        e11.Twin = e12;
        e12.Twin = e11;
        e31.Twin = e32;
        e32.Twin = e31;
        e41.Twin = e42;
        e42.Twin = e41;

        e12.Next = e32;
        e32.Next = e41;
        e41.Next = e12;

        e12.Previous = e41;
        e32.Previous = e12;
        e41.Previous = e32;

        Face f2 = new Face(0);
        f2.AddOuterComponent(e32);
        f2.AddOuterComponent(e41);
        f2.AddOuterComponent(e12);

        e32.IncidentFace = f2;
        e41.IncidentFace = f2;
        e12.IncidentFace = f2;

        v1.AddIncidentEdge(e11);
        v1.AddIncidentEdge(e32);

        v2.AddIncidentEdge(e42);
        v2.AddIncidentEdge(e12);

        v3.AddIncidentEdge(e31);
        v3.AddIncidentEdge(e41);

        List<HalfEdge> halfEdges = new List<HalfEdge>
        {
            e11, e12,
            e31, e32,
            e41, e42
        };

        List<Face> faces = new List<Face>
        {
            f2
        };

        List<Vertex> dcelVertices = new List<Vertex> { v1, v2, v3 };

        DCEL thisDCEL = new DCEL(
            dcelVertices.AsReadOnly(),
            halfEdges.AsReadOnly(),
            faces.AsReadOnly()
        );

        DCEL otherDCEL = new DCEL(
            dcelVertices.AsReadOnly(),
            halfEdges.AsReadOnly(),
            faces.AsReadOnly()
        );

        Assert.IsTrue(BaseDCEL().Equals(BaseDCEL()));
        Assert.IsTrue(BaseDCEL().Equals(BaseDCEL()));

        Assert.AreEqual(BaseDCEL().GetHashCode(), BaseDCEL().GetHashCode());
    }

    [Test, MaxTime(1000)]
    public void Equals_NotEqualWithFullDCEL_differentVertexPositionsDCEL()
    {
        DCEL baseDCEl = BaseDCEL();
        DCEL otherDCEL = DifferentVertexPositionsDCEL();

        Assert.IsFalse(baseDCEl.Equals(otherDCEL));
        Assert.IsFalse(otherDCEL.Equals(baseDCEl));

        Assert.AreNotEqual(baseDCEl.GetHashCode(), otherDCEL.GetHashCode());
    }

    [Test, MaxTime(1000)]
    public void Equals_NotEqualWithFullDCEL_differentVertexIncidentEdgesDCEL()
    {
        DCEL baseDCEl = BaseDCEL();
        DCEL otherDCEL = DifferentIncidentEdgesForVerticesDCEL();

        Assert.IsFalse(baseDCEl.Equals(otherDCEL));
        Assert.IsFalse(otherDCEL.Equals(baseDCEl));

        Assert.AreNotEqual(baseDCEl.GetHashCode(), otherDCEL.GetHashCode());
    }

    [Test, MaxTime(1000)]
    public void Equals_NotEqualWithFullDCEL_DifferentEdgesExtraPairDCEL()
    {
        DCEL baseDCEl = BaseDCEL();
        DCEL otherDCEL = DifferentEdgesExtraEdge13DCEL();

        Assert.IsFalse(baseDCEl.Equals(otherDCEL));
        Assert.IsFalse(otherDCEL.Equals(baseDCEl));

        Assert.AreNotEqual(baseDCEl.GetHashCode(), otherDCEL.GetHashCode());
    }

    [Test, MaxTime(1000)]
    public void Equals_NotEqualWithFullDCEL_DifferentEdgesExtraEdge13NothingSetDCEL()
    {
        DCEL baseDCEl = BaseDCEL();
        DCEL otherDCEL = DifferentEdgesExtraEdge13NothingSetDCEL();

        Assert.IsFalse(baseDCEl.Equals(otherDCEL));
        Assert.IsFalse(otherDCEL.Equals(baseDCEl));

        Assert.AreNotEqual(baseDCEl.GetHashCode(), otherDCEL.GetHashCode());
    }

    [Test, MaxTime(1000)]
    public void Equals_NotEqualWithFullDCEL_DifferentEdgesExtraEdge13OnlyTwinSet()
    {
        DCEL baseDCEl = BaseDCEL();
        DCEL otherDCEL = DifferentEdgesExtraEdge13OnlyTwinSetDCEL();

        Assert.IsFalse(baseDCEl.Equals(otherDCEL));
        Assert.IsFalse(otherDCEL.Equals(baseDCEl));

        Assert.AreNotEqual(baseDCEl.GetHashCode(), otherDCEL.GetHashCode());
    }

    [Test, MaxTime(1000)]
    public void Equals_NotEqualWithFullDCEL_DifferentEdgesExtraEdge13OnlyTwinNextSet()
    {
        DCEL baseDCEl = BaseDCEL();
        DCEL otherDCEL = DifferentEdgesExtraEdge13OnlyTwinNextSetDCEL();

        Assert.IsFalse(baseDCEl.Equals(otherDCEL));
        Assert.IsFalse(otherDCEL.Equals(baseDCEl));

        Assert.AreNotEqual(baseDCEl.GetHashCode(), otherDCEL.GetHashCode());
    }

    [Test, MaxTime(1000)]
    public void Equals_NotEqualWithFullDCEL_DifferentEdgesExtraEdge13OnlyTwinNextPreviousSet()
    {
        DCEL baseDCEl = BaseDCEL();
        DCEL otherDCEL = DifferentEdgesExtraEdge13OnlyTwinNextPreviousSetDCEL();

        Assert.IsFalse(baseDCEl.Equals(otherDCEL));
        Assert.IsFalse(otherDCEL.Equals(baseDCEl));

        Assert.AreNotEqual(baseDCEl.GetHashCode(), otherDCEL.GetHashCode());
    }

    private DCEL DifferentVertexPositionsDCEL()
    {
        Vertex v1 = new Vertex(new Vector3(2, 3, 4));
        Vertex v2 = new Vertex(new Vector3(7, 3, 5));
        Vertex v3 = new Vertex(new Vector3(6, 5, 9));

        HalfEdge e11 = new HalfEdge(v1);
        HalfEdge e12 = new HalfEdge(v2);
        HalfEdge e31 = new HalfEdge(v3);
        HalfEdge e32 = new HalfEdge(v1);
        HalfEdge e41 = new HalfEdge(v3);
        HalfEdge e42 = new HalfEdge(v2);

        e11.Twin = e12;
        e12.Twin = e11;
        e31.Twin = e32;
        e32.Twin = e31;
        e41.Twin = e42;
        e42.Twin = e41;

        e12.Next = e32;
        e32.Next = e41;
        e41.Next = e12;

        e12.Previous = e41;
        e32.Previous = e12;
        e41.Previous = e32;

        Face f2 = new Face(0);
        f2.AddOuterComponent(e32);
        f2.AddOuterComponent(e41);
        f2.AddOuterComponent(e12);

        e32.IncidentFace = f2;
        e41.IncidentFace = f2;
        e12.IncidentFace = f2;

        v1.AddIncidentEdge(e11);
        v1.AddIncidentEdge(e32);

        v2.AddIncidentEdge(e42);
        v2.AddIncidentEdge(e12);

        v3.AddIncidentEdge(e31);
        v3.AddIncidentEdge(e41);

        List<HalfEdge> halfEdges = new List<HalfEdge>
        {
            e11, e12,
            e31, e32,
            e41, e42
        };

        List<Face> faces = new List<Face>
        {
            f2
        };

        List<Vertex> dcelVertices = new List<Vertex> { v1, v2, v3 };

        DCEL dcel = new DCEL(
            dcelVertices.AsReadOnly(),
            halfEdges.AsReadOnly(),
            faces.AsReadOnly()
        );

        return dcel;
    }

    private DCEL DifferentIncidentEdgesForVerticesDCEL()
    {
        Vertex v1 = new Vertex(new Vector3(2, 3, 4));
        Vertex v2 = new Vertex(new Vector3(7, 3, 5));
        Vertex v3 = new Vertex(new Vector3(6, 4, 9));

        HalfEdge e11 = new HalfEdge(v1);
        HalfEdge e12 = new HalfEdge(v2);
        HalfEdge e31 = new HalfEdge(v3);
        HalfEdge e32 = new HalfEdge(v1);
        HalfEdge e41 = new HalfEdge(v3);
        HalfEdge e42 = new HalfEdge(v2);

        e11.Twin = e12;
        e12.Twin = e11;
        e31.Twin = e32;
        e32.Twin = e31;
        e41.Twin = e42;
        e42.Twin = e41;

        e12.Next = e32;
        e32.Next = e41;
        e41.Next = e12;

        e12.Previous = e41;
        e32.Previous = e12;
        e41.Previous = e32;

        Face f2 = new Face(0);
        f2.AddOuterComponent(e32);
        f2.AddOuterComponent(e41);
        f2.AddOuterComponent(e12);

        e32.IncidentFace = f2;
        e41.IncidentFace = f2;
        e12.IncidentFace = f2;

        v1.AddIncidentEdge(e11);
        v1.AddIncidentEdge(e32);

        v2.AddIncidentEdge(e42);
        v2.AddIncidentEdge(e12);

        //The different incident edge
        v3.AddIncidentEdge(e11);
        v3.AddIncidentEdge(e41);

        List<HalfEdge> halfEdges = new List<HalfEdge>
        {
            e11, e12,
            e31, e32,
            e41, e42
        };

        List<Face> faces = new List<Face>
        {
            f2
        };

        List<Vertex> dcelVertices = new List<Vertex> { v1, v2, v3 };

        DCEL dcel = new DCEL(
            dcelVertices.AsReadOnly(),
            halfEdges.AsReadOnly(),
            faces.AsReadOnly()
        );

        return dcel;
    }

    private DCEL DifferentEdgesExtraEdge13DCEL()
    {
        Vertex v1 = new Vertex(new Vector3(2, 3, 4));
        Vertex v2 = new Vertex(new Vector3(7, 3, 5));
        Vertex v3 = new Vertex(new Vector3(6, 4, 9));

        HalfEdge e11 = new HalfEdge(v1);
        HalfEdge e12 = new HalfEdge(v2);
        HalfEdge e31 = new HalfEdge(v3);
        HalfEdge e32 = new HalfEdge(v1);
        HalfEdge e41 = new HalfEdge(v3);
        HalfEdge e42 = new HalfEdge(v2);
        HalfEdge e13 = new HalfEdge(v1);

        e11.Twin = e12;
        e12.Twin = e11;
        e31.Twin = e32;
        e32.Twin = e31;
        e41.Twin = e42;
        e42.Twin = e41;
        e13.Twin = e31;

        e12.Next = e32;
        e32.Next = e41;
        e41.Next = e12;
        e13.Next = e11;

        e12.Previous = e41;
        e32.Previous = e12;
        e41.Previous = e32;
        e13.Previous = e41;

        Face f2 = new Face(0);
        f2.AddOuterComponent(e32);
        f2.AddOuterComponent(e41);
        f2.AddOuterComponent(e12);

        e32.IncidentFace = f2;
        e41.IncidentFace = f2;
        e12.IncidentFace = f2;
        e13.IncidentFace = f2;

        v1.AddIncidentEdge(e11);
        v1.AddIncidentEdge(e32);
        v1.AddIncidentEdge(e13);

        v2.AddIncidentEdge(e42);
        v2.AddIncidentEdge(e12);

        v3.AddIncidentEdge(e31);
        v3.AddIncidentEdge(e41);

        List<HalfEdge> halfEdges = new List<HalfEdge>
        {
            e11, e12, e13,
            e31, e32,
            e41, e42
        };

        List<Face> faces = new List<Face>
        {
            f2
        };

        List<Vertex> dcelVertices = new List<Vertex> { v1, v2, v3 };

        DCEL dcel = new DCEL(
            dcelVertices.AsReadOnly(),
            halfEdges.AsReadOnly(),
            faces.AsReadOnly()
        );

        return dcel;
    }

    private DCEL DifferentEdgesExtraEdge13NothingSetDCEL()
    {
        Vertex v1 = new Vertex(new Vector3(2, 3, 4));
        Vertex v2 = new Vertex(new Vector3(7, 3, 5));
        Vertex v3 = new Vertex(new Vector3(6, 4, 9));

        HalfEdge e11 = new HalfEdge(v1);
        HalfEdge e12 = new HalfEdge(v2);
        HalfEdge e31 = new HalfEdge(v3);
        HalfEdge e32 = new HalfEdge(v1);
        HalfEdge e41 = new HalfEdge(v3);
        HalfEdge e42 = new HalfEdge(v2);
        HalfEdge e13 = new HalfEdge(v1);

        e11.Twin = e12;
        e12.Twin = e11;
        e31.Twin = e32;
        e32.Twin = e31;
        e41.Twin = e42;
        e42.Twin = e41;

        e12.Next = e32;
        e32.Next = e41;
        e41.Next = e12;

        e12.Previous = e41;
        e32.Previous = e12;
        e41.Previous = e32;

        Face f2 = new Face(0);
        f2.AddOuterComponent(e32);
        f2.AddOuterComponent(e41);
        f2.AddOuterComponent(e12);

        e32.IncidentFace = f2;
        e41.IncidentFace = f2;
        e12.IncidentFace = f2;

        v1.AddIncidentEdge(e11);
        v1.AddIncidentEdge(e32);

        v2.AddIncidentEdge(e42);
        v2.AddIncidentEdge(e12);

        v3.AddIncidentEdge(e31);
        v3.AddIncidentEdge(e41);

        List<HalfEdge> halfEdges = new List<HalfEdge>
        {
            e11, e12, e13,
            e31, e32,
            e41, e42
        };

        List<Face> faces = new List<Face>
        {
            f2
        };

        List<Vertex> dcelVertices = new List<Vertex> { v1, v2, v3 };

        DCEL dcel = new DCEL(
            dcelVertices.AsReadOnly(),
            halfEdges.AsReadOnly(),
            faces.AsReadOnly()
        );

        return dcel;
    }

    private DCEL DifferentEdgesExtraEdge13OnlyTwinSetDCEL()
    {
        Vertex v1 = new Vertex(new Vector3(2, 3, 4));
        Vertex v2 = new Vertex(new Vector3(7, 3, 5));
        Vertex v3 = new Vertex(new Vector3(6, 4, 9));

        HalfEdge e11 = new HalfEdge(v1);
        HalfEdge e12 = new HalfEdge(v2);
        HalfEdge e31 = new HalfEdge(v3);
        HalfEdge e32 = new HalfEdge(v1);
        HalfEdge e41 = new HalfEdge(v3);
        HalfEdge e42 = new HalfEdge(v2);
        HalfEdge e13 = new HalfEdge(v1);

        e11.Twin = e12;
        e12.Twin = e11;
        e31.Twin = e32;
        e32.Twin = e31;
        e41.Twin = e42;
        e42.Twin = e41;
        e13.Twin = e31;

        e12.Next = e32;
        e32.Next = e41;
        e41.Next = e12;

        e12.Previous = e41;
        e32.Previous = e12;
        e41.Previous = e32;

        Face f2 = new Face(0);
        f2.AddOuterComponent(e32);
        f2.AddOuterComponent(e41);
        f2.AddOuterComponent(e12);

        e32.IncidentFace = f2;
        e41.IncidentFace = f2;
        e12.IncidentFace = f2;

        v1.AddIncidentEdge(e11);
        v1.AddIncidentEdge(e32);

        v2.AddIncidentEdge(e42);
        v2.AddIncidentEdge(e12);

        v3.AddIncidentEdge(e31);
        v3.AddIncidentEdge(e41);

        List<HalfEdge> halfEdges = new List<HalfEdge>
        {
            e11, e12, e13,
            e31, e32,
            e41, e42
        };

        List<Face> faces = new List<Face>
        {
            f2
        };

        List<Vertex> dcelVertices = new List<Vertex> { v1, v2, v3 };

        DCEL dcel = new DCEL(
            dcelVertices.AsReadOnly(),
            halfEdges.AsReadOnly(),
            faces.AsReadOnly()
        );

        return dcel;
    }

    private DCEL DifferentEdgesExtraEdge13OnlyTwinNextSetDCEL()
    {
        Vertex v1 = new Vertex(new Vector3(2, 3, 4));
        Vertex v2 = new Vertex(new Vector3(7, 3, 5));
        Vertex v3 = new Vertex(new Vector3(6, 4, 9));

        HalfEdge e11 = new HalfEdge(v1);
        HalfEdge e12 = new HalfEdge(v2);
        HalfEdge e31 = new HalfEdge(v3);
        HalfEdge e32 = new HalfEdge(v1);
        HalfEdge e41 = new HalfEdge(v3);
        HalfEdge e42 = new HalfEdge(v2);
        HalfEdge e13 = new HalfEdge(v1);

        e11.Twin = e12;
        e12.Twin = e11;
        e31.Twin = e32;
        e32.Twin = e31;
        e41.Twin = e42;
        e42.Twin = e41;
        e13.Twin = e31;

        e12.Next = e32;
        e32.Next = e41;
        e41.Next = e12;
        e13.Next = e11;

        e12.Previous = e41;
        e32.Previous = e12;
        e41.Previous = e32;

        Face f2 = new Face(0);
        f2.AddOuterComponent(e32);
        f2.AddOuterComponent(e41);
        f2.AddOuterComponent(e12);

        e32.IncidentFace = f2;
        e41.IncidentFace = f2;
        e12.IncidentFace = f2;

        v1.AddIncidentEdge(e11);
        v1.AddIncidentEdge(e32);

        v2.AddIncidentEdge(e42);
        v2.AddIncidentEdge(e12);

        v3.AddIncidentEdge(e31);
        v3.AddIncidentEdge(e41);

        List<HalfEdge> halfEdges = new List<HalfEdge>
        {
            e11, e12, e13,
            e31, e32,
            e41, e42
        };

        List<Face> faces = new List<Face>
        {
            f2
        };

        List<Vertex> dcelVertices = new List<Vertex> { v1, v2, v3 };

        DCEL dcel = new DCEL(
            dcelVertices.AsReadOnly(),
            halfEdges.AsReadOnly(),
            faces.AsReadOnly()
        );

        return dcel;
    }

    private DCEL DifferentEdgesExtraEdge13OnlyTwinNextPreviousSetDCEL()
    {
        Vertex v1 = new Vertex(new Vector3(2, 3, 4));
        Vertex v2 = new Vertex(new Vector3(7, 3, 5));
        Vertex v3 = new Vertex(new Vector3(6, 4, 9));

        HalfEdge e11 = new HalfEdge(v1);
        HalfEdge e12 = new HalfEdge(v2);
        HalfEdge e31 = new HalfEdge(v3);
        HalfEdge e32 = new HalfEdge(v1);
        HalfEdge e41 = new HalfEdge(v3);
        HalfEdge e42 = new HalfEdge(v2);
        HalfEdge e13 = new HalfEdge(v1);

        e11.Twin = e12;
        e12.Twin = e11;
        e31.Twin = e32;
        e32.Twin = e31;
        e41.Twin = e42;
        e42.Twin = e41;
        e13.Twin = e31;

        e12.Next = e32;
        e32.Next = e41;
        e41.Next = e12;
        e13.Next = e11;

        e12.Previous = e41;
        e32.Previous = e12;
        e41.Previous = e32;
        e13.Previous = e41;

        Face f2 = new Face(0);
        f2.AddOuterComponent(e32);
        f2.AddOuterComponent(e41);
        f2.AddOuterComponent(e12);

        e32.IncidentFace = f2;
        e41.IncidentFace = f2;
        e12.IncidentFace = f2;

        v1.AddIncidentEdge(e11);
        v1.AddIncidentEdge(e32);

        v2.AddIncidentEdge(e42);
        v2.AddIncidentEdge(e12);

        v3.AddIncidentEdge(e31);
        v3.AddIncidentEdge(e41);

        List<HalfEdge> halfEdges = new List<HalfEdge>
        {
            e11, e12, e13,
            e31, e32,
            e41, e42
        };

        List<Face> faces = new List<Face>
        {
            f2
        };

        List<Vertex> dcelVertices = new List<Vertex> { v1, v2, v3 };

        DCEL dcel = new DCEL(
            dcelVertices.AsReadOnly(),
            halfEdges.AsReadOnly(),
            faces.AsReadOnly()
        );

        return dcel;
    }

    private DCEL BaseDCEL()
    {
        Vertex v1 = new Vertex(new Vector3(2, 3, 4));
        Vertex v2 = new Vertex(new Vector3(7, 3, 5));
        Vertex v3 = new Vertex(new Vector3(6, 4, 9));

        HalfEdge e11 = new HalfEdge(v1);
        HalfEdge e12 = new HalfEdge(v2);
        HalfEdge e31 = new HalfEdge(v3);
        HalfEdge e32 = new HalfEdge(v1);
        HalfEdge e41 = new HalfEdge(v3);
        HalfEdge e42 = new HalfEdge(v2);

        e11.Twin = e12;
        e12.Twin = e11;
        e31.Twin = e32;
        e32.Twin = e31;
        e41.Twin = e42;
        e42.Twin = e41;

        e12.Next = e32;
        e32.Next = e41;
        e41.Next = e12;

        e12.Previous = e41;
        e32.Previous = e12;
        e41.Previous = e32;

        Face f2 = new Face(0);
        f2.AddOuterComponent(e32);
        f2.AddOuterComponent(e41);
        f2.AddOuterComponent(e12);

        e32.IncidentFace = f2;
        e41.IncidentFace = f2;
        e12.IncidentFace = f2;

        v1.AddIncidentEdge(e11);
        v1.AddIncidentEdge(e32);

        v2.AddIncidentEdge(e42);
        v2.AddIncidentEdge(e12);

        v3.AddIncidentEdge(e31);
        v3.AddIncidentEdge(e41);

        List<HalfEdge> halfEdges = new List<HalfEdge>
        {
            e11, e12,
            e31, e32,
            e41, e42
        };

        List<Face> faces = new List<Face>
        {
            f2
        };

        List<Vertex> dcelVertices = new List<Vertex> { v1, v2, v3 };

        DCEL dcel = new DCEL(
            dcelVertices.AsReadOnly(),
            halfEdges.AsReadOnly(),
            faces.AsReadOnly()
        );

        return dcel;
    }
}