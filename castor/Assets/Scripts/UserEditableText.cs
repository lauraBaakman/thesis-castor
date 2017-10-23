using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UserEditableText : MonoBehaviour
{

	public InputField field;
	private Text text;

	void Start ()
	{
		text = GetComponent<Text> ();
		field.text = text.text;

		toggleEditMode (false);

		setUpEventTriggers ();
	}

	void Update ()
	{
		if (field.isFocused) {
			Debug.Log ("Field has focus!");
		}
	}

	private void setUpEventTriggers ()
	{
		setUpTextEventTriggers ();
		setUpFieldEventTriggers ();
	}

	private void setUpTextEventTriggers ()
	{
		//Add Event Trigger to the game object
		EventTrigger eventTrigger = text.gameObject.AddComponent (typeof(EventTrigger)) as EventTrigger;
		
		
		EventTrigger.TriggerEvent triggerEvent = new EventTrigger.TriggerEvent ();
		triggerEvent.AddListener ((PointerEventData) => onPointerClick ());
		EventTrigger.Entry entry = new EventTrigger.Entry () {
			callback = triggerEvent, eventID = EventTriggerType.PointerClick
		};
		eventTrigger.triggers.Add (entry);
	}

	private void setUpFieldEventTriggers ()
	{
		InputField.SubmitEvent submitEvent = new InputField.SubmitEvent ();
		submitEvent.AddListener (onSubmit);
		field.onEndEdit = submitEvent;
	}

	public void onPointerClick ()
	{
		enterEditMode ();
	}

	public void onSubmit (string inputText)
	{
		Debug.Log ("On Submit!");
	}

	public void toggleEditMode (bool toggle)
	{
		text.gameObject.SetActive (!toggle);
		field.gameObject.SetActive (toggle);
	}

	private void enterEditMode ()
	{
		toggleEditMode (true);
		field.Select ();
		field.ActivateInputField ();
	}

	private void exitEditMode ()
	{
		toggleEditMode (false);
		text.text = field.text;
	}
}