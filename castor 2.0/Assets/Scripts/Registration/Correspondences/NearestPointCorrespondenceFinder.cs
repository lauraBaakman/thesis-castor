using UnityEngine;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using System.Collections;

namespace Registration
{
    /// <summary>
    /// Find the correspondences using the nearest point method. The method computes 
    /// the distances of all point pairs and creates correspondences starting from the 
    /// pair with the smallest distance until all points of at least one of the meshes 
    /// are involved in a correspondence.
    /// </summary>
    public class NearstPointCorrespondenceFinder : ICorrespondenceFinder
    {
        public List<Correspondence> Find(ReadOnlyCollection<Point> staticPoints, ReadOnlyCollection<Point> modelPoints)
        {

            List<DistanceNode> distanceNodes = CreateDistanceNodeList(staticPoints, modelPoints);
            List<Correspondence> correspondences = CreateCorrespondenceList(distanceNodes, Mathf.Min(staticPoints.Count, modelPoints.Count));

            return correspondences;
        }

        public List<Correspondence> Find(ReadOnlyCollection<Point> staticPoints, IPointSelector sampler, SamplingInformation modelSamplingInformation)
        {
            List<Point> modelPoints = sampler.Select(modelSamplingInformation);
            return Find(staticPoints, modelPoints.AsReadOnly());
        }

        /// <summary>
        /// Create the list of elements, where each element contains the distance
        /// between two points of the each of the meshes.
        /// </summary>
        /// <returns>The list with distance nodes..</returns>
        /// <param name="staticPoints">Static points.</param>
        /// <param name="modelPoints">Model points.</param>
        public List<DistanceNode> CreateDistanceNodeList(ReadOnlyCollection<Point> staticPoints, ReadOnlyCollection<Point> modelPoints)
        {
            List<DistanceNode> nodes = new List<DistanceNode>();

            Point staticPoint, modelPoint;
            for (int staticIdx = 0; staticIdx < staticPoints.Count; staticIdx++)
            {
                staticPoint = staticPoints[staticIdx];

                for (int modelIdx = 0; modelIdx < modelPoints.Count; modelIdx++)
                {
                    modelPoint = modelPoints[modelIdx];

                    nodes.Add(
                        new DistanceNode(
                            staticPoint: staticPoint,
                            modelPoint: modelPoint,
                            distance: SquaredEuclideanDistance(staticPoint, modelPoint)
                        )
                    );
                }
            }
            return nodes;
        }

        private float SquaredEuclideanDistance(Point staticPoint, Point modelPoint)
        {
            return (staticPoint.Position - modelPoint.Position).sqrMagnitude;
        }

        public List<Correspondence> CreateCorrespondenceList(List<DistanceNode> distanceNodes, int numPointsSmallestFragment)
        {
            return new CorrespondenceListBuilder(distanceNodes, numPointsSmallestFragment).Build();
        }
    }

    internal class CorrespondenceListBuilder
    {
        private Stack<DistanceNode> DistanceNodes;
        private HashSet<Point> StaticPointsInACorrespondence;
        private HashSet<Point> ModelPointsInACorrespondence;
        private List<Correspondence> Correspondences;
        private int FinalCorrespondenceCount;

        internal CorrespondenceListBuilder(List<DistanceNode> distanceNodes, int numPointsSmallestFragment)
        {
            distanceNodes.Sort(DistanceNode.SortDescendingOnDistance());
            DistanceNodes = new Stack<DistanceNode>(distanceNodes);

            StaticPointsInACorrespondence = new HashSet<Point>();
            ModelPointsInACorrespondence = new HashSet<Point>();

            FinalCorrespondenceCount = numPointsSmallestFragment;

            Correspondences = new List<Correspondence>();
        }

        internal List<Correspondence> Build()
        {
            DistanceNode currentNode;
            while (!FinishedCreatingCorrespondences())
            {
                currentNode = DistanceNodes.Pop();

                if (ShouldBeCorrespondence(currentNode))
                {
                    AddNodeToCorrespondences(currentNode);
                }
            }
            return Correspondences;
        }

        private bool FinishedCreatingCorrespondences()
        {
            // Found all correspondences
            if (Correspondences.Count >= FinalCorrespondenceCount) return true;

            // Reviewed all distance nodes
            if (DistanceNodes.Count <= 0)
            {
                Debug.LogError(
                    "Stopped creating correspondences because we ran out of " +
                    "posible pairs, not because all correspondences were found."
                );
                return true;
            }

            // Continue with finding correspondences
            return false;
        }

        private void AddNodeToCorrespondences(DistanceNode node)
        {
            Correspondences.Add(new Correspondence(node));

            StaticPointsInACorrespondence.Add(node.StaticPoint);
            ModelPointsInACorrespondence.Add(node.ModelPoint);
        }

        private bool ShouldBeCorrespondence(DistanceNode node)
        {
            return (
                !StaticPointsInACorrespondence.Contains(node.StaticPoint) &&
                !ModelPointsInACorrespondence.Contains(node.ModelPoint)
            );
        }
    }

    class SortDistanceNodeDescending : IComparer<DistanceNode>
    {
        public int Compare(DistanceNode x, DistanceNode y)
        {
            int original = x.CompareTo(y);

            if (original < 0) return +1;
            if (original > 0) return -1;

            return 0;
        }
    }

    public class DistanceNode : IComparable<DistanceNode>, IEquatable<DistanceNode>
    {
        private readonly Point staticPoint;
        public Point StaticPoint
        {
            get
            {
                return staticPoint;
            }
        }

        private readonly Point modelPoint;
        public Point ModelPoint
        {
            get
            {
                return modelPoint;
            }
        }

        private readonly float distance;
        public float Distance
        {
            get
            {
                return distance;
            }
        }

        public DistanceNode(Point staticPoint, Point modelPoint, float distance)
        {
            this.staticPoint = staticPoint;
            this.modelPoint = modelPoint;
            this.distance = distance;
        }

        public int CompareTo(DistanceNode other)
        {
            if (other == null) return 1;

            /// If the distances are equal compare the points to ensure a consistent ordering for tests
            int distanceComparison = this.distance.CompareTo(other.distance);
            if (distanceComparison != 0) return distanceComparison;

            int staticPointComparison = this.staticPoint.CompareTo(other.staticPoint);
            if (staticPointComparison != 0) return staticPointComparison;

            return this.modelPoint.CompareTo(other.modelPoint);
        }

        public bool Equals(DistanceNode other)
        {
            if (other == null) return false;

            return (
                this.staticPoint.Equals(other.staticPoint) &&
                this.modelPoint.Equals(other.modelPoint) &&
                Mathf.Approximately(this.distance, other.distance)
            );
        }

        public override bool Equals(System.Object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            DistanceNode other = (DistanceNode)obj;
            return this.Equals(other);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format(
                "[DistanceNode: StaticPoint={0}, ModelPoint={1}, Distance={2}]",
                StaticPoint, ModelPoint, Distance
            );
        }

        public static IComparer<DistanceNode> SortDescendingOnDistance()
        {
            return (IComparer<DistanceNode>)new SortDistanceNodeDescending();
        }
    }
}
