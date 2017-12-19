using UnityEngine;

public static class Vector3Extensions
{
    private static float ClampFloat(float value, float min, float max){
        value = value < min ? min : value;
        value = value > max ? max : value;
        return value;
    }

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
            ClampFloat(vector.x, min, max),
            ClampFloat(vector.y, min, max),
            ClampFloat(vector.z, min, max)
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
}
