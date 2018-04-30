
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Registration;
using Registration.Messages;

namespace Experiment
{
    public class ExperimentRunner : MonoBehaviour, IICPListener
    {
        private Configuration configuration;

        public static string ExperimentFragmentPrefabPath = "ExperimentFragment";

        private GameObject staticFragment;
        private List<RunExecuter.Run> runs;

        private List<Settings> ICPSettings;

        private IO.FragmentImporter fragmentImporter;
        private IO.FragmentExporter fragmentExporter;

        private string outputDirectory;

        private StreamWriter streamWriter;

        public void Init(Configuration configuration)
        {
            this.configuration = configuration;

            this.fragmentImporter = new IO.FragmentImporter(
                this.gameObject, FragmentReaderCallBack,
                copyVerticesToTexture: true,
                randomizeTransform: false
            );
            this.fragmentExporter = new IO.FragmentExporter(FragmentWriterCallBack);

            this.ICPSettings = new List<Settings>();

            SetUp();
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

        private void SetUp()
        {
            HandleStaticFragment();

            GenerateICPSettings();

            this.runs = CollectRuns();
        }

        private void GenerateICPSettings()

        {
            ICPSettings.Add(
                new Settings(
                    this.gameObject.transform,
                    new IGDTransformFinder(
                        new IGDTransformFinder.Configuration(
                            convergenceError: 0.001,
                            learningRate: 0.001,
                            maxNumIterations: 200,
                            errorMetric: new Registration.Error.IntersectionTermError(0.5, 0.5)
                        )
                    )
                )
            );

            ICPSettings.Add(
                new Settings(
                    this.gameObject.transform,
                    new IGDTransformFinder(
                        new IGDTransformFinder.Configuration(
                            convergenceError: 0.001,
                            learningRate: 0.001,
                            maxNumIterations: 200,
                            errorMetric: new Registration.Error.WheelerIterativeError()
                        )
                    )
                )
            );

            ICPSettings.Add(
                new Settings(
                    this.gameObject.transform,
                    new HornTransformFinder()
                )
            );

            ICPSettings.Add(
                new Settings(
                    this.gameObject.transform,
                    new LowTransformFinder()
                )
            );
        }

        private List<RunExecuter.Run> CollectRuns()
        {
            return RunExecuter.Run.FromCSV(configuration.configurations);
        }

        private string CreateResultsDirectory()
        {
            string basePath = configuration.outputDirectory;
            string directoryName = CreateResultsDirectoryName();

            string directory = Path.Combine(basePath, directoryName);

            Directory.CreateDirectory(directory);

            return directory;
        }

        private string CreateResultsDirectoryName()
        {
            System.DateTime now = System.DateTime.Now.ToLocalTime();
            return string.Format(
                "{0}", now.ToString("MM-dd_HH-mm-ss-fff"));
        }

        private void HandleStaticFragment()
        {
            this.staticFragment = Import(configuration.lockedFragmentFile);

            Lock(staticFragment);
        }

        private GameObject Import(string path)
        {
            return fragmentImporter.Import(path, prefabPath: ExperimentFragmentPrefabPath);
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
            string path = System.IO.Path.Combine(
                path1: this.outputDirectory,
                path2: Path.GetFileName(configuration.lockedFragmentFile)
            );
            fragmentExporter.Export(fragment, path);
        }

        public IEnumerator<object> Execute()
        {
            RunExecuter executer = new RunExecuter(this.gameObject, staticFragment,
                                                   fragmentExporter, fragmentImporter);

            foreach (Settings ICPSetting in this.ICPSettings)
            {
                this.outputDirectory = CreateResultsDirectory();
                yield return null;

                Write(staticFragment);
                yield return null;

                ICPSetting.ToJson(Path.Combine(this.outputDirectory, "settings.json"));
                yield return null;

                streamWriter = new StreamWriter(Path.Combine(this.outputDirectory, "data.csv"));
                streamWriter.WriteLine(
                    string.Format(
                        "{0}, {1}, {2}",
                        "id", "termination message", "termination error"
                    )
                );
                yield return null;

                foreach (RunExecuter.Run run in runs)
                {
                    run.ICPSettings = ICPSetting;
                    executer.OutputDirectory = this.outputDirectory;

                    streamWriter.Write(string.Format("{0}, ", run.id));
                    yield return null;

                    StartCoroutine(executer.Execute(run));
                    yield return new WaitUntil(executer.IsCurrentRunFinished);
                }
                streamWriter.Close();
            }
        }

        #region ICPInterface
        public void OnPreparationStepCompleted(ICPPreparationStepCompletedMessage message) { }

        public void OnStepCompleted(ICPStepCompletedMessage message) { }

        public void OnICPTerminated(ICPTerminatedMessage message)
        {
            streamWriter.WriteLine(string.Format("'{0}', {1}", message.Message, message.errorAtTermination));
        }
        #endregion

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