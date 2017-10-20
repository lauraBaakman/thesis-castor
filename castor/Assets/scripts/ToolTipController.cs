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
		float toolTipDelayInS = PlayerPrefs.GetFloat("help.tooltips.delayInS");

		timer = new Timer (toolTipDelayInS);

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
		timer.Activate ();
	}

	public void OnPointerExit (PointerEventData data)
	{
		timer.Deactivate ();
		HideToolTip ();
	}
}

public class Timer
{
	private float TimeToReach = 0;
	private float TimePassed = 0;

	private bool active = false;

	public Timer (float timeToReach)
	{
		TimeToReach = timeToReach;
	}

	public void Activate ()
	{
		active = true;
		Reset ();
	}

	public void Deactivate ()
	{
		active = false;
		Reset ();
	}

	public void Reset ()
	{
		TimePassed = 0;
	}

	public bool IsCompleted ()
	{
		return 	active && TimePassed >= TimeToReach;
	}

	public void update ()
	{
		if (active) {
			TimePassed += Time.deltaTime;	
		}
	}
}