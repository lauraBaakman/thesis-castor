using UnityEngine;

public class PanController : MonoBehaviour
{
    private static string HorizontalKeyboardAxisName = "Horizontal";
    private static string HorizontalMouseAxisName = "Mouse X";

    private static string VerticalKeyboardAxisName = "Vertical";
    private static string VerticalMouseAxisName = "Mouse Y";

    private float KeyboardSpeedScalingFactor;

    void Awake()
    {
        KeyboardSpeedScalingFactor = KeyboardSpeedScalingFactor = PlayerPrefs.GetFloat("ui.viewpoint.pan.speed");
    }

    void Update()
    {
        //Debug.Log("Local Position: " + transform.localPosition);
        if (DetectedKeyboardPan())
        {
            HandleKeyBoardPan(HorizontalKeyboardAxisName);
            HandleKeyBoardPan(VerticalKeyboardAxisName);
        }

        if (DetectedMousePan())
        {
            HandleMousePan();
        }

    }

    private bool DetectedKeyboardPan()
    {
        return Input.GetButton(HorizontalKeyboardAxisName) || Input.GetButtonUp(VerticalKeyboardAxisName);
    }

    private void HandleKeyBoardPan(string axisName)
    {
        float speed = Input.GetAxis(axisName);
        Vector3 direction = HorizontalDirectionToVector(speed);

        Pan(speed, direction);
    }

    private bool DetectedMousePan()
    {
        float value = Input.GetAxis(HorizontalMouseAxisName);
        return false;
    }

    private void HandleMousePan()
    {
        Debug.Log("Handled Mouse Pan");
    }

    private Vector3 HorizontalDirectionToVector(float axisDirection)
    {
        if (axisDirection.Equals(0.0f))
        {
            return new Vector3();
        }
        return axisDirection < 0 ? Vector3.left : Vector3.right;
    }

    private Vector3 VerticalDirectionToVector(float axisDirection)
    {
        Debug.Log(axisDirection);
        if(axisDirection.Equals(0.0f))
        {
            return new Vector3();
        }
        return axisDirection < 0 ? Vector3.down : Vector3.up;
    }

    private void Pan(float speed, Vector3 direction)
    {
        transform.localPosition += direction * (speed * KeyboardSpeedScalingFactor);
    }
}
