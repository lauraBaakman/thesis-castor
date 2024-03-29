using UnityEngine;
using Registration.Messages;
using Registration;
using System.Collections.Generic;

namespace Fragments
{
	public class ICPCorrespondencesController : MonoBehaviour, IICPListener
	{
		private List<Correspondence> Correspondences = new List<Correspondence>();
		private Transform ReferenceTransform;

		public bool showCorrespondences = true;

		static Material CorrespondenceMaterial;

		public void Start()
		{
			if (CLI.Instance.CLIModeActive) Destroy(this);
		}

		#region Correspondences
		private void RenderCorrespondences()
		{
			if (CorrespondencesPresent() && showCorrespondences)
			{
				Debug.Assert(ReferenceTransform, "The reference transform needs to be set");

				//Needs to happen every render
				CreateSetApplyCorrespondenceMaterial();

				// Set transformation matrix to match our transform
				GL.PushMatrix();
				GL.MultMatrix(ReferenceTransform.localToWorldMatrix);

				GL.Begin(GL.LINES);

				DrawCorrespondences();

				GL.End();
				GL.PopMatrix();
			}
		}

		private void DrawCorrespondences()
		{
			foreach (Correspondence correspondence in Correspondences)
			{
				DrawCorrespondence(correspondence);
			}
		}

		private void DrawCorrespondence(Correspondence correspondence)
		{
			GL.Color(correspondence.Color);

			GL.Vertex3(
				correspondence.StaticPoint.Position.x,
				correspondence.StaticPoint.Position.y,
				correspondence.StaticPoint.Position.z
			);
			GL.Vertex3(
				correspondence.ModelPoint.Position.x,
				correspondence.ModelPoint.Position.y,
				correspondence.ModelPoint.Position.z
			);
		}

		private bool CorrespondencesPresent()
		{
			return Correspondences.Count > 0;
		}

		private void ClearCorrespondences()
		{
			Correspondences.Clear();
		}

		static void CreateSetApplyCorrespondenceMaterial()
		{
			//Source https://docs.unity3d.com/ScriptReference/GL.html
			Shader shader = Shader.Find("Hidden/Internal-Colored");

			CorrespondenceMaterial = new Material(shader)
			{
				hideFlags = HideFlags.HideAndDontSave
			};

			// Turn on alpha blending
			CorrespondenceMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
			CorrespondenceMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);

			// Turn backface culling off
			CorrespondenceMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);

			// Turn off depth writes
			CorrespondenceMaterial.SetInt("_ZWrite", 0);

			// Apply the material
			CorrespondenceMaterial.SetPass(0);
		}
		#endregion

		/// <summary>
		/// GL calls need to be placed in OnRenderObject to avoid executing before 
		/// the camera is rendered, since the camera clears the screen. 
		/// Source: https://docs.unity3d.com/ScriptReference/GL.html
		/// </summary
		public void OnRenderObject()
		{
			if (showCorrespondences) RenderCorrespondences();
		}

		#region ICPProgress
		public void OnStepCompleted(ICPStepCompletedMessage message)
		{
			ClearCorrespondences();
		}

		public void OnICPTerminated(ICPTerminatedMessage message)
		{
			ClearCorrespondences();
		}

		public void OnPreparationStepCompleted(ICPPreparationStepCompletedMessage message)
		{
			if (!showCorrespondences) return;

			Correspondences.AddRange(message.Correspondences);
			ReferenceTransform = message.Transform;
		}
		#endregion
	}
}