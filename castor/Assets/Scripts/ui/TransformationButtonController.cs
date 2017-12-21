﻿using UnityEngine;
using UnityEngine.UI;

public class TransformationButtonController : MonoBehaviour
{

    private ColorBlock ColorBlock;
    private Image Image;

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
