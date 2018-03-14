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
            throw new System.NotImplementedException();
        }
    }
}