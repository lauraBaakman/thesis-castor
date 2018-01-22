using UnityEngine;
using System.Collections.Generic;


public class ICPController : MonoBehaviour, Registration.IICPListener {

    public float ParticleSize = 0.01f;
    public Color ParticleColor = Color.white;
    private ParticleSystem ParticleSystem;

    private void Awake()
    {
        ParticleSystem = GetComponent<ParticleSystem>();
        Debug.Assert(ParticleSystem, "Could not get the ParticleSystem component.");
    }

    /// <summary>
    /// If the points to be used for ICP are selected this function visualizes them.
    /// </summary>
    /// <param name="points">The selected points.</param>
    public void OnICPPointsSelected( List<Vector3> points )
    {
        VisualizePoints(points);
    }

    private void VisualizePoints( List<Vector3> points )
    {
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[points.Count];
        for (int i = 0; i < points.Count; i++) {
            particles[i].position = points[i];
            particles[i].startColor = ParticleColor;
            particles[i].startSize = ParticleSize;

        }
        ParticleSystem.SetParticles(particles, particles.Length);
    }
}
