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
		public string Name;
		public string DistanceMetric;
		public float MaxDistance;
		public Utils.SerializableTransform ReferenceTransform;

		private SerializebleCorrespondenceFinder(string name, string distanceMetric, float maxDistance, Utils.SerializableTransform referenceTransform)
		{
			this.Name = name;
			this.DistanceMetric = distanceMetric;
			this.MaxDistance = maxDistance;
			this.ReferenceTransform = referenceTransform;
		}

		public SerializebleCorrespondenceFinder(string name, string distanceMetric)
			: this(name, distanceMetric, float.PositiveInfinity, null)
		{ }

		public SerializebleCorrespondenceFinder(string name, float maxDistance, Transform referenceTransform)
			: this(name, "none", maxDistance, new Utils.SerializableTransform(referenceTransform))
		{ }
	}
}