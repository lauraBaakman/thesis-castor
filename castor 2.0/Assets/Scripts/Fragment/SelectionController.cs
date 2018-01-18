using UnityEngine;

namespace Fragment
{
    public class SelectionController : MonoBehaviour, IFragmentStateDependent
    {

        private GameObject SelectedFragments;
        private GameObject DeselectedFragments;

        EventHandler MouseDownHandler;

        private void Awake()
        {
            MouseDownHandler = SelectFragment;
        }

        private void Start()
        {
            DeselectedFragments = transform.root.gameObject;
            SelectedFragments = DeselectedFragments.FindChildByName("Selected Fragments");
        }

        public void OnMouseDown()
        {
            MouseDownHandler();
        }

        private void SelectFragment()
        {
            SendMessage(
                methodName: "OnToggleSelectionState",
                value: true,
                options: SendMessageOptions.DontRequireReceiver
            );
        }

        private void DeselectFragment()
        {
            SendMessage(
                methodName: "OnToggleSelectionState",
                value: false,
                options: SendMessageOptions.DontRequireReceiver
            );
        }

        public void OnToggledLockedState(bool locked)
        {
            //No need to do antyhing
        }

        public void OnToggleSelectionState(bool selected)
        {
            MouseDownHandler = selected ? new EventHandler(DeselectFragment) : new EventHandler(SelectFragment);
            gameObject.transform.parent = selected ? SelectedFragments.transform : DeselectedFragments.transform;
        }
    }

}

