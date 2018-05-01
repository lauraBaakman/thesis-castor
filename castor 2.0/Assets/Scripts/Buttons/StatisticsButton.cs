using System.Collections.Generic;
using SimpleFileBrowser;
using UnityEngine;
using IO;
using System.IO;

namespace Buttons
{
    public class StatisticsButton : AbstractButton
    {
        private static string initialPath;

        private string directory;

        private List<string> objFilePaths;

        protected override void Awake()
        {
            base.Awake();
            initialPath = Application.isEditor ? "/Users/laura/Repositories/thesis-experiment/simulated" : null;
        }

        protected override void ExecuteButtonAction()
        {
            RetrieveExperimentResultFolder();
        }

        protected override bool HasDetectedKeyBoardShortCut()
        {
            return Input.GetButtonDown("Statistics");
        }

        private void RetrieveExperimentResultFolder()
        {
            FileBrowser.ShowLoadDialog(
                onSuccess: (path) => StartCoroutine(ProcessExperimentResultsFolder(path)),
                onCancel: () => { },
                folderMode: true,
                initialPath: initialPath,
                title: "Select a results directory.",
                loadButtonText: "Select"
            );
        }

        private IEnumerator<object> ProcessExperimentResultsFolder(string inputDirectory)
        {
            StartCoroutine(FindObjectIDs(inputDirectory));
            yield return new WaitWhile(() => objFilePaths == null);
            Debug.Log("Read all obj file paths");
        }

        private IEnumerator<object> FindObjectIDs(string inputDirectory)
        {
            this.directory = inputDirectory;
            yield return null;

            string csvDataFile = GetCSVDataFile();
            yield return null;

            List<Dictionary<string, object>> csvData = new CSVFileReader().Read(csvDataFile);
            yield return null;

            List<string> objFiles = new List<string>(csvData.Count);
            foreach (Dictionary<string, object> row in csvData)
            {
                objFiles.Add(ExtractObjFile(row));
                yield return null;
            }

            this.objFilePaths = objFiles;
        }

        private string GetCSVDataFile()
        {
            string[] csvFiles = System.IO.Directory.GetFiles(directory, "*.csv");
            ValidateCSVFiles(csvFiles);
            return csvFiles[0];
        }

        private void ValidateCSVFiles(string[] csvFiles)
        {
            if (csvFiles.Length != 1)
            {
                Debug.LogError(
                    string.Format(
                        "Unexpectedly found {0} csv file(s), expected exactly one file.",
                        csvFiles.Length
                    )
                );
            }
        }

        private string ExtractObjFile(Dictionary<string, object> csvRow)
        {
            string id = csvRow["id"] as string;
            string path = Path.Combine(directory, id);
            return path;
        }
    }
}