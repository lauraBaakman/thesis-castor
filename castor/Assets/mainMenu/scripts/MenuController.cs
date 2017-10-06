using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{

	public void Start ()
	{
		//TODO disable the settings button
		//TODO disable the load reduction button
		AttachListeners ();
	}

	public void AttachListeners ()
	{

		Button button;

		button = findButtonByName ("New Fracture Reduction Button");
		button.onClick.AddListener (OnNewReductionButton);
	}

	private Button findButtonByName (string name)
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
