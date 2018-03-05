using UnityEngine;

public static class Matrix4x4Extensions
{
    /// <summary>
    ///Extracts the translation from the matrix.
    /// Source: https://answers.unity.com/answers/402281/view.html
    /// </summary>
    /// <returns>The translation.</returns>
    /// <param name="matrix">Matrix.</param>
    public static Vector3 ExtractTranslation(this Matrix4x4 matrix)
    {
        Vector3 position = matrix.GetColumn(3);
        return position;
    }

    /// <summary>
    /// Extracts the rotation from the matrix.
    /// Source: https://answers.unity.com/answers/402281/view.html and https://forum.unity.com/threads/how-to-assign-matrix4x4-to-transform.121966/
    /// </summary>
    /// <returns>The rotation.</returns>
    /// <param name="matrix">Matrix.</param>
    public static Quaternion ExtractRotation(this Matrix4x4 matrix)
    {
        Vector3 forward = matrix.GetColumn(2);
        Vector3 upwards = matrix.GetColumn(1);

        Quaternion rotation = Quaternion.LookRotation(
            forward: forward,
            upwards: upwards
        );
        return rotation;
    }
}
