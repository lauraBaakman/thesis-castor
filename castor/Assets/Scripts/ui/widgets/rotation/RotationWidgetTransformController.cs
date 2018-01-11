using UnityEngine;

public class RotationWidgetTransformController : WidgetTransformController
{
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
