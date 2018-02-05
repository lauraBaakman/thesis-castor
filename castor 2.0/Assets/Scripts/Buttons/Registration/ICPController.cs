using UnityEngine;
using System.Collections;
using Registration;

namespace Buttons
{
    public class ICPController : MonoBehaviour, Registration.IICPListener
    {
        public AbstractButton RegistrationButton;
        public AbstractRegistrationButton StepButton;
        public AbstractRegistrationButton PlayButton;
        public AbstractRegistrationButton StopButton;

        public GameObject SelectedFragments;

        private ICPRegisterer registerer;

        public void InitializeICP()
        {
            GameObject modelFragment, staticFragment;
            GetModelAndStaticFragment(out modelFragment, out staticFragment);

            registerer = new ICPRegisterer(
                modelFragment: modelFragment,
                staticFragment: staticFragment,
                settings: new Settings(
                    referenceTransform: SelectedFragments.transform
                )
            );
            registerer.AddListener(SelectedFragments);
            registerer.AddListener(this.gameObject);

            StepButton.Registerer = registerer;

            ToggleICPModeInGUI(true);
        }

        public void OnICPCorrespondencesChanged(ICPCorrespondencesChanged message) { }

        public void OnICPPointsSelected(ICPPointsSelectedMessage message) { }

        private void ToggleICPModeInGUI(bool toggle)
        {
            StepButton.Button.interactable = toggle;
            //PlayButton.Button.interactable = toggle;
            //StopButton.Button.interactable = toggle;
            RegistrationButton.Button.interactable = !toggle;
        }

        public void OnICPTerminated()
        {
            ToggleICPModeInGUI(false);
        }

        public void OnPreparetionStepCompleted()
        {
            StepButton.Button.interactable = true;
        }

        public void OnStepCompleted()
        {
            StepButton.Button.interactable = true;
        }

        private void GetModelAndStaticFragment(out GameObject modelFragment, out GameObject staticFragment)
        {
            //We are interested in all children of SelectedFragmetns that have meshrenderes, i.e. the meshes.
            MeshRenderer[] childMeshes = SelectedFragments.GetComponentsInChildren<MeshRenderer>();
            Debug.Assert(
                childMeshes.Length == 2,
                "Expected SelectedFragments to have exactly two children with MeshRenders, not " + childMeshes.Length
            );
            modelFragment = childMeshes[0].gameObject;
            staticFragment = childMeshes[1].gameObject;
        }
    }


}

