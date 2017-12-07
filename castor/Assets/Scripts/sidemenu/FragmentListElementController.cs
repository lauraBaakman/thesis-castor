using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class FragmentListElementController : MonoBehaviour
{
	/// <summary>
	/// The name of the fragment.
	/// </summary>
	public Text name;

	private Fragment fragment;

	void Start ()
	{
	}

	/// <summary>
	/// On the delete fragment button click the fragment is deleted from <FractureFragments cref="FractureFragments">
	/// </summary>
	public void onDeleteFragmentClick ()
	{
		Debug.Log ("Pressed the delete fragment button.");
	}

	/// <summary>
	/// On the change fragment color button click the user should be able to change the color of the fragment.
	/// </summary>
	public void onChangeColorFragmentClick ()
	{
		Debug.Log ("Pressed the change color fragment button.");
	}

	/// <summary>
	/// Ons the toggle fragment value changed, the fragment should be shown or hidden depending on its previous state.
	/// </summary>
	/// <param name="toggle">If set to <c>true</c> the fragment should be shown.</param>
	public void onToggleFragmentValueChanged (bool toggle)
	{
		Debug.Log ("Pressed the toggle fragment checkmark");
	}

	/// <summary>
	/// Sets the name of the fragment.
	/// </summary>
	/// <param name="name">Name contains the new name of the fragment.</param>
	public void SetName (string name)
	{
		this.name.text = name;
	}

	/// <summary>
	/// Sets the fragment.
	/// </summary>
	/// <param name="fragment">Fragment contains the reference to the fragment.</param>
	public void SetFragment (Fragment fragment)
	{
		this.fragment = fragment;
	}
}