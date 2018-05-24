using UnityEngine;
using Registration;
using Registration.Messages;

using Buttons.RegistrationButtons;
using Fragments;

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
				settings: Settings.SeminalICP(ICPFragments.transform)
//				settings: new Settings(
//					name: "igdTransformFinderWithIntersectionError",
//					referenceTransform: ICPFragments.transform,
//					transformFinder: new IGDTransformFinder(
//						new IGDTransformFinder.Configuration(
//							convergenceError: 0.001f,
//							learningRate: 0.001f,
//							maxNumIterations: 200,
//							errorMetric: new Registration.Error.IntersectionTermError(0.5f, 0.5f)
//						)
//					)
//				)
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
			if (SelectedFragments.GetComponent<LockController>().AreAnySelectedFragmentsLocked())
			{
				GetModelAndStaticFragmentWithLockedObjects(out modelFragment, out staticFragment);
			}
			else
			{
				MeshRenderer[] childMeshes = SelectedFragments.GetComponentsInChildren<MeshRenderer>();

				modelFragment = childMeshes[0].gameObject;
				staticFragment = childMeshes[1].gameObject;
			}
		}

		private void GetModelAndStaticFragmentWithLockedObjects(out GameObject modelFragment, out GameObject staticFragment)
		{
			LockController lockController = SelectedFragments.GetComponent<LockController>();
			staticFragment = lockController.LockedObjects[0];

			if (lockController.LockedObjects.Count > 1) modelFragment = lockController.LockedObjects[1];
			else modelFragment = lockController.UnLockedObjects[0];
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

