using UnityEngine;
using UnityEngine.EventSystems;

public class HelpMessageSender : MonoBehaviour, IPointerEnterHandler
{

    public string Message;
    public string KeyBoard;
    public GameObject Receiver;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Receiver.SendMessage(
            methodName: "OnMessage",
            value: new Ticker.Message.HelpMessage(
                text: Message,
                keyboard: KeyBoard
            ),
            options: SendMessageOptions.RequireReceiver
        );
    }
}
