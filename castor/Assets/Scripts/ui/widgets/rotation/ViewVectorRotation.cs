using UnityEngine;

public class ViewVectorRotation : MonoBehaviour
{
    //All stored positions are in world coordinates

    public GameObject RotatedObject;
    public GameObject ClickPositionIndicator;

    private static Vector3 RotationAxis;
    private static string VerticalMouseAxis = "Mouse X";
    private static string HorizontalMouseAxis = "Mouse Y";

    private Vector3 WidgetCenter;

    private Quaternion InitialRotation;
    private Vector3 InitialVector;

    private bool OnWidget = false;
    private int State = 1;


    private void Awake()
    {
        RotationAxis = new Vector3(0, 0, 1);
    }

    void Start()
    {
        gameObject.AddComponent<MeshCollider>();
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

    private bool MouseMoved()
    {
        return (!Input.GetAxis(VerticalMouseAxis).Equals(0.0f) ||
                !Input.GetAxis(HorizontalMouseAxis).Equals(0.0f));
    }

    private bool CancelButtonPressed()
    {
        return Input.GetButton("Cancel");
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
            // Show rotation
            Vector3 currentVector = GetVectorFromCenterToCurrentMousePosition();
            Debug.DrawRay(WidgetCenter, currentVector.normalized * 200, Color.blue, 200);
            float angle = Vector3.SignedAngle(InitialVector, currentVector, RotationAxis);

            //TODO Apply rotation to RotatedObject
        }
    }

    private void MouseDownState1()
    {
        //Store Intial Rotation
        InitialRotation = transform.rotation;

        //Store Mouse Position
        InitialVector = GetVectorFromCenterToCurrentMousePosition();
        Debug.DrawRay(WidgetCenter, InitialVector.normalized * 200, Color.red, 200);

        //TODO Hide Sphere

        //Show Click Position
        ClickPositionIndicator.SetActive(true);
        Vector3 currentVector = GetVectorFromCenterToCurrentMousePosition();
        Vector3 position = ClickPositionIndicator.transform.position;
        Vector3 initialVector = position - WidgetCenter;
        float angle = Vector3.SignedAngle(initialVector, currentVector, RotationAxis);
        ClickPositionIndicator.transform.RotateAround(WidgetCenter, RotationAxis, angle);
        //TODO rotate indicator to indicate position

        State = 2;
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
        OnWidget = false;
        WidgetCenter = Vector3.zero;
        InitialRotation = Quaternion.identity;
        InitialVector = Vector3.zero;

        //TODO Show Sphere

        //Hide Click Position
        ClickPositionIndicator.SetActive(false);
    }

    private Vector3 GetVectorFromCenterToCurrentMousePosition()
    {
        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        position.z = 0;

        Vector3 vector = position - WidgetCenter;
        return vector;
    }
}
