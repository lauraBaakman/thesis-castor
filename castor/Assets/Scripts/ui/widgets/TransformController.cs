using UnityEngine;

public abstract class TransformController : MonoBehaviour
{
    public GameObject Widget;

    protected int NumberOfSelectedObjects = 0;

    public void ToggleActivity(bool toggle)
    {
        Widget.SetActive(toggle);
    }

    public void FragmentSelectionStateChanged(bool newSelectionState){
        FragmentSelected(newSelectionState);
    }

    protected void BaseStart()
    {
        ToggleActivity(false);
    }

    protected abstract void FragmentSelected(bool selected);
}
