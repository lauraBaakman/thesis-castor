using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class FragmentListElementController : MonoBehaviour
{
	public Text name;

	private Fragment fragment;

	void Start ()
	{
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

	public void SetName (string name)
	{
		this.name.text = name;
	}

	public void SetFragment (Fragment fragment)
	{
		this.fragment = fragment;
	}
}