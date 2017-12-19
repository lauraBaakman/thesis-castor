using UnityEngine;
using System.Collections;

public class PanController : MonoBehaviour
{
    private static string KeyboardAxisName = "Keyboard Pan";
    private static string MouseAxisName = "Mouse Pan";

    void Start()
    {

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

    private void DetectKeyboardPan(){
        if (Input.GetButton(KeyboardAxisName)){
            Debug.Log("Detected Keyboard Pan");
        }
    }

    private void HandleKeyBoardPan(){
        Debug.Log("Handled Keyboard Pan");
    }

    private void DetectMousePan(){
        Debug.Log("Detected Mouse Pan");
    }

    private void HandleMousePan()
    {
        Debug.Log("Handled Mouse Pan");
    }
}
