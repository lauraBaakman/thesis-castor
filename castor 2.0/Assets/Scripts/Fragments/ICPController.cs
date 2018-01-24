using UnityEngine;
using System.Collections;
using Registration;
using System.Collections.Generic;
using System;

namespace Fragments
{
    public class ICPController : MonoBehaviour, Registration.IICPListener
    {
        public Color CorrespondenceColor;

        private List<Correspondence> Correspondences = new List<Correspondence>();
        private Transform ReferenceTransform;

        static Material CorrespondenceMaterial;

        #region Points
        public void OnICPPointsSelected(ICPPointsSelectedMessage message)
        {
            //no need to do anything, the fragments handle the rendering of points.
        }
        #endregion

        #region Correspondences
        public void OnICPCorrespondencesDetermined(ICPCorrespondencesDeterminedMessage message)
        {
            Correspondences.Clear();
            Correspondences.AddRange(message.Correspondences);

            ReferenceTransform = message.Transform;
        }

        private void DrawCorrespondences()
        {
            if (CorrespondencesPresent())
            {
                CreateSetApplyCorrespondenceMaterial();

                // Set transformation matrix to match our transform
                GL.PushMatrix();
                GL.MultMatrix(transform.localToWorldMatrix);

                int lineCount = 10;
                float radius = 3.0f;

                GL.Begin(GL.LINES);
                GL.Color(CorrespondenceColor);
                for (int i = 0; i < lineCount; ++i)
                {
                    float a = i / (float)lineCount;
                    float angle = a * Mathf.PI * 2;
                    // One vertex at transform position
                    GL.Vertex3(0, 0, 0);
                    // Another vertex at edge of circle
                    GL.Vertex3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0);
                }

                GL.End();
                GL.PopMatrix();
            }
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

            CorrespondenceMaterial = new Material(shader);
            CorrespondenceMaterial.hideFlags = HideFlags.HideAndDontSave;

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
        /// </summary>
        public void OnRenderObject()
        {
            DrawCorrespondences();
        }

        public IEnumerator OnICPFinished()
        {
            if (Application.isEditor)
            {
                Debug.Log("Fragments:ICPController:OnICPFinished: yielding for a while");
                yield return new WaitForSeconds(3);
            }

            ClearCorrespondences();
        }
    }
}