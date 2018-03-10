using System;
using UnityEngine;

namespace Registration
{
    public class LowTransformFinder : ITransformFinder
    {
        private static int numUnknowns = 6;

        public Matrix4x4 FindTransform(CorrespondenceCollection correspondences)
        {
            // Correspondences.Count x 6 matrix
            double[,] A = BuildA(correspondences);

            // Correspondences.Count x 1 matrix
            double[] b = BuildB(correspondences);

            return ComputeTransform(A, b);
        }

        /* Public, for testing, should be private */
        public double[,] BuildA(CorrespondenceCollection correspondences)
        {
            double[,] A = new double[correspondences.Count, numUnknowns];

            Vector3 crossProduct;
            Correspondence correspondence;

            for (int row = 0; row < correspondences.Count; row++)
            {
                correspondence = correspondences[row];
                crossProduct = Vector3.Cross(
                    correspondence.StaticPoint.Position,
                    correspondence.ModelPoint.Normal
                );

                A[row, 0] = crossProduct[0];
                A[row, 1] = crossProduct[1];
                A[row, 2] = crossProduct[2];

                A[row, 3] = correspondence.ModelPoint.Normal.normalized[0];
                A[row, 4] = correspondence.ModelPoint.Normal.normalized[1];
                A[row, 5] = correspondence.ModelPoint.Normal.normalized[2];
            }
            return A;
        }

        /* Public, for testing, should be private */
        public double[] BuildB(CorrespondenceCollection correspondences)
        {
            double[] b = new double[correspondences.Count];

            Correspondence correspondence;

            for (int row = 0; row < correspondences.Count; row++)
            {
                correspondence = correspondences[row];
                b[row] = Vector3.Dot(correspondence.ModelPoint.Normal, correspondence.ModelPoint.Position)
                               - Vector3.Dot(correspondence.ModelPoint.Normal, correspondence.StaticPoint.Position);
            }
            return b;
        }

        /* Public, for testing, should be private */
        private Matrix4x4 ComputeTransform(double[,] A, double[] b)
        {
            double[,] U, S, Vt;
            SVD(A, out U, out S, out Vt);

            //TODO Compute pseudo inverse of S

            //TODO Compute pseudo inverse of A

            //TODO Compute xOpt
            double[] xOpt = new double[6];

            return TransformationMatrixFromXOpt(xOpt);
        }

        /// <summary>
        /// The algorithm calculates the singular value decomposition of A: A = U * S * V^T
        /// </summary>
        private void SVD(double[,] A,
                         out double[,] U, out double[,] S, out double[,] Vt)
        {
            // numUnknowns x 1 matrix
            double[] singularValues = new double[numUnknowns];

            // Correspondences.Count x Correspondences.Count matrix
            U = new double[A.GetLength(0), A.GetLength(0)];

            // numUnknowns x numUnknowns matrix
            Vt = new double[numUnknowns, numUnknowns];

            bool succes = alglib.rmatrixsvd(
                A, A.GetLength(0), A.GetLength(1),
                uneeded: 2, vtneeded: 2, additionalmemory: 2,
                w: out singularValues, vt: out Vt, u: out U
            );

            S = Utils.ArrayMatrixUtils.ToDiagonalMatrix(singularValues);
        }

        /* Public, for testing, should be private */
        /// <summary>
        /// Generate a transformation matrix based on the vector x_Opt =
        /// [alfa, beta, gamma, t_x, t_y, t_z], where alfa, beta and gamma are 
        /// the rotations in radians around the x, y, and z-axis, respectively.
        /// And t_x, t_y, t_z, denote the translation in the x, y and z direction.
        /// </summary>
        /// <returns>The opt to transformation matrix.</returns>
        /// <param name="xOpt">X opt.</param>
        public Matrix4x4 TransformationMatrixFromXOpt(double[] xOpt)
        {
            Matrix4x4 translation = TranslationMatrixFromXOpt(xOpt);
            Matrix4x4 rotation = RotationMatrixFromXOpt(xOpt);
            return translation * rotation;
        }

        /* Public, for testing, should be private */
        public Matrix4x4 TranslationMatrixFromXOpt(double[] xOpt)
        {
            Matrix4x4 translation = Matrix4x4.identity;
            translation[0, 3] = (float)xOpt[3];
            translation[1, 3] = (float)xOpt[4];
            translation[2, 3] = (float)xOpt[5];
            translation[3, 3] = 1.0f;
            return translation;
        }

        /* Public, for testing, should be private */
        public Matrix4x4 RotationMatrixFromXOpt(double[] xOpt)
        {
            float alfa = (float)xOpt[0];
            float beta = (float)xOpt[1];
            float gamma = (float)xOpt[2];

            Matrix4x4 rotation = Matrix4x4.identity;

            rotation[0, 0] = +Mathf.Cos(gamma) * Mathf.Cos(beta);
            rotation[0, 1] = -Mathf.Sin(gamma) * Mathf.Cos(alfa) + Mathf.Cos(gamma) * Mathf.Sin(beta) * Mathf.Sin(alfa);
            rotation[0, 2] = +Mathf.Sin(gamma) * Mathf.Sin(alfa) + Mathf.Cos(gamma) * Mathf.Sin(beta) * Mathf.Cos(alfa);

            rotation[1, 0] = +Mathf.Sin(gamma) * Mathf.Cos(beta);
            rotation[1, 1] = +Mathf.Cos(gamma) * Mathf.Cos(alfa) + Mathf.Sin(gamma) * Mathf.Sin(beta) * Mathf.Sin(alfa);
            rotation[1, 2] = -Mathf.Cos(gamma) * Mathf.Sin(alfa) + Mathf.Sin(gamma) * Mathf.Sin(beta) * Mathf.Cos(alfa);

            rotation[2, 0] = -Mathf.Sin(beta);
            rotation[2, 1] = Mathf.Cos(beta) * Mathf.Sin(alfa);
            rotation[2, 2] = Mathf.Cos(beta) * Mathf.Cos(alfa);

            return rotation;
        }
    }
}