using System.Collections.Generic;
using UnityEngine;
using IO;
using System;
using Registration;

public class StatisticsComputer : MonoBehaviour
{
	public Dictionary<string, object> Results { get { return transformComputer.Run.ToDictionary(); } }

	private bool done;
	public bool Done { get { return done; } }

	private _TransformationComputer transformComputer;

	public IEnumerator<object> Compute(StatisticsComputer.RunResult run)
	{
		this.done = false;

		transformComputer = new _TransformationComputer(run);
		yield return null;

		transformComputer.ReadObjFile();
		yield return null;

		transformComputer.CollectCorrespondences();
		yield return null;

		transformComputer.ComputeTransformationMatrix();
		yield return null;

		transformComputer.ExtractTranslationAndRotation();
		yield return null;

		this.done = true;
	}

	public class RunData
	{
		/// <summary>
		/// The id of the model fragment.
		/// </summary>
		public readonly string id;

		public RunData(string id)
		{
			this.id = id;
		}

		public static RunData RunDataForTests()
		{
			return new RunData("test run data don't use in production");
		}
	}

	public class RunResult
	{
		/// <summary>
		/// The distance used to normalize the distance between the actual and
		/// expected translation. The 2.0 reflects the radius of the sphere that
		/// was used to place the model fragments. 
		/// </summary>
		public static float maxDistance = 2.0f;

		/// <summary>
		/// Information on the run.
		/// </summary>
		public readonly RunData RunData;

		/// <summary>
		/// The string to the path with the obj file that has the final position
		/// in its vertex coordinates, and the initial position in its
		/// texture coordinates.
		/// </summary>
		public readonly string objPath;

		/// <summary>
		/// The rotation used to bring the modelpoints to the static points, i.e.
		/// the rotation determined by the registration process.
		/// </summary>
		public Quaternion ActualRotation
		{
			get { return actualRotation; }
			set
			{
				actualRotation = value;
				ComputeRotationDifference();
			}
		}
		private Quaternion actualRotation;

		/// <summary>
		/// The expected rotation, i.e. the inverse of the rotation used to
		/// create the dataset.
		/// </summary>      
		public readonly Quaternion ExpectedRotation;

		/// <summary>
		/// The translation used to bring the modelpoints to the static points, i.e.
		/// the translation determined by the registration process.
		/// </summary>
		public Vector3 ActualTranslation
		{
			get { return actualTranslation; }
			set
			{
				actualTranslation = value;
				ComputeTranslationDifference();
			}
		}
		private Vector3 actualTranslation;

		/// <summary>
		/// The translation error, i.e. the euclidean distance, normalized 
		/// with maxDistance, between the actual and expected translation. If 
		/// the actual translation is not set an argumentexception is thrown.
		/// </summary>
		public float TranslationError
		{
			get
			{
				if (float.IsNaN(translationError))
					throw new ArgumentException("Set the actual translation before retrieving the translation eror");
				return translationError;
			}
		}
		private float translationError = float.NaN;

		/// <summary>
		/// The rotation error in zxy euler angles, i.e. expected rotation - actual 
		/// rotation. Normalized to ensure that the difference in rotation lies
		///  between -180 and +180. If the actual rotation is not set an 
		/// argumentexception is thrown.
		/// </summary>
		/// <value>The rotation error.</value>
		public Vector3 RotationError
		{
			get
			{
				if (rotationError.ContainsNaNs())
					throw new ArgumentException("Set the actual rotation before retrieving the rotation eror");
				return rotationError;
			}
		}
		private Vector3 rotationError = new Vector3(float.NaN, float.NaN, float.NaN);

		/// <summary>
		/// The expected translation, i.e. the inverse of the translation used to
		/// create the dataset.
		/// </summary>
		public readonly Vector3 ExpectedTranslation;

		public RunResult(string objPath, Quaternion expectedRotation, Vector3 expectedTranslation, RunData runData)
		{
			this.ExpectedRotation = expectedRotation;
			this.ExpectedTranslation = expectedTranslation;

			this.RunData = runData;

			this.objPath = objPath;
		}

