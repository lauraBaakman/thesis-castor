using System;
using System.Collections;
using System.Collections.Generic;
using SimpleFileBrowser;
using UnityEngine;

namespace Buttons
{
    public class ExperimentButton : AbstractButton
    {
        private string staticFragmentFile;
        private string outputDirectory;
        private List<String> modelFragmentFiles;

        private IEnumerator retrieveModelFragmentFilesCoroutine;

        private static string initialPath;

        private void Start()
        {
            initialPath = Application.isEditor ? "/Users/laura/Repositories/thesis-experiment" : null;
            modelFragmentFiles = new List<string>();
        }

        protected override void ExecuteButtonAction()
        {
            Reset();
            RetrieveExperimentInputData();
        }

        protected override bool HasDetectedKeyBoardShortCut()
        {
            return Input.GetButtonDown("Experiment");
        }

        private void RetrieveExperimentInputData()
        {
            RetrieveStaticFragment();
        }

        private void Reset()
        {
            this.staticFragmentFile = null;
            this.outputDirectory = null;
            this.modelFragmentFiles.Clear();
            this.retrieveModelFragmentFilesCoroutine = null;
        }

        private void RetrieveStaticFragment()
        {
            FileBrowser.ShowLoadDialog(
                onSuccess: (path) =>
                {
                    this.staticFragmentFile = path;
                    RetrieveModelFragmentDirectory();
                },
                onCancel: OnCancel,
                folderMode: false,
                initialPath: initialPath,
                title: "Select the Static Fragment",
                loadButtonText: "Select"
            );
        }

        private void RetrieveModelFragmentDirectory()
        {
            FileBrowser.ShowLoadDialog(
                onSuccess: (path) =>
                {
                    this.retrieveModelFragmentFilesCoroutine = RetrieveModelFragmentFiles(path);
                    StartCoroutine(retrieveModelFragmentFilesCoroutine);

                    RetrieveOutputDirectory();
                },
                onCancel: OnCancel,
                folderMode: true,
                initialPath: initialPath,
                title: "Select the Model Fragment Directory",
                loadButtonText: "Select"
            );
        }

        private IEnumerator RetrieveModelFragmentFiles(string result)
        {
            ///Use a local variable so that we can use the state of the 
            /// property to check if this function is finished.
            List<string> files = new List<string>();
            for (int i = 0; i < 400; i++)
            {
                files.Add("Hi!");
                yield return null;
            }
            this.modelFragmentFiles.AddRange(files);

            StartExperiment();
        }

        void RetrieveOutputDirectory()
        {
            FileBrowser.ShowSaveDialog(
                onSuccess: (path) =>
                {
                    this.outputDirectory = path;
                    StartExperiment();
                },
                onCancel: OnCancel,
                folderMode: true,
                initialPath: initialPath,
                title: "Select the Output Directory",
                saveButtonText: "Select"
            );
        }

        private void OnCancel()
        {
            if (retrieveModelFragmentFilesCoroutine != null) StopCoroutine(retrieveModelFragmentFilesCoroutine);
        }

        private void StartExperiment()
        {
            if (!ExperimentDataReady()) return;
            throw new NotImplementedException("Write the code to actually run the experiment!");
        }

        private bool ExperimentDataReady()
        {
            return (
                this.outputDirectory != null &&
                !this.modelFragmentFiles.IsEmpty()
            );
        }
    }
}
