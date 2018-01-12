using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectTranslationController : TransformController
{

    public Toggle Toggle;

    public void Start()
    {
        Toggle.interactable = false;
        BaseStart();
    }

    public void FragmentSelected(bool selected)
    {
        NumberOfSelectedObjects += (selected ? 1 : -1);
        Toggle.interactable = ObjectsSelected();

        if (NoObjectsSelected()) ExitTranslationMode();
    }

    private void ExitTranslationMode(){
        Widget.SetActive(false);
        Toggle.isOn = false;
    }

    private bool NoObjectsSelected(){
        return !ObjectsSelected();
    }

    private bool ObjectsSelected()
    {
        return NumberOfSelectedObjects > 0;
    }
}
