using UnityEngine;
using System.Collections.Generic;

namespace Registration
{
    public interface ITransformFinder
    {
        Transform FindTransform(List<Correspondence> correspondences);
    }
}
