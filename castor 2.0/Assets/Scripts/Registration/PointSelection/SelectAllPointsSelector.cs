using System.Collections.Generic;
using UnityEngine;

namespace Registration
{
    /// <summary>
    /// The simple point selector simply selects all the points of the mesh.
    /// </summary>
    public class SelectAllPointsSelector : IPointSelector
    {

        /// <summary>
        /// The transform that the points should be sampled in.
        /// </summary>
        private readonly Transform ReferenceTransform;

        public SelectAllPointsSelector(Transform referenceTransform)
        {
            ReferenceTransform = referenceTransform;
        }

        public List<Vector3> Select(Transform fragmentTransform, Mesh fragment)
        {
            ///Use a set to avoid duplicate points when the mesh has duplicate vertices
            HashSet<Vector3> points = new HashSet<Vector3>();

            foreach(Vector3 vertex in fragment.vertices){
                points.Add(vertex);
            }
            return ToReferenceTransfrom(new List<Vector3>(points), fragmentTransform);
        }

        private List<Vector3> ToReferenceTransfrom(List<Vector3> pointsLocalTransform, Transform localTransform)
        {
            List<Vector3> pointsReferenceTransform = new List<Vector3>(pointsLocalTransform.Count);
            foreach (Vector3 localPoint in pointsLocalTransform)
            {
                Vector3 worldTransformPoint = localTransform.TransformPoint(localPoint);
                Vector3 referenceTransformPoint = ReferenceTransform.InverseTransformPoint(worldTransformPoint);

                pointsReferenceTransform.Add(referenceTransformPoint);
            }
            return pointsReferenceTransform;
        }
    }
}