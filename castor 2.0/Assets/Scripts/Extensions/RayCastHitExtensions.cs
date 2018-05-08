using Registration;
using UnityEngine;

public static class RayCastHitExtensions
{
	/// <summary>
	/// To a Point in the passed transform.
	/// </summary>
	/// <returns>The point.</returns>
	/// <param name="rayCastHit">Ray cast hit.</param>
	/// <param name="pointTransform">The transform of the point.</param>
	public static Point ToPoint(this RaycastHit rayCastHit, Transform pointTransform)
	{
		Vector3 positonInPointTransform = pointTransform.InverseTransformPoint(rayCastHit.point);
		Vector3 normalInPointTransform = pointTransform.InverseTransformDirection(rayCastHit.normal);

		return new Point(
			position: positonInPointTransform,
			normal: normalInPointTransform
		);
	}
}