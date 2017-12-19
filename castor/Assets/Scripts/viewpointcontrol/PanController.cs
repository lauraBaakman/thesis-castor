using UnityEngine;
using System.Collections;

public class PanController : MonoBehaviour
{
    private static string KeyboardAxisName = "Horizontal";
    private static string MouseAxisName = "Mouse X";

    private float KeyboardSpeed;

    void Awake()
    {
        KeyboardSpeed = KeyboardSpeed = PlayerPrefs.GetFloat("ui.viewpoint.pan.speed");
    }

    void Update()
    {
        if (DetectedKeyboardPan())
        {
            HandleKeyBoardPan();
        }

        if (DetectedMousePan())
        {
            HandleMousePan();
        }

    }

    private bool DetectedKeyboardPan()
    {
        return Input.GetButton(KeyboardAxisName);
    }

    private void HandleKeyBoardPan()
    {
        Vector3 direction = DirectionToVector(Input.GetAxis(KeyboardAxisName));
        Pan(KeyboardSpeed, direction);
    }

    private bool DetectedMousePan()
    {
        return false;
    }

    private void HandleMousePan()
    {
        Debug.Log("Handled Mouse Pan");
    }

    private Vector3 DirectionToVector(float axisDirection)
    {
        return axisDirection < 0 ? Vector3.left : Vector3.right;
    }

    private void Pan(float speed, Vector3 direction)
    {
        transform.localPosition += direction * (speed * Time.deltaTime);
    }
}
