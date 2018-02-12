using System;
using System.Collections.Generic;
using UnityEngine;

namespace Registration
{
    /// <summary>
    /// The simple point selector simply selects all the points of the mesh.
    /// </summary>
    public class SelectAllPointsSelector : IPointSelector
    {

        private delegate List<Point> SelectionFunction(Transform fragmentTransform, Mesh fragment);

        /// <summary>
        /// The transform that the points should be sampled in.
        /// </summary>
        private readonly Transform ReferenceTransform;

        /// <summary>
        /// The function used to select the points, chosen based on the paramters 
        /// passed to the constructor.
        /// </summary>
        private SelectionFunction selectionFunction;

        public SelectAllPointsSelector(Transform referenceTransform, bool includeNormals = false)
        {
            ReferenceTransform = referenceTransform;
            if (includeNormals) selectionFunction = SelectWithNormals;
            else selectionFunction = SelectNoNormals;
        }

        public List<Point> Select(Transform fragmentTransform, Mesh fragment)
        {
            return selectionFunction(fragmentTransform, fragment);
        }

        private List<Point> SelectNoNormals(Transform fragmentTransform, Mesh fragment)
        {
            ///Use a set to avoid duplicate points when the mesh has duplicate vertices
            HashSet<Vector3> points = new HashSet<Vector3>();

            foreach (Vector3 vertex in fragment.vertices)
            {
                points.Add(vertex);
            }
            return ToReferenceTransfrom(new List<Vector3>(points), fragmentTransform);
        }

        private List<Point> SelectWithNormals(Transform fragmentTransform, Mesh fragment)
        {
            List<Point> points = new List<Point>();

            return points;
        }

        private List<Point> ToReferenceTransfrom(List<Vector3> pointsLocalTransform, Transform localTransform)
        {
            List<Point> pointsReferenceTransform = new List<Point>(pointsLocalTransform.Count);
            foreach (Vector3 localPoint in pointsLocalTransform)
            {
                Vector3 worldTransformPoint = localTransform.TransformPoint(localPoint);
                Vector3 referenceTransformPoint = ReferenceTransform.InverseTransformPoint(worldTransformPoint);

                pointsReferenceTransform.Add(new Point(referenceTransformPoint));
            }
            return pointsReferenceTransform;
        }
    }
}