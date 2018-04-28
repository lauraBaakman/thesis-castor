using System.Collections.ObjectModel;
using UnityEngine;

namespace Registration
{
    public interface ICorrespondenceFinder
    {
        CorrespondenceCollection Find(ReadOnlyCollection<Point> staticPoints, ReadOnlyCollection<Point> modelPoints);

        CorrespondenceCollection Find(ReadOnlyCollection<Point> staticPoints, SamplingInformation modelSamplingInformation);

        SerializebleCorrespondenceFinder Serialize();
    }

    [System.Serializable]
    public class SerializebleCorrespondenceFinder
    {
        public string DistanceMetric;
        public float MaxDistance;
        public Utils.SerializableTransform ReferenceTransform;

        private SerializebleCorrespondenceFinder(string distanceMetric, float maxDistance, Utils.SerializableTransform referenceTransform)
        {
            this.DistanceMetric = distanceMetric;
            this.MaxDistance = maxDistance;
            this.ReferenceTransform = referenceTransform;
        }

        public SerializebleCorrespondenceFinder(string distanceMetric)
            : this(distanceMetric, float.PositiveInfinity, null)
        { }

        public SerializebleCorrespondenceFinder(float maxDistance, Transform referenceTransform)
            : this("none", maxDistance, new Utils.SerializableTransform(referenceTransform))
        { }
    }
}