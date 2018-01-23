using UnityEngine;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Registration
{

    public interface ICorrespondenceFinder
    {
        List<Correspondence> Find( ReadOnlyCollection<Vector3> staticPoints, ReadOnlyCollection<Vector3> modelPoints );
    }
}