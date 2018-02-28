using System.Collections;
using System.Collections.Generic;
using Registration;
using UnityEngine;

/// <summary>
/// Normalize points to ensure that they all fall within the unit sphere. This
/// ensures that the magnitude of the angles and the distances are comparable.
/// </summary>
public class PointNormalizer
{
    public Matrix4x4 ComputeNormalizationMatrix(IEnumerable<Point> points)
    {
        throw new System.NotImplementedException();
    }

    public IEnumerable<Point> Normalize(IEnumerable<Point> points)
    {
        throw new System.NotImplementedException();
    }
}
