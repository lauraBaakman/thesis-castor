using UnityEngine;
using System.Collections;
using Registration;

namespace Buttons
{
    public class ICPController : MonoBehaviour, Registration.IICPListener
    {
        public AbstractButton RegistrationButton;
        public AbstractButton StepButton;
        public AbstractButton PlayButton;
        public AbstractButton StopButton;

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

            while (!registerer.HasTerminated)
            {
                registerer.PrepareStep();
                registerer.Step();
            }
        }

        public void OnICPCorrespondencesChanged(ICPCorrespondencesChanged message) { }

        public void OnICPPointsSelected(ICPPointsSelectedMessage message) { }

        public IEnumerator OnICPTerminated()
        {
            Debug.Log("Buttons:ICPController:OnICPTerminated");
            yield return new WaitForSeconds(1);
        }

        public void OnPreparetionStepCompleted()
        {
            Debug.Log("Buttons:ICPController:OnPreparetionStepCompleted");
        }

        public void OnStepCompleted()
        {
            Debug.Log("Buttons:ICPController:OnStepCompleted");
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

