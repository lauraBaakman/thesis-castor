using UnityEngine;

public static class SystemRandomExtensions
{
	static System.Random randomGenerator = new System.Random();

	/// <summary>
	/// Generate a random float in the range [min, max).
	/// </summary>
	/// <param name="random">Random.</param>
	/// <param name="min">The minimum generated value, inclusive.</param>
	/// <param name="max">The max generated value, exclusive.</param>
	public static float NextInRange(this System.Random random, float min, float max)
	{
		if (min > max) throw new System.ArgumentException("Max should be greater than min.");

		float value = (float)randomGenerator.NextDouble();

		return min + (value * (max - min));
	}
}
