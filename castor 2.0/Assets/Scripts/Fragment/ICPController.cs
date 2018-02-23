using UnityEngine;
using Registration;

namespace Fragment
{
    public enum ICPFragmentType { Model, Static };

    [RequireComponent(typeof(ICPModelFragmentController))]
    [RequireComponent(typeof(ICPStaticFragmentController))]
    public class ICPController : MonoBehaviour, IICPListener
    {

        private ICPModelFragmentController modelFragmentController;
        private ICPStaticFragmentController staticFragmentController;

        public bool IsStaticFragment
        {
            get
            {
                return staticFragmentController.enabled && staticFragmentController.Active;
            }
        }

        public bool IsModelFragment
        {
            get
            {
                return modelFragmentController.enabled && modelFragmentController.Active;
            }
        }

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
            modelFragmentController.enabled = toggle;
            modelFragmentController.Active = toggle;
        }

        public void OnToggleIsICPFragment(ICPFragmentType type)
        {
            ToggleIsStaticFragment((type == ICPFragmentType.Static));
            ToggleIsModelFragment((type == ICPFragmentType.Model));
        }

        #region ICPListener
        public void OnICPPointsSelected(ICPPointsSelectedMessage message) { }

        public void OnICPCorrespondencesChanged(ICPCorrespondencesChanged message) { }

        public void OnStepCompleted() { }

        public void OnICPTerminated(ICPTerminatedMessage message)
        {
            ToggleIsModelFragment(false);
            ToggleIsStaticFragment(false);
        }

        public void OnPreparationStepCompleted(ICPPreparationStepCompletedMessage message) { }
        #endregion
    }
}