		private void ComputeRotationDifference()
		{
			Vector3 actualEuler = actualRotation.eulerAngles;
			Vector3 expectedEuler = ExpectedRotation.eulerAngles;

			Vector3 difference = expectedEuler - actualEuler;
			for (int i = 0; i < 3; i++) rotationError[i] = NormalizeAngle(difference[i]);
		}

		/// <summary>
		/// Normalizes the angle by ensure that it falls in the range (-180, +180].
		/// </summary>
		/// <returns>The angle.</returns>
		/// <param name="angle">Angle.</param>
		static private float NormalizeAngle(float angle)
		{
			// Ensure that angle in [0, 360).
			angle = (angle + Mathf.Ceil(Mathf.Abs(angle / 360f)) * 360f) % 360f;

			// Ensure that angle in (-180, +180]
			if (angle > 180) angle -= 360;

			return angle;
		}

		private void ComputeTranslationDifference()
		{
			float distance = (ExpectedTranslation - actualTranslation).magnitude;
			translationError = distance / maxDistance;
		}

		public Dictionary<string, object> ToDictionary()
		{
			Dictionary<string, object> dict = new Dictionary<string, object>
			{
				{ "id", RunData.id },

				{ "actual translation x", actualTranslation.x },
				{ "actual translation y", actualTranslation.y },
				{ "actual translation z", actualTranslation.z },

				{ "actual rotation quaternion x", actualRotation.x },
				{ "actual rotation quaternion y", actualRotation.y },
				{ "actual rotation quaternion z", actualRotation.z },
				{ "actual rotation quaternion w", actualRotation.w },

				{ "actual rotation zxy euler x", actualRotation.eulerAngles.x },
				{ "actual rotation zxy euler y", actualRotation.eulerAngles.y },
				{ "actual rotation zxy euler z", actualRotation.eulerAngles.z },

				{ "rotation error x", RotationError.x },
				{ "rotation error y", RotationError.y },
				{ "rotation error z", RotationError.z },
			};

			return dict;
		}
	}
}

//Shouldn't be public, but wanted to test it
public class _TransformationComputer
{
	private Mesh mesh;
	public Mesh Mesh { get { return mesh; } }

	private CorrespondenceCollection correspondences;
	public CorrespondenceCollection Correspondences { get { return correspondences; } }

	private StatisticsComputer.RunResult run;
	public StatisticsComputer.RunResult Run { get { return run; } }

	private Matrix4x4 transformationMatrix;
	public Matrix4x4 TransformationMatrix { get { return transformationMatrix; } }

	public _TransformationComputer(StatisticsComputer.RunResult run)
	{
		correspondences = new CorrespondenceCollection();

		this.run = run;
	}

	public void ReadObjFile()
	{
		ReadResult result = ObjFile.Read(run.objPath);
		if (result.Failed)
			throw new InvalidObjFileException(
				string.Format(
					"Encountered the error {0} while reading the obj file {1}.",
					result.Message, run.objPath
				)
			);

		this.mesh = result.Mesh;
	}

	public void CollectCorrespondences()
	{
		int vertexCount = mesh.vertexCount;

		for (int i = 0; i < vertexCount; i++)
			AddCorrespondence(i);
	}

	private void AddCorrespondence(int idx)
	{
		Vector3 newPosition = mesh.vertices[idx];

		Vector3 oldPosition = new Vector3(
			x: mesh.uv2[idx].x,
			y: mesh.uv2[idx].y,
			z: mesh.uv3[idx].x
		);

		this.correspondences.Add(
			new Correspondence(
				staticPosition: newPosition,
				modelPosition: oldPosition
			)
		);
	}

	/// <summary>
	/// Find the transformation matrix that transforms the original vertex
	/// positions, stored in the texture coordinates, to the output vertex
	/// position, stord in vertex coordinates.
	/// </summary>
	public void ComputeTransformationMatrix()
	{
		HornTransformFinder horn = new HornTransformFinder();
		transformationMatrix = horn.FindTransform(this.correspondences);
	}

	public void ExtractTranslationAndRotation()
	{
		run.ActualTranslation = ExtractTranslation();
		run.ActualRotation = ExtractRotation();
	}

	private Vector3 ExtractTranslation()
	{
		return transformationMatrix.ExtractTranslation();
	}

	private Quaternion ExtractRotation()
	{
		return transformationMatrix.ExtractRotation();
	}
}
