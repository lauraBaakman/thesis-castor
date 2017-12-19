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
