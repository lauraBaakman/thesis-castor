using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentController : MonoBehaviour
{
	/// <summary>
	/// Gets or sets the fragment.
	/// </summary>
	/// <value>The fragment.</value>
	public Fragment Fragment { get; set; }

	private FragmentListElementController ListElementController; 

	private FragmentsController FragmentsController;

	void Start(){
		FragmentsController = GetComponentInParent<FragmentsController> ();
	}

	/// <summary>
	/// Deletes the fragment associated with this controller from the reduction.
	/// </summary>
	public void DeleteFragment(){
		bool succes = Fragments.GetInstance.RemoveFragment(Fragment);

		if (!succes) {
			Debug.Log ("Could not delete the fragment with name: " + Fragment.Name);
			return;
		}

		ListElementController.Delete ();

		Destroy (gameObject);
	}

	/// <summary>
	/// Populate the FragmentController.
	/// </summary>
	/// <param name="listElementController">List element controller associated with the Fragment controlled by this FragmentController.</param>
	public void Populate(FragmentListElementController listElementController){
		this.ListElementController = listElementController;
	}

	public void ToggleSelection(bool toggle){
		FragmentsController.ToggleFragmentSelection (this, toggle);
	}

	public void Deselect(){
		Debug.Log("The fragment with name " + Fragment.Name + " has been deselected.");
		ListElementController.Deselect ();
	}
}
