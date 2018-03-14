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
            state.Locked = locked;

            NotifyOtherComponentsOfStateChange();
        }

        public void OnToggleSelectionState(bool selected)
        {
            state.Selected = selected;

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
        private bool locked = false;
        public bool Locked
        {
            get { return locked; }
            internal set
            {
                this.lockedStatuesChanged = (this.locked != value);
                this.locked = value;
            }
        }


        private bool lockedStatuesChanged = false;
        public bool LockedStatuesChanged
        {
            get { return lockedStatuesChanged; }
        }

        private bool selected = false;
        public bool Selected
        {
            get { return selected; }
            internal set
            {
                this.selectedStatusChanged = (this.selected != value);
                this.selected = value;
            }
        }

        private bool selectedStatusChanged = false;
        public bool SelectedStatusChanged
        {
            get { return selectedStatusChanged; }
        }


        public bool Deselected
        {
            get { return !Selected; }
        }

        public bool UnLocked
        {
            get { return !Locked; }
        }

        public bool SelectedLockedObject
        {
            get { return Locked && Selected; }
        }

        public bool SelectedUnLockedObject
        {
            get { return UnLocked && Selected; }
        }

        public FragmentState(bool locked, bool selected)
        {
            this.locked = locked;
            this.selected = selected;
        }

        public override string ToString()
        {
            return string.Format(
                "State( locked : {0} (changed: {1}), selected: {1} (changed: {2}))",
                locked, lockedStatuesChanged,
                selected, selectedStatusChanged
            );
        }
    }
}

