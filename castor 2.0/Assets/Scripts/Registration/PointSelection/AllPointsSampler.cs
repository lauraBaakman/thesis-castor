using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections.ObjectModel;
using DoubleConnectedEdgeList;

namespace Registration
{
    /// <summary>
    /// The simple point selector simply selects all the points of the mesh.
    /// </summary>
    public class AllPointsSampler : IPointSampler
    {
        private delegate List<Point> SamplingFunction(SamplingInformation info);

        /// <summary>
        /// The transform that the points should be sampled in.
        /// </summary>
        private readonly Transform ReferenceTransform;

        /// <summary>
        /// The function used to select the points, chosen based on the paramters 
        /// passed to the constructor.
        /// </summary>
        private SamplingFunction samplingFunction;

        public AllPointsSampler(SamplingConfiguration configuration)
        {
            ReferenceTransform = configuration.referenceTransform;

            samplingFunction = SelectSamplingFunction(configuration.normalProcessing);
        }

        private SamplingFunction SelectSamplingFunction(SamplingConfiguration.NormalProcessing normalProcessing)
        {
            switch (normalProcessing)
            {
                case SamplingConfiguration.NormalProcessing.NoNormals:
                    return NoNormals;
                case SamplingConfiguration.NormalProcessing.VertexNormals:
                    return VertexNormals;
                case SamplingConfiguration.NormalProcessing.AreaWeightedSmoothNormals:
                    return SmoothNormals;
                default:
                    throw new ArgumentException();
            }
        }

        public List<Point> Sample(SamplingInformation samplingInfo)
        {
            return samplingFunction(samplingInfo);
        }

        private List<Point> NoNormals(SamplingInformation info)
        {
            ReadOnlyCollection<Vertex> vertices = info.DCEL.Vertices;
            List<Point> points = new List<Point>(vertices.Count);

            Vector3 position;
            foreach (Vertex vertex in vertices)
            {
                position = vertex.Position;
                points.Add(
                    new Point(
                        position.ChangeTransformOfPosition(info.Transform, ReferenceTransform)
                    )
                );
            }
            return points;
        }

        private List<Point> VertexNormals(SamplingInformation info)
        {
            Mesh fragment = info.Mesh;
            List<Point> points = new List<Point>();

            for (int i = 0; i < fragment.vertices.Length; i++)
            {
                points.Add(
                    new Point(
                        position: fragment.vertices[i].ChangeTransformOfPosition(
                            info.Transform, ReferenceTransform),
                        normal: fragment.normals[i].ChangeTransformOfDirection(
                            info.Transform, ReferenceTransform)
                    )
                );
            }
            return points;
        }

        private List<Point> SmoothNormals(SamplingInformation info)
        {
            ReadOnlyCollection<Vertex> vertices = info.DCEL.Vertices;
            List<Point> points = new List<Point>(vertices.Count);

            Vector3 position, normal;
            foreach (Vertex vertex in vertices)
            {
                position = vertex.Position;
                normal = vertex.SmoothedNormal();
                points.Add(
                    new Point(
                        position: position.ChangeTransformOfPosition(info.Transform, ReferenceTransform),
                        normal: normal.ChangeTransformOfDirection(info.Transform, ReferenceTransform)
                    )
                );
            }
            return points;
        }
    }
}