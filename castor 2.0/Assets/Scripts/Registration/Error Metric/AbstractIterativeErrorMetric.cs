using System.Collections.Generic;
using UnityEngine;
namespace Registration.Error
{
    public abstract class AbstractIterativeErrorMetric : ErrorMetric
    {
        protected AbstractIterativeErrorMetric(Configuration configuration)
            : base(configuration)
        { }

        public abstract override float ComputeError(CorrespondenceCollection correspondences, Transform originalTransform, Transform newTransform);

        /// <summary>
        /// Computes the rotational gradient of the error function.
        /// </summary>
        /// <returns>The rotational gradient.</returns>
        /// <param name="XCs">The model points premultiplied with the rotation matrix.</param>
        /// <param name="Ps">The static points in the same order as the model points.</param>
        /// <param name="translation">The translation vector</param>/// 
        public abstract Vector4 RotationalGradient(List<Vector4> XCs, List<Vector4> Ps, Vector4 translation);

        /// <summary>
        /// Computes the rotation gradient of the error function.
        /// </summary>
        /// <returns>The translational gradient.</returns>
        /// <param name="XCs">The model points premultiplied with the rotation matrix.</param>
        /// <param name="Ps">The static points in the same order as the model points.</param>
        /// <param name="translation">The translation vector</param>/// 
        public abstract Vector4 TranslationalGradient(List<Vector4> XCs, List<Vector4> Ps, Vector4 translation);
    }
}