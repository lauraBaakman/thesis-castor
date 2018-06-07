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
		private Message currentMessage;

		private delegate void WriteMethod(Message message);
		WriteMethod write;

		private static string noMessageText = "";

		private void Awake()
		{
			TickerText = FindTickerTextComponent();
			if (Timer == null) Timer = new Timer(OnMessageHasDecayed);

			write = WriteToTicker;
		}

		/// <summary>
		/// Run the Ticker in Headless mode, i.e. write everything to Debug.Log instead of to the Ticker.
		/// </summary>
		public void ToHeadLessMode()
		{
			if (Timer == null) Timer = new Timer(OnMessageHasDecayed);
			write = WriteToConsole;
		}

		/// <summary>
		/// On the receiving of a message this function is called, it displays the message and starts a timer for hiding it.
		/// </summary>
		/// <param name="message">Message.</param>
		public void OnMessage(Message message)
		{
			if (IsMoreImportantThanCurrentMessage(message)) Display(message);
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

		private void Display(Message message)
		{
			currentMessage = message;
			this.write(message);
			Timer.Set(message.DecayInS);
		}

		private void Update()
		{
			Timer.Tic();
		}

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

		private void WriteToTicker(Message message)
		{
			TickerText.text = message.Text;
			TickerText.color = message.Color;
		}

		private void WriteToConsole(Message message)
		{
			Debug.Log(message.Text);
		}
	}
}

