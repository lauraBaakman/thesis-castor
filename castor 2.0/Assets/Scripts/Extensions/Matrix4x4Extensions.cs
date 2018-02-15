using UnityEngine;
using System.Collections;

public static class Matrix4x4Extensions
{
    /// <summary>
    ///Extracts the translation from the matrix.
    /// Source: https://answers.unity.com/answers/402281/view.html
    /// </summary>
    /// <returns>The translation.</returns>
    /// <param name="matrix">Matrix.</param>
    public static Vector3 ExtractTranslation(this Matrix4x4 matrix){
        Vector3 position = matrix.GetColumn(3);
        return position;
    }

    /// <summary>
    /// Extracts the rotation from the matrix.
    /// Source: https://answers.unity.com/answers/402281/view.html and https://forum.unity.com/threads/how-to-assign-matrix4x4-to-transform.121966/
    /// </summary>
    /// <returns>The rotation.</returns>
    /// <param name="matrix">Matrix.</param>
    public static Quaternion ExtratRotation(this Matrix4x4 matrix)
    {
        Vector3 forward = matrix.GetColumn(2);
        Vector3 upwards = matrix.GetColumn(1);
                                
        Quaternion rotation = Quaternion.LookRotation(
            forward:forward, 
            upwards:upwards
        );
        return rotation;
    }

    /// <summary>
    /// Extracts the rotation around X-Axis from the matrix.
    /// </summary>
    /// <returns>The rotation around X-Axis.</returns>
    public static float ExtractRotationAroundXAxis(this Matrix4x4 matrix){
        Quaternion rotation = matrix.ExtratRotation();
        Vector3 angles = rotation.eulerAngles;
        return angles.x;
    }

    /// <summary>
    /// Extracts the rotation around Y-Axis from the matrix.
    /// </summary>
    /// <returns>The rotation around Y-Axis.</returns>
    public static float ExtractRotationAroundYAxis(this Matrix4x4 matrix)
    {
        Quaternion rotation = matrix.ExtratRotation();
        Vector3 angles = rotation.eulerAngles;
        return angles.y;
    }

    /// <summary>
    /// Extracts the rotation around Z-Axis from the matrix.
    /// </summary>
    /// <returns>The rotation around Z-Axis.</returns>
    public static float ExtractRotationAroundZAxis(this Matrix4x4 matrix)
    {
        Quaternion rotation = matrix.ExtratRotation();
        Vector3 angles = rotation.eulerAngles;
        return angles.z;
    }
}
