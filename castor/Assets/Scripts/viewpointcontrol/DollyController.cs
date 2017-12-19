using UnityEngine;

public class DollyController : MonoBehaviour
{

    private float KeyboardSpeed;

    private static string KeyboardAxisName = "Keyboard Dolly";
    private static string MouseAxisName = "Mouse Dolly";

    private static float minLocalScale = float.Epsilon;
    private static float maxLocalScale = float.MaxValue;

    void Awake()
    {
        KeyboardSpeed = PlayerPrefs.GetFloat("ui.viewpoint.dolly.speed");
    }

    public void Update()
    {
        if (Input.GetButton(KeyboardAxisName))
        {
            Dolly(KeyboardSpeed, Input.GetAxis(KeyboardAxisName));
        };

        float mouseScroll = Input.GetAxis(MouseAxisName);
        if (mouseScroll > 0.05 && mouseScroll < -0.05)
        {
            Dolly(mouseScroll, Input.GetAxis(MouseAxisName));
        }
    }

    private void Dolly(float speed, float direction)
    {
        float scalingTerm = direction * speed * Time.deltaTime;

        Vector3 newLocalScale = transform.localScale + new Vector3().Fill(scalingTerm);
        newLocalScale = newLocalScale.Clamped(minLocalScale, maxLocalScale);

        transform.localScale = newLocalScale;
    }
}
