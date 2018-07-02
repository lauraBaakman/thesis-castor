using UnityEngine;
using UnityEngine.UI;

using Utils;

namespace Ticker
{
	/// <summary>
	/// Receiver of messages that displays them.
	/// </summary>
	[RequireComponent(typeof(Text))]
	public class Receiver : RTEditor.MonoSingletonBase<Receiver>
	{
		private Text TickerText;
		private Timer Timer;
		private Message currentMessage = null;

		private delegate void DisplayMethod(Message message);
		private delegate void UpdateMethod();
		DisplayMethod Display;
		UpdateMethod ModeSpecificUpdate;

		private static string noMessageText = "";

		private void Start()
		{
			if (CLI.Instance.CLIModeActive) CLIAwake();
			else GUIAwake();
		}

		private void CLIAwake()
		{
			Display = DisplayWithConsole;
			ModeSpecificUpdate = CLIUpdate;
		}

		private void GUIAwake()
		{
			Timer = new Timer(OnMessageHasDecayed);
			TickerText = FindTickerTextComponent();

			Display = DisplayWithTicker;
			ModeSpecificUpdate = GUIUpdate;
		}

		/// <summary>
		/// On the receiving of a message this function is called, it displays the message and starts a timer for hiding it.
		/// </summary>
		/// <param name="message">Message.</param>
		public void OnMessage(Message message)
		{
			if (IsMoreImportantThanCurrentMessage(message)) this.Display(message);
		}

		private bool IsMoreImportantThanCurrentMessage(Message message)
		{
			if (!TickerIsShowingMessage()) return true;

			return message.Priority >= currentMessage.Priority;
		}

		private bool TickerIsShowingMessage()
		{
			return currentMessage != null;
		}

		private void ResetCurretMessage()
		{
			currentMessage = null;
		}

		private void Update()
		{
			ModeSpecificUpdate();
		}

		private void GUIUpdate()
		{
			Timer.Tic();
		}

		private void CLIUpdate() { }

		private Text FindTickerTextComponent()
		{
			GameObject tickerTextGO = gameObject.FindChildByName("Ticker Text");
			TickerText = tickerTextGO.GetComponent<Text>();

			if (!TickerText) Debug.LogError("Could not find the Text Component of Ticker Text");
			return TickerText;
		}

		private void OnMessageHasDecayed()
		{
			TickerText.text = noMessageText;
			ResetCurretMessage();
		}

		private void DisplayWithTicker(Message message)
		{
			currentMessage = message;
			WriteToTicker(message);
			Timer.Set(message.DecayInS);
		}

		private void WriteToTicker(Message message)
		{
			TickerText.text = message.Text;
			TickerText.color = message.Color;
		}

		private void DisplayWithConsole(Message message)
		{
			Debug.Log(message.Text);
		}
	}
}

