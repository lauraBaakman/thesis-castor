using UnityEngine;

namespace Ticker
{
    public class Sender : MonoBehaviour
    {
        public void OnSendMessageToTicker(Message message)
        {
            Receiver.Instance.SendMessage(
                methodName: "OnMessage",
                value: message,
                options: SendMessageOptions.RequireReceiver
            );
        }
    }

}

