using RTEditor;
using UnityEngine;

namespace Fragment
{
    public class SelectionController : MonoBehaviour, IFragmentStateElementToggled, RTEditor.IRTEditorEventListener
    {

        private GameObject SelectedFragments;
        private GameObject DeselectedFragments;

        private void Awake()
        {
        }

        private void Start()
        {
            DeselectedFragments = transform.root.gameObject;
            SelectedFragments = DeselectedFragments.FindChildByName("Selected Fragments");
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
                options: SendMessageOptions.RequireReceiver
            );
        }

        public void OnToggledLockedState(bool locked) { }

        public void OnToggleSelectionState(bool selected)
        {
            gameObject.transform.parent = selected ? SelectedFragments.transform : DeselectedFragments.transform;
        }

        public bool OnCanBeSelected(ObjectSelectEventArgs selectEventArgs)
        {
            return true;
        }

        public void OnSelected(ObjectSelectEventArgs selectEventArgs)
        {
            SelectFragment();
        }

        public void OnDeselected(ObjectDeselectEventArgs deselectEventArgs)
        {
            DeselectFragment();
        }

        public void OnAlteredByTransformGizmo(Gizmo gizmo)
        {
            //Not relevant
        }
    }

}

