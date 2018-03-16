using UnityEngine;
using Ticker;
using IO;

namespace Buttons
{
    public class ExportFragmentsButton : AbstractButton
    {
        public GameObject FragmentsRoot;

        protected override void ExecuteButtonAction()
        {
            new IO.FragmentsExporter(
                fragmentsRoot: FragmentsRoot,
                callback: NotifyUser
            ).Export();
        }

        protected override bool HasDetectedKeyBoardShortCut()
        {
            return false;
        }

        private void NotifyUser(IO.WriteResult result)
        {
            Receiver.Instance.SendMessage(
                methodName: "OnMessage",
                value: result.ToTickerMessage(),
                options: SendMessageOptions.RequireReceiver
            );
        }
    }
}

