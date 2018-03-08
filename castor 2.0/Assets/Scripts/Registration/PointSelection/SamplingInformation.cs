using UnityEngine;

namespace Registration
{
    public class SamplingInformation
    {
        public readonly Transform Transform;
        public readonly Mesh Mesh;
        public readonly Collider Collider;

        public SamplingInformation(Transform transform, Mesh mesh, Collider collider)
        {
            if (mesh == null) throw new System.ArgumentNullException("mesh");
            if (collider == null) throw new System.ArgumentNullException("collider");

            this.Transform = transform;
            this.Mesh = mesh;
            this.Collider = collider;
        }

        public SamplingInformation(GameObject gameObject)
            : this(
                transform: gameObject.transform,
                mesh: gameObject.GetComponent<MeshFilter>().mesh,
                collider: gameObject.GetComponent<Collider>()
            )
        { }
    }
}