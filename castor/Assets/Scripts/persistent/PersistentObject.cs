using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SimpleFileBrowser;

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