using UnityEngine;
using System.Collections.Generic;

namespace Registration
{
    public interface IPointSelector
    {
        List<Vector3> Select(Mesh fragment);
    }
}