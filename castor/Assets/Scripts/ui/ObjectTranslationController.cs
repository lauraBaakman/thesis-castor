using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectTranslationController : TransformController
{

    public Toggle Toggle;
    public GameObject Widget;

    private int NumberOfSelectedObjects = 0;

    public void Start()
    {
        Toggle.interactable = false;
        Widget.SetActive(false);
    }

    public override void ToggleActivity(bool toggle)
    {
        Widget.SetActive(toggle);
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
