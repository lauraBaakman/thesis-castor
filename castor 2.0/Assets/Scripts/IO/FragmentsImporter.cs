using System.IO;
using UnityEngine;
using Utils;

namespace IO
{
    public delegate void CallBack(string path, GameObject fragment);

    public class FragmentsImporter
    {
        public GameObject FragmentsRoot;

        private CallBack CallBack;

        public FragmentsImporter(GameObject fragmentParent, CallBack callBack)
        {
            FragmentsRoot = fragmentParent;
            CallBack = callBack;
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
            FragmentImporter fragmentImporter = new FragmentImporter(FragmentsRoot, CallBack);
            fragmentImporter.Import(path);
        }
    }

    internal class FragmentImporter
    {
        private static string PrefabPath = "Fragment";
        private readonly GameObject Parent;

        private CallBack CallBack;

        internal FragmentImporter(GameObject parent, CallBack callBack)
        {
            Parent = parent;
            CallBack = callBack;
        }

        internal void Import(string path)
        {
            Mesh mesh = ObjFileReader.ImportFile(path);
            string name = ExtractObjectName(path);
            GameObject fragment = AddFragmentToScene(name, mesh);
         
            CallBack(path, fragment);
        }

        private string ExtractObjectName(string path)
        {
            return Path.GetFileNameWithoutExtension(path);
        }

        private GameObject AddFragmentToScene(string name, Mesh mesh)
        {
            GameObject fragment = UnityEngine.Object.Instantiate(
                original: Resources.Load(PrefabPath),
                parent: Parent.transform
            ) as GameObject;

            fragment.name = name;

            MeshFilter filter = fragment.GetComponent<MeshFilter>();
            filter.mesh = mesh;

            MeshRenderer renderer = fragment.GetComponent<MeshRenderer>();
            Material material = renderer.material;
            material.color = ColorGenerator.Instance.GetNextColor();
            renderer.material = material;

            return fragment;
        }
    }

}
