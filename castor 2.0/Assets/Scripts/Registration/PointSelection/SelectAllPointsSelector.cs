using System.Collections.Generic;
using UnityEngine;

namespace Registration
{
    /// <summary>
    /// The simple point selector simply selects all the points of the mesh.
    /// </summary>
    public class SelectAllPointsSelector : IPointSelector
    {
        public List<Vector3> Select(Mesh fragment)
        {
            List<Vector3> points = new List<Vector3>(
                fragment.vertices
            );
            return points;
        }
    }
}