using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

using Utils;
using System.Linq;

namespace DoubleConnectedEdgeList
{
    public class DCEL : IEquatable<DCEL>
    {
        private Dictionary<Vector3, Vertex> vertices;
        private List<HalfEdge> halfEdges;
        private Dictionary<int, Face> faces;

        public ReadOnlyCollection<Vertex> Vertices
        {
            get { return new ReadOnlyCollection<Vertex>(vertices.Values.ToList()); }
        }

        public ReadOnlyCollection<Face> Faces
        {
            get { return new ReadOnlyCollection<Face>(faces.Values.ToList<Face>()); }
        }

        public DCEL(ReadOnlyCollection<Vertex> vertices,
                   ReadOnlyCollection<HalfEdge> edges,
                   ReadOnlyCollection<Face> faces
                   ) : this()
        {
            foreach (Vertex vertex in vertices) AddVertex(vertex);
            this.halfEdges.AddRange(edges);
            foreach (Face face in faces) AddFace(face);
        }

        internal DCEL()
        {
            vertices = new Dictionary<Vector3, Vertex>();
            halfEdges = new List<HalfEdge>();
            faces = new Dictionary<int, Face>();
        }

        /// <summary>
        /// Build a double connected edge list (DCEL) from the specified mesh. 
        /// Note that this method is limited to triangle meshes in CCW order.
        /// </summary>
        /// <returns>The build DCEL.</returns>
        /// <param name="mesh">Mesh.</param>
        public static DCEL FromMesh(Mesh mesh)
        {
            return new DCELMeshBuilder(mesh).Build();
        }

        public override string ToString()
        {
            return string.Format(
                "[DCEL:\nvertices\n{0}\nhalfEdges\n{1}\nfaces\n{2}\n]",
                vertices.ElementsToString("\t", "\n"), halfEdges.ElementsToString("\t", "\n"), faces.ElementsToString("\t", "\n")
            );
        }

        public Face GetFace(int meshIdx)
        {
            Face face;
            bool succes = faces.TryGetValue(meshIdx, out face);
            if (!succes) throw new ArgumentException(string.Format("Could not find the face with meshidx {0}.", meshIdx));
            return face;
        }

        public Vertex GetVertex(Vector3 position)
        {
            Vertex vertex;
            bool succes = vertices.TryGetValue(position, out vertex);
            if (!succes) throw new ArgumentException(string.Format("Could not find the vertex at {0}", position));
            return vertex;
        }

        #region IEquatable
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            return this.Equals(obj as DCEL);
        }

        public bool Equals(DCEL other)
        {
            return (
                this.vertices.UnorderedElementsAreEqual(other.vertices, new Vertex.KeyValueComparer()) &&
                this.faces.UnorderedElementsAreEqual(other.faces, new Face.KeyValueComparer<int>()) &&
                this.halfEdges.UnorderedElementsAreEqual(other.halfEdges)
            );
        }

        public override int GetHashCode()
        {
            int hash = 17;
            if (!this.vertices.IsEmpty()) hash *= (31 + vertices.UnorderedElementsGetHashCode(new Vertex.KeyValueComparer()));
            if (!this.halfEdges.IsEmpty()) hash *= (31 + halfEdges.UnorderedElementsGetHashCode());
            if (!this.faces.IsEmpty()) hash *= (31 + faces.UnorderedElementsGetHashCode(new Face.KeyValueComparer<int>()));
            return hash;
        }
        #endregion

        #region internal
        /// <summary>
        /// Add the vertex to the mesh, if another vertex at this position does 
        /// not already exists at this location. If the latter is the case, the 
        /// existing vertex is returned.
        /// </summary>
        /// <param name="vertex">The vertex to be added. </param>
        /// <returns>The vertex in the mesh at the position of the passed vertex.</returns>
        internal Vertex AddVertexIfNew(Vertex vertex)
        {
            if (!this.vertices.ContainsKey(vertex.Position)) AddVertex(vertex);

            Vertex result;
            vertices.TryGetValue(vertex.Position, out result);

            return result;
        }

        internal void AddVertex(Vertex vertex)
        {
            if (this.vertices.ContainsKey(vertex.Position)) throw new InvalidOperationException("The DCEL already has a vertex at the position " + vertex.Position);

            this.vertices.Add(vertex.Position, vertex);
        }

        internal void AddEdge(HalfEdge halfEdge)
        {
            this.halfEdges.Add(halfEdge);
        }

