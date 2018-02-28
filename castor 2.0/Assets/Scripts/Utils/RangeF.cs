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
            validateRange();
            return (this.Max - this.Min);
        }
    }

    public float Center
    {
        get
        {
            validateRange();
            return this.Min + (this.Length / 2.0f);
        }
    }

    public RangeF(float min, float max)
    {
        Min = min;
        Max = max;
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

    private void validateRange()
    {
        if (Max < Min) throw new ArgumentException("The maximum is smaller than the minimum");
    }
}
