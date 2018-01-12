using UnityEngine;

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

    protected override void FragmentSelected(bool selected)
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
