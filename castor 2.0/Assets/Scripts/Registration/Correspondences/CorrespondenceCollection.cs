
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Registration
{
    public class CorrespondenceCollection : IEnumerable<Correspondence>, IEquatable<CorrespondenceCollection>
    {
        #region members
        public ReadOnlyCollection<Point> ModelPoints
        {
            get { return modelpoints.AsReadOnly(); }
        }
        private List<Point> modelpoints;

        public ReadOnlyCollection<Point> StaticPoints
        {
            get { return staticpoints.AsReadOnly(); }
        }
        private List<Point> staticpoints;

        public ReadOnlyCollection<Correspondence> Correspondences
        {
            get { return correspondences.AsReadOnly(); }
        }
        private List<Correspondence> correspondences;
        #endregion

        #region properties
        public int Count
        {
            get { return correspondences.Count; }
        }

        public IEnumerable<Point> StaticPointsEnumerator
        {
            get { return new _PointEnumerator(this.staticpoints); }
        }

        public IEnumerable<Point> ModelPointsEnumerator
        {
            get { return new _PointEnumerator(this.modelpoints); }
        }
        #endregion

        public CorrespondenceCollection(List<Point> modelpoints, List<Point> staticpoints, List<Correspondence> correspondences)
        {
            this.modelpoints = modelpoints;
            this.staticpoints = staticpoints;
            this.correspondences = correspondences;
        }

        public CorrespondenceCollection()
        {
            this.modelpoints = new List<Point>();
            this.staticpoints = new List<Point>();
            this.correspondences = new List<Correspondence>();
        }

        #region forwarding methods and properties
        public void Add(DistanceNode node)
        {
            modelpoints.Add(node.ModelPoint);
            staticpoints.Add(node.StaticPoint);
            correspondences.Add(new Correspondence(node));
        }

        public void Add(Correspondence correspondence)
        {
            modelpoints.Add(correspondence.ModelPoint);
            staticpoints.Add(correspondence.StaticPoint);
            correspondences.Add(correspondence);
        }

        public void Clear()
        {
            correspondences.Clear();
            modelpoints.Clear();
            staticpoints.Clear();
        }
        #endregion

        #region Enumerators
        public IEnumerator<Correspondence> GetEnumerator()
        {
            return correspondences.GetEnumerator();
        }

        IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion

        #region IEquatable
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            CorrespondenceCollection other = (CorrespondenceCollection)obj;
            return this.Equals(other);
        }

        public override int GetHashCode()
        {
            int hash = 17;

            hash *= (31 + this.modelpoints.UnorderedElementsGetHashCode());
            hash *= (31 + this.staticpoints.UnorderedElementsGetHashCode());
            hash *= (31 + this.correspondences.UnorderedElementsGetHashCode());

            return hash;
        }

        public bool Equals(CorrespondenceCollection other)
        {
            return (
                this.correspondences.UnorderedElementsAreEqual(other.correspondences) &&
                this.modelpoints.UnorderedElementsAreEqual(other.modelpoints) &&
                this.staticpoints.UnorderedElementsAreEqual(other.staticpoints)
            );
        }
        #endregion

        public override string ToString()
        {
            return string.Format("[CorrespondenceCollection: \n{}", this.correspondences.ElementsToString());
        }
    }

    internal class _PointEnumerator : IEnumerable<Point>
    {
        private IEnumerable<Point> points;

        public _PointEnumerator(IEnumerable<Point> points)
        {
            this.points = points;
        }

        public IEnumerator<Point> GetEnumerator()
        {
            return this.points.GetEnumerator();
        }

        IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}