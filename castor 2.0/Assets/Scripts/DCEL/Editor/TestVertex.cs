using UnityEngine;
using NUnit.Framework;

using DoubleConnectedEdgeList;

namespace Tests
{
    [TestFixture]
    public class VertexTest
    {
        [SetUp]
        public void Init()
        {
            Random.InitState(42);
        }

        [Test, MaxTime(2000)]
        public void TestEquals_Equals()
        {
            Vector3 position = Auxilaries.RandomPosition();

            Vertex thisVertex = new Vertex(position, 1);
            Vertex otherVertex = new Vertex(position, 1);

            HalfEdge edge1 = Auxilaries.RandomHalfEdge();
            HalfEdge edge2 = Auxilaries.RandomHalfEdge();

            thisVertex.AddIncidentEdge(edge1);
            thisVertex.AddIncidentEdge(edge2);

            otherVertex.AddIncidentEdge(edge1);
            otherVertex.AddIncidentEdge(edge2);

            Assert.IsTrue(thisVertex.Equals(otherVertex));
            Assert.IsTrue(otherVertex.Equals(thisVertex));
            Assert.AreEqual(thisVertex.GetHashCode(), otherVertex.GetHashCode());
        }

        [Test, MaxTime(2000)]
        public void TestEquals_MeshIdxNotEqual()
        {
            Vector3 position = Auxilaries.RandomPosition();

            Vertex thisVertex = new Vertex(position, 0);
            Vertex otherVertex = new Vertex(position, 1);

            Assert.IsTrue(thisVertex.Equals(otherVertex));
            Assert.IsTrue(otherVertex.Equals(thisVertex));
            Assert.AreEqual(thisVertex, otherVertex);
        }

        [Test, MaxTime(2000)]
        public void TestEquals_PositionNotEqual()
        {
            Vertex thisVertex = Auxilaries.RandomVertex();
            Vertex otherVertex = Auxilaries.RandomVertex();

            Assert.IsFalse(thisVertex.Equals(otherVertex));
            Assert.IsFalse(otherVertex.Equals(thisVertex));
            Assert.AreNotEqual(thisVertex, otherVertex);
        }

        [Test, MaxTime(2000)]
        public void TestEquals_IncidentEdgesNotEqual()
        {
            Vector3 position = Auxilaries.RandomPosition();

            Vertex thisVertex = new Vertex(position);
            Vertex otherVertex = new Vertex(position);

            HalfEdge edge1 = Auxilaries.RandomHalfEdge();
            HalfEdge edge2 = Auxilaries.RandomHalfEdge();
            HalfEdge edge3 = Auxilaries.RandomHalfEdge();

            thisVertex.AddIncidentEdge(edge1);
            thisVertex.AddIncidentEdge(edge2);

            otherVertex.AddIncidentEdge(edge3);

            Assert.IsFalse(thisVertex.Equals(otherVertex));
            Assert.IsFalse(otherVertex.Equals(thisVertex));
            Assert.AreNotEqual(thisVertex.GetHashCode(), otherVertex.GetHashCode());
        }

        [Test, MaxTime(2000)]
        public void TestEquals_IncidentEdgesSuperSet()
        {
            Vector3 position = Auxilaries.RandomPosition();

            Vertex thisVertex = new Vertex(position);
            Vertex otherVertex = new Vertex(position);

            HalfEdge edge1 = Auxilaries.RandomHalfEdge();
            HalfEdge edge2 = Auxilaries.RandomHalfEdge();
            HalfEdge edge3 = Auxilaries.RandomHalfEdge();

            thisVertex.AddIncidentEdge(edge1);
            thisVertex.AddIncidentEdge(edge2);
            thisVertex.AddIncidentEdge(edge3);

            otherVertex.AddIncidentEdge(edge3);

            Assert.IsFalse(thisVertex.Equals(otherVertex));
            Assert.IsFalse(otherVertex.Equals(thisVertex));
            Assert.AreNotEqual(thisVertex.GetHashCode(), otherVertex.GetHashCode());
        }

