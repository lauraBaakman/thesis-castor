using UnityEngine;

public class ViewVectorRotation : MonoBehaviour
{
    //All stored positions are in world coordinates

    public GameObject RotatedObject;

    private static Vector3 RotationAxis;
    private static string VerticalMouseAxis = "Mouse X";
    private static string HorizontalMouseAxis = "Mouse Y";

    private Vector3 WidgetCenter;

    private Quaternion InitialRotation;
    private Vector3 InitialVector;

    private bool OnWidget = false;
    private int State = 0;


    private void Awake()
    {
        RotationAxis = new Vector3(0, 0, -1);
    }

    void Start()
    {
        MeshCollider meshCollider = gameObject.AddComponent<MeshCollider>();
    }

    public void Update()
    {
        WidgetCenter = gameObject.GetComponent<MeshRenderer>().bounds.center;

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
            case 0:
                MouseDownState0();
                break;
            case 1:
                MouseDownState1();
                break;
        }
    }

    public void OnMouseMoved()
    {
        switch (State)
        {
            case 0:
                MouseMovedState0();
                break;
            case 1:
                MouseMovedState1();
                break;
        }
    }

    public void OnMouseEnter()
    {
        if (State == 1)
        {
            OnWidget = true;
        }
    }

    public void OnMouseExit()
    {
        if (State == 1)
        {
            OnWidget = false;
        }
    }

    private void MouseMovedState0()
    {
        //Do Nothing
    }

    private void MouseMovedState1()
    {
        if (OnWidget)
        {
            Vector3 currentVector = GetVectorFromCenterToCurrentMousePosition();

            Debug.DrawRay(WidgetCenter, currentVector.normalized * 200, Color.blue, 200);
        }
    }

    private void MouseDownState0()
    {
        //Store Intial Rotation
        InitialRotation = transform.rotation;

        //Store Mouse Position
        InitialVector = GetVectorFromCenterToCurrentMousePosition();

        Debug.DrawRay(WidgetCenter, InitialVector.normalized * 200, Color.red, 200);

        //Show Mouse Position

        State = 1;
    }

    private void MouseDownState1()
    {
        StateToZero();
    }

    private void OnEsc()
    {
        switch (State)
        {
            case 0:
                EscState0();
                break;
            case 1:
                EscState1();
                break;
        }
    }

    private void EscState1()
    {
        //Debug.Log("EscState1");
        RotatedObject.transform.rotation = InitialRotation;

        StateToZero();
    }

    private void EscState0()
    {
        //Debug.Log("EscState0");
        //Do nothing
    }

    private void StateToZero()
    {
        State = 0;
        OnWidget = false;
    }

    private Vector3 GetVectorFromCenterToCurrentMousePosition()
    {
        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 vector = position - WidgetCenter;
        return vector;
    }
}
