using UnityEngine;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class OpenTKMatrix4dTests
    {

        [Test]
        public void Test_ToUnityMatrix()
        {
            OpenTK.Matrix4d openTKmatrix = new OpenTK.Matrix4d(
                row0: new OpenTK.Vector4d(1.0, 2.0, 3.0, 4.0),
                row1: new OpenTK.Vector4d(5.0, 6.0, 7.0, 8.0),
                row2: new OpenTK.Vector4d(9.0, 1.1, 1.2, 1.3),
                row3: new OpenTK.Vector4d(1.4, 1.5, 1.6, 1.7)
            );

            Matrix4x4 actual = openTKmatrix.ToUnityMatrix();

            Matrix4x4 expected = new Matrix4x4(
                column0: new Vector4(1.0f, 5.0f, 9.0f, 1.4f),
                column1: new Vector4(2.0f, 6.0f, 1.1f, 1.5f),
                column2: new Vector4(3.0f, 7.0f, 1.2f, 1.6f),
                column3: new Vector4(4.0f, 8.0f, 1.3f, 1.7f)
            );
            Assert.AreEqual(
                actual: actual,
                expected: expected
            );
        }
    }
}