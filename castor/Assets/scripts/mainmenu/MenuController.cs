using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


/// <summary>
/// The Menu controller controls the menu that is shown on start up.  
/// </summary>
public class MenuController : MonoBehaviour
{

	/// <summary>
	/// Start this instance of the menu controller by getting buttons from the scene.
	/// </summary>
	public void Start ()
	{
		Button loadButton = GetButtonByName ("Load Fracture Reduction Button");
		loadButton.interactable = false;

		Button settingsButton = GetButtonByName ("Settings Button");
		settingsButton.interactable = false;
		
		AttachListeners ();
	}

	/// <summary>
	/// Attachs the listeners to the buttons in menu.
	/// </summary>
	public void AttachListeners ()
	{
		Button button = GetButtonByName ("New Fracture Reduction Button");
		button.onClick.AddListener (OnNewReductionButton);
	}

    private Button GetButtonByName (string buttonName)
	{
        GameObject buttonGO = GameObject.Find (buttonName);
		Button button = buttonGO.GetComponent<Button> ();
		return button;
	}

	/// <summary>
	/// If the new reduction button is clicked this function is called to create a new empty scene. 
	/// </summary>
	public void OnNewReductionButton ()
	{
		SceneManager.LoadScene ("reduction");
	}
}
