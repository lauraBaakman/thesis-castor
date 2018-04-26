using System.Collections.Generic;
using IO;
using UnityEngine;
using Registration;
using System;
using System.IO;

namespace Experiment
{
    public class RunExecuter
    {
        private GameObject staticFragment;

        private Registration.Settings icpSettings;

        private FragmentExporter fragmentExporter;
        private FragmentImporter fragmentImporter;

        private string outputDirectory;

        public RunExecuter(GameObject staticFragment, Registration.Settings IPCsettings,
                   FragmentExporter fragmentExporter, FragmentImporter fragmentImporter,
                  string outputDirectory)
        {
            this.staticFragment = staticFragment;
            this.icpSettings = IPCsettings;

            this.fragmentExporter = fragmentExporter;
            this.fragmentImporter = fragmentImporter;

            this.outputDirectory = outputDirectory;
        }

        public void Execute(Run run)
        {
            GameObject modelFragment = fragmentImporter.Import(run.modelFragmentPath);

            ICPRegisterer icp = new ICPRegisterer(staticFragment, modelFragment, icpSettings);

            throw new NotImplementedException("Actually do ICP");

            fragmentExporter.Export(staticFragment, run.GetOutputPath(this.outputDirectory));

            // Delete the model fragment from the scene
            throw new NotImplementedException("Delete the model fragment from the scene");
        }

        public class Run
        {
            public readonly string modelFragmentPath;
            public readonly string id;

            public Run(string modelFragmentPath, string id)
            {
                this.modelFragmentPath = modelFragmentPath;
                this.id = id;
            }

            public string GetOutputPath(string outputDirectory)
            {
                return Path.Combine(outputDirectory, string.Format("{0}.obj", this.id));
            }

            public static List<Run> ConfigurationsFromCSV(List<Dictionary<string, object>> rows)
            {
                List<Run> configurations = new List<Run>(rows.Count);

                foreach (Dictionary<string, object> entry in rows) configurations.Add(FromCSV(entry));

                return configurations;
            }

            public static List<Run> FromCSV(string csvPath)
            {
                List<Dictionary<string, object>> configurations = new IO.CSVFileReader().Read(csvPath);
                return ConfigurationsFromCSV(configurations);
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
