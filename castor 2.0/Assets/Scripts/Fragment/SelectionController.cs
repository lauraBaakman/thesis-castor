using Registration;
using RTEditor;
using UnityEngine;

namespace Fragment
{
    public class SelectionController : MonoBehaviour, IFragmentStateElementToggled, RTEditor.IRTEditorEventListener, Registration.IICPStartEndListener
    {

        private GameObject SelectedFragments;
        private GameObject DeselectedFragments;



        public bool Selectable
        {
            get { return selectable; }
            set
            {
                selectable = value;
                if (!selectable) DeselectFragment();
            }
        }
        private bool selectable;

        private void Start()
        {
            DeselectedFragments = transform.root.gameObject;
            SelectedFragments = DeselectedFragments.FindChildByName("Selected Fragments");

            Selectable = true;
        }

        private void SelectFragment()
        {
            SendMessage(
                methodName: "OnToggleSelectionState",
                value: true,
                options: SendMessageOptions.RequireReceiver
            );
        }

        private void DeselectFragment()
        {
            SendMessage(
                methodName: "OnToggleSelectionState",
                value: false,
                //Breaks if we are deselecting the fragment before destroying it
                options: SendMessageOptions.DontRequireReceiver
            );
        }

        #region IFragmentStateElementToggled
        public void OnToggledLockedState(bool locked) { }

        public void OnToggleSelectionState(bool selected)
        {
            gameObject.transform.parent = selected ? SelectedFragments.transform : DeselectedFragments.transform;
        }
        #endregion

        #region IRTEditorEventListener
        public bool OnCanBeSelected(ObjectSelectEventArgs selectEventArgs)
        {
            return Selectable;
        }

        public void OnSelected(ObjectSelectEventArgs selectEventArgs)
        {
            SelectFragment();
        }

        public void OnDeselected(ObjectDeselectEventArgs deselectEventArgs)
        {
            DeselectFragment();
        }

        public void OnAlteredByTransformGizmo(Gizmo gizmo) { }
        #endregion

        #region IICPStartEndListener
        public void OnICPStarted()
        {
            Selectable = false;
        }

        public void OnICPTerminated(ICPTerminatedMessage message)
        {
            Selectable = true;
        }
        #endregion
    }

}

