using System.Collections.Generic;
using UnityEngine;

namespace Registration {
    /// <summary>
    /// The simple point selector simply selects all the points of the mesh.
    /// </summary>
    public class SelectAllPointsSelector : IPointSelector {

        /// <summary>
        /// The transform that the points should be sampled in.
        /// </summary>
        private readonly Transform ReferenceTransform;

        public SelectAllPointsSelector( Transform referenceTransform )
        {
            ReferenceTransform = referenceTransform;
        }

        public List<Vector3> Select( Transform fragmentTransform, Mesh fragment )
        {
            List<Vector3> points = new List<Vector3>(
                fragment.vertices
            );
            return ToReferenceTransfrom(fragmentTransform, points);
        }

        private List<Vector3> ToReferenceTransfrom( Transform localTransform, List<Vector3> pointsLocalTransform )
        {
            List<Vector3> pointsReferenceTransform = new List<Vector3>(pointsLocalTransform.Count);
            foreach (Vector3 localPoint in pointsLocalTransform) {
                pointsReferenceTransform.Add(
                    //World space point to reference transform space
                    ReferenceTransform.InverseTransformPoint(
                        //Local point to world space
                        localTransform.TransformPoint(
                            localPoint
                        )
                    )
                );
            }
            return pointsReferenceTransform;
        }
    }
}