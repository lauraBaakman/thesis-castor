using UnityEngine;

/// <summary>
/// Rotation widget transform controller, controls the transform of the rotation 
/// widget. It attempts to ensure that the RotatedObject fits within the sphere
///  of the widget.
/// </summary>
public class RotationWidgetTransformController : WidgetTransformController
{
    /// <summary>
    /// The element of the widget that controls the size of the complete widget.
    /// </summary>
    public GameObject SizeControllingWidgetElement;

    protected override void FitWidgetToControlledObject()
    {
        Bounds objectBounds = ObjectControlledByWidget.Bounds();
        Bounds widgetBounds = SizeControllingWidgetElement.Bounds();

        Vector3 scale = ClampScale(
            scale: ComputeScale(objectBounds, widgetBounds),
            widgetTransform: transform
        );

        float uniformScale = scale.Max();

        transform.localScale = new Vector3().Fill(uniformScale);
        transform.position = objectBounds.center;
    }
}
