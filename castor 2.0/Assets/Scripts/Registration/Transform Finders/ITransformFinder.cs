using UnityEngine;
using System.Collections.Generic;

namespace Registration
{
    public interface ITransformFinder
    {
        Matrix4x4 FindTransform(List<Correspondence> correspondences);
    }
}
