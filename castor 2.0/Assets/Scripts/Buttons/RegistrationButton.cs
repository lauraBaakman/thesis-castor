using UnityEngine;
using UnityEngine.UI;

using Registration;

namespace Buttons {
    public class RegistrationButton : AbstractButton, Fragments.ISelectionControllerListener {
        private int RequiredNumberOfSelectedFragments = 2;

        public GameObject SelectedFragments;


        public void OnEnable()
        {
            OnNumberOfSelectedObjectsChanged(RTEditor.EditorObjectSelection.Instance.NumberOfSelectedObjects);
        }

        /// <summary>
        /// Called once the number of selected objects has changed, the button should only be active when two objects are selected.
        /// </summary>
        /// <param name="currentCount">Current nmber of selected fragments.</param>
        public void OnNumberOfSelectedObjectsChanged( int currentCount )
        {
            Button.interactable = (currentCount == RequiredNumberOfSelectedFragments);
        }

        protected override void ExecuteButtonAction()
        {
            GameObject modelFragment, staticFragment;
            GetModelAndStaticFragment(out modelFragment, out staticFragment);

            IterativeClosestPointRegisterer registerer = new IterativeClosestPointRegisterer(
                modelFragment: modelFragment,
                staticFragment: staticFragment,
                callBack: FinishedRegistrationCallBack
            );

            NotifyUserOfStartingRegistration(modelFragment.name, staticFragment.name);

            registerer.Register();
        }

        private void NotifyUserOfStartingRegistration(string modelFragmentName, string staticFragmentName){
            SendMessage(
                methodName: "OnSendMessageToTicker",
                value: new Ticker.Message.InfoMessage(
                    string.Format(
                        "Starting registration of {0} to {1}",
                        modelFragmentName, staticFragmentName
                    )
                ),
                options: SendMessageOptions.RequireReceiver
            );            
        }

        private void GetModelAndStaticFragment( out GameObject modelFragment, out GameObject staticFragment )
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

        protected override bool HasDetectedKeyBoardShortCut()
        {
            return RTEditor.InputHelper.IsAnyCtrlOrCommandKeyPressed() && Input.GetButtonDown("Register");
        }

        private void FinishedRegistrationCallBack()
        {
            Debug.Log("FinishedRegistrationCallBack!");
        }
    }

}
