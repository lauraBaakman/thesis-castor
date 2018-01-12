using UnityEngine.UI;

public class ObjectTranslationController : TransformController
{

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

    protected override void FragmentSelected(bool selected)
    {
        Toggle.interactable = ObjectsSelected();
        if (NoObjectsSelected()) ExitTranslationMode();
    }
}
