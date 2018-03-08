using UnityEngine;

namespace Fragment
{
    /// <summary>
    /// State tracker keep strack of the current state of the fragment.
    /// 
    /// Any component can change an element of the state of the fragment by calling
    /// "OnToggledLockedState" or "OnToggleSelectionState", if the state of the object
    /// is changed with these method a message with the general new state is sent to OnStateChanged.
    /// </summary>
    public class StateTracker : MonoBehaviour, IFragmentStateElementToggled
    {
        public FragmentState State
        {
            get { return state; }
        }
        private FragmentState state;

        void Start()
        {
            state = new FragmentState(locked: false, selected: false);
        }

        public void OnToggledLockedState(bool locked)
        {
            state.locked = locked;

            NotifyOtherComponentsOfStateChange();
        }

        public void OnToggleSelectionState(bool selected)
        {
            state.selected = selected;

            NotifyOtherComponentsOfStateChange();
        }

        public void NotifyOtherComponentsOfStateChange()
        {
            SendMessage(
                methodName: "OnStateChanged",
                value: state,
                options: SendMessageOptions.RequireReceiver
            );
        }
    }

    public class FragmentState
    {
        internal bool locked = false;
        public bool Locked
        {
            get
            {
                return locked;
            }
        }

        internal bool selected = false;
        public bool Selected
        {
            get
            {
                return selected;
            }
        }

        public FragmentState(bool locked, bool selected)
        {
            this.locked = locked;
            this.selected = selected;
        }

        public override string ToString()
        {
            return string.Format(
                "State( locked : {0}, selected: {1})",
                locked, selected
            );
        }
    }
}

