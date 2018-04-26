using System.IO;
using UnityEngine;
using Utils;

namespace IO
{
    public class FragmentsImporter
    {
        public delegate void CallBack(IO.ReadResult result);

        public GameObject FragmentsRoot;

        private readonly CallBack callBack;

        public FragmentsImporter(GameObject fragmentParent, CallBack callBack)
        {
            FragmentsRoot = fragmentParent;
            this.callBack = callBack;
        }

        public void Import()
        {
            GetFragmentFiles();
        }

        public void Import(string file, bool randomizeTransform = false)
        {
            ProcessFragmentFile(file, randomizeTransform);
        }

        private void GetFragmentFiles()
        {
            SimpleFileBrowser.FileBrowser.ShowLoadDialog(
                onSuccess: ProcessFragmentFile,
                onCancel: () => { },
                initialPath: Application.isEditor ? "/Users/laura/Repositories/thesis-castor/castor 2.0/Assets/Models" : null
            );
        }

        private void ProcessFragmentFile(string path)
        {
            ProcessFragmentFile(path, false);
        }

        private void ProcessFragmentFile(string path, bool randomizeTransform)
        {
            FragmentImporter fragmentImporter = new FragmentImporter(FragmentsRoot, callBack, randomizeTransform);
            fragmentImporter.Import(path);
        }
    }

    public class FragmentImporter
    {
        private static string PrefabPath = "Fragment";
        private readonly GameObject Parent;

        private bool RandomizeTransform;

        private FragmentsImporter.CallBack CallBack;

        internal FragmentImporter(GameObject parent, FragmentsImporter.CallBack callBack, bool randomizeTransform)
        {
            Parent = parent;
            CallBack = callBack;

            RandomizeTransform = randomizeTransform;
        }

        public FragmentImporter(GameObject parent, FragmentsImporter.CallBack callBack)
            : this(parent, callBack, false)
        { }

        public GameObject Import(string path, bool initUI = true)
        {
            ReadResult result = IO.ObjFile.Read(path);
            GameObject fragment = null;

            if (result.Succeeded())
            {
                string name = ExtractObjectName(path);
                fragment = AddFragmentToScene(name, result.Mesh);
            }
            CallBack(result);
            return fragment;
        }

        private string ExtractObjectName(string path)
        {
            return Path.GetFileNameWithoutExtension(path);
        }

        private GameObject AddFragmentToScene(string name, Mesh mesh, bool initUI = true)
        {
            GameObject fragment = UnityEngine.Object.Instantiate(
                original: Resources.Load(PrefabPath),
                parent: Parent.transform
            ) as GameObject;

            fragment.name = name;

            SetDoubleConnectedEdgeList(fragment, mesh);
            SetMesh(fragment, mesh);
            SetMaterial(fragment);


            if (RandomizeTransform) RandomizeTheTransform(fragment);

            return fragment;
        }

        private void SetDoubleConnectedEdgeList(GameObject fragment, Mesh mesh)
        {
            Fragment.DoubleConnectedEdgeListStorage DCELStorage = fragment.GetComponent<Fragment.DoubleConnectedEdgeListStorage>();
            DCELStorage.DCEL = DoubleConnectedEdgeList.DCEL.FromMesh(mesh);
        }

        private void SetMesh(GameObject fragment, Mesh mesh)
        {
            MeshFilter filter = fragment.GetComponent<MeshFilter>();
            filter.mesh = mesh;

            MeshCollider collider = fragment.GetComponent<MeshCollider>();
            collider.sharedMesh = mesh;
        }

        private void RandomizeTheTransform(GameObject fragment)
        {
            fragment.AddComponent<Fragment.RandomTransformer>();
        }

        private void SetMaterial(GameObject fragment)
        {
            MeshRenderer renderer = fragment.GetComponent<MeshRenderer>();
            Material material = renderer.material;
            material.color = ColorGenerator.Instance.GetNextColor();
            renderer.material = material;
        }
    }

}
