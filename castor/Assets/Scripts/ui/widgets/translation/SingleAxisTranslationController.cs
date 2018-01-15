using UnityEngine;

/// <summary>
/// Controls translations along an axis that directly maps to an axis on the screen.
/// </summary>
public class SingleAxisTranslationController : AbstractAxisController
{
    /// <summary>
    /// The name of the controlled axis as used by Unity.
    /// </summary>
    public string MouseAxis;

    protected override void Translate()
    {
        float speed = Input.GetAxis(MouseAxis);
        Vector3 translation = DirectionVector * speed;
        TranslatedObject.transform.position += translation;
    }
}
