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

	public void toggleEditMode (bool toggle)
	{
		text.gameObject.SetActive (!toggle);
		input.gameObject.SetActive (toggle);
	}

	public void onPointerClick ()
	{
		enterEditMode ();
	}

	private void enterEditMode ()
	{
		toggleEditMode (true);
	}

	private void exitEditMode ()
	{
		toggleEditMode (false);
		text.text = input.text;
	}

	//Levae edit mode upon:
	// tab
	// Enter
}
