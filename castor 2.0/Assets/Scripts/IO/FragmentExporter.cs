using System;
using UnityEngine;
namespace IO
{
    public delegate void Callback(string path, GameObject fragment);

    public class FragmentsExporter
    {
        private readonly GameObject FragmentsRoot;

        private readonly CallBack CallBack;

        public FragmentsExporter(GameObject fragmentsRoot, CallBack callback)
        {
            FragmentsRoot = fragmentsRoot;
            CallBack = callback;
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
            Debug.Log("Exporting to " + directory);
        }
    }
}