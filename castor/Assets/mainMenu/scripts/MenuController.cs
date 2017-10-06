using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MenuController : MonoBehaviour
{

	public void Start ()
	{
		Button loadButton = GetButtonByName ("Load Fracture Reduction Button");
		loadButton.interactable = false;

		Button settingsButton = GetButtonByName ("Settings Button");
		settingsButton.interactable = false;
		
		AttachListeners ();
	}

	public void AttachListeners ()
	{
		Button button = GetButtonByName ("New Fracture Reduction Button");
		button.onClick.AddListener (OnNewReductionButton);
	}

	private Button GetButtonByName (string name)
	{
		GameObject gameObject = GameObject.Find (name);
		Button button = gameObject.GetComponent<Button> ();
		return button;
	}

	public void OnNewReductionButton ()
	{
		Debug.Log ("Button Pressed!");
	}
}
