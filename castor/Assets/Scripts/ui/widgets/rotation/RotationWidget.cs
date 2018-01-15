using UnityEngine;

/// <summary>
/// Rotation widget controls the general behaviour of the rotation widget. This 
/// class ensures that the correct elemetns of the widget are visible at the 
/// correct moment.
/// </summary>
public class RotationWidget : MonoBehaviour
{

    /// <summary>
    /// The fragments that are controlled by the rotation widget.
    /// </summary>
    public GameObject Fragments;

    private GameObject Donut;
    private GameObject ClickPositionIndicator;
    private GameObject Sphere;

    void Awake()
    {
        Donut = gameObject.FindChildByName("OuterDonut");
        ClickPositionIndicator = gameObject.FindChildByName("ViewVectorClickPositionIndicator");
        Sphere = gameObject.FindChildByName("InnerSphere");
    }

    /// <summary>
    /// This method receives the RotationModeChanged messages and ensures that the
    /// widget is in the correct state, based on the new rotation mode.
    /// </summary>
    /// <param name="message">The Message that notifies the widget of a change in rotation mode.</param>
    public void OnRotationModeChanged(RotationModeChangedMessage message){
        switch(message.NewMode){
            case RotationMode.Initial:
                EnterInitialRotationMode();
                break;
            case RotationMode.ViewVector:
                EnterViewVectorRotationMode();
                break;
            case RotationMode.OtherVectors:
                EnterOtherVectorsRotationMode();
                break;
        }
    }

    private void EnterInitialRotationMode(){
        Fragments.BroadcastMessage(
            methodName: "ToggleSelectability",
            parameter: true,
            options: SendMessageOptions.DontRequireReceiver
        );
        Donut.SetActive(true);
        Sphere.SetActive(true);
        ClickPositionIndicator.SetActive(false);
    }

    private void EnterElementRotationMode(){
        Fragments.BroadcastMessage(
            methodName: "ToggleSelectability",
            parameter: false,
            options: SendMessageOptions.DontRequireReceiver
        );        
    }

    private void EnterViewVectorRotationMode(){
        EnterElementRotationMode();

        Donut.SetActive(true);
        ClickPositionIndicator.SetActive(true);
        Sphere.SetActive(false);
    }

    private void EnterOtherVectorsRotationMode(){
        EnterElementRotationMode();

        Donut.SetActive(false);
        ClickPositionIndicator.SetActive(false);
        Sphere.SetActive(true);
    }

    private void OnEnable()
    {
        EnterInitialRotationMode();
    }
}

/// <summary>
/// Rotation mode, enum with the different possible rotation modes:
/// * Initial: The complete widget is shown, the user has to chose around which vector they want to rotate.
/// * ViewVector: The user can rotate the object around the view vector.
/// * OtherVectors: The user can rotate the object around other vectors than the view vector.
/// </summary>
public enum RotationMode { Initial, ViewVector, OtherVectors };

/// <summary>
/// Rotation mode changed message is sent when the rotation mode has been changed.
/// </summary>
public class RotationModeChangedMessage{
    public RotationMode NewMode;

    public RotationModeChangedMessage(RotationMode newMode){
        NewMode = newMode;
    }
}