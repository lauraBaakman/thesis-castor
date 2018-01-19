using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Help message sender sends help message on hover to be displayed by the specified receiver.
/// </summary>
public class HelpMessageSender : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    /// <summary>
    /// The message to be sent.
    /// </summary>
    [TextArea]
    public string Message;

    /// <summary>
    /// The key board shortcut for this operation.
    /// </summary>
    public string KeyBoard;

    /// <summary>
    /// The receiver of the message.
    /// </summary>
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
