using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Registration.Error
{
    public interface IErrorMetric
    {
        float ComputeError(CorrespondenceCollection correspondences, Transform originalTransform, Transform newTransform);

        void Set(GameObject staticModel, Transform referenceTransform);
    }

    public interface IIterativeErrorMetric : IErrorMetric
    {
        double ComputeError(List<Vector4D> XCs, List<Vector4D> Ps, Vector4D translation, object sharedParameters);

        /// <summary>
        /// Computes the rotational gradient of the error function.
        /// </summary>
        /// <returns>The rotational gradient.</returns>
        /// <param name="XCs">The model points premultiplied with the rotation matrix.</param>
        /// <param name="Ps">The static points in the same order as the model points.</param>
        /// <param name="translation">The translation vector</param>/// 
        QuaternionD RotationalGradient(List<Vector4D> XCs, List<Vector4D> Ps, Vector4D translation, object sharedParameters);

        /// <summary>
        /// Computes the rotation gradient of the error function.
        /// </summary>
        /// <returns>The translational gradient.</returns>
        /// <param name="XCs">The model points premultiplied with the rotation matrix.</param>
        /// <param name="Ps">The static points in the same order as the model points.</param>
        /// <param name="translation">The translation vector</param>/// 
        Vector4D TranslationalGradient(List<Vector4D> XCs, List<Vector4D> Ps, Vector4D translation, object sharedParameters);

        /// <summary>
        /// Computes the paramters that are shared between the error function and the gradients.
        /// </summary>
        /// <returns>The shared paramters.</returns>
        /// <param name="modelPoints">Model points.</param>
        /// <param name="staticPOints">Static Points.</param>
        /// <param name="translation">Translation.</param>
        object ComputeSharedParameters(List<Vector4D> rotatedModelPoints, List<Vector4D> staticPoints, Vector4D translation);
    }
}