        internal void AddFace(Face face)
        {
            if (this.faces.ContainsKey(face.MeshIdx)) throw new InvalidOperationException("The DCEL already has a face with this idx");

            this.faces.Add(face.MeshIdx, face);
        }
        #endregion
    }

    internal class DCELMeshBuilder
    {
        private Mesh Mesh;
        private DCEL DCEL;

        private Dictionary<int, Vertex> Vertices;
        private Dictionary<VertexPair, HalfEdge> HalfEdges;

        internal DCELMeshBuilder(Mesh simpleMesh)
        {
            Mesh = simpleMesh;

            DCEL = new DCEL();
            Vertices = new Dictionary<int, Vertex>();
            HalfEdges = new Dictionary<VertexPair, HalfEdge>();
        }

        internal DCEL Build()
        {
            AddVertices(Mesh);
            AddFacesAndEdges(Mesh);
            return DCEL;
        }

        private void AddVertices(Mesh mesh)
        {
            for (int idx = 0; idx < mesh.vertices.Length; idx++)
            {
                AddVertex(idx, mesh.vertices[idx]);
            }
        }

        private void AddVertex(int idx, Vector3 position)
        {
            Vertex vertex = new Vertex(position);
            vertex = DCEL.AddVertexIfNew(vertex);
            Vertices.Add(idx, vertex);
        }

        private void AddFacesAndEdges(Mesh mesh)
        {
            int maxIdx = mesh.triangles.Length - 3;
            for (int i = 0; i <= maxIdx; i += 3)
            {
                AddFaceAndItsEdges(
                    i / 3,
                    Vertices[mesh.triangles[i + 0]],
                    Vertices[mesh.triangles[i + 1]],
                    Vertices[mesh.triangles[i + 2]]
                );
            }
        }

        private void AddFaceAndItsEdges(
            int faceIdx,
            Vertex a, Vertex b, Vertex c
        )
        {
            Vector3 faceNormal = NumericalMath.NewellsMethod(
                a.Position, b.Position, c.Position
            );

            Face face = new Face(faceIdx, faceNormal);

            HalfEdge ab = AddEdgeWithTwin(a, b);
            HalfEdge bc = AddEdgeWithTwin(b, c);
            HalfEdge ca = AddEdgeWithTwin(c, a);

            // Set next
            ab.Next = bc;
            bc.Next = ca;
            ca.Next = ab;

            // Set previous
            ab.Previous = ca;
            bc.Previous = ab;
            ca.Previous = bc;

            // Set incident edges of face and the incident face of edge
            LinkeEdgeAndFace(ab, face);
            LinkeEdgeAndFace(bc, face);
            LinkeEdgeAndFace(ca, face);

            // Add face to DCEl
            DCEL.AddFace(face);
        }

        private HalfEdge AddEdgeWithTwin(Vertex origin, Vertex destination)
        {
            HalfEdge originDestination = CreateOrGetEdge(origin, destination);
            HalfEdge destinationOrigin = CreateOrGetEdge(destination, origin);

            originDestination.Twin = destinationOrigin;
            destinationOrigin.Twin = originDestination;

            origin.AddIncidentEdge(originDestination);
            destination.AddIncidentEdge(destinationOrigin);

            return originDestination;
        }

        private HalfEdge CreateOrGetEdge(Vertex origin, Vertex destination)
        {
            HalfEdge edge;
            //Does the edge already exists?
            if (!HalfEdges.TryGetValue(new VertexPair(origin, destination), out edge))
            {
                edge = CreateEdge(origin, destination);
            }
            return edge;
        }

        private HalfEdge CreateEdge(Vertex origin, Vertex destination)
        {
            //Create edge
            HalfEdge edge = new HalfEdge(origin);

            //Add to auxilary structure
            HalfEdges.Add(new VertexPair(origin, destination), edge);

            //Add to DCEl
            DCEL.AddEdge(edge);

            return edge;
        }

        private void LinkeEdgeAndFace(HalfEdge edge, Face face)
        {
            face.AddOuterComponent(edge);
            edge.IncidentFace = face;
        }

    }

    internal class VertexPair : Pair<Vertex, Vertex>, IEquatable<VertexPair>
    {
        public VertexPair(Vertex first, Vertex second)
            : base(first, second)
        { }

        public override int GetHashCode()
        {
            return First.Position.GetHashCode() + Second.Position.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            return this.Equals(obj as VertexPair);
        }

        public bool Equals(VertexPair other)
        {
            return (
                this.First.Position.Equals(other.First.Position) &
                this.Second.Position.Equals(other.Second.Position)
            );
        }
    }
}