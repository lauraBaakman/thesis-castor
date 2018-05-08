using UnityEngine;
using System;

public class RangeF : IEquatable<RangeF>
{
	public float Min { get; set; }
	public float Max { get; set; }

	public float Length
	{
		get
		{
			ValidateRange();
			return (this.Max - this.Min);
		}
	}

	public float Center
	{
		get
		{
			ValidateRange();
			return this.Min + (this.Length / 2.0f);
		}
	}

	public RangeF(float min = float.MaxValue, float max = float.MinValue)
	{
		Min = min;
		Max = max;
	}

	/// <summary>
	/// Updates the minimum, if the passed minimum is smaller than the current minimum.
	/// </summary>
	/// <param name="newMin">New minimum.</param>
	public void UpdateMin(float newMin)
	{
		this.Min = Math.Min(this.Min, newMin);
	}

	/// <summary>
	/// Updates the maximum, if the passed maximum is smaller than the current maximum.
	/// </summary>
	/// <param name="newMin">New maximum.</param>
	public void UpdateMax(float newMax)
	{
		this.Max = Math.Max(this.Max, newMax);
	}

	public bool Equals(RangeF other)
	{
		return (
			this.Min.Equals(other.Min) &&
			this.Max.Equals(other.Max)
		);
	}

	public override string ToString()
	{
		return string.Format("[Range: min={0}, max={1}]", Min, Max);
	}

	public override bool Equals(object obj)
	{
		if (obj == null || GetType() != obj.GetType())
			return false;
		return this.Equals(obj as RangeF);
	}

	public override int GetHashCode()
	{
		int hash = 17;
		hash *= (31 + Min.GetHashCode());
		hash *= (31 + Max.GetHashCode());
		return hash;
	}

	private void ValidateRange()
	{
		if (Max < Min) throw new ArgumentException("The maximum is smaller than the minimum");
	}
}
