using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherRotation : MonoBehaviour
{

    //All stored positions are in world coordinates

    public GameObject RotatedObject;
    public GameObject Fragments;
    public GameObject Donut;

    private static string VerticalMouseAxis = "Mouse X";
    private static string HorizontalMouseAxis = "Mouse Y";

    private Quaternion InitialRotation;

    private bool OnWidget = false;
    private bool InRotationMode = false;

    void Start()
    {
        gameObject.AddComponent<MeshCollider>();
    }

    void Update()
    {
        if (InRotationMode)
        {
            if (CancelButtonPressed()) CancelRotation();
            if (MouseMovedOnWidget()) Rotate();
        }
    }

    private bool MouseMovedOnWidget()
    {
        bool mouseMoved = (
            !Input.GetAxis(VerticalMouseAxis).Equals(0.0f) ||
            !Input.GetAxis(HorizontalMouseAxis).Equals(0.0f)
        );
        return mouseMoved && OnWidget;
    }

    private bool CancelButtonPressed()
    {
        return Input.GetButton("Cancel");
    }

    public void OnMouseEnter()
    {
        OnWidget = true;
    }

    public void OnMouseExit()
    {
        OnWidget = false;
    }

    private void EnterRotationMode()
    {
        Debug.Log("Enter Rotation Mode");
        InRotationMode = true;
        Donut.SetActive(false);
        InitialRotation = RotatedObject.transform.rotation;

        //TODO Show click position
        ToggleMeshCollidersOnFragments(false);
    }

    private void ExitRotationMode()
    {
        Debug.Log("Exit Rotation Mode");
        InRotationMode = false;
        Donut.SetActive(true);

        //TODO Hide click position
        ToggleMeshCollidersOnFragments(true);
    }

    private void CancelRotation(){
        RotatedObject.transform.rotation = InitialRotation;
        ExitRotationMode();   
    }

    private void Rotate()
    {
        Debug.Log("Rotate!");
    }

    public void OnMouseDown()
    {
        if (InRotationMode)
        {
            ExitRotationMode();
        }
        else
        {
            EnterRotationMode();
        }
    }

    private void ToggleMeshCollidersOnFragments(bool toggle){
        MeshCollider[] colliders = Fragments.GetComponentsInChildren<MeshCollider>();

        foreach (MeshCollider meshCollider in colliders)
            meshCollider.enabled = toggle;
    }
}
