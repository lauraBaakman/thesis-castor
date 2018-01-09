using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeController : MonoBehaviour {

    public GameObject ObjectControlledByWidget;

    public Vector3 MinimumSize = Vector3.one;
    private Vector3 MaximumSize;

    private void Start()
    {
        //MaximumSize = ComputeMaximumSize();
    }

    void Update()
    {
        transform.position = ObjectControlledByWidget.Bounds().center;
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
