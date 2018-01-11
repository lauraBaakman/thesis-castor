using UnityEngine;

public class TranslationWidgetTransformController : WidgetTransformController
{
    protected override void FitWidgetToControlledObject()
    {
        Bounds objectBounds = ObjectControlledByWidget.Bounds();
        Bounds widgetBounds = gameObject.Bounds();

        transform.localScale = ClampScale(
            scale: ComputeScale(objectBounds, widgetBounds),
            widgetTransform: transform
        );
        transform.position = objectBounds.center;
    }
}
