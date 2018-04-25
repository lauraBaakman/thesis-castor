using UnityEngine;
using System.IO;
using System;

namespace Experiment
{
    public class Experiment
    {
        private readonly Configuration configuration;

        private string outputDirectory;

        public Experiment(Configuration configuration)
        {
            this.configuration = configuration;
        }

        public void SetUp()
        {
            CreateResultsDirectory();

            //Find the static fragment, read it, lock it, write it

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
    }
}