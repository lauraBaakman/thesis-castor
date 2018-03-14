using UnityEngine;
using Fragment;

namespace Fragments
{
    public class LockController : MonoBehaviour
    {
        public GameObject TransformPanel;

        public void OnChildFragmentStateChanged()
        {
            bool anyLockedFragments = AreAnySelectedFragmentsLocked();

            if (anyLockedFragments) TurnOfGizmos();

            ToggleTransformPanel(!anyLockedFragments);
        }

        public bool AreAnySelectedFragmentsLocked()
        {
            foreach (Transform child in transform)
            {
                if (child.GetComponent<StateTracker>().State.Locked) return true;
            }
            return false;
        }

        private void TurnOfGizmos()
        {
            RTEditor.EditorGizmoSystem.Instance.TurnOffGizmos();
        }

        private void ToggleTransformPanel(bool toggle)
        {
            TransformPanel.BroadcastMessage(
                methodName: "OnToggleButtonInteractability",
                parameter: toggle,
                options: SendMessageOptions.RequireReceiver
            );
        }
    }
}


