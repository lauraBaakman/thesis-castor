﻿using UnityEngine;

/// <summary>
/// Transform controller for widgets, that keeps tracks of the number of selected objects.
/// </summary>
public abstract class TransformController : MonoBehaviour
{
    public GameObject Widget;

    private int NumberOfSelectedObjects = 0;

    public virtual void ToggleActivity(bool toggle)
    {
        Widget.SetActive(toggle);
    }

    public void FragmentSelectionStateChanged(bool newSelectionState){
        UpdateNumberOfSelectedObjects(newSelectionState);
        FragmentSelected();
    }

    private void UpdateNumberOfSelectedObjects(bool newSelectionState){
        NumberOfSelectedObjects += (newSelectionState ? 1 : -1);   
    }

    protected void BaseStart()
    {
        ToggleActivity(false);
    }

    protected bool NoObjectsSelected()
    {
        return !ObjectsSelected();
    }

    protected bool ObjectsSelected()
    {
        return NumberOfSelectedObjects > 0;
    }

    protected abstract void FragmentSelected();
}
