using UnityEngine;

/// <summary>
/// Controller of translation along the z-axis.
/// </summary>
public class ZAxisTranslationController : AbstractAxisController
{

    private static string VerticalMouseAxis = "Mouse X";
    private static string HorizontalMouseAxis = "Mouse Y";

    protected override void Translate()
    {
        float verticalSpeed = Input.GetAxis(VerticalMouseAxis);
        float horizontalSpeed = Input.GetAxis(HorizontalMouseAxis);

        float diagonalSpeed = Mathf.Sqrt(
            verticalSpeed * verticalSpeed +
            horizontalSpeed * horizontalSpeed
        );

        Vector3 translation = DirectionVector * diagonalSpeed;
        TranslatedObject.transform.position += translation;
    }
}