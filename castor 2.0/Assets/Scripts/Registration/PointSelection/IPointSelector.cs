using UnityEngine;
using System.Collections.Generic;

namespace Registration
{
    public interface IPointSelector
    {
        List<Point> Select(Transform fragmentTransform, Mesh fragment);
    }
}
