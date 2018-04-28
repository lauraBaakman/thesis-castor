using UnityEngine;
using System.Collections.Generic;
using System;

namespace Registration
{
    public interface IPointSampler
    {
        List<Point> Sample(SamplingInformation samplingInfo);

        SerializablePointSampler ToSerializableObject();
    }

    public class SerializablePointSampler
    {
        SerializablePointSampler()
        {
            throw new NotImplementedException();
        }
    }
}
