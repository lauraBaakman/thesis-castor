﻿using UnityEngine;

public abstract class TransformController : MonoBehaviour
{
    public GameObject Widget;

    protected int NumberOfSelectedObjects = 0;

    public void ToggleActivity(bool toggle)
    {
        Widget.SetActive(toggle);
    }

    protected void BaseStart()
    {
        ToggleActivity(false);
    }
}
