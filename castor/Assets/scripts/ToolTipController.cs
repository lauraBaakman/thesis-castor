using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolTipController : MonoBehaviour, 
	IPointerEnterHandler, 
	IPointerExitHandler
{
	public GameObject ToolTip;

	void Start ()
	{
		HideToolTip ();
	}

	void Update ()
	{
		
	}

	private void ShowTooltip ()
	{
		ToolTip.gameObject.SetActive (true);	
	}

	private void HideToolTip ()
	{
		ToolTip.gameObject.SetActive (false);	
	}

	public void OnPointerEnter (PointerEventData data)
	{
		ShowTooltip ();
	}

	public void OnPointerExit (PointerEventData data)
	{
		HideToolTip ();
	}
}
