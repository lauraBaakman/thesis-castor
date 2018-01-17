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
                callBack: NotifyUser
            ).Import();
        }

        private void NotifyUser(Ticker.Message message)
        {
            //TODO Send mesage actually to ticker
            Debug.Log(message.Text);
        }
    }
}