using UnityEngine;

namespace Ticker
{
    public class Sender : MonoBehaviour
    {
        public GameObject Receiver;

        public void OnSendMessageToTicker(Message message)
        {
            Receiver.SendMessage(
                methodName: "OnMessage",
                value: message,
                options: SendMessageOptions.RequireReceiver
            );
        }
    }

}

