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
	public InputField Field;
	private Text Text;

	void Start ()
	{
		Text = GetComponent<Text> ();
		Field.text = Text.text;

		ToggleEditMode (false);

		SetUpEventTriggers ();
	}

	private void SetUpEventTriggers ()
	{
		SetUpTextEventTriggers ();
		SetUpFieldEventTriggers ();
	}

	private void SetUpTextEventTriggers ()
	{
		//Add Event Trigger to the game object
		EventTrigger eventTrigger = Text.gameObject.AddComponent (typeof(EventTrigger)) as EventTrigger;
		
		
		EventTrigger.TriggerEvent triggerEvent = new EventTrigger.TriggerEvent ();
		triggerEvent.AddListener ((PointerEventData) => OnPointerClick ());
		EventTrigger.Entry entry = new EventTrigger.Entry () {
			callback = triggerEvent, eventID = EventTriggerType.PointerClick
		};
		eventTrigger.triggers.Add (entry);
	}

	private void SetUpFieldEventTriggers ()
	{
		InputField.SubmitEvent submitEvent = new InputField.SubmitEvent ();
		submitEvent.AddListener (OnSubmit);
		Field.onEndEdit = submitEvent;
	}

	/// <summary>
	/// On the pointer click the field becomes editable, i.e. the text is replaced with an input field.
	/// </summary>
	public void OnPointerClick ()
	{
		EnterEditMode ();
	}

	/// <summary>
	/// On the submit the inputfield is no longer shown, instead the updated text is shown.
	/// </summary>
	/// <param name="inputText">Input text contains the new value of <c>text</c>.</param>
	public void OnSubmit (string inputText)
	{
		ExitEditMode ();
	}

	/// <summary>
	/// Toggles the edit mode, i.e. the static text is replaced with an input field.
	/// </summary>
	/// <param name="toggle">If set to <c>true</c> the text becomes editable.</param>
	public void ToggleEditMode (bool toggle)
	{
		Text.gameObject.SetActive (!toggle);
		Field.gameObject.SetActive (toggle);
	}

	private void EnterEditMode ()
	{
		ToggleEditMode (true);
		Field.Select ();
		Field.ActivateInputField ();
	}

	private void ExitEditMode ()
	{
		ToggleEditMode (false);
		Text.text = Field.text;
	}
}