using UnityEngine;

public class RotationController : TransformController
{
    void Start()
    {
        BaseStart();
    }

    protected override void FragmentSelected(bool selected)
    {
        //Do nothing
    }
}
