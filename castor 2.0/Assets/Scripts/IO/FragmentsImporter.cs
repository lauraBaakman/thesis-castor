using System;
using SFB;
using UnityEngine;

namespace IO
{
    public class FragmentImporter
    {
        //TODO Fix Colors
        //TODO Add to fragment Singleton

        public GameObject FragmentsRoot;

        public void Import()
        {
            GetFragmentFiles();
            //Mesh mesh = ReadMeshFromFile(file);
            //GameObject fragment = CreateGameObject(mesh);
            //AddGameObjectToScene(fragment);
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
            Debug.Log("Hi!");
        }

        private Mesh ReadMeshFromFile(string file)
        {
            throw new NotImplementedException();
        }

        private GameObject CreateGameObject(Mesh mesh)
        {
            throw new NotImplementedException();
        }

        private void AddGameObjectToScene(GameObject fragment)
        {
            throw new NotImplementedException();
        }
    }

}
