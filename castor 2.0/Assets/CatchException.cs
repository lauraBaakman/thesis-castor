using UnityEngine;

public class CatchException : MonoBehaviour
{
	private void OnEnable()
	{
		Application.logMessageReceived += HandleLogEntry;
	}

	private void OnDisable()
	{
		Application.logMessageReceived -= HandleLogEntry;
	}

	private void HandleLogEntry(string logEntry, string stackTrace, LogType logType)
	{
		switch (logType)
		{
			case LogType.Error:
				TerminateApplication(logEntry, stackTrace, logType);
				break;
			case LogType.Exception:
				TerminateApplication(logEntry, stackTrace, logType);
				break;
			case LogType.Warning:
				LogEntryToConsole(logEntry, stackTrace, logType);
				break;
			case LogType.Assert:
				LogEntryToConsole(logEntry, stackTrace, logType);
				break;
			case LogType.Log:
				//do nothing
				break;
		}
	}

	private void TerminateApplication(string logEntry, string stackTrace, LogType logType)
	{
		LogEntryToConsole(logEntry, stackTrace, logType);
		Application.Quit();
	}

	private void LogEntryToConsole(string logEntry, string stackTrace, LogType logType)
	{
		Debug.Log(string.Format("{0}: {1}\n{2}", logType, logEntry, stackTrace));
	}
}

