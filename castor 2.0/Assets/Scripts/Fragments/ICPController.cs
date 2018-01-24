using UnityEngine;
using System.Collections;
using Registration;
using System.Collections.Generic;
using System;

namespace Fragments
{
    public class ICPController : MonoBehaviour, Registration.IICPListener
    {
        private List<Correspondence> Correspondences = new List<Correspondence>();
        private Transform ReferenceTransform;

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
                
            }
        }

        private bool CorrespondencesPresent()
        {
            return Correspondences.Count > 0;
        }

        public IEnumerator OnICPFinished()
        {
            ClearCorrespondences();
            if (Application.isEditor)
            {
                Debug.Log("Fragments:ICPController:OnICPFinished: yielding for a while");
                yield return new WaitForSeconds(3);
            }
        }

        private void ClearCorrespondences()
        {
            Correspondences.Clear();
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
    }
}