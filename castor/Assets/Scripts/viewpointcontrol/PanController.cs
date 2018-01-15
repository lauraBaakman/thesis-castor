using UnityEngine;

/// <summary>
/// Controls the Pan of the viewpoint in a single direction.
/// </summary>
abstract public class DirectionlessPanController : MonoBehaviour
{
    private string KeyBoardAxis;
    private string MouseAxis;

    protected Vector3 DirectionVector;
    protected float ScalingFactor;

    void Update()
    {
        if (DetectedKeyboardPan()) Pan(KeyBoardAxis);
        if (DetectedMousePan()) Pan(MouseAxis);

        ClampPositionToViewPort();
    }

    private void ClampPositionToViewPort()
    {
        Vector3 position = Camera.main.WorldToViewportPoint(transform.position);

        position.x = Mathf.Clamp01(position.x);
        position.y = Mathf.Clamp01(position.y);

        transform.position = Camera.main.ViewportToWorldPoint(position);
    }

    private bool DetectedKeyboardPan()
    {
        return Input.GetButton(KeyBoardAxis);
    }

    private bool DetectedMousePan()
    {
        float value = Input.GetAxis(MouseAxis);
        return !value.Equals(0.0f) && Input.GetKey(KeyCode.Mouse0);
    }

    private void Pan(string axis)
    {
        float speed = Input.GetAxis(axis);
        Vector3 panVector = DirectionVector * (speed * ScalingFactor);
        transform.localPosition += panVector;
    }

    public void Populate(string mouseAxis, string keyboardAxis)
    {
        MouseAxis = mouseAxis;
        KeyBoardAxis = keyboardAxis;
    }

    protected void BaseAwake()
    {
        ScalingFactor = PlayerPrefs.GetFloat("ui.viewpoint.pan.speed");
    }
}


/// <summary>
/// Controls the pan along the vertical axis.
/// </summary>
public class VerticalPanController : DirectionlessPanController
{
    void Awake()
    {
        BaseAwake();
        DirectionVector = Vector3.up;
    }
}

/// <summary>
/// Controls the pan along the horizontal axis.
/// </summary>
public class HorizontalPanController : DirectionlessPanController
{
    private void Awake()
    {
        BaseAwake();
        DirectionVector = Vector3.right;
    }
}