using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class FragmentListElementController : MonoBehaviour
{
	/// <summary>
	/// The UI element that displays the name of the fragment.
	/// </summary>
	public Text FragmentNameText;

	private FragmentController FragmentController;

	void Start ()
	{
	}

	/// <summary>
	/// Populate the properties of the fragmentController.
	/// </summary>
	/// <param name="fragmentController">Fragment controller.</param>
	public void Populate (FragmentController fragmentController){
		this.FragmentController = fragmentController;
	}

	/// <summary>
	/// On the delete fragment button click the fragment is deleted from <Fragments cref="Fragments">, the 3D view and the list view.
	/// </summary>
	public void OnDeleteFragmentClick ()
	{
		Debug.Log ("Pressed the delete fragment button.");
	}

	/// <summary>
	/// On the change fragment color button click the user should be able to change the color of the fragment.
	/// </summary>
	public void OnChangeColorFragmentClick ()
	{
		Debug.Log ("Pressed the change color fragment button.");
	}

	/// <summary>
	/// Ons the toggle fragment value changed, the fragment should be shown or hidden depending on its previous state.
	/// </summary>
	/// <param name="toggle">If set to <c>true</c> the fragment should be shown.</param>
	public void OnToggleFragmentValueChanged (bool toggle)
	{
		Debug.Log ("Pressed the toggle fragment checkmark");
	}

	/// <summary>
	/// Change the name of the fragment.
	/// </summary>
	/// <param name="name">the new name of the fragment.</param>
	public void ChangeFragmentName (string newName)
	{
		this.FragmentNameText.text = newName;
	}
}