using UnityEngine;
using System.Collections.Generic;
using Registration.Messages;
using Registration;

namespace Fragment
{
    public class ICPNormalController : MonoBehaviour, IICPListener
    {
        static Material normalMaterial;
        public bool ShowNormals = true;

        private List<Normal> Normals = new List<Normal>();
        private Transform ReferenceTransform;

        private ICPController ICPControllerOfParent;

        private void Awake()
        {
            GameObject parent = this.transform.parent.gameObject;
            ICPControllerOfParent = parent.GetComponent<ICPController>();
            Debug.Assert(ICPControllerOfParent != null, "The parent gameobject of the object that has the " + this.name + " is expected to have an ICPController.");
        }

        public void OnRenderObject()
        {
            if (ShowNormals) RenderNormals();
        }

        #region Normals
        private void StoreNormals(IEnumerable<Point> points)
        {
            foreach (Point point in points)
            {
                if (point.HasNormal) Normals.Add(new Normal(point));
            }
        }

        private void RenderNormals()
        {
            if (!NormalsPresent()) return;

            Debug.Assert(ReferenceTransform, "The reference transform needs to be set");

            CreateSetApplyNormalMaterial();

            // Set transformation matrix to match our transform
            GL.PushMatrix();
            GL.MultMatrix(ReferenceTransform.localToWorldMatrix);

            GL.Begin(GL.LINES);

            DrawNormals();

            GL.End();
            GL.PopMatrix();

        }

        private void DrawNormals()
        {
            foreach (Normal normal in Normals) DrawNormal((normal));
        }

        private void DrawNormal(Normal normal)
        {
            GL.Color(normal.Color);

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

        private bool NormalsPresent()
        {
            return Normals.Count > 0;
        }

        private void Clear()
        {
            Normals.Clear();
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
        #endregion

        #region progress
        public void OnStepCompleted(ICPStepCompletedMessage message)
        {
            Clear();
        }

        public void OnICPTerminated(ICPTerminatedMessage message)
        {
            Clear();
        }

        public void OnPreparationStepCompleted(ICPPreparationStepCompletedMessage message)
        {
            ReferenceTransform = message.Transform;
            ICPFragmentType type = ICPControllerOfParent.FragmentType;

            if (ShowNormals) StoreNormals(message.Correspondences.GetPointsByType(type));
        }
        #endregion
    }

    internal class Normal
    {
        private static float MagnitudeFactor = 0.5f;
        private static Color DefaultColor = Color.white;

        public readonly Vector3 Start;
        public readonly Vector3 End;
        public readonly Color Color;

        public Normal(Point point) :
        this(
                position: point.Position,
                direction: point.Normal
            )
        { }

        public Normal(Vector3 position, Vector3 direction)
        {
            Start = position;
            End = ComputeEnd(position, direction);
            Color = DefaultColor;
        }

        private Vector3 ComputeEnd(Vector3 position, Vector3 direction)
        {
            return position + direction * MagnitudeFactor * direction.magnitude;
        }
    }
}