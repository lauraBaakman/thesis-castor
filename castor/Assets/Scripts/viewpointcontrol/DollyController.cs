using UnityEngine;

/// <summary>
/// Controls the Dolly of the Viewoint.
/// </summary>
public class DollyController : MonoBehaviour
{

    private float ScalingFactor;

    private static string KeyboardAxisName = "Keyboard Dolly";
    private static string MouseAxisName = "Mouse Dolly";

    private static float minLocalScale = float.Epsilon;
    private static float maxLocalScale = float.MaxValue;

    void Awake()
    {
        ScalingFactor = PlayerPrefs.GetFloat("ui.viewpoint.dolly.speed");
    }

    public void Update()
    {
        if (DetectedKeyboardDolly()) Dolly(KeyboardAxisName);
        if (DetectedMouseDolly()) Dolly(MouseAxisName);
        ClampScale();
    }

    private void ClampScale(){
        Vector3 scale = transform.localScale;

        scale = scale.Clamped(minLocalScale, maxLocalScale);

        transform.localScale = scale;
    }

    private bool DetectedKeyboardDolly()
    {
        return Input.GetButton(KeyboardAxisName);
    }

    private bool DetectedMouseDolly(){
        float value = Input.GetAxis(MouseAxisName);
        return !value.Equals(0.0f);
    }

    private void Dolly(string axis)
    {
        float scale = ScalingFactor * Input.GetAxis(axis);
        transform.localScale = transform.localScale + new Vector3().Fill(scale);
    }
}
