using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Registration
{
    public interface ICorrespondenceFinder
    {
        CorrespondenceCollection Find(ReadOnlyCollection<Point> staticPoints, ReadOnlyCollection<Point> modelPoints);

        CorrespondenceCollection Find(ReadOnlyCollection<Point> staticPoints, SamplingInformation modelSamplingInformation);
    }
}