using System.Collections;
using System.Collections.Generic;
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
                callback: NotifyUser
            ).Export();
        }

        protected override bool HasDetectedKeyBoardShortCut()
        {
            return false;
        }

        private void NotifyUser(string path, GameObject fragment)
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
    }
}

