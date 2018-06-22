using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class RealExperimentLogger : RTEditor.MonoSingletonBase<RealExperimentLogger>
{
	private string outputPath;

	public void SetInputDirectory(string path)
	{
		string directoryName = Path.GetDirectoryName(path);
		Debug.Log(directoryName);
		//TODO build the output path
	}
}
