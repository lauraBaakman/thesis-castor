using UnityEngine;
using UnityEngine.EventSystems;

public class HelpMessageSender : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public string Message;
    public string KeyBoard;
    public GameObject Receiver;

    private bool Hovering = false;

    private void Update()
    {
        if (Hovering) SendHelpMessage();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Hovering = true;
        SendHelpMessage();
    }

    private void SendHelpMessage(){
        Receiver.SendMessage(
            methodName: "OnMessage",
            value: new Ticker.Message.HelpMessage(
                text: Message,
                keyboard: KeyBoard
            ),
            options: SendMessageOptions.RequireReceiver
        );        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Hovering = false;
    }
}
