using System.IO;
using UnityEngine;

namespace Experiment
{
    [System.Serializable]
    public class Configuration
    {
        public string lockedFragmentFile;
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