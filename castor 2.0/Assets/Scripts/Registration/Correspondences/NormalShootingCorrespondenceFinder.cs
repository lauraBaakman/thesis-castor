using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.Assertions.Must;

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
		private readonly float MaxDistanceFactor;
		private float MaxDistance;

		/// <summary>
		/// The reference transform.
		/// </summary>
		private readonly Transform ReferenceTransform;

		/// <summary>
		/// Offset of the point before it is used as the basis of a ray to ensure
		/// that we can find correspondences between points that lie at the same
		/// position.    
		/// </summary>
		private static float epsilon = 0.0000001f;

		/// <summary>
		/// Initializes a new instance of the 
		/// <see cref="T:Registration.NormalShootingCorrespondenceFinder"/> class.
		/// </summary>
		/// <param name="settings">Settings.</param>
		public NormalShootingCorrespondenceFinder(Settings settings)
		{
			MaxDistanceFactor
			= settings.MaxWithinCorrespondenceDistanceFactor;
			ReferenceTransform = settings.ReferenceTransform;
		}

		/// <summary>
		/// Find the specified staticPoints and modelPoints. This function is not supported for this approach to correspondence finding. It will throw an exception.
		/// </summary>
		/// <returns>The find.</returns>
		/// <param name="staticPoints">Static points.</param>
		/// <param name="modelPoints">Model points.</param>
		public CorrespondenceCollection Find(ReadOnlyCollection<Point> staticPoints, ReadOnlyCollection<Point> modelPoints)
		{
			throw new System.ArgumentException("The NormalShootingCorrespondenceFinder cannot find correspondences between two sets of points.");
		}

		/// <summary>
		/// Find the point where for the specified staticPoints their normal, 
		/// hits the model fragment. If no intersection is found for the normal 
		/// shooting outward of the object the normal is reversed.
		/// </summary>
		/// <returns>A new correspondence collection.</returns>
		/// <param name="staticPoints">Static points.</param>
		/// <param name="modelSamplingInformation">Model sampling information.</param>
		public CorrespondenceCollection Find(ReadOnlyCollection<Point> staticPoints, SamplingInformation modelSamplingInformation)
		{
			ComputeMaxDistance(staticPoints, modelSamplingInformation.bounds);

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

		private void ComputeMaxDistance(ReadOnlyCollection<Point> staticPoints, Bounds modelFragmentBounds)
		{
			foreach (Point staticPoint in staticPoints) modelFragmentBounds.Encapsulate(staticPoint.Position);

			//Compute the average of the three dimensions of the bounding box
			this.MaxDistance = (modelFragmentBounds.extents.x + modelFragmentBounds.extents.y + modelFragmentBounds.extents.z) * 2.0f / 3.0f;

			RealExperimentLogger.Instance.Log(
				string.Format(
						"Model Fragment Bounds: {0}, Max Distance Factor: {1}, Max Distance: {2}",
					modelFragmentBounds, this.MaxDistanceFactor, this.MaxDistance)
				);
		}

		/// <summary>
		/// Serialize this instance.
		/// </summary>
		/// <returns>The serialize.</returns>
		public SerializebleCorrespondenceFinder Serialize()
		{
			return new SerializebleCorrespondenceFinder(
				"normal shooting",
				MaxDistanceFactor, ReferenceTransform
			);
		}

		/// <summary>
		/// Finds the correspondence for static point. 
		/// </summary>
		/// <returns>The correspondence.</returns>
		/// <param name="staticPoint">Static point.</param>
		/// <param name="model">Model.</param>
		private Correspondence FindCorrespondence(Point staticPoint, SamplingInformation model)
		{
			Point intersectionInReferenceTransform = FindIntersection(staticPoint, model);

			if (intersectionInReferenceTransform == null) return null;

			return new Correspondence(
				staticPoint: staticPoint,
				modelPoint: intersectionInReferenceTransform
			);
		}

		/// <summary>
		/// Finds the intersection of the (reversed) normal of the static point 
		/// with the model fragment. Returns null if no intersection is found.
		/// </summary>
		/// <returns>The intersection.</returns>
		/// <param name="staticPoint">Static point.</param>
		/// <param name="model">Model.</param>
		private Point FindIntersection(Point staticPoint, SamplingInformation model)
		{
			Point hit;

			Ray forwardRay = staticPoint.ToForwardWorldSpaceRay(model.Transform, epsilon);
			hit = FindIntersection(forwardRay, model);

			if (hit == null)
			{
				Ray backwardRay = staticPoint.ToBackwardWorldSpaceRay(model.Transform, epsilon);
				hit = FindIntersection(backwardRay, model);
			}
			return hit;
		}

		/// <summary>
		/// Finds the intersection of the ray with the model fragment. Returns 
		/// null if no intersection is found.
		/// </summary>
		/// <returns>The intersection.</returns>
		/// <param name="ray">Ray.</param>
		/// <param name="model">Model.</param>
		private Point FindIntersection(Ray ray, SamplingInformation model)
		{
			RaycastHit hit;
			bool collided = model.Collider.Raycast(ray, out hit, MaxDistance);

			if (!collided) return null;

			return hit.ToPoint(ReferenceTransform);
		}
	}
}