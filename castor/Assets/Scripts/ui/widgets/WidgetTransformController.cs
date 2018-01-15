using UnityEngine;

/// <summary>
/// Abstract implementation of a widget transform controller.
/// </summary>
public abstract class WidgetTransformController : MonoBehaviour
{
    public GameObject ObjectControlledByWidget;

    public float MinimumScale = 1.0f;
    public float MaximumScale = 5.0f;

    public float ScalingFactor = 1.25f;

    private void Update()
    {
        FitWidgetToControlledObject();
    }

    protected abstract void FitWidgetToControlledObject();

    protected Vector3 ClampScale(Vector3 scale, Transform widgetTransform)
    {
        scale = widgetTransform.localScale.Multiply(scale);
        scale = scale.Clamped(MinimumScale, MaximumScale);
        return scale;
    }

    protected Vector3 ComputeScale(Bounds objectsBounds, Bounds widgetBounds)
    {
        Vector3 widgetSize = widgetBounds.size;
        Vector3 objectSize = ScalingFactor * objectsBounds.size;

        Vector3 scale = objectSize.DivideElementWise(widgetSize);

        return scale;
    }
}
