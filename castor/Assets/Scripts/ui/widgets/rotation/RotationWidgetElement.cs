using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationWidgetElement : MonoBehaviour
{
    private static string VerticalMouseAxis = "Mouse X";
    private static string HorizontalMouseAxis = "Mouse Y";

    public GameObject Fragments;
    public GameObject RotatedObject;

    protected Quaternion InitialRotation;

    protected bool MouseMoved()
    {
        bool mouseMoved = (
            !Input.GetAxis(VerticalMouseAxis).Equals(0.0f) ||
            !Input.GetAxis(HorizontalMouseAxis).Equals(0.0f)
        );
        return mouseMoved;
    }

    protected bool CancelButtonPressed()
    {
        return Input.GetButton("Cancel");
    }
}