using UnityEngine;

/// <summary>
/// Fragments controller controls the parent object that contails all fragments.
/// </summary>
public class FragmentsController : MonoBehaviour
{

    private void Awake()
    {
        gameObject.AddComponent<DollyController>();
        gameObject.AddComponent<VerticalPanController>().Populate(
            mouseAxis: "Mouse Y",
            keyboardAxis: "Vertical"
        );
        gameObject.AddComponent<HorizontalPanController>().Populate(
            mouseAxis: "Mouse X",
            keyboardAxis: "Horizontal"
        );
        //The Transform-Rotation button needs a reference to the rotation controller, therefore
        //the rotation controller is added to Fragments in the editor.
    }

    /// <summary>
    /// Adds a fragment to the visible fragments.
    /// </summary>
    /// <param name="fragment">Fragment to add.</param>
    public FragmentController AddFragment(Fragment fragment)
    {

        GameObject fragmentGameObject = new GameObject(fragment.Name);

        FragmentController controller = fragmentGameObject.AddComponent<FragmentController>();
        fragmentGameObject.transform.parent = gameObject.transform;

        return controller;
    }
}
