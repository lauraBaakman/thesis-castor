using UnityEngine;

public class PlayerPrefsUpdater
{

    private readonly bool Replace;

    public PlayerPrefsUpdater(bool replace = false){
        Replace = replace;
    }

    /// <summary>
    /// Update this instance of the player preferencs, if any preferences are not set in the local preferences file the default preferences in <updateHelpPreferences cref="updateHelpPreferences"/>. are added to the local preferences.
    /// </summary>
    public void Update()
    {
        UpdateHelpPreferences();
        UpdateUIPreferences();
        PlayerPrefs.Save();
    }

    private void UpdateHelpPreferences()
    {
        UpdatePlayerPref("help.tooltips.delayInS", 1.0f);
    }

    private void UpdateUIPreferences()
    {
        UpdatePlayerPref("ui.viewpoint.dolly.speed", 0.3f);
        UpdatePlayerPref("ui.viewpoint.pan.speed", 5.0f);

        UpdatePlayerPref("ui.translate.scalingFactor", 5.0f);
    }

    private void UpdatePlayerPref(string variableName, float value)
    {
        if (Replace || !PlayerPrefs.HasKey(variableName))
        {
            PlayerPrefs.SetFloat(variableName, value);
        }
    }
}
