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
}
