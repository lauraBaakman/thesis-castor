using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FileMenuBoxController : MonoBehaviour {

	/// <summary>
	/// On home button click this function loads the main menu.
	/// </summary>
	public void onHomeClick(){
		SceneManager.LoadScene ("mainMenu");
	}

	/// <summary>
	/// On the save button click this function saves the current scene to a file.
	/// </summary>
	public void onSaveClick(){
		Debug.Log ("Save Button Clicked");
	}

	/// <summary>
	/// Ons the quit click.
	/// </summary>
	public void onQuitClick(){
		//Note: Application.Quit() is ignored when playing in editor mode.
		Application.Quit ();
	}
}
