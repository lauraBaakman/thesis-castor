using System;
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

    private Vector3 ClickPosition;

    private Sphere InnerSphere;

    private bool OnWidget = false;
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

    public void OnMouseEnter()
    {
        OnWidget = true;
    }

    public void OnMouseExit()
    {
        OnWidget = false;
    }

    private void ToggleRotationMode(bool toggle){
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
        Quaternion rotation = ComputeRotation(ClickPosition, hoverPosition);

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
        if (InRotationMode){
            InitialRotation = RotatedObject.transform.rotation;
            ClickPosition = MousePositionToSphereCoordinates(Input.mousePosition);   
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
        } else {
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

    private void ToggleMeshCollidersOnFragments(bool toggle)
    {
        MeshCollider[] colliders = Fragments.GetComponentsInChildren<MeshCollider>();

        foreach (MeshCollider meshCollider in colliders)
            meshCollider.enabled = toggle;
    }

    public void OnDrawGizmos()
    {
        //Bounds bounds = gameObject.Bounds();
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(bounds.center, bounds.extents.Max());
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