using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UserEditableText : MonoBehaviour
{

	/// <summary>
	/// The field represents the InputField that is used when the user is editing the text.
	/// </summary>
	public InputField field;
	private Text text;

	void Start ()
	{
		text = GetComponent<Text> ();
		field.text = text.text;

		toggleEditMode (false);

		setUpEventTriggers ();
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

	/// <summary>
	/// On the pointer click the field becomes editable, i.e. the text is replaced with an input field.
	/// </summary>
	public void onPointerClick ()
	{
		enterEditMode ();
	}

	/// <summary>
	/// On the submit the inputfield is no longer shown, instead the updated text is shown.
	/// </summary>
	/// <param name="inputText">Input text contains the new value of <c>text</c>.</param>
	public void onSubmit (string inputText)
	{
		exitEditMode ();
	}

	/// <summary>
	/// Toggles the edit mode, i.e. the static text is replaced with an input field.
	/// </summary>
	/// <param name="toggle">If set to <c>true</c> the text becomes editable.</param>
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