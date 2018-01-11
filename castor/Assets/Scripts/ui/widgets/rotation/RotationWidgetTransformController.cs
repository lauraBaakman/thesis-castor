using UnityEngine;

public class RotationWidgetTransformController : WidgetTransformController
{
    public GameObject SizeControllingWidgetElement;

    private float ComputeLocalScale(Bounds ObjectBounds)
    {
        Bounds widgetBounds = SizeControllingWidgetElement.Bounds();
        Vector3 widgetSize = widgetBounds.size;

        Vector3 objectSize = ObjectBounds.size;

        Vector3 localScale = objectSize.DivideElementWise(widgetSize);

        return localScale.Max() * ScalingFactor;
    }

    protected override void FitWidgetToControlledObject()
    {
        Bounds objectBounds = ObjectControlledByWidget.Bounds();

        float scalingFactor = ComputeLocalScale(objectBounds);
        scalingFactor = Mathf.Clamp(scalingFactor, MinimumScale, MaximumScale);

        transform.position = objectBounds.center;
        transform.localScale = transform.localScale * scalingFactor;
    }
}
