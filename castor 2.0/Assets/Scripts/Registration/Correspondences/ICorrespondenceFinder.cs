using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;

namespace Registration
{
    public interface ICorrespondenceFinder
    {
        CorrespondenceCollection Find(ReadOnlyCollection<Point> staticPoints, ReadOnlyCollection<Point> modelPoints);

        CorrespondenceCollection Find(ReadOnlyCollection<Point> staticPoints, SamplingInformation modelSamplingInformation);

        SerializebleCorrespondenceFinder Serialize();
    }

    public class SerializebleCorrespondenceFinder
    {
        public SerializebleCorrespondenceFinder()
        {
            throw new NotImplementedException();
        }
    }
}