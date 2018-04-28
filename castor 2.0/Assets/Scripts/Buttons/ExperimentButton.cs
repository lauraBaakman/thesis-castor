using System;
using SimpleFileBrowser;
using UnityEngine;
using Experiment;

namespace Buttons
{
    public class ExperimentButton : AbstractButton
    {
        public AbstractButton DeleteButton;
        public AbstractButton SelectAllButton;

        public GameObject ICPFragmentParent;

        private static string initialPath;

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
            RetrieveExperimentInputData();
        }

        protected override bool HasDetectedKeyBoardShortCut()
        {
            return Input.GetButtonDown("Experiment");
        }

        private void RetrieveExperimentInputData()
        {
            //FileBrowser.ShowLoadDialog(
            //    onSuccess: ProcessExperimentConfigurtionFile,
            //    onCancel: () => { },
            //    folderMode: false,
            //    initialPath: initialPath,
            //    title: "Select the configuration file that was generated with the obj files.",
            //    loadButtonText: "Select"
            //);
            Debug.Log("Temporarily using a fixed path");
            ProcessExperimentConfigurtionFile("/Users/laura/Repositories/thesis-experiment/simulated/test_data_single_run/cube_1773d72ee0fdb6fae90788445db0bb76.json");
        }

        private void ProcessExperimentConfigurtionFile(string path)
        {
            ExperimentRunner.Configuration configuration;
            try { configuration = ExperimentRunner.Configuration.FromJson(path); }
            catch (Exception e)
            {
                Debug.LogError("Could not read the file " + path + "\n\t error: " + e.Message);
                return;
            }

            ClearScene();

            ExperimentRunner experiment = ICPFragmentParent.GetComponent<ExperimentRunner>();
            experiment.Init(configuration);

            StartCoroutine(experiment.Execute());
        }

        private void ClearScene()
        {
            SelectAllButton.OnExecuteButtonAction();
            DeleteButton.OnExecuteButtonAction();
        }
    }
}
