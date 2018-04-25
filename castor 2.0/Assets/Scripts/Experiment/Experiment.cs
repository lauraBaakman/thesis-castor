using UnityEngine;
using System.IO;
using System;

namespace Experiment
{
    public class Experiment
    {
        private readonly Configuration configuration;

        GameObject staticFragment;

        IO.FragmentImporter fragmentImporter;

        private string outputDirectory;

        public Experiment(Configuration configuration, GameObject fragments)
        {
            this.configuration = configuration;
            this.fragmentImporter = new IO.FragmentImporter(fragments);
        }

        public void SetUp()
        {
            CreateResultsDirectory();

            HandleStaticFragment();

            //Find the list of model fragments
        }

        public void Run()
        {

        }

        private void CreateResultsDirectory()
        {
            string basePath = configuration.outputDirectory;
            string directoryName = CreateOutputDirectoryName();

            this.outputDirectory = Path.Combine(basePath, directoryName);

            Directory.CreateDirectory(outputDirectory);
        }

        private string CreateOutputDirectoryName()
        {
            DateTime now = DateTime.Now.ToLocalTime();
            return now.ToString("MM-dd_HH-mm-ss");
        }

        private void HandleStaticFragment()
        {
            this.staticFragment = Import(configuration.lockedFragmentFile);

            Lock(staticFragment);

            string outputPath = "";
            Write(staticFragment, outputPath);
        }

        private GameObject Import(string path)
        {
            return fragmentImporter.Import(path);
        }

        private void Lock(GameObject fragment)
        {
            Debug.Log("Lock the fragment");
        }

        private void Write(GameObject fragment, string path)
        {
            Debug.Log("Write the fragment");
        }
    }
}