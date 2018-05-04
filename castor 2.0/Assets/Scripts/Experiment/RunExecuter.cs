using System.Collections.Generic;
using IO;
using UnityEngine;
using System.IO;
using Fragment;
using Registration;

namespace Experiment
{
    public class RunExecuter
    {
        private GameObject staticFragment;
        private GameObject listener;

        private FragmentExporter fragmentExporter;
        private FragmentImporter fragmentImporter;

        private string outputDirectory;

        private int currentRunNumber = 0;

        private bool isCurrentRunFinished = false;

        public RunExecuter(GameObject listener, GameObject staticFragment,
                           FragmentExporter fragmentExporter, FragmentImporter fragmentImporter)
        {
            this.staticFragment = staticFragment;
            this.listener = listener;

            this.fragmentExporter = fragmentExporter;
            this.fragmentImporter = fragmentImporter;
        }

        public string OutputDirectory
        {
            get { return outputDirectory; }

            set { outputDirectory = value; }
        }

        public IEnumerator<object> Execute(Run run)
        {
            currentRunNumber++;
            isCurrentRunFinished = false;

            string message = string.Format("Starting run number {0} with fragment {1}.",
                                           currentRunNumber, run.id);

            //Notify the user via the ticker and via the debug log
            Ticker.Receiver.Instance.SendMessage(
                 methodName: "OnMessage",
                value: new Ticker.Message.InfoMessage(message)
             );
            Debug.Log(message);
            yield return null;

            // Load ModelFragment
            GameObject modelFragment = fragmentImporter.Import(run.modelFragmentPath, prefabPath: ExperimentRunner.ExperimentFragmentPrefabPath);
            yield return null;

            // Run ICP
            ICPRegisterer icp = new ICPRegisterer(staticFragment, modelFragment, run.ICPSettings);
            icp.AddListener(this.listener);

            while (!icp.HasTerminated)
            {
                icp.PrepareStep();
                yield return null;

                icp.Step();
                yield return null;
            }

            // Export Current Position of the ModelFragment
            fragmentExporter.Export(modelFragment, run.GetOutputPath(this.outputDirectory));
            yield return null;

            // Delete the ModelFragment
            modelFragment.GetComponent<FragmentDestroyer>().DestroyFragment();

            isCurrentRunFinished = true;
        }

        public bool IsCurrentRunFinished()
        {
            return isCurrentRunFinished;
        }

        public class Run
        {
            public readonly string modelFragmentPath;
            public readonly string id;

            public Settings ICPSettings;

            public Run(string modelFragmentPath, string id)
            {
                this.modelFragmentPath = modelFragmentPath;
                this.id = id;
            }

            public string GetOutputPath(string outputDirectory)
            {
                return Path.Combine(outputDirectory, string.Format("{0}.obj", this.id));
            }

            public static List<Run> FromCSV(List<Dictionary<string, object>> rows)
            {
                List<Run> configurations = new List<Run>(rows.Count);

                foreach (Dictionary<string, object> entry in rows) configurations.Add(FromCSV(entry));

                return configurations;
            }

            public static List<Run> FromCSV(string csvPath)
            {
                List<Dictionary<string, object>> configurations = new IO.CSVFileReader().Read(csvPath);
                return FromCSV(configurations);
            }

            public static Run FromCSV(Dictionary<string, object> csvRow)
            {
                return new Run(
                    modelFragmentPath: (string)csvRow["path"],
                    id: (string)csvRow["uuid"]
                );
            }
        }
    }
}
