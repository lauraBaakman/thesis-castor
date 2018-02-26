using UnityEngine;

namespace Registration
{
    public class SamplingInformation
    {
        public readonly Transform transform;
        public readonly Mesh mesh;

        public SamplingInformation(Transform transform, Mesh mesh)
        {
            this.transform = transform;
            this.mesh = mesh;
        }
    }
}