        [Test, MaxTime(2000)]
        public void TestEquals_IncidentEdgesAddedTwice()
        {
            Vector3 position = Auxilaries.RandomPosition();

            Vertex thisVertex = new Vertex(position);
            Vertex otherVertex = new Vertex(position);

            HalfEdge edge1 = Auxilaries.RandomHalfEdge();
            HalfEdge edge2 = Auxilaries.RandomHalfEdge();

            thisVertex.AddIncidentEdge(edge1);
            thisVertex.AddIncidentEdge(edge2);
            thisVertex.AddIncidentEdge(edge1);

            otherVertex.AddIncidentEdge(edge1);
            otherVertex.AddIncidentEdge(edge2);

            Assert.IsTrue(thisVertex.Equals(otherVertex));
            Assert.IsTrue(otherVertex.Equals(thisVertex));
            Assert.AreEqual(thisVertex.GetHashCode(), otherVertex.GetHashCode());
        }

        [Test, MaxTime(2000)]
        public void TestAddIncidentEdges_NoOtherPropertiesSet()
        {
            Vertex v1 = Auxilaries.RandomVertex();
            HalfEdge e11 = new HalfEdge(v1);
            v1.AddIncidentEdge(e11);

            Assert.Contains(e11, v1.IncidentEdges);
        }

        [Test, MaxTime(2000)]
        public void TestAddIncidentEdges_TwinsSet()
        {
            Vertex v1 = Auxilaries.RandomVertex();
            Vertex v2 = Auxilaries.RandomVertex();
            Vertex v3 = Auxilaries.RandomVertex();

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

            v1.AddIncidentEdge(e32);

            Assert.Contains(e32, v1.IncidentEdges);
        }

        [Test, MaxTime(2000)]
        public void TestAddIncidentEdges_TwinsAndNextSet()
        {
            Vertex v1 = Auxilaries.RandomVertex();
            Vertex v2 = Auxilaries.RandomVertex();
            Vertex v3 = Auxilaries.RandomVertex();

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

            v1.AddIncidentEdge(e32);

            Assert.Contains(e32, v1.IncidentEdges);
        }

        [Test, MaxTime(2000)]
        public void TestAddIncidentEdges_TwinsAndNextAndPreviousSet()
        {
            Vertex v1 = Auxilaries.RandomVertex();
            Vertex v2 = Auxilaries.RandomVertex();
            Vertex v3 = Auxilaries.RandomVertex();

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

            v1.AddIncidentEdge(e32);

            Assert.Contains(e32, v1.IncidentEdges);
        }

        [Test, MaxTime(2000)]
        public void TestAddIncidentEdges_TwinsAndNextAndPreviousOuterComponentSet()
        {
            Vertex v1 = Auxilaries.RandomVertex();
            Vertex v2 = Auxilaries.RandomVertex();
            Vertex v3 = Auxilaries.RandomVertex();

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

            Face f2 = new Face(2);
            f2.AddOuterComponent(e32);
            f2.AddOuterComponent(e41);
            f2.AddOuterComponent(e12);

            v1.AddIncidentEdge(e32);

            Assert.Contains(e32, v1.IncidentEdges);
        }

        [Test, MaxTime(2000)]
        public void TestAddIncidentEdges_AddSingleEdge()
        {
            Vertex v1 = Auxilaries.RandomVertex();
            Vertex v2 = Auxilaries.RandomVertex();
            Vertex v3 = Auxilaries.RandomVertex();

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

            Face f2 = new Face(2);
            f2.AddOuterComponent(e32);
            f2.AddOuterComponent(e41);
            f2.AddOuterComponent(e12);

            e32.IncidentFace = f2;
            e41.IncidentFace = f2;
            e12.IncidentFace = f2;

            v1.AddIncidentEdge(e11);

            Assert.Contains(e11, v1.IncidentEdges);
        }

        [Test, MaxTime(2000)]
        public void TestAddIncidentEdges_AddOtherSingleEdge()
        {
            Vertex v1 = Auxilaries.RandomVertex();
            Vertex v2 = Auxilaries.RandomVertex();
            Vertex v3 = Auxilaries.RandomVertex();

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

            Face f2 = new Face(2);
            f2.AddOuterComponent(e32);
            f2.AddOuterComponent(e41);
            f2.AddOuterComponent(e12);

            e32.IncidentFace = f2;
            e41.IncidentFace = f2;
            e12.IncidentFace = f2;

            v1.AddIncidentEdge(e32);

            Assert.Contains(e32, v1.IncidentEdges);
        }

