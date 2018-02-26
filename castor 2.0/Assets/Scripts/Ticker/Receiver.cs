﻿using UnityEngine;
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

        private static string noMessageText = "";

        /// <summary>
        /// On the receiving of a message this function is called, it displays the message and starts a timer for hiding it.
        /// </summary>
        /// <param name="message">Message.</param>
        public void OnMessage(Message message)
        {
            DisplayMessage(message);
            Timer.Set(message.DecayInS);
        }

        private void Awake()
        {
            TickerText = FindTickerTextComponent();
            Timer = new Timer(OnMessageHasDecayed);
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
        }

        private void DisplayMessage(Message message)
        {
            TickerText.text = message.Text;
            TickerText.color = message.Color;
        }
    }
}

