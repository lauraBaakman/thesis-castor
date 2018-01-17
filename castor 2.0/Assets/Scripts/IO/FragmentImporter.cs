using System;
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
            string file = GetFragmentFile();
            Mesh mesh = ReadMeshFromFile(file);
            GameObject fragment = CreateGameObject(mesh);
            AddGameObjectToScene(fragment);
        }

        private string GetFragmentFile()
        {
            throw new NotImplementedException();
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
