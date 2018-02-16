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
            staticFragmentController = GetComponent<ICPStaticFragmentController>();

            ToggleIsStaticFragment(false);
            ToggleIsModelFragment(false);
        }

        private void ToggleIsStaticFragment(bool toggle)
        {
            staticFragmentController.enabled = toggle;
            staticFragmentController.Active = toggle;
        }

        private void ToggleIsModelFragment(bool toggle)
        {
            Debug.Log("Temporarily always disable the model fragment stuff!");
            modelFragmentController.enabled = false;
            modelFragmentController.Active = false;
        }

        public void OnToggleIsICPFragment(ICPFragmentType type)
        {
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
            ToggleIsModelFragment(false);
            ToggleIsStaticFragment(false);
        }
        #endregion
    }
}


