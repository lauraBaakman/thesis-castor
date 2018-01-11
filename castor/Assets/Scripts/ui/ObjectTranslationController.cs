using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectTranslationController : TransformController
{

    public Toggle TranslationToggle;

    private int NumberOfSelectedObjects = 0;

    public void Start()
    {
        TranslationToggle.interactable = false;
    }

    public override void ToggleActivity(bool toggle)
    {
        Debug.Log("ObjectTranslationController:ToggleActivity");
    }

    public void FragmentSelected(bool selected)
    {
        NumberOfSelectedObjects += (selected ? 1 : -1);
        TranslationToggle.interactable = (NumberOfSelectedObjects > 0);
    }
}
