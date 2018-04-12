using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Registration.Error
{
    public interface IIterativeErrorMetric
    {
        float ComputeError(List<Vector4D> XCs, List<Vector4D> Ps, Vector4D translation);

        /// <summary>
        /// Computes the rotational gradient of the error function.
        /// </summary>
        /// <returns>The rotational gradient.</returns>
        /// <param name="XCs">The model points premultiplied with the rotation matrix.</param>
        /// <param name="Ps">The static points in the same order as the model points.</param>
        /// <param name="translation">The translation vector</param>/// 
        Vector4D RotationalGradient(List<Vector4D> XCs, List<Vector4D> Ps, Vector4D translation);

        /// <summary>
        /// Computes the rotation gradient of the error function.
        /// </summary>
        /// <returns>The translational gradient.</returns>
        /// <param name="XCs">The model points premultiplied with the rotation matrix.</param>
        /// <param name="Ps">The static points in the same order as the model points.</param>
        /// <param name="translation">The translation vector</param>/// 
        Vector4D TranslationalGradient(List<Vector4D> XCs, List<Vector4D> Ps, Vector4D translation);
    }
}