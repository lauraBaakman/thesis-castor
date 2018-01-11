using UnityEngine;

public class RotationWidgetTransformController : MonoBehaviour
{

    public GameObject ObjectControlledByWidget;
    public GameObject SizeControllingWidgetElement;

    //The Scaling factor is applied before the minimum and maximum scale.
    public static float ScalingFactor = 2.5f;

    public float MinimumScale = 1.0f;
    public float MaximumScale = 5.0f;


    private void Start() { }

    void Update() { }

    public void OnEnable()
    {
        FitWidgetToControlledObject();
    }

    private void FitWidgetToControlledObject()
    {
        Bounds objectBounds = ObjectControlledByWidget.Bounds();

        float scalingFactor = ComputeLocalScale(objectBounds);
        scalingFactor = Mathf.Clamp(scalingFactor, MinimumScale, MaximumScale);

        transform.position = objectBounds.center;
        transform.localScale = transform.localScale * scalingFactor;
    }

    private float ComputeLocalScale(Bounds ObjectBounds)
    {
        Bounds widgetBounds = SizeControllingWidgetElement.Bounds();
        Vector3 widgetSize = widgetBounds.size;

        Vector3 objectSize = ObjectBounds.size;

        Vector3 localScale = objectSize.DivideElementWise(widgetSize);

        return localScale.Max() * ScalingFactor;
    }
}
