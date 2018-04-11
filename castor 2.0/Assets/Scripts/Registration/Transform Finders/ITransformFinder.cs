using UnityEngine;

namespace Registration
{
    public interface ITransformFinder
    {
        Matrix4x4 FindTransform(CorrespondenceCollection correspondences);
    }
}
