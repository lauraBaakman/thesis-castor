using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class FragmentListElementController : MonoBehaviour
{

	private EditableTextField description;

	void Start ()
	{
		description = new EditableTextField (
			text: transform.Find ("Description").GetComponent<Text> (), 
			input: transform.Find ("Description Input").GetComponent<InputField> ()
		);
		description.toggleEditMode (false);
	}

	public void onDeleteFragmentClick ()
	{
		Debug.Log ("Pressed the delete fragment button.");
	}

	public void onChangeColorFragmentClick ()
	{
		Debug.Log ("Pressed the change color fragment button.");
	}

	public void onToggleFragmentValueChanged (bool toggle)
	{
		Debug.Log ("Pressed the toggle fragment checkmark");
	}
}

public class EditableTextField
{
	private Text text;
	private InputField input;

	public EditableTextField (Text text, InputField input)
	{
		this.text = text;
		this.input = input;

		setUpEventTriggers ();
	}

	private void setUpEventTriggers ()
	{
		setUpTextEventTriggers ();
	}

	private void setUpTextEventTriggers(){
		//Add Event Trigger to the game object
		EventTrigger eventTrigger = text.gameObject.AddComponent (typeof(EventTrigger)) as EventTrigger;

		// Create a new trigger event
		EventTrigger.TriggerEvent triggerEvent = new EventTrigger.TriggerEvent ();

		// Assign a listener to the trigger event
		triggerEvent.AddListener ((PointerEventData) => onPointerClick ());

		// Create a EventTrigger Entry
		EventTrigger.Entry entry = new EventTrigger.Entry () {
			callback = triggerEvent, eventID = EventTriggerType.PointerClick
		};

		// Register the EventTrigger Entry
		eventTrigger.triggers.Add (entry);				
	}

	public void toggleEditMode (bool toggle)
	{
		text.gameObject.SetActive (!toggle);
		input.gameObject.SetActive (toggle);
	}

	public void onPointerClick ()
	{
		Debug.Log ("A click has been detected!");
	}

	//Hide the input field

	//Onclick of the text field:
	// hide the text field,
	// show the input field


	//On leaving the input field:
	// hide the input field
	// store the text in the input field in the text field
	// show the text field
}
