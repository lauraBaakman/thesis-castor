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

		//Remove Fragment ListView
	}
}
