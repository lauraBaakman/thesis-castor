using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Registration
{
    public interface ICorrespondenceFinder
    {
        List<Correspondence> Find(ReadOnlyCollection<Point> staticPoints, ReadOnlyCollection<Point> modelPoints);

        List<Correspondence> Find(ReadOnlyCollection<Point> staticPoints, SamplingInformation modelSamplingInformation);
    }
}