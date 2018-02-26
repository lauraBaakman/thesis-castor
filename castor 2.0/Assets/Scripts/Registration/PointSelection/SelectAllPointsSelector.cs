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

        public List<Point> Select(SamplingInformation samplingInfo)
        {
            return selectionFunction(samplingInfo.Transform, samplingInfo.Mesh);
        }

        private List<Point> SelectNoNormals(Transform fragmentTransform, Mesh fragment)
        {
            ///Use a set to avoid duplicate points when the mesh has duplicate vertices
            HashSet<Vector3> points = new HashSet<Vector3>();

            foreach (Vector3 vertex in fragment.vertices)
            {
                points.Add(vertex);
            }
            return PositionsToReferenceTransfrom(new List<Vector3>(points), fragmentTransform);
        }

        private List<Point> SelectWithNormals(Transform fragmentTransform, Mesh fragment)
        {
            List<Point> points = new List<Point>();

            for (int i = 0; i < fragment.vertices.Length; i++)
            {
                points.Add(
                    new Point(
                        position: PositionToReferenceTransform(
                            fragment.vertices[i],
                            fragmentTransform
                        ),
                        normal: NormalToReferenceTransform(
                            fragment.normals[i],
                            fragmentTransform
                        )
                    )
                );
            }
            return points;
        }

        private List<Point> PositionsToReferenceTransfrom(List<Vector3> pointsLocalTransform, Transform localTransform)
        {
            List<Point> pointsReferenceTransform = new List<Point>(pointsLocalTransform.Count);
            foreach (Vector3 pointLocalTransform in pointsLocalTransform)
            {
                pointsReferenceTransform.Add(new Point(PositionToReferenceTransform(pointLocalTransform, localTransform)));
            }
            return pointsReferenceTransform;
        }

        private Vector3 PositionToReferenceTransform(Vector3 pointLocalTransform, Transform localTransform)
        {
            Vector3 worldTransformPoint = localTransform.TransformPoint(pointLocalTransform);
            Vector3 referenceTransformPoint = ReferenceTransform.InverseTransformPoint(worldTransformPoint);

            return referenceTransformPoint;
        }

        private Vector3 NormalToReferenceTransform(Vector3 normalLocalTransform, Transform localTransform)
        {
            Vector3 worldTransformNormal = localTransform.TransformDirection(normalLocalTransform);
            Vector3 referenceTransformNormal = ReferenceTransform.InverseTransformDirection(worldTransformNormal);

            return referenceTransformNormal;
        }
    }
}