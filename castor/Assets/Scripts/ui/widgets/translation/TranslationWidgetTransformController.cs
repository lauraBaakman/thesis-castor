using UnityEngine;

/// <summary>
/// Translation widget transform controller, controls the transform of the 
/// translation widget. It attempts to ensure that widget is slightly bigger
/// than the translated object, and this it stays wihin a fixed size.
/// </summary>
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
