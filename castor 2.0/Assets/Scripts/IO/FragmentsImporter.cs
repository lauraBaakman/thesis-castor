using System;
using System.IO;
using UnityEngine;

namespace IO
{
    public class FragmentsImporter
    {
        public GameObject FragmentsRoot;

        public FragmentsImporter(GameObject fragments)
        {
            FragmentsRoot = fragments;
        }

        public void Import()
        {
            GetFragmentFiles();
        }

        private void GetFragmentFiles()
        {
            SimpleFileBrowser.FileBrowser.SetDefaultFilter(".obj");
            SimpleFileBrowser.FileBrowser.ShowLoadDialog(
                onSuccess: ProcessFragmentFile,
                onCancel: () => { },
                initialPath: Application.isEditor ? "/Users/laura/Repositories/thesis-castor/castor 2.0/Assets/Models" : null
            );
        }

        private void ProcessFragmentFile(string path)
        {
            FragmentImporter fragmentImporter = new FragmentImporter(FragmentsRoot);
            fragmentImporter.Import(path);
        }
    }

    internal class FragmentImporter
    {
        private static string PrefabPath = "Fragment";
        private readonly GameObject Parent;

        internal FragmentImporter(GameObject parent)
        {
            Parent = parent;
        }

        internal void Import(string path)
        {
            Mesh mesh = ObjFileReader.ImportFile(path);
            string name = ExtractObjectName(path);
            AddFragmentToScene(name, mesh);
        }

        private string ExtractObjectName(string path)
        {
            return Path.GetFileNameWithoutExtension(path);
        }

        private void AddFragmentToScene(string name, Mesh mesh)
        {
            GameObject fragment = UnityEngine.Object.Instantiate(
                original: Resources.Load(PrefabPath),
                parent: Parent.transform
            ) as GameObject;

            fragment.name = name;

            MeshFilter filter = fragment.GetComponent<MeshFilter>();
            filter.mesh = mesh;
        }
    }

}
