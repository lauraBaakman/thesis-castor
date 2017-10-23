using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FileMenuBoxController : MonoBehaviour {

	public void onHomeClick(){
		SceneManager.LoadScene ("mainMenu");
	}

	public void onSaveClick(){
		Debug.Log ("Save Button Clicked");
	}

	public void onQuitClick(){
		Debug.Log ("Quit Button Clicked");
	}
}
