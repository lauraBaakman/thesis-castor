using System;
using System.Collections;
using System.Collections.Generic;

namespace Registration
{
	public class CorrespondenceCollection : IEnumerable<Correspondence>, IEquatable<CorrespondenceCollection>
	{
		#region members
		public List<Point> ModelPoints
		{
			get { return modelpoints; }
		}
		private List<Point> modelpoints;

		public List<Point> StaticPoints
		{
			get { return staticpoints; }
		}
		private List<Point> staticpoints;

		public List<Correspondence> Correspondences
		{
			get { return correspondences; }
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

		public CorrespondenceCollection(IEnumerable<Correspondence> correspondences)
			: this()
		{
			foreach (Correspondence correspondence in correspondences) Add(correspondence);
		}

		public CorrespondenceCollection()
		{
			this.modelpoints = new List<Point>();
			this.staticpoints = new List<Point>();
			this.correspondences = new List<Correspondence>();
		}

		public CorrespondenceCollection(List<Point> modelpoints, List<Point> staticpoints)
			: this()
		{
			for (int i = 0; i < modelpoints.Count; i++)
			{
				Add(
					modelPoint: modelpoints[i],
					staticPoint: staticpoints[i]
				);
			}
		}

		#region forwarding methods and properties
		public void Add(DistanceNode node)
		{
			Add(new Correspondence(node));
		}

		public void Add(Correspondence correspondence)
		{
			//Avoid correspondence collection with duplicate correspondences
			if (this.correspondences.Contains(correspondence)) return;

			modelpoints.Add(correspondence.ModelPoint);
			staticpoints.Add(correspondence.StaticPoint);
			correspondences.Add(correspondence);
		}

		private void Add(Point modelPoint, Point staticPoint)
		{
			Add(
				new Correspondence(
					modelPoint: modelPoint,
					staticPoint: staticPoint
				)
			);
		}

		public Correspondence this[int index]
		{
			get { return correspondences[index]; }
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

		public List<Point> GetPointsByType(Fragment.ICPFragmentType type)
		{
			switch (type)
			{
				case Fragment.ICPFragmentType.Model:
					return this.ModelPoints;
				case Fragment.ICPFragmentType.Static:
					return this.StaticPoints;
				default:
					throw new System.ArgumentException("Invalid enum type.");
			}
		}

		public bool IsEmpty()
		{
			return Count <= 0;
		}

		public override string ToString()
		{
			return string.Format("[CorrespondenceCollection: \n{0}", this.correspondences.ElementsToString());
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