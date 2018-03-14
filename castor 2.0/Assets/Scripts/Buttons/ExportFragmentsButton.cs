using UnityEngine;
using Ticker;

namespace Buttons
{
    public class ExportFragmentsButton : AbstractButton
    {
        public GameObject FragmentsRoot;

        protected override void ExecuteButtonAction()
        {
            new IO.FragmentsExporter(
                fragmentsRoot: FragmentsRoot,
                onSucces: NotifyUserOfSucces,
                onFailure: NotifyUserOfFailure
            ).Export();
        }

        protected override bool HasDetectedKeyBoardShortCut()
        {
            return false;
        }

        private void NotifyUserOfSucces(string path, GameObject fragment)
        {
            Message.InfoMessage message = new Message.InfoMessage(
                string.Format(
                    "Exported the fragment '{0}' to the file {1}.",
                    fragment.name, path
                )
            );
            Receiver.Instance.SendMessage(
                methodName: "OnMessage",
                value: message,
                options: SendMessageOptions.RequireReceiver
            );
        }

        private void NotifyUserOfFailure(string path, GameObject fragment)
        {
            Message.ErrorMessage message = new Message.ErrorMessage(
                string.Format(
                    "Could not export the fragment {0}.",
                    fragment.name
                )
            );
            Receiver.Instance.SendMessage(
                methodName: "OnMessage",
                value: message,
                options: SendMessageOptions.RequireReceiver
            );
        }
    }
}

