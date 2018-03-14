using UnityEngine;
using Fragment;
using System.Collections.Generic;
using System;

namespace Fragments
{
    public class LockController : MonoBehaviour
    {
        public GameObject TransformPanel;

        public List<GameObject> LockedObjects
        {
            get { return GetLockedObjects(); }
        }

        public List<GameObject> UnLockedObjects
        {
            get { return GetUnLockedObjects(); }
        }

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

        private List<GameObject> GetLockedObjects()
        {
            List<GameObject> lockedObjects = new List<GameObject>();
            foreach (Transform child in transform)
            {
                if (child.GetComponent<StateTracker>().State.Locked) lockedObjects.Add(child.gameObject);
            }
            return lockedObjects;
        }

        private List<GameObject> GetUnLockedObjects()
        {
            List<GameObject> unLockedObjects = new List<GameObject>();
            foreach (Transform child in transform)
            {
                if (child.GetComponent<StateTracker>().State.UnLocked) unLockedObjects.Add(child.gameObject);
            }
            return unLockedObjects;
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


