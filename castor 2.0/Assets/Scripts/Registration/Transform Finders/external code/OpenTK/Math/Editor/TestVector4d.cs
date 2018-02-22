using UnityEngine;
using NUnit.Framework;

[TestFixture]
public class OpenTKVector4dTests
{
    [Test]
    public void Test_ToUnityVector()
    {
        OpenTK.Vector4d openTKvector = new OpenTK.Vector4d(1.0, 2.0, 3.0, 4.0);

        Vector4 actual = openTKvector.ToUnityVector();
        Vector4 expected = new Vector4(1.0f, 2.0f, 3.0f, 4.0f);

        Assert.AreEqual(
            actual: actual,
            expected: expected
        );
    }
}