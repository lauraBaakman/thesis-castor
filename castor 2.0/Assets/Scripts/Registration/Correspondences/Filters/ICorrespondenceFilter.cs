using UnityEngine;
using System.Collections.Generic;

namespace Registration
{
    public interface ICorrespondenceFilter
    {
        List<Correspondence> Filter(IList<Correspondence> correspondences);
    }
}