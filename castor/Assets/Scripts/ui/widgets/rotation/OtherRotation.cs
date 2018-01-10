using System;
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

    private Vector3 ClickPositionOnSphere;
    private Vector3 ClickPositionWorld;

    private Sphere InnerSphere;

    private bool InRotationMode = false;

    private void Awake()
    {
        gameObject.AddComponent<MeshCollider>();
    }

    void Update()
    {
        if (InRotationMode && CancelButtonPressed()) CancelRotation();
        if (InRotationMode && MouseMoved()) Rotate();
    }

    private bool MouseMoved()
    {
        bool mouseMoved = (
            !Input.GetAxis(VerticalMouseAxis).Equals(0.0f) ||
            !Input.GetAxis(HorizontalMouseAxis).Equals(0.0f)
        );
        return mouseMoved;
    }

    private bool CancelButtonPressed()
    {
        return Input.GetButton("Cancel");
    }


    private void ToggleRotationMode(bool toggle)
    {
        InRotationMode = toggle;
        Donut.SetActive(!toggle);

        ToggleMeshCollidersOnFragments(!toggle);
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

        //DrawArc(arcStart: ClickPositionWorld,
        //        arcEnd: Camera.main.ScreenToWorldPoint(Input.mousePosition),
        //        numPieces: 3
        //);

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
            ClickPositionWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
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

        //Temporary
        DrawArc(arcStart:Camera.main.ScreenToWorldPoint(new Vector3(876.8f, 579.7f, 0.0f)),
                arcEnd:Camera.main.ScreenToWorldPoint(new Vector3(868.5f, 462.9f, 0.0f)), 
                numPieces:3);
    }

    private void ToggleMeshCollidersOnFragments(bool toggle)
    {
        MeshCollider[] colliders = Fragments.GetComponentsInChildren<MeshCollider>();

        foreach (MeshCollider meshCollider in colliders)
            meshCollider.enabled = toggle;
    }

    //Source: Shoemake, Ken. "Animating rotation with quaternion curves." ACM SIGGRAPH computer graphics. Vol. 19. No. 3. ACM, 1985.
    private void DrawArc(Vector3 arcStart, Vector3 arcEnd, int numPieces = 10)
    {
        //TODO only draw if arcEnd is on the sphere
        //TODO fix the computation of the arc points, right now it seems to draw a straight line
        //TODO make the lines show up in game view

        if (arcStart.Equals(arcEnd)) return;

        float dotProduct = Vector3.Dot(arcStart, arcEnd);

        List<Vector3> points = new List<Vector3>(numPieces + 1);

        float angle = Mathf.Acos(dotProduct / (arcStart.magnitude * arcEnd.magnitude));
        float sinAngle = Mathf.Sin(angle);

        Vector3 point;
        float stepSize = 1.0f / numPieces;

        points.Add(arcStart);
        for (float u = stepSize; u < 1.0; u += stepSize)
        {
            point = (Mathf.Sin((1 - u) * angle) / sinAngle) * arcStart +
                     (Mathf.Sin(u * angle) / sinAngle) * arcEnd;
            points.Add(point);
        }
        points.Add(arcEnd);

        Vector3 previous, current;
        previous = points[0];
        for (int i = 1; i < points.Count; i++)
        {
            current = points[i];

            Debug.DrawLine(previous, current, Color.red, duration:1, depthTest:false);

            previous = current;
        }
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