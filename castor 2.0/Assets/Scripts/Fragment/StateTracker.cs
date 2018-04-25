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

        void Awake()
        {
            state = new FragmentState(locked: false, selected: false);
        }

        public void OnToggledLockedState(bool locked)
        {
            state.Locked = locked;

            BroadCastStateChange();
        }

        public void OnToggleSelectionState(bool selected)
        {
            state.Selected = selected;

            BroadCastStateChange();
        }

        public void BroadCastStateChange()
        {
            NotifyComponents();
            NotifyParent();
        }

        private void NotifyComponents()
        {
            SendMessage(
                methodName: "OnStateChanged",
                value: state,
                options: SendMessageOptions.RequireReceiver
            );
        }

        private void NotifyParent()
        {
            //Do not set in Start, the parent changes.
            GameObject parent = transform.parent.gameObject;
            parent.SendMessage(
                methodName: "OnChildFragmentStateChanged",
                value: null,
                options: SendMessageOptions.DontRequireReceiver
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
                this.lockedStatusChanged = (this.locked != value);
                this.locked = value;
            }
        }


        private bool lockedStatusChanged = false;
        public bool LockedStatusChanged
        {
            get { return lockedStatusChanged; }
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
                locked, lockedStatusChanged,
                selected, selectedStatusChanged
            );
        }
    }
}

