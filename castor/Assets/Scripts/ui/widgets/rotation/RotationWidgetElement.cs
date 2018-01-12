﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationWidgetElement : MonoBehaviour
{
    private static string VerticalMouseAxis = "Mouse X";
    private static string HorizontalMouseAxis = "Mouse Y";

    protected bool MouseMoved()
    {
        bool mouseMoved = (
            !Input.GetAxis(VerticalMouseAxis).Equals(0.0f) ||
            !Input.GetAxis(HorizontalMouseAxis).Equals(0.0f)
        );
        return mouseMoved;
    }
}