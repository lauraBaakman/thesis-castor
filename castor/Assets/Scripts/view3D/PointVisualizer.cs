using System.Collections.Generic;
using UnityEngine;

namespace Visualization
{
    public class PointVisualizer : MonoBehaviour
    {
        public int NumberOfPoints = 10;

        public float Size = 0.1f;
        public Color Color = Color.red;

        private ParticleSystem.Particle[] Points;
        private ParticleSystem ParticleSystem;

        private void Start()
        {
            Points = CreateParticles(CreatePoints(10));
            ParticleSystem = GetComponent<ParticleSystem>();
        }

        private void Update()
        {
            ParticleSystem.SetParticles(Points, Points.Length);
        }

        private List<Point> CreatePoints(int numberOfPoints){
            List<Point> points = new List<Point>();

            float increment = 1f / (numberOfPoints - 1);
            float x;
            for (int i = 0; i < numberOfPoints; i++)
            {
                x = i * increment;
                points.Add(
                    new Point(
                        new Vector3(x, 0f, 0f), 
                        Color, 
                        Size
                    )
                );
            }
            return points;
        }

        private ParticleSystem.Particle[] CreateParticles(List<Point> points)
        {
            ParticleSystem.Particle[] particles = new ParticleSystem.Particle[points.Count];

            for (int length = points.Count, i = 0; i < length; i++){
                particles[i] = points[i].ToParticle();    
            }
            return particles;
        }
    }

    public class Point
    {
        private readonly Vector3 Position;
        private readonly Color Color;
        private readonly float Size;

        private static Color DefaultColor = Color.black;
        private static float DefaultSize = 0.1f;

        public Point(Vector3 position, Color color, float size){
            Position = position;
            Color = color;
            Size = size;
        }

        public Point(Vector3 position, float size)
        {
            Position = position;
            Color = DefaultColor;
            Size = size;
        }

        public Point(Vector3 position, Color color)
        {
            Position = position;
            Color = color;
            Size = DefaultSize;
        }

        public ParticleSystem.Particle ToParticle(){
            ParticleSystem.Particle particle = new ParticleSystem.Particle
            {
                position = Position,
                startColor = Color,
                startSize = Size
            };
            return particle;
        }
    }
}

