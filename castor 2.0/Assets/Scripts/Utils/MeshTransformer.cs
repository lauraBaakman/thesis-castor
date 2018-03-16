using UnityEngine;

namespace Utils
{
    public class MeshTransformer
    {
        private readonly Matrix4x4 transformation;

        public MeshTransformer(Matrix4x4 transformation)
        {
            this.transformation = transformation;
        }

        public Mesh Transform(Mesh mesh)
        {
            Mesh transformedMesh = new Mesh();

            if (mesh.subMeshCount > 1) throw new System.NotSupportedException("Meshes with more than one submesh are not supported");

            ///Expected order: vertices, mesh then the optionals such as normals
            transformedMesh.vertices = TransformVertices(mesh.vertices);
            transformedMesh.triangles = mesh.triangles;
            transformedMesh.normals = TransformNormals(mesh.normals);

            ///Copy properties that are not influenced by the transform
            transformedMesh.uv = mesh.uv;
            transformedMesh.uv2 = mesh.uv2;
            transformedMesh.uv3 = mesh.uv3;
            transformedMesh.uv4 = mesh.uv4;

            return transformedMesh;
        }

        private Vector3[] TransformNormals(Vector3[] normals)
        {
            Vector3[] transformedNormals = new Vector3[normals.Length];
            for (int i = 0; i < normals.Length; i++)
            {
                transformedNormals[i] = this.transformation.MultiplyVector(normals[i]).normalized;
            }
            return transformedNormals;
        }

        private Vector3[] TransformVertices(Vector3[] vertices)
        {
            Vector3[] transformedVertices = new Vector3[vertices.Length];
            for (int i = 0; i < vertices.Length; i++)
            {
                Vector3 originalVertex = vertices[i];
                Vector3 transformedVertex = this.transformation.MultiplyPoint(originalVertex);

                transformedVertices[i] = this.transformation.MultiplyPoint(vertices[i]);
            }
            return transformedVertices;
        }
    }
}