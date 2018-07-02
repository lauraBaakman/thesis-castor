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
		public readonly Bounds bounds;

		public int VertexCount
		{
			get { return this.Mesh.vertexCount; }
		}

		public SamplingInformation(GameObject gameObject)
			: this(
				transform: gameObject.transform,
				meshFilter: gameObject.GetComponent<MeshFilter>(),
				collider: gameObject.GetComponent<Collider>(),
				dcelStorage: gameObject.GetComponent<DoubleConnectedEdgeListStorage>()
			)
		{ }

		private SamplingInformation(Transform transform, MeshFilter meshFilter, Collider collider, DoubleConnectedEdgeListStorage dcelStorage)
		{
			if (meshFilter == null) throw new System.ArgumentNullException("mesh filter");
			if (collider == null) throw new System.ArgumentNullException("collider");
			if (dcelStorage == null) throw new System.ArgumentNullException("dcel storage");

			this.Transform = transform;
			this.Mesh = Application.isEditor ? meshFilter.sharedMesh : meshFilter.mesh;
			this.bounds = this.Mesh.bounds;
			this.Collider = collider;
			this.DCEL = dcelStorage.DCEL;
		}
	}

	public class SamplingConfiguration
	{
		public readonly Transform referenceTransform;

		public SamplingConfiguration(Transform referenceTransform)
		{
			this.referenceTransform = referenceTransform;
		}
	}
}