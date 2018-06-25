using SimpleFileBrowser;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplicationInitializer : MonoBehaviour
{
	public string nextSceneName = "reduction";

	void Start()
	{
		Initialize();
		SceneManager.LoadScene(nextSceneName, LoadSceneMode.Single);
	}

	private void Initialize()
	{
		SharedInitialization();
		if (Application.isEditor) EditorSpecificInitialization();
		else DeploySpecificInitialization();
	}

	/// <summary>
	/// Initialization that always needs to be done, indepedent of the appliction.
	/// </summary>
	private void SharedInitialization()
	{
		FileBrowser.Filter configurationFilter = new FileBrowser.Filter("Configuration", ".json");
		FileBrowser.Filter fragmentFilter = new FileBrowser.Filter("Fragments", ".obj");
		FileBrowser.Filter csvFilter = new FileBrowser.Filter("csv", ".csv");

		FileBrowser.SetFilters(true, configurationFilter, fragmentFilter, csvFilter);
		FileBrowser.SetDefaultFilter(fragmentFilter.defaultExtension);
		FileBrowser.SetExcludedExtensions(".lnk", ".tmp", ".zip", ".rar", ".exe");

		// Continue computations if the applicaiton is no longer in focus
		Application.runInBackground = true;

		// Set the seed fo the random generator, which is used to randomly translate fragments
		Random.InitState(42);
	}

	/// <summary>
	/// Initialization that only needs to be done if we are running from a deployed application.
	/// </summary>
	private void DeploySpecificInitialization()
	{ }

	/// <summary>
	/// Initialization that only needs to be done if wer are running from the editor.
	/// </summary>
	private void EditorSpecificInitialization()
	{
		Random.InitState(42);
	}
}