        [Test, MaxTime(2000)]
        public void TestAddIncidentEdges_AddSingleEdgePerVertex()
        {
            Vertex v1 = Auxilaries.RandomVertex();
            Vertex v2 = Auxilaries.RandomVertex();
            Vertex v3 = Auxilaries.RandomVertex();

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

            Face f2 = new Face(2);
            f2.AddOuterComponent(e32);
            f2.AddOuterComponent(e41);
            f2.AddOuterComponent(e12);

            e32.IncidentFace = f2;
            e41.IncidentFace = f2;
            e12.IncidentFace = f2;

            v1.AddIncidentEdge(e11);

            v2.AddIncidentEdge(e42);

            v3.AddIncidentEdge(e31);

            Assert.Contains(e11, v1.IncidentEdges);
            Assert.Contains(e42, v2.IncidentEdges);
            Assert.Contains(e31, v3.IncidentEdges);
        }

        [Test, MaxTime(2000)]
        public void TestAddIncidentEdges_AddTwoEdgesToSingleVertex()
        {
            Vertex v1 = Auxilaries.RandomVertex();
            Vertex v2 = Auxilaries.RandomVertex();
            Vertex v3 = Auxilaries.RandomVertex();

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

            Face f2 = new Face(2);
            f2.AddOuterComponent(e32);
            f2.AddOuterComponent(e41);
            f2.AddOuterComponent(e12);

            e32.IncidentFace = f2;
            e41.IncidentFace = f2;
            e12.IncidentFace = f2;

            v1.AddIncidentEdge(e11);
            v1.AddIncidentEdge(e32);

            Assert.Contains(e11, v1.IncidentEdges);
            Assert.Contains(e32, v1.IncidentEdges);
        }

        [Test, MaxTime(2000)]
        public void TestAddIncidentEdges_ReplicateSegmentationFault()
        {
            Vertex v1 = Auxilaries.RandomVertex();
            Vertex v2 = Auxilaries.RandomVertex();
            Vertex v3 = Auxilaries.RandomVertex();

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

            Face f2 = new Face(2);
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

            Assert.Contains(e11, v1.IncidentEdges);
            Assert.Contains(e32, v1.IncidentEdges);

            Assert.Contains(e42, v2.IncidentEdges);
            Assert.Contains(e12, v2.IncidentEdges);

            Assert.Contains(e31, v3.IncidentEdges);
            Assert.Contains(e41, v3.IncidentEdges);
        }

        [Test, MaxTime(2000)]
        public void TestCompareTo_ObjNull()
        {
            Vertex vertex = Auxilaries.RandomVertex();

            int expected = 1;
            int actual = vertex.CompareTo(null);
            Assert.AreEqual(expected, actual);
        }

        [Test, MaxTime(2000)]
        public void TestCompareTo_ObjNotVertex()
        {
            Assert.Throws(typeof(System.ArgumentException), new TestDelegate(TestCompareTo_ObjNotVertex_Helper));
        }

        void TestCompareTo_ObjNotVertex_Helper()
        {
            Vertex vertex = Auxilaries.RandomVertex();
            HalfEdge edge = Auxilaries.RandomHalfEdge();

            vertex.CompareTo(edge);
        }

        [Test, MaxTime(2000)]
        public void TestCompareTo_MeshIdxSmaller()
        {
            Vector3 position = Auxilaries.RandomPosition();

            Vertex thisVertex = new Vertex(position, 0);
            Vertex otherVertex = new Vertex(position, 1);

            int expected = -1;
            int actual = thisVertex.CompareTo(otherVertex);

            Assert.AreEqual(expected, actual);
        }

        [Test, MaxTime(2000)]
        public void TestCompareTo_MeshIdxLarger()
        {
            Vector3 position = Auxilaries.RandomPosition();

            Vertex thisVertex = new Vertex(position, 1);
            Vertex otherVertex = new Vertex(position, 0);

            int expected = 1;
            int actual = thisVertex.CompareTo(otherVertex);

            Assert.AreEqual(expected, actual);
        }

        [Test, MaxTime(2000)]
        public void TestCompareTo_ObjSmallerMeshIdxEqual()
        {
            Vertex thisVertex = new Vertex(new Vector3(0, 1, 2));
            Vertex otherVertex = new Vertex(new Vector3(3, 4, 5));

            int expected = -1;
            int actual = thisVertex.CompareTo(otherVertex);

            Assert.AreEqual(expected, actual);
        }

