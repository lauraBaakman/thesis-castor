using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationWidgetTransformController : MonoBehaviour {

    public GameObject ObjectControlledByWidget;
    public GameObject SizeControllingWidgetElement;

    public Vector3 MinimumSize = Vector3.one;
    private Vector3 MaximumSize;


    private void Start()
    {
        //MaximumSize = ComputeMaximumSize();
    }

    void Update()
    {
        Bounds objectBounds = ObjectControlledByWidget.Bounds();

        transform.position = objectBounds.center;
        transform.localScale = ComputeLocalScale(objectBounds);
    }

    private Vector3 ComputeLocalScale(Bounds ObjectBounds)
    {
        Bounds widgetBounds = SizeControllingWidgetElement.GetComponent<MeshRenderer>().bounds;
        Vector3 widgetSize = widgetBounds.size;

        Vector3 objectSize = ObjectBounds.size;

        //Vector3 localScale = objectSize / widgetSize;

        //Fix minimum 

        //Fix maximum

        return new Vector3(3.0f, 3.0f, 3.0f);
    }

    private Vector3 ComputeMaximumSize(){
        throw new NotImplementedException("The maximum size of the thing needs to be controlled based on the screensize, minus the sidebar, or something.");
        Vector3 size = new Vector3();
        return size;
    }

    private void OnDrawGizmosSelected()
    {
        Bounds bounds = ObjectControlledByWidget.Bounds();
        Debug.Log("Bounds: " + bounds);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(bounds.center, bounds.size);
    }
}
