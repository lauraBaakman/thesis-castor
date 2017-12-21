using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewVectorRotation : MonoBehaviour {

    public GameObject RotatedObject;

    private int State = 0;
    private Quaternion initialRotation;

    private static string VerticalMouseAxis = "Mouse X";
    private static string HorizontalMouseAxis = "Mouse Y";

    void Start () {
        MeshCollider meshCollider = gameObject.AddComponent<MeshCollider>();

	}
	
	void Update () {
        if(CancelButtonPressed()){
            OnEsc();
        }

        if(MouseMoved()){
            OnMouseMoved();
        }
	}

    private bool MouseMoved(){
        return (!Input.GetAxis(VerticalMouseAxis).Equals(0.0f) ||
                !Input.GetAxis(HorizontalMouseAxis).Equals(0.0f));
    }

    private bool CancelButtonPressed(){
        return Input.GetButton("Cancel");
    }

    public void OnMouseDown()
    {
        switch(State){
            case 0:
                MouseDownState0();
                break;
            case 1:
                MouseDownState1();
                break;
        }
    }

    public void OnMouseMoved(){
        switch (State)
        {
            case 0:
                MouseMovedState0();
                break;
            case 1:
                MouseMovedState1();
                break;
        }        
    }

    private void MouseMovedState0()
    {
        //Do Nothing
        Debug.Log("MouseMovedState0");
    }

    private void MouseMovedState1()
    {
        Debug.Log("MouseMovedState1");
    }

    private void MouseDownState0(){
        Debug.Log("MouseDownState0");
        initialRotation = transform.rotation;

        State = 1;
    }

    private void MouseDownState1(){
        Debug.Log("MouseDownState1");

        State = 0;
    }

    private void OnEsc(){
        switch (State)
        {
            case 0:
                EscState0();
                break;
            case 1:
                EscState1();
                break;
        }        
    }

    private void EscState1()
    {
        Debug.Log("EscState1");
        RotatedObject.transform.rotation = initialRotation;

        State = 0;
    }

    private void EscState0()
    {
        Debug.Log("EscState0");
        //Do nothing
    }
}
