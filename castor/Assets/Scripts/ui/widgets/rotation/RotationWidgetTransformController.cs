using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationWidgetTransformController : MonoBehaviour
{

    public GameObject ObjectControlledByWidget;
    public GameObject SizeControllingWidgetElement;

    public float MinimumScale = 1.0f;


    private void Start() { }

    void Update() { }

    private float ComputeLocalScale(Bounds ObjectBounds)
    {
        Bounds widgetBounds = SizeControllingWidgetElement.GetComponent<MeshRenderer>().bounds;
        Vector3 widgetSize = widgetBounds.size;

        Vector3 objectSize = ObjectBounds.size;

        Vector3 localScale = objectSize.DivideElementWise(widgetSize);

        return localScale.Max();
    }

    private float ComputeMaximumScale(Vector3 position)
    {
        return 5.0f;
    }

    public void OnEnable()
    {
        Bounds objectBounds = ObjectControlledByWidget.Bounds();

        float localScale = ComputeLocalScale(objectBounds);

        float maximumScale = ComputeMaximumScale(objectBounds.center);

        float scale = Mathf.Clamp(localScale, MinimumScale, maximumScale);

        transform.position = objectBounds.center;
        transform.localScale = new Vector3().Fill(scale);
    }

    private void OnDrawGizmosSelected()
    {
        Bounds bounds = ObjectControlledByWidget.Bounds();
        Debug.Log("Bounds: " + bounds);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(bounds.center, bounds.size);
    }
}
