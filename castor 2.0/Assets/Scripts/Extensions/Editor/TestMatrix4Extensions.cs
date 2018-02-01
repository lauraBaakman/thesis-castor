using UnityEngine;
using System.Collections;
using NUnit.Framework;

[TestFixture]
public class Matrix4ExtensionTests
{
    [Test]
    public void TestExtractTranslation_OnlyTranslationSet()
    {
        Vector3 translation = new Vector3(Random.value, Random.value, Random.value);

        Matrix4x4 matrix = new Matrix4x4();
        matrix.SetTRS(
            pos: translation,
            q: new Quaternion(),
            s: new Vector3(1.0f, 1.0f, 1.0f)
        );

        Vector3 expected = translation;
        Vector3 actual = matrix.ExtractTranslation();

        Assert.AreEqual(
            actual: actual,
            expected: expected
        );
    }

    [Test]
    public void TestExtractTranslation_TRSSet()
    {
        Vector3 translation = new Vector3(Random.value, Random.value, Random.value);
        Quaternion rotation = new Quaternion(Random.value, Random.value, Random.value, Random.value);
        Vector3 scale = new Vector3(Random.value, Random.value, Random.value);

        Matrix4x4 matrix = new Matrix4x4();
        matrix.SetTRS(
            pos: translation,
            q: rotation,
            s: scale
        );

        Vector3 expected = translation;
        Vector3 actual = matrix.ExtractTranslation();

        Assert.AreEqual(
            actual: actual,
            expected: expected
        );
    }

    [Test]
    public void TestExtractRotation_OnlyRotationSet()
    {
        Vector3 translation = new Vector3(Random.value, Random.value, Random.value);
        Quaternion rotation = new Quaternion(Random.value, Random.value, Random.value, Random.value);
        Vector3 scale = new Vector3(Random.value, Random.value, Random.value);

        Matrix4x4 matrix = new Matrix4x4();
        matrix.SetTRS(
            pos: new Vector3(),
            q: rotation,
            s: new Vector3(1.0f, 1.0f, 1.0f)
        );

        Quaternion expected = rotation;
        Quaternion actual = matrix.ExtratRotation();

        Assert.AreEqual(
            actual: actual,
            expected: expected
        );
    }

    [Test]
    public void TestExtractRotation_TRSSet()
    {
        Vector3 translation = new Vector3(Random.value, Random.value, Random.value);
        Quaternion rotation = new Quaternion(Random.value, Random.value, Random.value, Random.value);
        Vector3 scale = new Vector3(Random.value, Random.value, Random.value);

        Matrix4x4 matrix = new Matrix4x4();
        matrix.SetTRS(
            pos: translation,
            q: rotation,
            s: scale
        );

        Quaternion expected = rotation;
        Quaternion actual = matrix.ExtratRotation();

        Assert.AreEqual(
            actual: actual,
            expected: expected
        );
    }
}