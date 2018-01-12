using UnityEngine;

public class ViewVectorRotation : RotationWidgetElement
{
    //All stored positions are in world coordinates

    public GameObject RotatedObject;
    public GameObject ClickPositionIndicator;
    public GameObject Sphere;

    public GameObject Fragments;

    private Vector3 WidgetCenter;

    private Quaternion InitialRotation;
    private Vector3 LastClickVector;

    private bool OnWidget = false;
    private int State = 1;

    private void Awake()
    {
    }

    void Start()
    {
        ClickPositionIndicator.SetActive(false);
    }

    public void Update()
    {
        WidgetCenter = gameObject.GetComponent<MeshRenderer>().bounds.center;
        WidgetCenter.z = 0;

        if (CancelButtonPressed())
        {
            OnEsc();
        }

        if (MouseMoved())
        {
            OnMouseMoved();
        }
    }

    public void OnMouseDown()
    {
        switch (State)
        {
            case 1:
                MouseDownState1();
                break;
            case 2:
                MouseDownState2();
                break;
        }
    }

    public void OnMouseMoved()
    {
        switch (State)
        {
            case 1:
                MouseMovedState1();
                break;
            case 2:
                MouseMovedState2();
                break;
        }
    }

    public void OnMouseEnter()
    {
        if (State == 2)
        {
            OnWidget = true;
        }
    }

    public void OnMouseExit()
    {
        if (State == 2)
        {
            OnWidget = false;
        }
    }

    private void MouseMovedState1()
    {
        //Do Nothing
    }

    private void MouseMovedState2()
    {
        if (OnWidget)
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
    }

    private void MouseDownState1()
    {
        //Store Intial Rotation
        InitialRotation = RotatedObject.transform.rotation;

        //Store Mouse Position
        LastClickVector = GetVectorFromWidgetCenterToCurrentMousePosition();

        ShowClickPosition();

        Sphere.SetActive(false);

        State = 2;

        OnWidget = true;

        Fragments.BroadcastMessage(
            methodName: "ToggleSelectability",
            parameter: false,
            options: SendMessageOptions.DontRequireReceiver
        );
    }

    private void ShowClickPosition()
    {
        ClickPositionIndicator.SetActive(true);

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

    private void MouseDownState2()
    {
        StateToOne();
    }

    private void OnEsc()
    {
        switch (State)
        {
            case 1:
                EscState1();
                break;
            case 2:
                EscState2();
                break;
        }
    }

    private void EscState2()
    {
        //Go back to initial rotation
        RotatedObject.transform.rotation = InitialRotation;

        StateToOne();
    }

    private void EscState1()
    {
        //Do nothing
    }

    private void StateToOne()
    {
        State = 1;

        // Reset Object
        Reset();

        //Show Sphere
        Sphere.SetActive(true);

        //Hide Click Position
        ClickPositionIndicator.SetActive(false);

        //Handle Selectability
        Fragments.BroadcastMessage(
            methodName: "ToggleSelectability",
            parameter: true,
            options: SendMessageOptions.DontRequireReceiver
        );
    }

    private void Reset()
    {
        OnWidget = false;
        WidgetCenter = Vector3.zero;
        InitialRotation = Quaternion.identity;
        LastClickVector = Vector3.zero;
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
