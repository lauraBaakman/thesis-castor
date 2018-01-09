using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationWidgetTransformController : MonoBehaviour
{

    public GameObject ObjectControlledByWidget;
    public GameObject SizeControllingWidgetElement;

    public float MinimumScale = 1.0f;
    public float MaximumScale = 5.0f;


    private void Start() { }

    void Update() { }

    private float ComputeLocalScale(Bounds ObjectBounds)
    {
        Bounds widgetBounds = SizeControllingWidgetElement.Bounds();
        Vector3 widgetSize = widgetBounds.size;

        Vector3 objectSize = ObjectBounds.size;

        Vector3 localScale = objectSize.DivideElementWise(widgetSize);

        return localScale.Max();
    }

    public void OnEnable()
    {
        Bounds objectBounds = ObjectControlledByWidget.Bounds();

        float scalingFactor = ComputeLocalScale(objectBounds);
        scalingFactor = Mathf.Clamp(scalingFactor, MinimumScale, MaximumScale);

        transform.position = objectBounds.center;
        transform.localScale = transform.localScale * scalingFactor;
    }

    private void OnDrawGizmosSelected()
    {
        //Draw Object Bounds
        Bounds objectBounds = ObjectControlledByWidget.Bounds();
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(objectBounds.center, objectBounds.size);
    }
}