        [Test, MaxTime(2000)]
        public void TestCompareTo_ObjEqualMeshIdxEqual()
        {
            Vertex thisVertex = new Vertex(new Vector3(1, 2, 3));
            Vertex otherVertex = new Vertex(new Vector3(1, 2, 3));

            int expected = 0;
            int actual = thisVertex.CompareTo(otherVertex);

            Assert.AreEqual(expected, actual);
        }

        [Test, MaxTime(2000)]
        public void TestCompareTo_ObjLargerMeshIdxEqual()
        {
            Vertex thisVertex = new Vertex(new Vector3(3, 4, 5));
            Vertex otherVertex = new Vertex(new Vector3(0, 1, 2));

            int expected = 1;
            int actual = thisVertex.CompareTo(otherVertex);

            Assert.AreEqual(expected, actual);
        }

        [Test, MaxTime(2000)]
        public void TestGetMeshIdx_NotSet()
        {
            Assert.Throws(typeof(System.ArgumentException), new TestDelegate(TestGetMeshIdx_NotSet_Helper));
        }

        public void TestGetMeshIdx_NotSet_Helper()
        {
            Vertex vertex = Auxilaries.RandomVertex();
            int meshIdx = vertex.MeshIdx;
        }

        [Test, MaxTime(2000)]
        public void TestGetMeshIdx_Set()
        {
            Vertex vertex = new Vertex(Auxilaries.RandomPosition(), 1);

            int expected = 1;
            int actual = vertex.MeshIdx;

            Assert.AreEqual(expected, actual);
        }
    }

    [TestFixture]
    public class VertexSimpleComparerTest
    {
        private Vertex.SimpleComparer comparer;

        [SetUp]
        public void Init()
        {
            comparer = new Vertex.SimpleComparer();
        }

        [Test, MaxTime(50)]
        public void XIsNull()
        {
            Vertex x = null;
            Vertex y = Auxilaries.RandomVertex();

            Assert.IsFalse(comparer.Equals(x, y));
            Assert.AreNotEqual(comparer.GetHashCode(x), comparer.GetHashCode(y));
        }

        [Test, MaxTime(50)]
        public void YIsNull()
        {
            Vertex x = Auxilaries.RandomVertex();
            Vertex y = null;

            Assert.IsFalse(comparer.Equals(x, y));
            Assert.AreNotEqual(comparer.GetHashCode(x), comparer.GetHashCode(y));
        }

        [Test, MaxTime(50)]
        public void XAndyAreNull()
        {
            Vertex x = null;
            Vertex y = null;

            Assert.IsTrue(comparer.Equals(x, y));
            Assert.AreEqual(comparer.GetHashCode(x), comparer.GetHashCode(y));
        }

        [Test, MaxTime(50)]
        public void XAndyAreEqual()
        {
            Vector3 positon = Auxilaries.RandomPosition();
            Vertex x = new Vertex(positon);
            Vertex y = new Vertex(positon);

            HalfEdge edge = Auxilaries.RandomHalfEdge();

            x.AddIncidentEdge(edge);
            y.AddIncidentEdge(edge);

            Assert.IsTrue(comparer.Equals(x, y));
            Assert.AreEqual(comparer.GetHashCode(x), comparer.GetHashCode(y));
        }

        [Test, MaxTime(50)]
        public void XAndyAreNotEqual_PositionDifferent()
        {
            Vertex x = Auxilaries.RandomVertex();
            Vertex y = Auxilaries.RandomVertex();

            Assert.IsFalse(comparer.Equals(x, y));
            Assert.AreNotEqual(comparer.GetHashCode(x), comparer.GetHashCode(y));
        }

        [Test, MaxTime(50)]
        public void XAndyAreEqual_IncidentEdgesDifferent()
        {
            Vector3 positon = Auxilaries.RandomPosition();
            Vertex x = new Vertex(positon);
            Vertex y = new Vertex(positon);

            x.AddIncidentEdge(Auxilaries.RandomHalfEdge());
            y.AddIncidentEdge(Auxilaries.RandomHalfEdge());

            Assert.IsTrue(comparer.Equals(x, y));
            Assert.AreEqual(comparer.GetHashCode(x), comparer.GetHashCode(y));
        }
    }
}