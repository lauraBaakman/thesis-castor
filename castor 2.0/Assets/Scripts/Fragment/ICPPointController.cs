using UnityEngine;
using System.Collections.Generic;
using Registration;

namespace Fragment
{
    [RequireComponent(typeof(ParticleSystem))]
    public class ICPPointController : MonoBehaviour, IICPListener
    {
        static Material normalMaterial;

        public float ParticleSize = 0.01f;

        private ParticleSystem ParticleSystem;

        private List<Normal> Normals = new List<Normal>();
        private Transform ReferenceTransform;

        private void Awake()
        {
            ParticleSystem = GetComponent<ParticleSystem>();
            Debug.Assert(ParticleSystem, "Could not get the ParticleSystem component.");
        }

        public void OnRenderObject()
        {
            RenderNormals();
        }

        #region Points
        /// <summary>
        /// If the points to be used for ICP are selected this function visualizes them.
        /// </summary>
        public void OnICPPointsSelected(ICPPointsSelectedMessage message)
        {
            SetParticleSystemTransform(message.Transform);
            VisualizePoints(message.Points);

            ReferenceTransform = message.Transform;
            StoreNormals(message.Points);
        }

        private void SetParticleSystemTransform(Transform newTransform)
        {
            ParticleSystem.MainModule main = ParticleSystem.main;
            main.simulationSpace = ParticleSystemSimulationSpace.Custom;
            main.customSimulationSpace = newTransform;
        }

        private void VisualizePoints(List<Point> points)
        {
            ParticleSystem.Particle[] particles = new ParticleSystem.Particle[points.Count];
            for (int i = 0; i < points.Count; i++)
            {
                particles[i].position = points[i].Position;
                particles[i].startColor = points[i].Color;
                particles[i].startSize = ParticleSize;
            }
            ParticleSystem.SetParticles(particles, particles.Length);
        }

        private void ClearPoints()
        {
            ParticleSystem.Clear();
        }
        #endregion

        #region Correspondences
        public void OnICPCorrespondencesChanged(ICPCorrespondencesChanged message)
        {
            //Do nothing, correspondences are handled by Fragments.ICPController.
        }
        #endregion

        #region Normals
        private void StoreNormals(List<Point> points)
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
            foreach (Normal normal in Normals)
            {
                DrawNormal((normal));
            }
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

        private void ClearNormals()
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
        public void OnPreparetionStepCompleted()
        {
            //Do nothing
        }

        public void OnStepCompleted()
        {
            Clear();
        }

        public void OnICPTerminated(ICPTerminatedMessage message)
        {
            Clear();
        }
        #endregion

        private void Clear()
        {
            ClearPoints();
            ClearNormals();
        }
    }

    internal class Normal
    {
        public readonly Vector3 Start;
        public readonly Vector3 End;
        public readonly Color Color;

        public Normal(Point point) :
        this(
                position: point.Position,
                direction: point.Normal,
                color: point.Color
            )
        { }

        public Normal(Vector3 position, Vector3 direction, Color color)
        {
            Start = position;
            End = ComputeEnd(position, direction);
            Color = color;
        }

        private Vector3 ComputeEnd(Vector3 position, Vector3 direction)
        {
            return position + direction * 50 * direction.magnitude;
        }
    }
}