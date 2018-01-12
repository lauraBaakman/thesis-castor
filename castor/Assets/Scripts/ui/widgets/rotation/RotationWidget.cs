﻿using UnityEngine;

public class RotationWidget : MonoBehaviour
{

    public GameObject Fragments;

    private GameObject Donut;
    private GameObject ClickPositionIndicator;
    private GameObject Sphere;


    void Start()
    {
        Donut = FindGameObjectByName("OuterDonut");
        ClickPositionIndicator = FindGameObjectByName("ViewVectorClickPositionIndicator");
        Sphere = FindGameObjectByName("InnerSphere");
    }

    private GameObject FindGameObjectByName(string childName)
    {
        Transform childTransform = gameObject.transform.Find(childName);
        if (childTransform) return childTransform.gameObject;

        Debug.LogError("Could not find the gameobject with name " + childName);
        return null;
    }

    public void OnRotationModeChanged(RotationModeChangedMessage message){
        switch(message.NewMode){
            case RotationMode.Initial:
                EnterInitialRotationMode();
                break;
            case RotationMode.ViewVector:
                EnterViewVectorRotationMode();
                break;
            case RotationMode.OtherVectors:
                EnterOtherVectorsRotationMode();
                break;
        }
    }

    private void EnterInitialRotationMode(){
        Fragments.BroadcastMessage(
            methodName: "ToggleSelectability",
            parameter: true,
            options: SendMessageOptions.DontRequireReceiver
        );
        Donut.SetActive(true);
        Sphere.SetActive(true);
        ClickPositionIndicator.SetActive(false);
    }

    private void EnterElementRotationMode(){
        Fragments.BroadcastMessage(
            methodName: "ToggleSelectability",
            parameter: false,
            options: SendMessageOptions.DontRequireReceiver
        );        
    }

    private void EnterViewVectorRotationMode(){
        EnterElementRotationMode();
    }

    private void EnterOtherVectorsRotationMode(){
        EnterElementRotationMode();
        Donut.SetActive(false);
    }
}

public enum RotationMode { Initial, ViewVector, OtherVectors };

public class RotationModeChangedMessage{
    public RotationMode NewMode;

    public RotationModeChangedMessage(RotationMode newMode){
        NewMode = newMode;
    }
}