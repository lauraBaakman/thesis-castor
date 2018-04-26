using UnityEngine;
using System.IO;
using System;
using System.Collections.Generic;
using IO;

namespace Experiment
{
    public class Experiment
    {
        private readonly Configuration configuration;

        GameObject staticFragment;
        List<RunExecuter.Run> runs;

        IO.FragmentImporter fragmentImporter;
        IO.FragmentExporter fragmentExporter;

        Registration.Settings ICPSettings;

        private string outputDirectory;

        public Experiment(Configuration configuration, GameObject ICPFragmentParentObject)
        {
            this.configuration = configuration;

            this.fragmentImporter = new IO.FragmentImporter(ICPFragmentParentObject, FragmentReaderCallBack);
            this.fragmentExporter = new IO.FragmentExporter(FragmentWriterCallBack);

            this.ICPSettings = new Registration.Settings(ICPFragmentParentObject.transform);
        }

        private void FragmentReaderCallBack(IO.ReadResult result)
        {
            if (result.Failed())
            {
                Ticker.Receiver.Instance.SendMessage(
                    methodName: "OnMessage",
                    value: result.ToTickerMessage(),
                    options: SendMessageOptions.RequireReceiver
                );
            }
        }

        private void FragmentWriterCallBack(IO.WriteResult result)
        {
            Ticker.Receiver.Instance.SendMessage(
                methodName: "OnMessage",
                value: result.ToTickerMessage(),
                options: SendMessageOptions.RequireReceiver
            );
        }

        public void SetUp()
        {
            CreateResultsDirectory();

            HandleStaticFragment();

            this.runs = CollectRuns();

            //throw new NotImplementedException("Write a copy of the settings file to the output directory");
        }

        private List<RunExecuter.Run> CollectRuns()
        {
            return RunExecuter.Run.FromCSV(configuration.configurations);
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

            Write(staticFragment);
        }

        private GameObject Import(string path)
        {
            return fragmentImporter.Import(path);
        }

        private void Lock(GameObject fragment)
        {
            fragment.SendMessage(
                methodName: "OnToggledLockedState",
                value: true,
                options: SendMessageOptions.DontRequireReceiver
            );
        }

        private void Write(GameObject fragment)
        {
            string path = Path.Combine(
                path1: this.outputDirectory,
                path2: Path.GetFileName(configuration.lockedFragmentFile)
            );
            fragmentExporter.Export(fragment, path);
        }

        public void Execute()
        {
            RunExecuter executer = new RunExecuter(
                staticFragment, ICPSettings,
                fragmentExporter, fragmentImporter, outputDirectory);
            foreach (RunExecuter.Run run in runs) executer.Execute(run);
        }

        [System.Serializable]
        public class Configuration
        {
            public string lockedFragmentFile;
            public string outputDirectory;
            public string configurations;
            public string id;

            public static Configuration FromJson(string path)
            {
                string json_string = File.ReadAllText(path);
                Configuration configuration = JsonUtility.FromJson<Configuration>(json_string);

                return configuration;
            }
        }
    }
}