using UnityEngine;

/// <summary>
/// Abstract class that handles the cancelling of a specific rotation mode and 
/// has some functionality that is shared between rotation widget elements.
/// </summary>
public abstract class RotationWidgetElement : MonoBehaviour
{
    private static string VerticalMouseAxis = "Mouse X";
    private static string HorizontalMouseAxis = "Mouse Y";

    /// <summary>
    /// The object that contains all fragments, i.e. the complete set of objects 
    /// that can be rotated.
    /// </summary>
    public GameObject Fragments;

    /// <summary>
    /// The initial rotation of the RotatedObject.
    /// </summary>
    protected Quaternion InitialRotation;

    /// <summary>
    /// The rotated object, the object that is currently manipulated by the widget.
    /// </summary>
    protected GameObject RotatedObject;

    /// <summary>
    /// Sets the rotated object.
    /// </summary>
    /// <param name="rotatedObject">Rotated object.</param>
    public void SetRotatedObject(GameObject rotatedObject){
        RotatedObject = rotatedObject;
    }

    /// <summary>
    /// Method that detects if the mouse has been moved.
    /// </summary>
    /// <returns><c>true</c>, if the mouse was moved, <c>false</c> otherwise.</returns>
    protected bool MouseMoved()
    {
        bool mouseMoved = (
            !Input.GetAxis(VerticalMouseAxis).Equals(0.0f) ||
            !Input.GetAxis(HorizontalMouseAxis).Equals(0.0f)
        );
        return mouseMoved;
    }

    /// <summary>
    /// Method that detects if the cancel button has been pressed.
    /// </summary>
    /// <returns><c>true</c>, if button pressed was canceled, <c>false</c> otherwise.</returns>
    protected bool CancelButtonPressed()
    {
        return Input.GetButton("Cancel");
    }

    /// <summary>
    /// Method that should be called to enable/disable the elements rotation mode.
    /// </summary>
    /// <param name="toggle">If set to <c>true</c> toggle.</param>
    protected abstract void ToggleRotationMode(bool toggle);

    /// <summary>
    /// Method that handles the rotation of the object.
    /// </summary>
    protected abstract void Rotate();

    /// <summary>
    /// Cancels the rotation, moves the rotation of the object back to 
    /// InitialRotation.
    /// </summary>
    protected void CancelRotation(){
        RotatedObject.transform.rotation = InitialRotation;
        ToggleRotationMode(false);
    }

    /// <summary>
    /// Stores the initial rotation.
    /// </summary>
    protected void StoreInitialRotation(){
        InitialRotation = RotatedObject.transform.rotation;
    }
}