using UnityEngine;

namespace Fragment
{
    [RequireComponent(typeof(StateTracker))]
    public class LockController : MonoBehaviour
    {
        StateTracker stateTracker;

        void Start()
        {
            stateTracker = GetComponent<StateTracker>();
        }

        private void Update()
        {
            if (stateTracker.State.Locked && transform.hasChanged) Debug.LogError("The transform of a locked object has changed, that should not be possible!");
        }
    }
}


