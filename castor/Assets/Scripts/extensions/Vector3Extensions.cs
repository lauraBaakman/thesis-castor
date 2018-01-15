using UnityEngine;

public static class Vector3Extensions
{
    /// <summary>
    /// Limit the values of this to the inclusive range 
    /// <paramref name="min"/>, <paramref name = "max" /> ,
    /// <remark>Note that this function creates a new clamped vector and does not change the current vector. </remark>
    /// </summary>
    /// <param name="min">Minimum of the the inclusive clamp range.</param>
    /// <param name="max">Maximum of the inclusive clamp range.</param>
    /// <returns>Returns the clamped vector.</returns>
    public static Vector3 Clamped(this Vector3 vector, float min, float max){
        Vector3 clampedVector = new Vector3(
            Mathf.Clamp(vector.x, min, max),
            Mathf.Clamp(vector.y, min, max),
            Mathf.Clamp(vector.z, min, max)
        );
        return clampedVector;
    }

    /// <summary>
    /// Place <paramref name="value"/> at all vector elements.
    /// <remark>Note that this function will change the current vector. </remark>
    /// </summary>
    /// <param name="value">The value that should fill the vector.</param>
    /// <returns>Returns the filled vector.</returns>
    public static Vector3 Fill(this Vector3 vector, float value){
        vector.x = value;
        vector.y = value;
        vector.z = value;
        return vector;        
    }


    /// <summary>
    /// Divides vector elementwise with the other vector.
    /// </summary>
    /// <returns>A new vector with the element wise division of this vector and 
    /// the passed vector.</returns>
    /// <param name="vector">Vector.</param>
    /// <param name="other">Other.</param>
    public static Vector3 DivideElementWise(this Vector3 vector, Vector3 other){
        Vector3 result = new Vector3(
            vector.x / other.x,
            vector.y / other.y,
            vector.z / other.z
        );
        return result;
    }

    /// <summary>
    /// Multiply the specified vector and other elementwise.
    /// </summary>
    /// <returns>A new vector containing the element wise multiplication of this and other.</returns>
    /// <param name="vector">The left hand side.</param>
    /// <param name="other">The right hand side.</param>
    public static Vector3 Multiply(this Vector3 vector, Vector3 other)
    {
        Vector3 result = new Vector3(
            vector.x * other.x,
            vector.y * other.y,
            vector.z * other.z
        );
        return result;
    }

    /// <summary>
    /// Returns the minimum value present in the specified vector.
    /// </summary>
    /// <returns>The minimum value in the vector.</returns>
    /// <param name="vector">Vector to find the minimum value of.</param>
    public static float Min(this Vector3 vector)
    {
        return Mathf.Min(Mathf.Min(vector.x, vector.y), vector.z);
    }

    /// <summary>
    /// Returns the maximum value present in the specified vector.
    /// </summary>
    /// <returns>The maximum value in the vector.</returns>
    /// <param name="vector">Vector to find the maximum value of.</param>
    public static float Max(this Vector3 vector)
    {
        return Mathf.Max(Mathf.Max(vector.x, vector.y), vector.z);
    }

}
