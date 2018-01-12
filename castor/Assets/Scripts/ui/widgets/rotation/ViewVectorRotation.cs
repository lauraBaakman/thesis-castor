﻿using UnityEngine;

public class ViewVectorRotation : RotationWidgetElement
{
    //All stored positions are in world coordinates
    public GameObject ClickPositionIndicator;

    private Vector3 WidgetCenter;
    private Vector3 LastClickVector;

    private bool InRotationMode = false;
    private bool OnWidget = false;

    public void Update()
    {
        UpdateWidgetCenter();

        if (InRotationMode && CancelButtonPressed()) CancelRotation();
        if (InRotationMode && MouseMovedOnWidget()) Rotate();
    }

    private void UpdateWidgetCenter()
    {
        WidgetCenter = gameObject.GetComponent<MeshRenderer>().bounds.center;
        WidgetCenter.z = 0;
    }

    private bool MouseMovedOnWidget()
    {
        return MouseMoved() && OnWidget;
    }

    public void OnMouseEnter()
    {
        OnWidget = true;
    }

    public void OnMouseExit()
    {
        OnWidget = false;
    }

    private void Rotate()
    {
        float angle = ComputeAngleBetweenPointAndMousePosition(LastClickVector);

        //Apply rotation to RotatedObject
        Vector3 position = RotatedObject.transform.position;
        RotatedObject.transform.position = Vector3.zero;
        RotatedObject.transform.Rotate(RotatedObject.transform.forward, angle, Space.Self);
        RotatedObject.transform.position = position;

        //Update the last click vector
        LastClickVector = GetVectorFromWidgetCenterToCurrentMousePosition();
    }

    public void OnMouseDown()
    {
        if (!InRotationMode) InitializeRotationMode();
        ToggleRotationMode(!InRotationMode);
    }

    private void InitializeRotationMode()
    {
        StoreInitialRotation();

        //Store Mouse Position
        LastClickVector = GetVectorFromWidgetCenterToCurrentMousePosition();

        ShowClickPosition();
    }

    protected override void ToggleRotationMode(bool toggle)
    {
        InRotationMode = toggle;

        RotationMode newMode = toggle ? RotationMode.ViewVector : RotationMode.Initial;
        gameObject.SendMessageUpwards(
            methodName: "OnRotationModeChanged",
            value: new RotationModeChangedMessage(newMode)
        );
    }

    private void ShowClickPosition()
    {
        Vector3 initialVector = GetVectorFromWidgetCenterTo(ClickPositionIndicator.transform.position);
        float angle = ComputeAngleBetweenPointAndMousePosition(initialVector);
        ClickPositionIndicator.transform.RotateAround(WidgetCenter, Vector3.forward, angle);
    }

    //Angle is based on the widget center in the x,y plane
    private float ComputeAngleBetweenPointAndMousePosition(Vector3 point)
    {
        Vector3 currentVector = GetVectorFromWidgetCenterToCurrentMousePosition();
        float angle = Vector3.SignedAngle(point, currentVector, Vector3.forward);
        return angle;
    }

    private Vector3 GetVectorFromWidgetCenterToCurrentMousePosition()
    {
        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        position.z = 0;

        return GetVectorFromWidgetCenterTo(position);
    }

    private Vector3 GetVectorFromWidgetCenterTo(Vector3 destination)
    {
        Vector3 vector = destination - WidgetCenter;
        return vector;
    }
}
