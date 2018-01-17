using UnityEngine;
using UnityEngine.UI;

namespace Ticker
{
    public class Receiver : MonoBehaviour
    {
        private Text TickerText;

        private void Awake()
        {
            TickerText = FindTickerTextComponent();
        }

        private Text FindTickerTextComponent(){
            GameObject tickerTextGO = gameObject.FindChildByName("Ticker Text");
            TickerText = tickerTextGO.GetComponent<Text>();            

            if(!TickerText){
                Debug.LogError("Could not find the Text Component of Ticker Text");
            }

            return TickerText;
        }

        public void OnMessage(Message message)
        {
            DisplayMessage(message);
        }

        private void DisplayMessage(Message message){
            TickerText.text = message.Text;
            TickerText.color = message.Color;
        }
    }
}

