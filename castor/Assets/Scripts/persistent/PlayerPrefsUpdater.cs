using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsUpdater {

	public PlayerPrefsUpdater(){}

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
