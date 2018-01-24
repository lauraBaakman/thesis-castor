using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Registration;
using System;

namespace Fragment
{
    public class ICPController : MonoBehaviour, IICPListener
    {

        public float ParticleSize = 0.01f;
        public Color ParticleColor = Color.white;

        private ParticleSystem ParticleSystem;

        private void Awake()
        {
            ParticleSystem = GetComponent<ParticleSystem>();
            Debug.Assert(ParticleSystem, "Could not get the ParticleSystem component.");
        }

        public IEnumerator OnICPFinished()
        {
            if (Application.isEditor)
            {
                Debug.Log("Fragment:ICPController:OnICPFinished: yielding for a while");
                yield return new WaitForSeconds(3);
            }

            ClearPoints();
        }

        #region Points
        /// <summary>
        /// If the points to be used for ICP are selected this function visualizes them.
        /// </summary>
        public void OnICPPointsSelected(ICPPointsSelectedMessage message)
        {
            SetParticleSystemTransform(message.Transform);
            VisualizePoints(message.Points);
        }

        private void SetParticleSystemTransform(Transform newTransform)
        {
            ParticleSystem.MainModule particleConfiguration = ParticleSystem.main;
            particleConfiguration.simulationSpace = ParticleSystemSimulationSpace.Custom;
            particleConfiguration.customSimulationSpace = newTransform;
        }

        private void VisualizePoints(List<Vector3> points)
        {
            ParticleSystem.Particle[] particles = new ParticleSystem.Particle[points.Count];
            for (int i = 0; i < points.Count; i++)
            {
                particles[i].position = points[i];
                particles[i].startColor = ParticleColor;
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
        public void OnICPCorrespondencesDetermined(ICPCorrespondencesDeterminedMessage message)
        {
            //Do nothing, correspondences are handled by Fragments.ICPController.
        }
        #endregion
    }
}