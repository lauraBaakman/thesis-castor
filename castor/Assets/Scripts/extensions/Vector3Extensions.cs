using UnityEngine;

public static class Vector3Extensions
{
    /// <summary>
    /// Limit the values of this to the inclusive range 
    /// <paramref name="min"/>, <paramref name = "max" /> ,
    /// <remark>Note that this function will change the current vector. </remark>
    /// </summary>
    /// <param name="vector">Vector to clamp.</param>
    /// <param name="min">Minimum of the the inclusive clamp range.</param>
    /// <param name="max">Maximum of the inclusive clamp range.</param>
    /// <returns>Returns zero.</returns>
    public static Vector3 Clamp(this Vector3 vector, float min, float max)
    {
        vector.x = ClampFloat(vector.x, min, max);
        vector.y = ClampFloat(vector.y, min, max);
        vector.z = ClampFloat(vector.z, min, max);
        return vector;
    }

    private static float ClampFloat(float value, float min, float max){
        value = value < min ? min : value;
        value = value > max ? max : value;
        return value;
    }
}
