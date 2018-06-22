using UnityEngine;
using UnityEngine.UI;

public class RealExperimentLogger : RTEditor.MonoSingletonBase<RealExperimentLogger>
{
	private string outputPath;

	public string OutputPath
	{
		get { return outputPath; }
		set { outputPath = value; }
	}
}
