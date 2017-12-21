using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewVectorRotation : MonoBehaviour {

    void Start () {
        MeshCollider meshCollider = gameObject.AddComponent<MeshCollider>();
	}
	
	void Update () {
		
	}

    public void OnMouseDown()
    {
        Debug.Log("Detected Mouse Click!");
    }
}
