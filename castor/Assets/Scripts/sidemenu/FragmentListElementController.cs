using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class FragmentListElementController : MonoBehaviour
{
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
}