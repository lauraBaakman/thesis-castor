using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Registration
{
	/// <summary>
	/// Find the correspondencecs using an adaption of the method proposed in 
	/// Chen, Yang, and Gérard Medioni. "Object modelling by registration of 
	/// multiple range images." Image and vision computing 10.3 (1992): 145-155.
	/// 
	/// This method shoots rays from the staticpoints, along the normal of that 
	/// point, if that ray intersects with the model fragment the intersection 
	/// point and the static point form a correspondence, if the distance 
	/// between the two points is smaller than MaxDistance.
	/// </summary>
	public class NormalShootingCorrespondenceFinder : ICorrespondenceFinder
	{
		/// <summary>
		/// The maximum distance between a static point and the intersection point on the model fragment.
		/// </summary>
		private readonly float MaxDistance;

		/// <summary>
		/// The reference transform.
		/// </summary>
		private readonly Transform ReferenceTransform;

		public NormalShootingCorrespondenceFinder(Settings settings)
		{
			MaxDistance = settings.MaxWithinCorrespondenceDistance;
			ReferenceTransform = settings.ReferenceTransform;
		}

		public CorrespondenceCollection Find(ReadOnlyCollection<Point> staticPoints, ReadOnlyCollection<Point> modelPoints)
		{
			throw new System.ArgumentException("The NormalShootingCorrespondenceFinder cannot find correspondences between two sets of points.");
		}

		public CorrespondenceCollection Find(ReadOnlyCollection<Point> staticPoints, SamplingInformation modelSamplingInformation)
		{
			Correspondence correspondence;
			CorrespondenceCollection correspondences = new CorrespondenceCollection();
			foreach (Point staticPoint in staticPoints)
			{
				correspondence = FindCorrespondence(staticPoint, modelSamplingInformation);
				if (correspondence == null) continue;

				correspondences.Add(correspondence);
			}
			return correspondences;
		}

		public SerializebleCorrespondenceFinder Serialize()
		{
			return new SerializebleCorrespondenceFinder(
				"normal shooting",
				MaxDistance, ReferenceTransform
			);
		}

		private Correspondence FindCorrespondence(Point staticPoint, SamplingInformation model)
		{
			Point intersectionInReferenceTransform = FindIntersection(staticPoint, model);

			if (intersectionInReferenceTransform == null) return null;

			return new Correspondence(
				staticPoint: staticPoint,
				modelPoint: intersectionInReferenceTransform
			);
		}

		private Point FindIntersection(Point staticPoint, SamplingInformation model)
		{
			Ray rayInWorldSpace = staticPoint.ToWorldSpaceRay(model.Transform);

			RaycastHit hit;
			bool collided = model.Collider.Raycast(rayInWorldSpace, out hit, MaxDistance);

			if (!collided) return null;

			return hit.ToPoint(ReferenceTransform);
		}
	}
}