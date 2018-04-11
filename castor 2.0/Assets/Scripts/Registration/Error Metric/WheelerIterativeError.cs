using UnityEngine;
using System.Collections.Generic;
using Utils;

namespace Registration.Error
{
    /// <summary>
    /// Implements the error proposed by Wheeler, M. D., and K. Ikeuchi. 
    /// "Iterative estimation of rotation and translation using the 
    /// quaternion: School of Computer Science." (1995).
    /// </summary>
    public class WheelerIterativeError : IIterativeErrorMetric
    {
        public float ComputeError(List<Vector4D> XCs, List<Vector4D> Ps, Vector4D translation)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Computes the rotational gradient.
        /// </summary>
        /// <returns>The gradient.</returns>
        /// <param name="XCs">The model points, premultiplied with the rotation matrix.</param>
        /// <param name="Ps">The static points.</param>
        /// <param name="translation">The current translation vector.</param>
        public Vector4D RotationalGradient(List<Vector4D> XCs, List<Vector4D> Ps, Vector4D translation)
        {
            int N = XCs.Count;
            Vector4D gradient = new Vector4D();
            for (int i = 0; i < N; i++)
            {
                gradient += RotationalGradient(XCs[i], Ps[i], translation);
            }
            gradient *= (1.0 / (2 * N));

            //Set the w value of the gradient to 1.
            gradient.w = 1;

            return gradient;
        }

        private Vector4D RotationalGradient(Vector4D Xc, Vector4D p, Vector4D translation)
        {
            Vector4D gradient = -4 * Vector4D.Cross(Xc, translation - p);
            return gradient;
        }

        /// <summary>
        /// Computes the translational gradient.
        /// </summary>
        /// <returns>The gradient.</returns>
        /// <param name="XCs">The model points, premultiplied with the rotation matrix.</param>
        /// <param name="Ps">The static points.</param>
        /// <param name="translation">The current translation vector.</param>
        public Vector4D TranslationalGradient(List<Vector4D> XCs, List<Vector4D> Ps, Vector4D translation)
        {
            int N = XCs.Count;
            Vector4D gradient = new Vector4D();
            for (int i = 0; i < N; i++)
            {
                gradient += TranslationalGradient(XCs[i], Ps[i], translation);
            }
            gradient *= (1.0 / (2 * N));
            return gradient;
        }

        private Vector4D TranslationalGradient(Vector4D Xc, Vector4D p, Vector4D translation)
        {
            return 2 * (Xc + translation - p);
        }
    }
}