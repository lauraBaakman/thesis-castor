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
            HashSet<Point> points = new HashSet<Point>();

            foreach (Vector3 vertex in fragment.vertices)
            {
                points.Add(
                    new Point(
                        vertex.ChangeTransformOfPosition(fragmentTransform, ReferenceTransform)
                    )
                );
            }
            return new List<Point>(points);
        }

        private List<Point> SelectWithNormals(Transform fragmentTransform, Mesh fragment)
        {
            List<Point> points = new List<Point>();

            for (int i = 0; i < fragment.vertices.Length; i++)
            {
                points.Add(
                    new Point(
                        position: fragment.vertices[i].ChangeTransformOfPosition(
                            fragmentTransform, ReferenceTransform),
                        normal: fragment.normals[i].ChangeTransformOfDirection(
                            fragmentTransform, ReferenceTransform)
                    )
                );
            }
            return points;
        }
    }
}