using UnityEngine;
using Registration.Messages;
using System.Collections;

namespace Fragment
{
	public enum ICPFragmentType { Model, Static };

	[RequireComponent(typeof(ICPModelFragmentController))]
	[RequireComponent(typeof(ICPStaticFragmentController))]
	public class ICPController : MonoBehaviour, IICPListener, IICPStartEndListener
	{

		private ICPModelFragmentController modelFragmentController;
		private ICPStaticFragmentController staticFragmentController;

		private Transform ICPFragments;
		private Transform Fragments;

		private static string ICPFragmentsName = "ICP Fragments";
		private static string FragmentsName = "Fragments";

		public void Awake()
		{
			GameObject FragmentsGO = GameObject.Find(FragmentsName);
			Debug.Assert(FragmentsGO, "Could not find the parent gameobject of this gameobject");
			Fragments = FragmentsGO.transform;

			GameObject ICPFragmentsGO = GameObject.Find(ICPFragmentsName);
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

		public void OnPreparationStepCompleted(ICPPreparationStepCompletedMessage message) { }
		#endregion

		#region ICPStartEndListener
		public void OnICPStarted(ICPStartedMessage message)
		{
			StartCoroutine(ChangeTransformOnObjectDeselection());
		}
		#endregion

		private IEnumerator ChangeTransformOnObjectDeselection()
		{
			StateTracker stateTracker = GetComponent<StateTracker>();

			yield return new WaitUntil(() => stateTracker.State.Deselected);

			transform.parent = ICPFragments;
		}
	}
}


