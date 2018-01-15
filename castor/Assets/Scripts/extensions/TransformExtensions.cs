using UnityEngine;

/// <summary>
/// Extension methods for the Transform class.
/// </summary>
public static class TransformExtensions
{

    /// <summary>
    /// Reset the local transform, i.e. localscale to one, local position to zero,
    /// and local rotation to the identity matrix.
    /// </summary>
    public static void ResetLocally(this Transform transform)
    {
        transform.localScale = Vector3.one;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    /// <summary>
    /// Transforms local bounds to bounds in world space.
    /// 
    /// Source: https://answers.unity.com/answers/1114000/view.html
    /// </summary>
    /// <returns>The bounds in world space.</returns>
    /// <param name="transform">The local transform.</param>
    /// <param name="localBounds">Local bounds.</param>
    public static Bounds TransformToWorldSpace(this Transform transform, Bounds localBounds)
    {
        var center = transform.TransformPoint(localBounds.center);

        // transform the local extents' axes
        var extents = localBounds.extents;
        var axisX = transform.TransformVector(extents.x, 0, 0);
        var axisY = transform.TransformVector(0, extents.y, 0);
        var axisZ = transform.TransformVector(0, 0, extents.z);

        // sum their absolute value to get the world extents
        extents.x = Mathf.Abs(axisX.x) + Mathf.Abs(axisY.x) + Mathf.Abs(axisZ.x);
        extents.y = Mathf.Abs(axisX.y) + Mathf.Abs(axisY.y) + Mathf.Abs(axisZ.y);
        extents.z = Mathf.Abs(axisX.z) + Mathf.Abs(axisY.z) + Mathf.Abs(axisZ.z);

        return new Bounds { center = center, extents = extents };
    }
}
