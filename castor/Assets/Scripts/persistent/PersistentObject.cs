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

		new PlayerPrefsUpdater ().Update ();

		FileBrowser.SetDefaultFilter (".obj");

		SceneManager.LoadScene ("mainMenu");
	}
		
}