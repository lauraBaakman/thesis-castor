using System;
using SFB;
using UnityEngine;

namespace IO
{
    public class FragmentsImporter
    {
        //TODO Fix Colors
        //TODO Add to fragment Singleton

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
            StandaloneFileBrowser.OpenFilePanelAsync(
                title: "Open File",
                directory: "",
                extension: "",
                multiselect: true,
                cb: ProcessFragmentFiles
            );
        }

        private void ProcessFragmentFiles(string[] paths)
        {
            FragmentImporter fragmentImporter = new FragmentImporter(FragmentsRoot);

            foreach (string path in paths)
            {
                fragmentImporter.Import(path);
            }
        }
    }

    internal class FragmentImporter
    {

        private GameObject Parent;

        internal FragmentImporter(GameObject parent)
        {
            Parent = parent;
        }

        internal void Import(string path)
        {
            Debug.Log("Trying to import " + path);
        }
    }   

}
