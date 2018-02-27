using UnityEngine;

public static class Vector3Extensions
{
    /// <summary>
    /// Implementation of CompareTo for Vector3, if the magnitued of this vector is greater than that of the passed vector the function returns 1, if the magnitude of the passed vector is greater it returns -1, if the magnitudes are equal 0 is returned.
    /// </summary>
    /// <returns>A value that indicates the relative order of the objects being compared.</returns>
    /// <param name="vector">This vector</param>
    /// <param name="other">A vector4 to compare with this instance.</param>
    public static int CompareTo(this Vector3 vector, Vector3 other)
    {
        int compareX = vector.x.CompareTo(other.x);
        if (compareX != 0) return compareX;

        int compareY = vector.y.CompareTo(other.y);
        if (compareY != 0) return compareY;

        return vector.z.CompareTo(other.z);
    }

    /// <summary>
    /// Changes the transform of the vector representing a position from the source transform to the destination transform.
    /// </summary>
    /// <returns>A new vector in the destination transform.</returns>
    /// <param name="vector">Vector.</param>
    /// <param name="sourceTransform">The current transform of the vector.</param>
    /// <param name="destinationTransform">Destination transform.</param>
    public static Vector3 ChangeTransformOfPosition(this Vector3 vector, Transform sourceTransform, Transform destinationTransform)
    {
        if (EitherOfTheTransformsIsNull(sourceTransform, destinationTransform)) return CopyVector(vector);

        Vector3 worldTransformPosition = sourceTransform.TransformPoint(vector);
        Vector3 destinationTransformPosition = destinationTransform.TransformPoint(worldTransformPosition);

        return destinationTransformPosition;
    }

    /// <summary>
    /// Changes the transform of the vector representing a vector from the source transform to the destination transform.
    /// </summary>
    /// <returns>A new vector in the destination transform.</returns>
    /// <param name="vector">Vector.</param>
    /// <param name="sourceTransform">The current transform of the vector.</param>
    /// <param name="destinationTransform">Destination transform.</param>
    public static Vector3 ChangeTransformOfDirection(this Vector3 vector, Transform sourceTransform, Transform destinationTransform)
    {
        if (EitherOfTheTransformsIsNull(sourceTransform, destinationTransform)) return CopyVector(vector);

        Vector3 worldTransformVector = sourceTransform.TransformDirection(vector);
        Vector3 destinationTransformVector = destinationTransform.InverseTransformDirection(worldTransformVector);

        return destinationTransformVector;
    }

    private static bool EitherOfTheTransformsIsNull(Transform sourceTransform, Transform destinationTransform)
    {
        return sourceTransform == null || destinationTransform == null;
    }

    private static Vector3 CopyVector(Vector3 vector)
    {
        return new Vector3(vector.x, vector.y, vector.z);
    }
}
