using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeController : MonoBehaviour {

    public GameObject ObjectControlledByWidget;

    public Vector3 MinimumSize = Vector3.one;
    private Vector3 MaximumSize;

    private void Start()
    {
        MaximumSize = ComputeMaximumSize();
    }

    private Vector3 ComputeMaximumSize(){
        Vector3 size = new Vector3();
        return size;
    }

    private void OnDrawGizmos()
    {
        Bounds bounds = ObjectControlledByWidget.Bounds();
        Debug.Log("Bounds: " + bounds);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(bounds.center, bounds.size);
    }

    void Update () {
		
	}
}
