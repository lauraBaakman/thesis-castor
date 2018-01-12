using UnityEngine;

public abstract class RotationWidgetElement : MonoBehaviour
{
    private static string VerticalMouseAxis = "Mouse X";
    private static string HorizontalMouseAxis = "Mouse Y";

    public GameObject Fragments;

    protected GameObject RotatedObject;

    public void SetRotatedObject(GameObject rotatedObject){
        RotatedObject = rotatedObject;
    }

    protected Quaternion InitialRotation;

    protected bool MouseMoved()
    {
        bool mouseMoved = (
            !Input.GetAxis(VerticalMouseAxis).Equals(0.0f) ||
            !Input.GetAxis(HorizontalMouseAxis).Equals(0.0f)
        );
        return mouseMoved;
    }

    protected bool CancelButtonPressed()
    {
        return Input.GetButton("Cancel");
    }

    protected abstract void ToggleRotationMode(bool toggle);

    protected abstract void Rotate();

    protected void CancelRotation(){
        RotatedObject.transform.rotation = InitialRotation;
        ToggleRotationMode(false);
    }

    protected void StoreInitialRotation(){
        InitialRotation = RotatedObject.transform.rotation;
    }
}