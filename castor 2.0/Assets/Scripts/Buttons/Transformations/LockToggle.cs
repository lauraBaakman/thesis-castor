using UnityEngine;

namespace Buttons
{
    public class LockToggle : AbstractToggle
    {
        public override void OnToggleValueChangedAction(bool isOn)
        {
            Debug.Log("LockToggle: OnToggleValueChangedAction");
        }

        protected override void OnEnableAction()
        {
            Debug.Log("LockToggle: OnEnableAction");
        }
    }

}

