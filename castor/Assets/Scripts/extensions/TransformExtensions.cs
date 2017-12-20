using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtensions
{

    /// <summary>
    /// Reset the local transform, i.e. localscale to one, local position to zero,
    /// and local rotation to the identity matrix.
    /// </summary>
    public static void ResetLocally(this Transform transform)
    {
        transform.localScale = Vector3.one;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }
}
