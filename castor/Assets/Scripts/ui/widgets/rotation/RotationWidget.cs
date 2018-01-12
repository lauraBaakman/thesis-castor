using UnityEngine;

public class RotationWidget : MonoBehaviour
{

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
                Debug.Log("To Initial Rotation Mode");
                break;
            case RotationMode.ViewVector:
                Debug.Log("To ViewVector Rotation Mode" );
                break;
            case RotationMode.OtherVectors:
                Debug.Log("To OtherVectors Rotation Mode");
                break;
            default:
                Debug.Log("Oh No Entered the Default Mode");
                break;
        }
    }
}

public enum RotationMode { Initial, ViewVector, OtherVectors };

public class RotationModeChangedMessage{
    public RotationMode NewMode;

    public RotationModeChangedMessage(RotationMode newMode){
        NewMode = newMode;
    }
}