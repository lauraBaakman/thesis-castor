using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TransformController : MonoBehaviour
{
    public abstract void ToggleActivity(bool toggle);
}

public class RotationController : TransformController
{
    public override void ToggleActivity(bool toggle)
    {
        Debug.Log("Toggle Activity: " + toggle);
    }

    void Start()
    {

    }

    void Update()
    {

    }
}
