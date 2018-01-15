using UnityEngine;

/// <summary>
/// Rotation controller handles the enable/disabling of the general rotation mode.
/// </summary>
public class RotationController : TransformController
{
    private GameObject Fragments;
    private GameObject SelectedFragments;

    void Start()
    {
        Fragments = gameObject;
        SelectedFragments = gameObject.FindChildByName("Selected Fragments");

        BaseStart();
    }

    public override void ToggleActivity(bool toggle)
    {
        Widget.SetActive(toggle);
        FindAndBroadcastRotatedObject();
    }

    protected override void FragmentSelected()
    {
        FindAndBroadcastRotatedObject();
    }

    private void FindAndBroadcastRotatedObject()
    {
        GameObject rotatedObject = ObjectsSelected() ? SelectedFragments : Fragments;

        //If the rotationwidget is not active there won't be a receiver
        Widget.BroadcastMessage(
            methodName: "SetRotatedObject",
            parameter: rotatedObject,
            options: SendMessageOptions.DontRequireReceiver
        );
    }
}
