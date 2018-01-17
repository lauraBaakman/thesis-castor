using UnityEngine;

namespace Ticker
{
    public class Receiver : MonoBehaviour
    {
        public void OnMessage(Message message)
        {
            Debug.Log("Received message: " + message.Text);
        }
    }
}

