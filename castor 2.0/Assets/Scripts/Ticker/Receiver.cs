using UnityEngine;
using UnityEngine.UI;

using Utils;

namespace Ticker
{
    public class Receiver : MonoBehaviour
    {
        private Text TickerText;
        private Timer Timer;

        private static string noMessageText = "";

        private void Awake()
        {
            TickerText = FindTickerTextComponent();
            Timer = new Timer(OnMessageHasDecayed);
        }

        private void Update()
        {
            Timer.Tic();
        }

        private Text FindTickerTextComponent(){
            GameObject tickerTextGO = gameObject.FindChildByName("Ticker Text");
            TickerText = tickerTextGO.GetComponent<Text>();            

            if(!TickerText){
                Debug.LogError("Could not find the Text Component of Ticker Text");
            }

            return TickerText;
        }

        private void OnMessageHasDecayed(){
            TickerText.text = noMessageText;
        }

        public void OnMessage(Message message)
        {
            DisplayMessage(message);
            Timer.Set(message.DecayInS);
        }

        private void DisplayMessage(Message message){
            TickerText.text = message.Text;
            TickerText.color = message.Color;
        }
    }
}

