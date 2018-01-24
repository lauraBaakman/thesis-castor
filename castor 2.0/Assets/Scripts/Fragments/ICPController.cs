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

        private void RenderCorrespondences()
        {
            if (CorrespondencesPresent())
            {
                Debug.Assert(ReferenceTransform, "The reference transform needs to be set");

                //Needs to happen every render
                CreateSetApplyCorrespondenceMaterial();

                // Set transformation matrix to match our transform
                GL.PushMatrix();
                GL.MultMatrix(ReferenceTransform.localToWorldMatrix);

                GL.Begin(GL.LINES);
                GL.Color(CorrespondenceColor);

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
            GL.Vertex3(
                correspondence.StaticPoint.x,
                correspondence.StaticPoint.y,
                correspondence.StaticPoint.z
            );
            GL.Vertex3(
                correspondence.ModelPoint.x,
                correspondence.ModelPoint.y,
                correspondence.ModelPoint.z
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
        /// </summary>
        public void OnRenderObject()
        {
            RenderCorrespondences();
        }

        public IEnumerator OnICPFinished()
        {
            if (Application.isEditor)
            {
                Debug.Log("Fragments:ICPController:OnICPFinished: yielding for a while");
                yield return new WaitForSeconds(300);
            }

            ClearCorrespondences();
        }
    }
}