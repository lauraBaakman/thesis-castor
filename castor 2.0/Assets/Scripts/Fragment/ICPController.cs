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

        private ParticleSystem ParticleSystem;

        private void Awake()
        {
            ParticleSystem = GetComponent<ParticleSystem>();
            Debug.Assert(ParticleSystem, "Could not get the ParticleSystem component.");
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

        public void OnPreparetionStepCompleted()
        {
            //Do nothing
        }

        public void OnStepCompleted()
        {
            ClearPoints();
        }

        public void OnICPTerminated(ICPTerminatedMessage message)
        {
            ClearPoints();
        }
    }
}