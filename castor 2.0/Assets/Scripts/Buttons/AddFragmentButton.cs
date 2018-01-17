using UnityEngine;

namespace Buttons{
    public class AddFragmentButton : AbstractButton
    {
        public override void OnClick()
        {
            AddFragment();
        }

        protected override void DetectKeyBoardShortCut()
        {
            if (Input.GetButton("Add Fragment")) AddFragment();
        }

        private void AddFragment(){
            Debug.Log("Time to get a fragment from a file!");
        }
    }    
}


