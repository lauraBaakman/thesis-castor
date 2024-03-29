﻿// Pogramming by
//     Douglas Andrade ( http://www.cmsoft.com.br, email: cmsoft@cmsoft.com.br)
//               Implementation of most of the functionality
//     Edgar Maass: (email: maass@logisel.de)
//               Code adaption, changed to user control
//
//Software used: 
//    OpenGL : http://www.opengl.org
//    OpenTK : http://www.opentk.com
//
// DISCLAIMER: Users rely upon this software at their own risk, and assume the responsibility for the results. Should this software or program prove defective, 
// users assume the cost of all losses, including, but not limited to, any necessary servicing, repair or correction. In no event shall the developers or any person 
// be liable for any loss, expense or damage, of any type or nature arising out of the use of, or inability to use this software or program, including, but not
// limited to, claims, suits or causes of action involving alleged infringement of copyrights, patents, trademarks, trade secrets, or unfair competition. 
//
using OpenTK;

namespace OpenTKLib
{
    //Extensios attached to the object which folloes the "this" 
    public static class Matrix3DExtension
    {
        
        /// <summary>Transform a direction vector by the given Matrix
        /// Assumes the matrix has a bottom row of (0,0,0,1), that is the translation part is ignored.
        /// </summary>
        /// <param name="vec">The vector to transform</param>
        /// <param name="mat">The desired transformation</param>
        /// <returns>The transformed vector</returns>
        public static Vector3d TransformVector(this Matrix3d mat, Vector3d vec)
        {
            Vector3d vNew = new Vector3d(mat.Row2);
            double val = Vector3d.Dot(vNew, vec);

            return new Vector3d(
                Vector3d.Dot(new Vector3d(mat.Row0), vec),
                Vector3d.Dot(new Vector3d(mat.Row1), vec),
                Vector3d.Dot(new Vector3d(mat.Row2), vec));
        }
        public static  Matrix3d Rotation30Degrees(this Matrix3d mat)
        {
            Matrix3d result = Matrix3d.Identity;
            //rotation 30 degrees
            result[0, 0] = 1F;
            result[1, 1] = result[2, 2] = 0.86603;
            result[1, 2] = -0.5;
            result[2, 1] = 0.5;
           
            return result;
        }
        /// <summary>
        /// rotates around x,y and z axis by the given amount in degrees
        /// </summary>
        /// <param name="mat"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public static Matrix3d RotationXYZ(this Matrix3d mat, double x, double y, double z)
        {
            Matrix3d Rx = Matrix3d.CreateRotationX(x);
            Matrix3d Ry = Matrix3d.CreateRotationY(y);
            Matrix3d Rz = Matrix3d.CreateRotationZ(z);
            Rx = Matrix3d.Mult(Rx, Ry);
            Rx = Matrix3d.Mult(Rx, Rz);

            return Rx;
        }
        public static Matrix3d Rotation60Degrees(this Matrix3d mat)
        {
            Matrix3d Rx = Matrix3d.CreateRotationX(60);
            Matrix3d Ry = Matrix3d.CreateRotationY(60);
            Matrix3d Rz = Matrix3d.CreateRotationZ(60);
            Rx = Matrix3d.Mult(Rx, Ry);
            Rx = Matrix3d.Mult(Rx, Rz);
            
            return Rx;

        }
        public static Matrix3d RotateSome(this Matrix3d mat)
        {
            Matrix3d Rx = Matrix3d.CreateRotationX(90);
            Matrix3d Ry = Matrix3d.CreateRotationY(124);
            Matrix3d Rz = Matrix3d.CreateRotationZ(-274);
            Rx = Matrix3d.Mult(Rx, Ry);
            Rx = Matrix3d.Mult(Rx, Rz);

            return Rx;

            
        }
        
        
    }
}
