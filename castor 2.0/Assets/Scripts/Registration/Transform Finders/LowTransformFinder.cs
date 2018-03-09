using System;
using UnityEngine;

namespace Registration
{
    public class LowTransformFinder : ITransformFinder
    {
        private static int pointDimension = 3;
        private static int numUnknowns = 6;

        public Matrix4x4 FindTransform(CorrespondenceCollection correspondences)
        {
            // Correspondences.Count x 6 matrix
            double[,] A = BuildA(correspondences);

            // Correspondences.Count x 1 matrix
            double[] b = BuildB(correspondences);

            return ComputeTransform(
                A: A, numRowsA: correspondences.Count, b: b);
        }

        private double[,] BuildA(CorrespondenceCollection correspondences)
        {
            throw new System.NotImplementedException();
        }

        private double[] BuildB(CorrespondenceCollection correspondences)
        {
            throw new System.NotImplementedException();
        }

        private Matrix4x4 ComputeTransform(double[,] A, int numRowsA, double[] b)
        {
            // numUnknowns x 1 matrix
            double[] singularValues = new double[numUnknowns];

            // Correspondences.Count x Correspondences.Count matrix
            double[,] U = new double[numRowsA, numRowsA];

            // numUnknowns x numUnknowns matrix
            double[,] Vt = new double[numUnknowns, numUnknowns];

            bool succes = alglib.rmatrixsvd(
                A, numRowsA, pointDimension,
                uneeded: 2, vtneeded: 2, additionalmemory: 2,
                w: out singularValues, vt: out Vt, u: out U
            );

            double[] xOpt = new double[numUnknowns];

            throw new NotImplementedException();

            //TODO Build sigma
            //TODO Compute Pseudo Inverse of Sigma
            //TODO Compute Pseudo Inverse of A
            //TODO Compute xOpt

            return xOptToTransformationMatrix(xOpt);
        }

        private Matrix4x4 xOptToTransformationMatrix(double[] xOpt)
        {
            throw new NotImplementedException();
        }
    }
}