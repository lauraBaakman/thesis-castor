# Computer ASsisTed Orhopaedic fracture Reconstruction (CAstOR)

## Manual

At some point I'll create a manual here.

## Requirements

- [Unity version 2017.1.1](https://unity3d.com/get-unity/download/archive).

## Running on the CommandLine
* To run a headless standalone `castor` on the commandline use under OS X:

		castor.app/Contents/MacOS/castor -batchmode -logFile /dev/stdout

* To run the experiment with the configuration defined in the file `config.json` under OS X use:

		castor.app/Contents/MacOS/castor -batchmode -logFile /dev/stdout -experiment full_path/config.json

	make sure to use the full path to the config file.

* To analyze the results of an experiment input the full path to the `data.csv` generated when generating the dataset and the full path to the directory with the results.

		castor.app/Contents/MacOS/castor -batchmode -logFile /dev/stdout -statistics full_path/data.csv full_path/results_Horn/