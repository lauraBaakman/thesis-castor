using Registration.Messages;
using UnityEngine;
using System;
using IO;
using System.Collections;

public class RealExperimentRunner : RTEditor.MonoSingletonBase<RealExperimentRunner>, IICPStartEndListener
{
	public void OnICPStarted(ICPStartedMessage message)
	{
		throw new NotImplementedException();
	}

	public void OnICPTerminated(ICPTerminatedMessage message)
	{
		throw new NotImplementedException();
	}


	private void Terminate()
	{
		throw new NotImplementedException();
	}

	IEnumerator Start()
	{
		Debug.Log("Start before yield");
		yield return new WaitForSeconds(5);
		Debug.Log("Start after yield");
	}

	private void DoNothing(WriteResult result) { }

	public void Run(string inputDirectory, string outputDirectory)
	{
		throw new NotImplementedException();
	}

	private void RegisterTopPair()
	{
		throw new NotImplementedException();
	}

	private void LoadFragments(string inputDirectory)
	{
		throw new NotImplementedException();
	}

	private void GatherFragments()
	{
		throw new NotImplementedException();
	}
}
