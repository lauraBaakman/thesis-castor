using UnityEngine;
using System.Collections;

public class PanController : MonoBehaviour
{
    private static string KeyboardAxisName = "Horizontal";
    private static string MouseAxisName = "Mouse X";

    private float KeyboardSpeed;

    void Awake(){
        KeyboardSpeed = KeyboardSpeed = PlayerPrefs.GetFloat("ui.viewpoint.pan.speed");
    }

    void Update()
    {
        if(DetectKeyboardPan()){
            HandleKeyBoardPan();
        }

        if(DetectMousePan()){
            HandleMousePan();
        }

    }

    private bool DetectKeyboardPan(){
        return Input.GetButton(KeyboardAxisName);
    }

    private void HandleKeyBoardPan(){
        Debug.Log("Handled Keyboard Pan");
    }

    private bool DetectMousePan(){
        return false;
    }

    private void HandleMousePan()
    {
        Debug.Log("Handled Mouse Pan");
    }
}
