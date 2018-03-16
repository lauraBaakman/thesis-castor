using UnityEngine;
using DoubleConnectedEdgeList;
using Fragment;

namespace Registration
{
    public class SamplingInformation
    {
        public readonly Transform Transform;
        public readonly Mesh Mesh;
        public readonly Collider Collider;
        public readonly DCEL DCEL;

        public SamplingInformation(GameObject gameObject)
            : this(
                transform: gameObject.transform,
                mesh: gameObject.GetComponent<MeshFilter>().mesh,
                collider: gameObject.GetComponent<Collider>(),
                dcel: gameObject.GetComponent<DoubleConnectedEdgeListStorage>().DCEL
            )
        { }

        private SamplingInformation(Transform transform, Mesh mesh, Collider collider, DCEL dcel)
        {
            if (mesh == null) throw new System.ArgumentNullException("mesh");
            if (collider == null) throw new System.ArgumentNullException("collider");
            if (dcel == null) throw new System.ArgumentNullException("dcel");

            this.Transform = transform;
            this.Mesh = mesh;
            this.Collider = collider;
            this.DCEL = dcel;
        }
    }

    public class SamplingConfiguration
    {
        public enum NormalProcessing { NoNormals, VertexNormals, AreaWeightedSmoothNormals };

        public readonly NormalProcessing normalProcessing;
        public readonly Transform referenceTransform;

        public SamplingConfiguration(Transform referenceTransform, NormalProcessing normalProcessing)
        {
            this.normalProcessing = normalProcessing;
            this.referenceTransform = referenceTransform;
        }
    }
}