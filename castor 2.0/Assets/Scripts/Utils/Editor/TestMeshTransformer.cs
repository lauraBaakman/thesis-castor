using System.IO;
using NUnit.Framework;
using UnityEngine;
using Utils;
using RTEditor;

namespace Tests.Utils
{
    [TestFixture]
    public class MeshTransformerTests
    {
        Mesh mesh;

        Quaternion rotation;
        Vector3 translation, scale;

        private Mesh ReadMesh(string fileName)
        {
            string path = BuildPath(fileName);
            return ObjFileReader.ImportFile(path);
        }

        private string BuildPath(string fileName)
        {
            string objFileDir = Path.Combine(Application.dataPath, "Scripts/Utils/Editor/objFiles");
            return Path.Combine(objFileDir, fileName + ".obj");
        }

        [SetUp]
        public void SetUp()
        {
            mesh = ReadMesh("NoTransform");

            rotation = new Quaternion();
            rotation.eulerAngles = new Vector3(+45, -45, +90);

            translation = new Vector3(+45, -45, +90);
            scale = new Vector3(1, 1, 1);
        }

        [Test]
        public void Test_Transform_NoTransform()
        {
            Matrix4x4 transform = Matrix4x4.identity;
            MeshTransformer transformer = new MeshTransformer(transform);

            Mesh actual = transformer.Transform(mesh);

            Mesh expected = ReadMesh("NoTransform");

            Assert.IsTrue(actual.MeshEquals(expected));
        }

        [Test]
        public void Test_Transform_OnlyTranslation()
        {
            Matrix4x4 transform = new Matrix4x4().SetTranslation(translation);
            MeshTransformer transformer = new MeshTransformer(transform);

            Mesh actual = transformer.Transform(mesh);

            Mesh expected = ReadMesh("Translation");

            Assert.IsTrue(actual.MeshEquals(expected));
        }

        [Test]
        public void Test_Transform_OnlyRotation()
        {
            Matrix4x4 transform = new Matrix4x4().SetRotation(rotation);

            MeshTransformer transformer = new MeshTransformer(transform);

            Mesh actual = transformer.Transform(mesh);

            Mesh expected = ReadMesh("Rotation");

            Assert.IsTrue(actual.MeshEquals(expected));
        }

        [Test]
        public void Test_Transform_TranslationAndRotation()
        {
            Matrix4x4 transform = new Matrix4x4();
            transform.SetTRS(pos: translation,
                             q: rotation,
                             s: scale);

            MeshTransformer transformer = new MeshTransformer(transform);

            Mesh actual = transformer.Transform(mesh);

            Mesh expected = ReadMesh("TranslationRotation");

            Assert.IsTrue(actual.MeshEquals(expected));
        }
    }
}