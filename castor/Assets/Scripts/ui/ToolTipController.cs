using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Ensures that the tooltip is shown when needed.
/// </summary>
public class ToolTipController : MonoBehaviour, 
	IPointerEnterHandler, 
	IPointerExitHandler
{
	/// <summary>
	/// The tool tip that shows up when hovering over the object.
	/// </summary>
	public GameObject ToolTip;

	private Timer Timer;

	void Start ()
	{
		float toolTipDelayInS = PlayerPrefs.GetFloat("help.tooltips.delayInS");

		Timer = new Timer (toolTipDelayInS);

		HideToolTip ();
		AddToToolTipLayer (ToolTip);
	}

	void Update ()
	{
		Timer.Update ();
		if (Timer.IsCompleted ()) {
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

	/// <summary>
	/// Raises the pointer enter event, this activates the timer that keeps track of how long one has hovered over this element..
	/// </summary>
	/// <param name="data">Data contains unused point event data.</param>
	public void OnPointerEnter (PointerEventData data)
	{
		Timer.Activate ();
	}

	/// <summary>
	/// Raises the pointer exit event, this stops the timer and hides the tooltip, if it was visible.
	/// </summary>
	/// <param name="data">Data contains unused pointer event data.</param>
	public void OnPointerExit (PointerEventData data)
	{
		Timer.Deactivate ();
		HideToolTip ();
	}
}

/// <summary>
/// Timer implements a timer.
/// </summary>
public class Timer
{
    private float TimeToReachInS = 0;
    private float TimePassedInS = 0;

	private bool Active = false;

	/// <summary>
	/// Initializes a new instance of the <see cref="Timer"/> class.
	/// </summary>
	/// <param name="timeToReach">Time to reach, the time the timer should run before it 'rings'.</param>
	public Timer (float timeToReach)
	{
		TimeToReachInS = timeToReach;
	}

	/// <summary>
	/// Activate the timer.
	/// </summary>
	public void Activate ()
	{
		Active = true;
		Reset ();
	}

	/// <summary>
	/// Deactivate and reset the timer.
	/// </summary>
	public void Deactivate ()
	{
		Active = false;
		Reset ();
	}

	/// <summary>
	/// Reset the timer.
	/// </summary>
	public void Reset ()
	{
		TimePassedInS = 0;
	}

	/// <summary>
	/// Determines whether the timer has been completed, i.e. if <TimeToReach cref="TimeToReachInS"> has passed since the timer was activated
	/// </summary>
	/// <returns><c>true</c> if this instance is completed; otherwise, <c>false</c>.</returns>
	public bool IsCompleted ()
	{
		return 	Active && TimePassedInS >= TimeToReachInS;
	}

	/// <summary>
	/// Update this instance with the time between frames.
	/// </summary>
	public void Update ()
	{
		if (Active) {
			TimePassedInS += Time.deltaTime;	
		}
	}
}