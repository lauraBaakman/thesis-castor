using UnityEngine;

public class TranslationWidgetTransformController : WidgetTransformController
{
    public float MinimumScale = 0.5f;
    public float MaximumScale = 5.0f;

    public float ScalingFactor = 1.25f;


    private void Update()
    {
        FitWidgetToControlledObject();
    }

    private void FitWidgetToControlledObject()
    {
        Bounds objectBounds = ObjectControlledByWidget.Bounds();
        Bounds widgetBounds = gameObject.Bounds();

        transform.localScale = ClampScale(
            scale: ComputeScale(objectBounds, widgetBounds),
            widgetTransform: transform
        );
        transform.position = objectBounds.center;
    }

    private Vector3 ClampScale(Vector3 scale, Transform widgetTransform)
    {
        scale = widgetTransform.localScale.Multiply(scale);
        scale = scale.Clamped(MinimumScale, MaximumScale);
        return scale;
    }

    private Vector3 ComputeScale(Bounds objectsBounds, Bounds widgetBounds)
    {
        Vector3 widgetSize = widgetBounds.size;
        Vector3 objectSize = ScalingFactor * objectsBounds.size;

        Vector3 scale = objectSize.DivideElementWise(widgetSize);

        return scale;
    }
}
