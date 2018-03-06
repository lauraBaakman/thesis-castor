using UnityEngine;
using System.Collections.Generic;

namespace Registration
{
    public interface ICorrespondenceFilter
    {
        CorrespondenceCollection Filter(CorrespondenceCollection correspondences);
    }
}