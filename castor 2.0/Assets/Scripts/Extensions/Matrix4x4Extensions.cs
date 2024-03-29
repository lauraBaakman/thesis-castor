using UnityEngine;
using Utils;
using System;

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

	/// <summary>
	/// Extracts the scale from the matrix.
	///
	/// Note that this function cannot handle negative scales.
	///
	/// Source: https://answers.unity.com/questions/402280/how-to-decompose-a-trs-matrix.html?childToView=402281#answer-402281
	/// </summary>
	/// <returns>The rotation.</returns>
	/// <param name="matrix">Matrix.</param>
	public static Vector3 ExtractScale(this Matrix4x4 matrix)
	{
		Vector3 scale = new Vector3(
			x: matrix.GetColumn(0).magnitude,
			y: matrix.GetColumn(1).magnitude,
			z: matrix.GetColumn(2).magnitude
		);
		return scale;
	}

	/// <summary>
	/// Sets the translation of the matrix.
	/// </summary>
	/// <param name="matrix">Matrix.</param>
	/// <param name="translation">Translation.</param>
	public static Matrix4x4 SetTranslation(this Matrix4x4 matrix, Vector3 translation)
	{
		matrix.SetTRS(translation, Quaternion.identity, new Vector3(1, 1, 1));
		return matrix;
	}

	/// <summary>
	/// Sets the scale of the matrix.
	/// </summary>
	/// <param name="matrix">Matrix.</param>
	/// <param name="scale">Scale.</param>
	public static Matrix4x4 SetScale(this Matrix4x4 matrix, Vector3 scale)
	{
		matrix.SetTRS(new Vector3(), Quaternion.identity, scale);
		return matrix;
	}

	/// <summary>
	/// Sets the uniform scale of the matrix.
	/// </summary>
	/// <returns>The scale.</returns>
	/// <param name="matrix">Matrix.</param>
	/// <param name="scale">Scale.</param>
	public static Matrix4x4 SetScale(this Matrix4x4 matrix, float scale)
	{
		return matrix.SetScale(new Vector3(scale, scale, scale));
	}
}