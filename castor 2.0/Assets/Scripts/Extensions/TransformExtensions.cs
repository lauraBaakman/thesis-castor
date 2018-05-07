using UnityEngine;

public static class TransformExtensions
{
    /// <summary>
    /// Reset the specified transform, i.e. position to the origin of the parent
    /// transform, scale back to unit scale and rotation of 0 around all angles
    /// repsective to the parent transform.
    /// </summary>
    /// <param name="transform">Transform.</param>
    public static void Reset(this Transform transform)
    {
        transform.localPosition = new Vector3();
        transform.localRotation = Quaternion.identity;
        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }

    /// <summary>
    /// Matrix that transforms a point from local space into other space.
    /// </summary>
    /// <returns>The transformation matrix</returns>
    /// <param name="other">The transform to transform to.</param>
    public static Matrix4x4 LocalToOther(this Transform transform, Transform other)
    {
        if (transform == null || other == null)
        {
            Debug.LogWarning("Returning the identity matrix since at least one of the passed transforms was null. This should only happen in tests!");
            return Matrix4x4.identity;
        }

        Matrix4x4 toWorld = transform.localToWorldMatrix;
        Matrix4x4 toOther = other.worldToLocalMatrix;

        return toOther * toWorld;
    }

    /// <summary>
    /// Transforms position from local space to world space.
    /// </summary>
    /// <returns>The point in world space.</returns>
    /// <param name="transform">Transform.</param>
    /// <param name="position">The position in local space.</param>
    public static Vector4 TransformPoint(this Transform transform, Vector4 position)
    {
        Vector3 transformed = transform.TransformPoint(new Vector3(position.x, position.y, position.z));
        return new Vector4(transformed.x, transformed.y, transformed.z, position.w);
    }

    /// <summary>
    /// Transforms direction from local space to world space.
    /// </summary>
    /// <returns>The direction in world space.</returns>
    /// <param name="transform">Transform.</param>
    /// <param name="direction">The direction in local space.</param>
    public static Vector4 TransformDirection(this Transform transform, Vector4 direction)
    {
        Vector3 transformed = transform.TransformDirection(direction.x, direction.y, direction.z);
        return new Vector4(transformed.x, transformed.y, transformed.z, direction.w);
    }
}
