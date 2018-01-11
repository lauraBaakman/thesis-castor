using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslationWidgetTransformController : MonoBehaviour {

    public GameObject ObjectControlledByWidget;

    public float MinimumScale = 0.5f;
    public float MaximumScale = 5.0f;


    private void Update()
    {
        FitWidgetToControlledObject();
    }

    private void FitWidgetToControlledObject(){
        Bounds bounds = ObjectControlledByWidget.Bounds();

        Vector3 scale = transform.localScale.Multiply(ComputeScale(bounds));
        scale = scale.Clamped(MinimumScale, MaximumScale);

        transform.localScale = scale;
        transform.position = bounds.center;
    }

    private Vector3 ComputeScale(Bounds objectsBounds){
        Vector3 widgetSize = gameObject.Bounds().size;
        Vector3 objectSize = objectsBounds.size;

        Vector3 scale = objectSize.DivideElementWise(widgetSize);

        return scale;
    }
}
