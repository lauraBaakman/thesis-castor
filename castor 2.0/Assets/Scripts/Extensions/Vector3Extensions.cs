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
    /// Duplicate the specified vector.
    /// </summary>
    /// <returns>The duplicate of the input vector.</returns>
    /// <param name="vector">Vector.</param>
    public static Vector3 Duplicate(this Vector3 vector)
    {
        return new Vector3(vector.x, vector.y, vector.z);
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
        if (EitherOfTheTransformsIsNull(sourceTransform, destinationTransform)) return vector.Duplicate();

        Vector3 worldTransformPosition = sourceTransform.TransformPoint(vector);
        Vector3 destinationTransformPosition = destinationTransform.InverseTransformPoint(worldTransformPosition);

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
        if (EitherOfTheTransformsIsNull(sourceTransform, destinationTransform)) return vector.Duplicate();

        Vector3 worldTransformVector = sourceTransform.TransformDirection(vector);
        Vector3 destinationTransformVector = destinationTransform.InverseTransformDirection(worldTransformVector);

        return destinationTransformVector;
    }

    private static bool EitherOfTheTransformsIsNull(Transform sourceTransform, Transform destinationTransform)
    {
        return sourceTransform == null || destinationTransform == null;
    }

    /// <summary>
    /// Detect if any of the values of this vector are nan. 
    /// </summary>
    /// <returns><c>true</c>, if any of the values of this vector contain a nan <c>false</c> otherwise.</returns>
    /// <param name="vector">Vector.</param>
    public static bool ContainsNaNs(this Vector3 vector)
    {
        return vector.ContainsValue(float.NaN);
    }

    /// <summary>
    /// Detect if any of the values of this vector are positive infinity. 
    /// </summary>
    /// <returns><c>true</c>, if any of the values of this vector contain a positive infinity <c>false</c> otherwise.</returns>
    /// <param name="vector">Vector.</param>
    public static bool ContainsPositiveInfinity(this Vector3 vector)
    {
        return vector.ContainsValue(float.PositiveInfinity);
    }

    /// <summary>
    /// Detect if any of the values of this vector are negative infinity. 
    /// </summary>
    /// <returns><c>true</c>, if any of the values of this vector contain a positive infinity <c>false</c> otherwise.</returns>
    /// <param name="vector">Vector.</param>
    public static bool ContainsNegativeInfinity(this Vector3 vector)
    {
        return vector.ContainsValue(float.NegativeInfinity);
    }

    private static bool ContainsValue(this Vector3 vector, float value)
    {
        return vector.x.Equals(value) || vector.y.Equals(value) || vector.z.Equals(value);
    }

    /// <summary>
    /// Detect if any of the values of this vector are non numerical, i.e. nan, positive inifinity or negative infinity.
    /// </summary>
    /// <returns><c>true</c>, if any of the values of this vector are non numerical <c>false</c> otherwise.</returns>
    /// <param name="vector">Vector.</param>
    public static bool ContainsNonNumerical(this Vector3 vector)
    {
        return vector.ContainsNaNs() || vector.ContainsNegativeInfinity() || vector.ContainsPositiveInfinity();
    }
}
