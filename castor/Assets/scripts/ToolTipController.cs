using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolTipController : MonoBehaviour, 
	IPointerEnterHandler, 
	IPointerExitHandler
{

	public string tooltiptext;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void OnPointerEnter (PointerEventData data)
	{
		Debug.Log ("Enter");
	}

	public void OnPointerExit (PointerEventData data)
	{
		Debug.Log ("Exit");
	}
}
