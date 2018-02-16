using UnityEngine;
using System.Collections;
using Registration;

namespace Fragment
{
    public enum ICPFragmentType { Model, Static };

    [RequireComponent(typeof(ICPModelFragmentController))]
    [RequireComponent(typeof(ICPStaticFragmentController))]
    public class ICPController : MonoBehaviour, Registration.IICPListener
    {

        private ICPModelFragmentController modelFragmentController;
        private ICPStaticFragmentController staticFragmentController;

        void Start()
        {
            modelFragmentController = GetComponent<ICPModelFragmentController>();
            modelFragmentController.enabled = false;

            staticFragmentController = GetComponent<ICPStaticFragmentController>();
            staticFragmentController.enabled = false;
        }

        private void ToggleIsStaticFragment(bool toggle)
        {
            staticFragmentController.enabled = toggle;
        }

        private void ToggleIsModelFragment(bool toggle)
        {
            modelFragmentController.enabled = toggle;
        }

        public void OnToggleIsICPFragment(ICPFragmentType type)
        {
            Debug.Log("OnToggleISICPFragment");
            ToggleIsStaticFragment((type == ICPFragmentType.Static));
            ToggleIsModelFragment((type == ICPFragmentType.Model));
        }

        #region ICPListener
        public void OnICPPointsSelected(ICPPointsSelectedMessage message) { }

        public void OnICPCorrespondencesChanged(ICPCorrespondencesChanged message) { }

        public void OnPreparetionStepCompleted() { }

        public void OnStepCompleted() { }

        public void OnICPTerminated(ICPTerminatedMessage message)
        {
            Debug.Log("ICPController: OnICPTerminated");
            ToggleIsModelFragment(false);
            ToggleIsStaticFragment(false);
        }
        #endregion
    }
}


