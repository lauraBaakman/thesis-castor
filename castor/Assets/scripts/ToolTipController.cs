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
		AddToToolTipLayer (ToolTip);
	}

	void Update ()
	{
		
	}

	private void AddToToolTipLayer (GameObject toolTip)
	{
		int layer = LayerMask.NameToLayer ("ToolTips");
		toolTip.layer = layer;
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
