using UnityEngine;

public class RotationController : TransformController
{
    public GameObject Widget;

    void Start()
    {
        ToggleWidget(false);
    }

    void Update()
    {

    }

    public override void ToggleActivity(bool toggle)
    {
        ToggleWidget(toggle);
    }

    private void ToggleWidget(bool toggle)
    {
        Widget.SetActive(toggle);
    }
}
