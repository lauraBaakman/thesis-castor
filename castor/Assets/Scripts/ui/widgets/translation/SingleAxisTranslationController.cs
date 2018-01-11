using UnityEngine;

public class SingleAxisTranslationController : AbstractAxisController
{

    public string MouseAxis;

    protected override void Translate()
    {
        float speed = Input.GetAxis(MouseAxis);
        Vector3 translation = DirectionVector * speed;
        TranslatedObject.transform.position += translation;
    }
}
