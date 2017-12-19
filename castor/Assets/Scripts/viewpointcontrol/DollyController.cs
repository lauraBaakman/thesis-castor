using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollyController : MonoBehaviour
{

    public float keyboardSpeed = 0.1f;

    private static string KeyboardAxisName = "Keyboard Dolly";
    private static string MouseAxisName = "Mouse Dolly";

    private static float minLocalScale = float.Epsilon;
    private static float maxLocalScale = float.MaxValue;

    // Use this for initialization
    void Start()
    {
        Debug.Log(Input.multiTouchEnabled);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton(KeyboardAxisName))
        {
            Dolly(keyboardSpeed, Input.GetAxis(KeyboardAxisName));
        };
        float mouseScroll = Input.GetAxis(MouseAxisName);
        if (mouseScroll > 0.05 && mouseScroll < -0.05)
        {
            Debug.Log("Detected Zoom with Scroll Wheel Axis");
            //TODO test with normal mouse, use mousescroll instead of speed when calling dolly()
        }
    }

    private void Dolly(float speed, float direction)
    {
        float scalingTerm = direction * speed;

        Vector3 newLocalScale = transform.localScale + new Vector3().Fill(scalingTerm);
        newLocalScale = newLocalScale.Clamped(minLocalScale, maxLocalScale);

        transform.localScale = newLocalScale;

        Debug.Log("Local Scale: " + transform.localScale);
    }
}
