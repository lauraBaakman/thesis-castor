using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeRenderer : MonoBehaviour {

    private Fragment Fragment;

	void Start () {
        Debug.Log("Starting the Edge Renderer");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Populate(Fragment fragment){
        Fragment = fragment;
    }
}
