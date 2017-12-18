using UnityEngine;
using UnityEngine.UI;

public class FragmentListElementController : MonoBehaviour
{
    /// <summary>
    /// The UI element that displays the name of the fragment.
    /// </summary>
    public Text FragmentNameText;

    private GameObject Fragment;

    private static Color SelectedColor;
    private static Color NormalColor;


    void Start()
    {
        NormalColor = gameObject.GetComponent<Image>().color;
        SelectedColor = new Color(
            NormalColor.r,
            NormalColor.g,
            NormalColor.b,
            NormalColor.a * 5.0f);
    }

    /// <summary>
    /// Populate the properties of the fragmentController.
    /// </summary>
    /// <param name="fragmentController">Fragment controller.</param>
    public void Populate(GameObject fragment)
    {
        this.Fragment = fragment;
    }

    /// <summary>
    /// On the delete fragment button click the fragment is deleted from <Fragments cref="Fragments">, the 3D view and the list view.
    /// </summary>
    public void OnDeleteFragmentClick()
    {
        Fragment.GetComponent<FragmentController>().DeleteFragment();
    }

    /// <summary>
    /// On the change fragment color button click the user should be able to change the color of the fragment.
    /// </summary>
    public void OnChangeColorFragmentClick()
    {
        Debug.Log("Pressed the change color fragment button.");
    }

    /// <summary>
    /// Ons the toggle fragment value changed, the fragment should be shown or hidden depending on its previous state.
    /// </summary>
    /// <param name="toggle">If set to <c>true</c> the fragment should be shown.</param>
    public void OnToggleVisibility(bool toggle) 
    {
        Debug.Log("Pressed the toggle visibility checkmark, toggle: " + toggle);
    }

    /// <summary>
    /// Toggle the selection of the associated fragment.
    /// </summary>
    /// <param name="toggle">If set to <c>true</c> the object is marked as selected.</param>
    public void OnToggleSelected(bool toggle)
    {
        Fragment.GetComponent<SelectableFragmentController>().ToggleVisibility(toggle);
        gameObject.GetComponent<Image>().color = toggle ? SelectedColor : NormalColor;
    }

    /// <summary>
    /// Change the name of the fragment.
    /// </summary>
    /// <param name="newName">the new name of the fragment.</param>
    public void ChangeFragmentName(string newName)
    {
        this.FragmentNameText.text = newName;
    }

    /// <summary>
    /// Delete the GameObject associated with this controller.
    /// </summary>
    public void Delete()
    {
        Destroy(gameObject);
    }
}