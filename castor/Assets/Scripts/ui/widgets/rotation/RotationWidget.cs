using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationWidget : MonoBehaviour {

    private GameObject Donut;
    private GameObject ClickPositionIndicator;
    private GameObject Sphere;


	void Start () {
        Donut = FindGameObjectByName("OuterDonut");
        ClickPositionIndicator = FindGameObjectByName("ViewVectorClickPositionIndicator");
        Sphere = FindGameObjectByName("InnerSphere");
	}
	
    private GameObject FindGameObjectByName(string childName){
        Transform childTransform = gameObject.transform.Find(childName);
        if(childTransform) return childTransform.gameObject;

        Debug.LogError("Could not find the gameobject with name " + childName);
        return null;
    }

	void Update () {
		
	}
}
