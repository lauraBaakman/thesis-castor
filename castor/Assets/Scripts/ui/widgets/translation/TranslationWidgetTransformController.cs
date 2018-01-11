using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslationWidgetTransformController : MonoBehaviour {

    public GameObject ObjectControlledByWidget;

    public float MinimumScale = 1.0f;
    public float MaximumScale = 9.0f;

    private void Update()
    {
        FitWidgetToControlledObject();
    }

    private void FitWidgetToControlledObject(){
        Bounds bounds = ObjectControlledByWidget.Bounds();

        transform.localScale = ComputeLocalScale(bounds);
        transform.position = bounds.center;
    }

    private Vector3 ComputeLocalScale(Bounds bounds){
        //TODO Implement
        return new Vector3(5.0f, 5.0f, 5.0f);
    }
}
