﻿using UnityEngine;

public class RotationWidget : MonoBehaviour
{

    public GameObject Fragments;

    private GameObject Donut;
    private GameObject ClickPositionIndicator;
    private GameObject Sphere;

    void Awake()
    {
        Donut = gameObject.FindChildByName("OuterDonut");
        ClickPositionIndicator = gameObject.FindChildByName("ViewVectorClickPositionIndicator");
        Sphere = gameObject.FindChildByName("InnerSphere");
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

        Donut.SetActive(true);
        ClickPositionIndicator.SetActive(true);
        Sphere.SetActive(false);
    }

    private void EnterOtherVectorsRotationMode(){
        EnterElementRotationMode();

        Donut.SetActive(false);
        ClickPositionIndicator.SetActive(false);
        Sphere.SetActive(true);
    }

    private void OnEnable()
    {
        EnterInitialRotationMode();
    }
}

public enum RotationMode { Initial, ViewVector, OtherVectors };

public class RotationModeChangedMessage{
    public RotationMode NewMode;

    public RotationModeChangedMessage(RotationMode newMode){
        NewMode = newMode;
    }
}