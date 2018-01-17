using UnityEngine;

namespace Buttons{
    public class AddFragmentButton : AbstractButton
    {
        public override void OnClick()
        {
            Debug.Log("Detected A Click");
            AddFragment();
        }

        protected override void DetectKeyBoardShortCut()
        {
            Debug.Log("Detected A KeyBoard Shortcut");
        }

        private void AddFragment(){
            Debug.Log("Time to get a fragment from a file!");
        }
    }    
}


