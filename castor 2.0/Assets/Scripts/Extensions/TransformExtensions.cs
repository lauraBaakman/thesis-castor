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
}
