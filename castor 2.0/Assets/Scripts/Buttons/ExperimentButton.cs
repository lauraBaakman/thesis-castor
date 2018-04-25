using System;
using SimpleFileBrowser;
using UnityEngine;

namespace Buttons
{
    public class ExperimentButton : AbstractButton
    {
        public AbstractButton DeleteButton;
        public AbstractButton SelectAllButton;

        private static string initialPath;
        private Experiment.Configuration configuration;

        private void Start()
        {
            initialPath = Application.isEditor ? "/Users/laura/Repositories/thesis-experiment/simulated" : null;

            ConfigureFileBrowser();
        }

        private void ConfigureFileBrowser()
        {
            FileBrowser.Filter configurationFilter = new FileBrowser.Filter("Configuration", ".json");
            FileBrowser.Filter fragmentFilter = new FileBrowser.Filter("Fragments", ".obj");

            FileBrowser.SetFilters(false, configurationFilter, fragmentFilter);
            FileBrowser.SetDefaultFilter(configurationFilter.defaultExtension);
            FileBrowser.SetExcludedExtensions(".lnk", ".tmp", ".zip", ".rar", ".exe", fragmentFilter.defaultExtension);
        }

        protected override void ExecuteButtonAction()
        {
            Reset();
            RetrieveExperimentInputData();
        }

        private void Reset()
        {
            configuration = null;
        }

        protected override bool HasDetectedKeyBoardShortCut()
        {
            return Input.GetButtonDown("Experiment");
        }

        private void RetrieveExperimentInputData()
        {
            FileBrowser.ShowLoadDialog(
                onSuccess: ProcessExperimentConfigurtionFile,
                onCancel: () => { },
                folderMode: false,
                initialPath: initialPath,
                title: "Select the configuration file generated when the obj files were generated",
                loadButtonText: "Select"
            );
        }

        private void ProcessExperimentConfigurtionFile(string path)
        {
            try
            {
                this.configuration = Experiment.Configuration.FromJson(path);
                RetrieveDataFromConfiguration();
            }
            catch (Exception e)
            {
                Debug.LogError("Could not read the file " + path + e.Message);
            }
        }

        private void RetrieveDataFromConfiguration()
        {
            Debug.Log("The output directory: " + configuration.outputDirectory);
            //Remove all currently existing objects

            //Get the current date time in a pretty format

            //Generate an output folder within the output folder in the configuration

            //Find the static fragment, read it, lock it, write it

            //Find the list of model fragments

            Debug.Log("Time to retrieve data from the configuration");
        }
    }
}
