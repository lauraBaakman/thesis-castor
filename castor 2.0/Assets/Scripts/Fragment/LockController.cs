using UnityEngine;
using RTEditor;

namespace Fragment
{
    [RequireComponent(typeof(StateTracker))]
    public class LockController : MonoBehaviour, IFragmentStateChanged
    {
        private GameObject transformationPanel;

        private StateTracker stateTracker;
        private static string transformationPanelName = "Transformation Panel";

        void Start()
        {
            stateTracker = GetComponent<StateTracker>();
            transformationPanel = findTransformationPanel();
        }

        private void Update()
        {
            if (stateTracker.State.Locked && transform.hasChanged) Debug.LogWarning("The transform of a locked object has changed, that should not be possible!");
        }

        private GameObject findTransformationPanel()
        {
            GameObject panel = GameObject.Find(transformationPanelName);
            if (panel == null) Debug.LogWarning("Could not find the transformation panel.");
            return panel;
        }

        public void OnStateChanged(FragmentState newState)
        {
            if (newState.Deselected) return;

            if (newState.SelectedLockedObject) EditorGizmoSystem.Instance.TurnOffGizmos();

            transformationPanel.BroadcastMessage(
                methodName: "OnToggleButtonInteractability",
                parameter: newState.UnLocked,
                options: SendMessageOptions.RequireReceiver
            );
        }
    }
}


