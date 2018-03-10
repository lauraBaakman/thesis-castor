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

        /* Public, for now for testing */
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

        private Matrix4x4 ComputeTransform(double[,] A, double[] b)
        {
            // numUnknowns x 1 matrix
            double[] singularValues = new double[numUnknowns];

            // Correspondences.Count x Correspondences.Count matrix
            double[,] U = new double[A.GetLength(0), A.GetLength(0)];

            // numUnknowns x numUnknowns matrix
            double[,] Vt = new double[numUnknowns, numUnknowns];

            bool succes = alglib.rmatrixsvd(
                A, A.GetLength(0), A.GetLength(1),
                uneeded: 2, vtneeded: 2, additionalmemory: 2,
                w: out singularValues, vt: out Vt, u: out U
            );

            double[] xOpt = new double[numUnknowns];

            throw new NotImplementedException();

            //TODO Build sigma
            //TODO Compute Pseudo Inverse of Sigma
            //TODO Compute Pseudo Inverse of A
            //TODO Compute xOpt

            //return xOptToTransformationMatrix(xOpt);
        }

        private Matrix4x4 xOptToTransformationMatrix(double[] xOpt)
        {
            throw new NotImplementedException();
        }
    }
}