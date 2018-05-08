using System;

namespace Utils
{
	public class Pair<T, U> : IEquatable<Pair<T, U>>
	{
		public T First { get; set; }
		public U Second { get; set; }

		public Pair() { }

		public Pair(T first, U second)
		{
			this.First = first;
			this.Second = second;
		}

		public override string ToString()
		{
			return string.Format("[Pair: First={0}, Second={1}]", First, Second);
		}

		public override bool Equals(object obj)
		{
			if (obj == null || GetType() != obj.GetType())
				return false;
			return this.Equals(obj as Pair<T, U>);
		}

		public bool Equals(Pair<T, U> other)
		{
			return (
				this.First.Equals(other.First) &&
				this.Second.Equals(other.Second)
			);
		}

		public override int GetHashCode()
		{
			int hash = 17;
			hash *= (31 + First.GetHashCode());
			hash *= (31 + Second.GetHashCode());
			return hash;
		}
	}
}

