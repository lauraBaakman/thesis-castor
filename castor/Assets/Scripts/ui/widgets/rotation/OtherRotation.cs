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
        if (InRotationMode && MouseMovedOnWidget()) Rotate();
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
        ClickPosition = MousePositionToSphereCoordinates(Input.mousePosition);

        ToggleMeshCollidersOnFragments(false);
    }

    private void ExitRotationMode()
    {
        Debug.Log("Exit Rotation Mode");
        InRotationMode = false;
        Donut.SetActive(true);

        ToggleMeshCollidersOnFragments(true);
    }

    private void CancelRotation()
    {
        RotatedObject.transform.rotation = InitialRotation;
        ExitRotationMode();
    }

    private void Rotate()
    {
        Debug.Log("Rotate!");
        Vector3 mousePosition = MousePositionToSphereCoordinates(Input.mousePosition);

        //TODO Compute rotation
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

    private Vector3 MousePositionToSphereCoordinates(Vector3 mousePosition)
    {
        // X and y of the mouse position on the sphere, with (0, 0, 0) in the center of the sphere
        Vector3 spherePosition = gameObject.transform.InverseTransformPoint(
            Camera.main.ScreenToWorldPoint(mousePosition)
        );

        spherePosition.z = Mathf.Abs(
            Mathf.Sqrt(
                (Radius * Radius) -
                (spherePosition.x * spherePosition.x) -
                (spherePosition.y * spherePosition.y)
            )
        );

        return spherePosition;
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

    public override string ToString(){
        return "Sphere (center: " + Center + ", radius: " + Radius;   
    }
}