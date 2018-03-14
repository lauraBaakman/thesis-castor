using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace IO
{
    public class FragmentsExporter
    {
        public delegate void CallBack(string path, GameObject fragment);

        private readonly GameObject FragmentsRoot;

        private readonly CallBack onSucces;
        private readonly CallBack onFailure;

        private static string extension = "obj";

        public FragmentsExporter(GameObject fragmentsRoot, CallBack onSucces, CallBack onFailure)
        {
            FragmentsRoot = fragmentsRoot;
            this.onSucces = onSucces;
            this.onFailure = onFailure;
        }

        public void Export()
        {
            GetOutputDirectory();
        }

        private void GetOutputDirectory()
        {
            SimpleFileBrowser.FileBrowser.ShowSaveDialog(
                onSuccess: ExportFragments,
                onCancel: () => { },
                folderMode: true,
                initialPath: Application.isEditor ? "/Users/laura/Repositories/thesis-castor/castor 2.0/Assets/Models" : null,
                title: "Export",
                saveButtonText: "Export"
            );
        }

        private void ExportFragments(string directory)
        {
            FragmentExporter exporter = new FragmentExporter(onSucces, onFailure);

            List<GameObject> exportFragments = GetExportFragments();

            foreach (GameObject fragment in exportFragments)
            {
                exporter.Export(fragment, BuildExportPath(directory, fragment.name));
            }
        }

        private string BuildExportPath(string directory, string fragment_name)
        {
            return Path.Combine(directory, fragment_name + '.' + extension);
        }

        private List<GameObject> GetExportFragments()
        {
            List<GameObject> exportFragments;

            exportFragments = GetSelectedFragments();

            if (exportFragments.IsEmpty()) exportFragments = GetAllFragments(exportFragments);

            return exportFragments;
        }

        private List<GameObject> GetSelectedFragments()
        {
            return new List<GameObject>(RTEditor.EditorObjectSelection.Instance.SelectedGameObjects);
        }

        private List<GameObject> GetAllFragments(List<GameObject> fragments)
        {
            foreach (Transform child in FragmentsRoot.transform)
                //Add only fragments, not the other children of 'Fragments'
                if (child.GetComponent<MeshFilter>() != null) fragments.Add(child.gameObject);

            return fragments;
        }
    }

    internal class FragmentExporter
    {
        private FragmentsExporter.CallBack onSucces;
        private FragmentsExporter.CallBack onFailure;

        public FragmentExporter(FragmentsExporter.CallBack onSucces, FragmentsExporter.CallBack onFailure)
        {
            this.onSucces = onSucces;
            this.onFailure = onFailure;
        }

        public void Export(GameObject fragment, string path)
        {
            Debug.Log("Exporting " + fragment.name + " to " + path);

            ValidateFragment(fragment);
            bool succeeded = Export(
                mesh: fragment.GetComponent<MeshFilter>().mesh,
                transformation: fragment.transform.localToWorldMatrix,
                path: path
            );

            FragmentsExporter.CallBack callback = succeeded ? onSucces : onFailure;
            callback(path, fragment);
        }

        private bool Export(Mesh mesh, Matrix4x4 transformation, string path)
        {
            Utils.MeshTransformer meshTransformer = new Utils.MeshTransformer(transformation);
            Mesh transformedMesh = meshTransformer.Transform(mesh);

            return mesh.ToFile(path);
        }

        private void ValidateFragment(GameObject fragment)
        {
            if (fragment.GetComponent<MeshFilter>() == null)
                Debug.LogError("Trying to write a gameobject without MeshFilter to file as a mesh.");
        }
    }
}