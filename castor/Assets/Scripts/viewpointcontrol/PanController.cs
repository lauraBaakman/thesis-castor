using UnityEngine;

abstract public class DirectionlessPanController : MonoBehaviour
{
    private string KeyBoardAxis;
    private string MouseAxis;

    protected Vector3 DirectionVector;
    protected float KeyboardSpeedScalingFactor;

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
        return Input.GetButton(KeyBoardAxis);
    }

    private void HandleKeyBoardPan()
    {
        float speed = Input.GetAxis(KeyBoardAxis);

        Pan(speed, DirectionVector);
    }

    private bool DetectedMousePan()
    {
        float value = Input.GetAxis(MouseAxis);
        return !value.Equals(0.0f) && Input.GetKey(KeyCode.Mouse0);
    }

    private void HandleMousePan()
    {
        float speed = Input.GetAxis(MouseAxis);

        Pan(speed, DirectionVector);
    }

    private void Pan(float speed, Vector3 direction)
    {
        Vector3 panVector = direction * (speed * KeyboardSpeedScalingFactor);
        transform.localPosition += panVector;
    }

    public void Populate(string mouseAxis, string keyboardAxis)
    {
        MouseAxis = mouseAxis;
        KeyBoardAxis = keyboardAxis;
    }

    protected void BaseAwake(){
        KeyboardSpeedScalingFactor = KeyboardSpeedScalingFactor = PlayerPrefs.GetFloat("ui.viewpoint.pan.speed");
    }
}

public class VerticalPanController : DirectionlessPanController
{
    void Awake()
    {
        BaseAwake();
        DirectionVector = Vector3.up;
    }
}

public class HorizontalPanController : DirectionlessPanController
{
    private void Awake()
    {
        BaseAwake();
        DirectionVector = Vector3.right;
    }
}