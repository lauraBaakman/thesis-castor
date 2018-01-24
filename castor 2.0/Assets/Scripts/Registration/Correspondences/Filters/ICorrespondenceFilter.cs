using UnityEngine;
using System.Collections;

namespace Registration
{
    public interface ICorrespondenceFilter
    {
        IList<Correspondence> Filter(IList<Correspondence> correspondences);
    }
}