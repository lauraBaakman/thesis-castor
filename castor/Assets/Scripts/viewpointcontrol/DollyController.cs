using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollyController : MonoBehaviour {

    public float speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetButton("Zoom")){
            Debug.Log("Detected Zoom with Keyboard!");
            //TODO get the direction of the input, somehow
        };
        float mouseScroll = Input.GetAxis("Zoom");
        if(mouseScroll > 0.05 && mouseScroll < -0.05){
            Debug.Log("Detected Zoom with Scroll Wheel Axis");
            //TODO test with normal mouse, use mousescroll instead of speed when calling dolly()
        }
	}

    private void Dolly(double speed){
        //TODO scale objects? Need to scale the full set of objects, not the objects individually, alternative move camera along view vector.       
    }
}
