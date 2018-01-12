using System;
using System.Collections.Generic;
using UnityEngine;

public class OtherRotation : RotationWidgetElement
{

    //All stored positions are in world coordinates
    public GameObject Donut;

    private Vector3 ClickPositionOnSphere;

    private Sphere InnerSphere;

    private bool InRotationMode = false;

    void Update()
    {
        if (InRotationMode && CancelButtonPressed()) CancelRotation();
        if (InRotationMode && MouseMoved()) Rotate();
    }

    private void ToggleRotationMode(bool toggle)
    {
        InRotationMode = toggle;
        Donut.SetActive(!toggle);
        Fragments.BroadcastMessage(
            methodName:"ToggleSelectability", 
            parameter:!toggle,
            options:SendMessageOptions.DontRequireReceiver
        );
    }

    private void CancelRotation()
    {
        RotatedObject.transform.rotation = InitialRotation;
        ToggleRotationMode(false);
    }

    private void Rotate()
    {
        Vector3 hoverPosition = MousePositionToSphereCoordinates(Input.mousePosition);
        Quaternion rotation = ComputeRotation(ClickPositionOnSphere, hoverPosition);

        RotatedObject.transform.rotation = rotation * InitialRotation;
    }

    private Quaternion ComputeRotation(Vector3 arcStart, Vector3 arcEnd)
    {
        Vector3 crossProduct = Vector3.Cross(arcStart, arcEnd);
        float dotProduct = Vector3.Dot(arcStart, arcEnd);

        Quaternion rotation = new Quaternion(
            crossProduct.x,
            crossProduct.y,
            crossProduct.z,
            dotProduct
        );
        return rotation;
    }

    public void OnMouseDown()
    {
        if (!InRotationMode)
        {
            InitialRotation = RotatedObject.transform.rotation;
            ClickPositionOnSphere = MousePositionToSphereCoordinates(Input.mousePosition);
        }
        ToggleRotationMode(!InRotationMode);
    }

    private Vector3 MousePositionToSphereCoordinates(Vector3 mousePosition)
    {
        Vector3 spherePoint = (mousePosition - InnerSphere.Center) / InnerSphere.Radius;

        float distance = (spherePoint.x * spherePoint.x)
            + (spherePoint.y * spherePoint.y);

        if (distance > 1.0)
        {
            //mousePosition is outside the sphere
            float s = 1.0f / Mathf.Sqrt(distance);
            spherePoint.x = s * spherePoint.x;
            spherePoint.y = s * spherePoint.y;
            spherePoint.z = 0.0f;
        }
        else
        {
            //mousePosition is within the sphere
            spherePoint.z = Mathf.Sqrt(1 - distance);
        }

        return spherePoint;
    }

    private void OnEnable()
    {
        Bounds innerSphereBounds = gameObject.Bounds();
        float radius = (
            Camera.main.WorldToScreenPoint(innerSphereBounds.max) -
            Camera.main.WorldToScreenPoint(innerSphereBounds.min)
        ).Max();
        InnerSphere = new Sphere(
            center: Camera.main.WorldToScreenPoint(innerSphereBounds.center),
            radius: radius
        );
    }
}

public class Sphere
{
    public Vector3 Center;
    public float Radius;

    public Sphere(Vector3 center, float radius)
    {
        Radius = radius;
        Center = center;
    }

    public override string ToString()
    {
        return "Sphere (center: " + Center + ", radius: " + Radius;
    }
}