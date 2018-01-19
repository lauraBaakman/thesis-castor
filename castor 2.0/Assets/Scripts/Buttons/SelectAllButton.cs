using UnityEngine;

namespace Buttons
{
    public class SelectAllButton : AbstractButton
    {
        public override void OnClick()
        {
            SelectAll();
        }

        protected override void DetectKeyBoardShortCut()
        {
            if (Input.GetButtonDown("Select All")) SelectAll();
        }

        private void SelectAll()
        {
            Debug.Log("Select All!");
        }
    }
}

