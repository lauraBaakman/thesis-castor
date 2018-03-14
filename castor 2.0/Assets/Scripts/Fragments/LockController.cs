using UnityEngine;
using Fragment;

namespace Fragments
{
    public class LockController : MonoBehaviour
    {
        public GameObject TransformPanel;

        private void Update()
        {
            bool anyLockedFragments = AreAnySelectedFragmentsLocked();

            if (anyLockedFragments) TurnOfGizmos();
            ToggleTransformPanel(anyLockedFragments);
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

        private void ToggleTransformPanel(bool anyLockedFragments)
        {
            TransformPanel.BroadcastMessage(
                methodName: "OnToggleButtonInteractability",
                parameter: !anyLockedFragments,
                options: SendMessageOptions.RequireReceiver
            );
        }
    }
}


