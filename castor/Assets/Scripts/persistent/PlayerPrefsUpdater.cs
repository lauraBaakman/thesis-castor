using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsUpdater {

	/// <summary>
	/// Initializes a new instance of the <see cref="PlayerPrefsUpdater"/> class.
	/// </summary>
	public PlayerPrefsUpdater(){}

	/// <summary>
	/// Update this instance of the player preferencs, if any preferences are not set in the local preferences file the default preferences in <updateHelpPreferences cref="updateHelpPreferences"/>. are added to the local preferences.
	/// </summary>
	public void update(){
		updateHelpPreferences ();

		PlayerPrefs.Save ();		
	}

	private void updateHelpPreferences(){
		UpdatePlayerPref ("help.tooltips.delayInS", 1.0f);
	}

	private void UpdatePlayerPref (string variableName, float value)
	{
		if (!PlayerPrefs.HasKey (variableName)) {
			PlayerPrefs.SetFloat (variableName, value);
		}
	}
}
