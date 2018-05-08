using UnityEngine;
using Utils;
using DoubleConnectedEdgeList;
using System.Collections.Generic;

namespace Fragment
{
	[RequireComponent(typeof(DoubleConnectedEdgeListStorage))]
	public class FaceNormalPainter : MonoBehaviour
	{
		static Material normalMaterial;

		public float normalScale = 1.0f;
		public Color normalColor = Color.white;

		private List<Normal> normals;

		public void Start()
		{
			Normal.MagnitudeFactor = normalScale;

			normals = new List<Normal>();
			DCEL dcel = GetComponent<DoubleConnectedEdgeListStorage>().DCEL;

			foreach (Face face in dcel.Faces) normals.Add(Normal.FaceNormal(face));
		}

		public void OnRenderObject()
		{
			RenderNormals();
		}

		private void RenderNormals()
		{
			CreateSetApplyNormalMaterial();

			// Set transformation matrix to match our transform
			GL.PushMatrix();
			GL.MultMatrix(this.transform.localToWorldMatrix);

			GL.Begin(GL.LINES);

			DrawNormals();

			GL.End();
			GL.PopMatrix();

		}

		private void CreateSetApplyNormalMaterial()
		{
			//Source https://docs.unity3d.com/ScriptReference/GL.html
			Shader shader = Shader.Find("Hidden/Internal-Colored");

			normalMaterial = new Material(shader)
			{
				hideFlags = HideFlags.HideAndDontSave
			};

			// Turn on alpha blending
			normalMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
			normalMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);

			// Turn backface culling off
			normalMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);

			// Turn off depth writes
			normalMaterial.SetInt("_ZWrite", 0);

			// Apply the material
			normalMaterial.SetPass(0);
		}

		private void DrawNormals()
		{
			foreach (Normal normal in normals) DrawNormal((normal));
		}

		private void DrawNormal(Normal normal)
		{
			GL.Color(normalColor);

			GL.Vertex3(
				normal.Start.x,
				normal.Start.y,
				normal.Start.z
			);
			GL.Vertex3(
				normal.End.x,
				normal.End.y,
				normal.End.z
			);
		}

	}
}
