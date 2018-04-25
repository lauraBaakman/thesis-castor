using System.Collections.Generic;
using System;
namespace Experiment
{
    public class Run
    {
        public class Configuration
        {
            public readonly string modelFragmentPath;
            public readonly string id;

            public Configuration(string modelFragmentPath, string id)
            {
                this.modelFragmentPath = modelFragmentPath;
                this.id = id;
            }

            public static List<Configuration> ConfigurationsFromCSV(List<Dictionary<string, object>> rows)
            {
                List<Configuration> configurations = new List<Configuration>(rows.Count);

                foreach (Dictionary<string, object> entry in rows) configurations.Add(FromCSV(entry));

                return configurations;
            }

            public static List<Configuration> FromCSV(string csvPath)
            {
                List<Dictionary<string, object>> configurations = new IO.CSVFileReader().Read(csvPath);
                return ConfigurationsFromCSV(configurations);
            }

            public static Configuration FromCSV(Dictionary<string, object> csvRow)
            {
                return new Configuration(
                    modelFragmentPath: (string)csvRow["path"],
                    id: (string)csvRow["uuid"]
                );
            }
        }
    }
}
