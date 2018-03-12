using Utils;
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

        private double[,] BuildA(CorrespondenceCollection correspondences)
        {
            double[,] A = new double[correspondences.Count, numUnknowns];

            Vector3 crossProduct;
            Correspondence correspondence;

            for (int row = 0; row < correspondences.Count; row++)
            {
                correspondence = correspondences[row];
                crossProduct = Vector3.Cross(
                    correspondence.StaticPoint.Position,
                    correspondence.ModelPoint.UnitNormal
                );

                A[row, 0] = crossProduct[0];
                A[row, 1] = crossProduct[1];
                A[row, 2] = crossProduct[2];

                A[row, 3] = correspondence.ModelPoint.UnitNormal[0];
                A[row, 4] = correspondence.ModelPoint.UnitNormal[1];
                A[row, 5] = correspondence.ModelPoint.UnitNormal[2];
            }
            return A;
        }

        private double[] BuildB(CorrespondenceCollection correspondences)
        {
            double[] b = new double[correspondences.Count];

            Correspondence correspondence;

            for (int row = 0; row < correspondences.Count; row++)
            {
                correspondence = correspondences[row];
                b[row] = Vector3.Dot(correspondence.ModelPoint.UnitNormal, correspondence.ModelPoint.Position)
                                - Vector3.Dot(correspondence.ModelPoint.UnitNormal, correspondence.StaticPoint.Position);
            }
            return b;
        }

        private Matrix4x4 ComputeTransform(double[,] A, double[] b)
        {
            double[,] Aplus = ArrayMatrixUtils.MoorePenroseInverse(A);
            double[] xOpt = ArrayMatrixUtils.Multiply(Aplus, b);

            return TransformationMatrixFromXOpt(xOpt);
        }

        /// <summary>
        /// Generate a transformation matrix based on the vector x_Opt =
        /// [alfa, beta, gamma, t_x, t_y, t_z], where alfa, beta and gamma are 
        /// the rotations in radians around the x, y, and z-axis, respectively.
        /// And t_x, t_y, t_z, denote the translation in the x, y and z direction.
        /// </summary>
        /// <returns>The opt to transformation matrix.</returns>
        /// <param name="xOpt">X opt.</param>
        private Matrix4x4 TransformationMatrixFromXOpt(double[] xOpt)
        {
            Matrix4x4 translation = TranslationMatrixFromXOpt(xOpt);
            Matrix4x4 rotation = RotationMatrixFromXOpt(xOpt);
            return translation * rotation;
        }

        private Matrix4x4 TranslationMatrixFromXOpt(double[] xOpt)
        {
            Matrix4x4 translation = Matrix4x4.identity;
            translation[0, 3] = (float)xOpt[3];
            translation[1, 3] = (float)xOpt[4];
            translation[2, 3] = (float)xOpt[5];
            translation[3, 3] = 1.0f;
            return translation;
        }

        private Matrix4x4 RotationMatrixFromXOpt(double[] xOpt)
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