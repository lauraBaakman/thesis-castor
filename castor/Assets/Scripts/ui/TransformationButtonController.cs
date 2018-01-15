using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// General implementation of the button controller for the buttons that handle 
/// transformations.
/// </summary>
public class TransformationButtonController : MonoBehaviour
{
    private ColorBlock ColorBlock;
    private Image Image;

    /// <summary>
    /// The associated TransformController that enables/disables the widget.
    /// </summary>
    public TransformController Controller;

    private void Awake()
    {
        Toggle toggle = GetComponent<Toggle>();
        ColorBlock = toggle.colors;

        Image = GetComponent<Image>();
    }

    public void OnClick(bool clicked)
    {
        Image.color = clicked ? ColorBlock.pressedColor : ColorBlock.normalColor;
        Controller.ToggleActivity(clicked);
    }

}
