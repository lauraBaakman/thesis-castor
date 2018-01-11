using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectTranslationController : TransformController {

    public Toggle TranslationToggle;

    public void Start()
    {
        TranslationToggle.interactable = false;
    }

    public override void ToggleActivity(bool toggle)
    {
        Debug.Log("ObjectTranslationController:ToggleActivity");
    }
}
