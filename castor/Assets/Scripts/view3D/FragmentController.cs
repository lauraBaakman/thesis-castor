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


	/// <summary>
	/// Deletes the fragment associated with this controller from the reduction.
	/// </summary>
	public void DeleteFragment(){
		bool succes = Fragments.GetInstance.RemoveFragment(Fragment);

		if (!succes) {
			Debug.Log ("Could not delete the fragment with name: " + Fragment.Name);
			return;
		}

		//Remove Fragment GameObject


	}

	/// <summary>
	/// Populate the FragmentController.
	/// </summary>
	/// <param name="listElementController">List element controller associated with the Fragment controlled by this FragmentController.</param>
	public void Populate(FragmentListElementController listElementController){
		this.ListElementController = listElementController;
	}
}
