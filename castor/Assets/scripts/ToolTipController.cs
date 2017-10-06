using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolTipController : MonoBehaviour, 
	IPointerEnterHandler, 
	IPointerExitHandler
{
	public GameObject ToolTip;

	private Timer timer;

	void Start ()
	{
		timer = new Timer (1.0f);

		HideToolTip ();
		AddToToolTipLayer (ToolTip);
	}

	void Update ()
	{
		timer.update ();
		if (timer.IsCompleted ()) {
			ShowTooltip ();
		}
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
		timer.Start ();
	}

	public void OnPointerExit (PointerEventData data)
	{
		timer.Reset ();
		HideToolTip ();
	}
}

public class Timer
{
	private float TimeToReach;
	private float TimePassed = 0;

	public Timer (float timeToReach)
	{
		TimeToReach = timeToReach;
	}

	public void Start ()
	{
		TimePassed = 0;
	}

	public void Reset ()
	{
		TimePassed = 0;
	}

	public bool IsCompleted ()
	{
		return 	TimePassed >= TimeToReach;
	}

	public void update ()
	{
		TimePassed += Time.deltaTime;
	}
}