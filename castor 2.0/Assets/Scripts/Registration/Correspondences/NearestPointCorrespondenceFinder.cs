using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Registration
{
    /// <summary>
    /// Find the correspondences using the nearest point method. The method computes 
    /// the distances of all point pairs and creates correspondences starting from the 
    /// pair with the smallest distance until all points of at least one of the meshes 
    /// are involved in a correspondence.
    /// </summary>
    public class NearstPointCorrespondenceFinder : ICorrespondenceFinder
    {
        public List<Correspondence> Find( List<Vector3> staticPoints, List<Vector3> modelPoints )
        {
            List<Correspondence> correspondences = new List<Correspondence>();

            //TODO implement!

            return correspondences;
        }
    }

}
