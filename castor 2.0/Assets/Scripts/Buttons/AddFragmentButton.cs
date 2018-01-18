using UnityEngine;

namespace Buttons
{
    public class AddFragmentButton : AbstractButton
    {
        public GameObject FragmentsRoot;

        public override void OnClick()
        {
            AddFragment();
        }

        protected override void DetectKeyBoardShortCut()
        {
            if (Input.GetButton("Add Fragment")) AddFragment();
        }

        private void AddFragment()
        {
            new IO.FragmentsImporter(
                fragmentParent: FragmentsRoot,
                callBack: NotifyUserOfAddedFragment
            ).Import();
        }

        private void NotifyUserOfAddedFragment(string path, GameObject fragment)
        {
            Ticker.Message.InfoMessage message = new Ticker.Message.InfoMessage(
                string.Format(
                    "Added a fragment '{0}' to the scene from the file {1}.",
                    fragment.name, path
                )
            );
            SendMessage(
                methodName: "OnSendMessageToTicker",
                value: message,
                options: SendMessageOptions.RequireReceiver
            );
        }
    }
}