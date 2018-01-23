using UnityEngine;
using System.Collections.Generic;

namespace Registration
{

    public interface ICorrespondenceFinder
    {
        List<Correspondence> Find( List<Vector3> staticPoints, List<Vector3> modelPoints );
    }
}