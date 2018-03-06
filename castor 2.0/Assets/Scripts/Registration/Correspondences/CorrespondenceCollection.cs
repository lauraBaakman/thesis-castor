
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

        public CorrespondenceCollection()
        {
            this.modelpoints = new List<Point>();
            this.staticpoints = new List<Point>();
            this.correspondences = new List<Correspondence>();
        }

        #region forwarding methods and properties
        public void Add(DistanceNode node)
        {
            throw new System.NotImplementedException();
        }

        public void Add(Correspondence correspondence)
        {
            throw new System.NotImplementedException();
        }

        public int Count
        {
            get { throw new System.NotImplementedException(); }
        }

        public void Clear()
        {
            throw new System.NotImplementedException();
        }
        #endregion

        #region enumerators
        public IEnumerator<Point> GetStaticPointEnumerator()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerator<Point> GetModelPointEnumerator()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerator<Correspondence> GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

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

        public override string ToString()
        {
            return string.Format("[CorrespondenceCollection: \n{}", this.correspondences.ElementsToString());
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
    }
}