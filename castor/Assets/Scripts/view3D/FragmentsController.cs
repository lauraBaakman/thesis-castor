using UnityEngine;

public class FragmentsController : MonoBehaviour
{

    private void Awake()
    {
        gameObject.AddComponent<DollyController>();
        gameObject.AddComponent<RotationController>();
        gameObject.AddComponent<VerticalPanController>().Populate(
            mouseAxis: "Mouse Y",
            keyboardAxis: "Vertical"
        );
        gameObject.AddComponent<HorizontalPanController>().Populate(
            mouseAxis: "Mouse X",
            keyboardAxis: "Horizontal"
        );
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
