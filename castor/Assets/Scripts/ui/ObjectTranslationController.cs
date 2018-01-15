using UnityEngine.UI;

/// <summary>
/// Rotation controller handles the enable/disabling of the general rotation mode.
/// </summary>
public class ObjectTranslationController : TransformController
{
    /// <summary>
    /// The toggle that enables/disable translation mode.
    /// </summary>
    public Toggle Toggle;

    public void Start()
    {
        Toggle.interactable = false;
        BaseStart();
    }

    private void ExitTranslationMode(){
        Widget.SetActive(false);
        Toggle.isOn = false;
    }

    protected override void FragmentSelected()
    {
        Toggle.interactable = ObjectsSelected();
        if (NoObjectsSelected()) ExitTranslationMode();
    }
}
