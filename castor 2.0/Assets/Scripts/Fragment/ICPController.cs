using UnityEngine;
using Registration.Messages;

namespace Fragment
{
    public enum ICPFragmentType { Model, Static };

    [RequireComponent(typeof(ICPModelFragmentController))]
    [RequireComponent(typeof(ICPStaticFragmentController))]
    public class ICPController : MonoBehaviour, IICPListener
    {

        private ICPModelFragmentController modelFragmentController;
        private ICPStaticFragmentController staticFragmentController;

        private Transform ICPFragments;
        private Transform Fragments;

        private static string ICPFragmentsName = "ICP Fragments";

        public void Awake()
        {
            GameObject FragmentsGO = transform.parent.gameObject;
            Debug.Assert(FragmentsGO, "Could not find the parent gameobject of this gameobject");
            Fragments = FragmentsGO.transform;

            GameObject ICPFragmentsGO = FragmentsGO.FindChildByName(ICPFragmentsName);
            Debug.Assert(ICPFragmentsGO, "Could not find the gameobject with the name " + ICPFragmentsName);
            ICPFragments = ICPFragmentsGO.transform;
        }

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

        public ICPFragmentType FragmentType
        {
            get
            {
                if (IsStaticFragment) return ICPFragmentType.Static;
                if (IsModelFragment) return ICPFragmentType.Model;

                throw new System.Exception("The fragment is not involved in ICP, do not request its type.");
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
        public void OnStepCompleted(ICPStepCompletedMessage message) { }

        public void OnICPTerminated(ICPTerminatedMessage message)
        {
            ToggleIsModelFragment(false);
            ToggleIsStaticFragment(false);

            transform.parent = Fragments;
        }

        /// <summary>
        /// If the first step is completed set the parent of this gameobject 
        /// to the "ICP Fragments" gameobject. 
        /// </summary>
        /// <param name="message">Message.</param>
        public void OnPreparationStepCompleted(ICPPreparationStepCompletedMessage message)
        {
            if (message.IsFirstPreparationStep()) transform.parent = ICPFragments;
        }
        #endregion
    }
}


