using UnityEngine;
using UnityEngine.UI;

public class FragmentListElementController : MonoBehaviour
{
	/// <summary>
	/// The UI element that displays the name of the fragment.
	/// </summary>
	public Text FragmentNameText;
	
    /// <summary>
	/// The selection toggle.
	/// </summary>
	public Toggle SelectionToggle;
	
    /// <summary>
	/// The visibility toggle.
	/// </summary>
	public Toggle VisibilityToggle;

	private GameObject Fragment;

	private static Color SelectedColor;
	private static Color NormalColor;

	void Start ()
	{
		NormalColor = gameObject.GetComponent<Image> ().color;
		SelectedColor = new Color (
			NormalColor.r,
			NormalColor.g,
			NormalColor.b,
			NormalColor.a * 5.0f);
	}

	/// <summary>
	/// Populate the properties of the fragmentController.
	/// </summary>
	/// <param name="fragment">Fragment.</param>
	public void Populate (GameObject fragment)
	{
		this.Fragment = fragment;
	}

	/// <summary>
	/// On the delete fragment button click the fragment is deleted from <Fragments cref="Fragments">, the 3D view and the list view.
	/// </summary>
	public void OnDeleteFragmentClick ()
	{
		Fragment.GetComponent<FragmentController> ().DeleteFragment ();
	}

	/// <summary>
	/// On the change fragment color button click the user should be able to change the color of the fragment.
	/// </summary>
	public void OnChangeColorFragmentClick ()
	{
		Debug.Log ("Pressed the change color fragment button.");
	}

	/// <summary>
	/// On changing the value of the visibility toggle the fragment is shown or 
    /// hidden ,depending on its previous state.
	/// </summary>
	/// <param name="toggle">If set to <c>true</c> the fragment should be shown.</param>
	public void OnToggleVisibility (bool toggle)
	{
		Fragment.GetComponent<FragmentController> ().ToggleVisibility (toggle);
	}

	/// <summary>
    /// On changing the value of the selection toggle the fragment is selected or 
    /// deselcted ,depending on its previous state.
	/// </summary>
	/// <param name="toggle">If set to <c>true</c> the object is marked as selected.</param>
	public void OnToggleSelected (bool toggle)
	{
		Fragment.GetComponent<FragmentController> ().ToggleSelection (toggle);
	}

	/// <summary>
	/// Change the name of the fragment.
	/// </summary>
	/// <param name="newName">the new name of the fragment.</param>
	public void ChangeFragmentName (string newName)
	{
		this.FragmentNameText.text = newName;
	}

	/// <summary>
	/// Delete the GameObject associated with this controller.
	/// </summary>
	public void Delete ()
	{
		Destroy (gameObject);
	}

	/// <summary>
	/// Toggles the selection locally, i.e. this function indicates within the listelement that the associated fragment
	/// is selected, but does not influence the fragment in any way.
	/// </summary>
	/// <param name="toggle">If set to <c>true</c> toggle.</param>
	public void ToggleSelectionLocally (bool toggle)
	{
		gameObject.GetComponent<Image> ().color = toggle ? SelectedColor : NormalColor;
		SelectionToggle.isOn = toggle;
	}

	/// <summary>
    /// Toggles the selection locally, i.e. this function indicates wihtin the listelement that the associated fragment
    /// is hidden/shown, but does not influence the fragment in any way.
	/// </summary>
	/// <param name="toggle">If set to <c>true</c> toggle.</param>
	public void ToggleVisibilityLocally (bool toggle)
	{
		VisibilityToggle.isOn = toggle;   
	}

    public void ToggleSelectability(bool toggle)
    {
        SelectionToggle.interactable = toggle;
    }
}