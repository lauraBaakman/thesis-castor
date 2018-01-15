using UnityEngine;
using UnityEngine.SceneManagement;
using SimpleFileBrowser;

/// <summary>
/// Persistent object, that is created when starting the application and only 
/// dies when the application is terminated.
/// </summary>
public class PersistentObject : MonoBehaviour
{
	void Start ()
	{
		DontDestroyOnLoad (gameObject);

        Debug.Log("Overwriting all player preferences with the default values.");
        new PlayerPrefsUpdater (replace:true).Update ();

		FileBrowser.SetDefaultFilter (".obj");

		SceneManager.LoadScene ("mainMenu");
	}
		
}