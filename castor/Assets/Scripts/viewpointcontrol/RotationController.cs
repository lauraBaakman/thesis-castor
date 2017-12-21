﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TransformController : MonoBehaviour
{
    public abstract void ToggleActivity(bool toggle);
}

public class RotationController : TransformController
{
    public GameObject RotationWidget;

    void Start()
    {
        ToggleWidget(false);
    }

    void Update()
    {

    }

    public override void ToggleActivity(bool toggle)
    {
        ToggleWidget(toggle);
    }

    private void ToggleWidget(bool toggle)
    {
        RotationWidget.SetActive(toggle);
    }
}
