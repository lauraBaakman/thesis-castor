using UnityEngine;
using System.Collections.Generic;

namespace Registration
{
    public interface IPointSampler
    {
        List<Point> Sample(SamplingInformation samplingInfo);
    }
}
