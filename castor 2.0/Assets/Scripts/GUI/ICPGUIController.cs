using UnityEngine;
using Registration;
using Registration.Messages;

using Buttons.RegistrationButtons;

namespace GraphicalUI
{
    public class ICPGUIController : MonoBehaviour, IICPListener
    {
        public RegistrationButton RegistrationButton;
        public AbstractRegistrationButton StepButton;
        public AbstractRegistrationButton PlayButton;
        public AbstractRegistrationButton StopButton;

        public GameObject ICPFragments;
        public GameObject SelectedFragments;

        private ICPRegisterer registerer;

        private void Awake()
        {
            RegistrationButton.ICPGUIController = this;
        }

        public void InitializeICP()
        {
            GameObject modelFragment, staticFragment;
            GetModelAndStaticFragment(out modelFragment, out staticFragment);

            registerer = new ICPRegisterer(
                modelFragment: modelFragment,
                staticFragment: staticFragment,
                settings: new Settings(
                    referenceTransform: ICPFragments.transform
                )
            );

            //Note these objects do not receive the ICPStarted message.
            registerer.AddListener(ICPFragments);
            registerer.AddListener(this.gameObject);

            StepButton.Registerer = registerer;
            PlayButton.Registerer = registerer;
            StopButton.Registerer = registerer;

            ToggleICPModeInGUI(true);
        }

        private void ToggleICPModeInGUI(bool toggle)
        {
            StepButton.Button.interactable = toggle;
            PlayButton.Button.interactable = toggle;
            StopButton.Button.interactable = toggle;
            RegistrationButton.Button.interactable = !toggle;
        }

        public void OnStepCompleted(ICPStepCompletedMessage message)
        {
            StepButton.Button.interactable = true;
        }

        private void GetModelAndStaticFragment(out GameObject modelFragment, out GameObject staticFragment)
        {
            //We are interested in all children of SelectedFragments that have meshrenderes, i.e. the meshes.
            MeshRenderer[] childMeshes = SelectedFragments.GetComponentsInChildren<MeshRenderer>();
            Debug.Assert(
                childMeshes.Length == 2,
                "Expected SelectedFragments to have exactly two children with MeshRenders, not " + childMeshes.Length
            );
            modelFragment = childMeshes[0].gameObject;
            staticFragment = childMeshes[1].gameObject;
        }

        public void OnICPTerminated(ICPTerminatedMessage message)
        {
            ToggleICPModeInGUI(false);
        }

        public void OnPreparationStepCompleted(ICPPreparationStepCompletedMessage message)
        {
            StepButton.Button.interactable = true;
        }
    }


}

