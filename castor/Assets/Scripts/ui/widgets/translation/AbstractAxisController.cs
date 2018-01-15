using UnityEngine;

/// <summary>
/// Abstract axis controller, abstract implementation of the translation along a 
/// single axis.
/// 
/// All stored positions are in world space
/// </summary>
[RequireComponent(typeof(MeshRenderer))]
public abstract class AbstractAxisController : MonoBehaviour
{

    /// <summary>
    /// The material of the axis when the axis is selected.
    /// </summary>
    public Material SelectedMaterial;
    private Material NormalMaterial;

    /// <summary>
    /// The gamobject that is translatd.
    /// </summary>
    public GameObject TranslatedObject;

    private bool InAxisTranslationMode = false;

    private MeshRenderer MeshRenderer;

    private static string VerticalMouseAxis = "Mouse X";
    private static string HorizontalMouseAxis = "Mouse Y";

    private Vector3 InitialPosition;

    /// <summary>
    /// The vector along which the translation occurs that is controlled by this
    ///  axis of the widget.
    /// </summary>
    public Vector3 DirectionVector;

    void Awake()
    {
        MeshRenderer = gameObject.GetComponent<MeshRenderer>();
        NormalMaterial = MeshRenderer.material;
    }

    void Update()
    {
        if (InAxisTranslationMode && CancelButtonPressed()) CancelTranslation();
        if (InAxisTranslationMode && MouseMoved()) Translate();
        if (InAxisTranslationMode && EnterButtonPressed()) ToggleAxisTranslationMode(false);
    }

    private bool CancelButtonPressed()
    {
        return Input.GetButton("Cancel");
    }

    private bool EnterButtonPressed()
    {
        return Input.GetKey(KeyCode.Return);
    }

    private bool MouseMoved()
    {
        bool mouseMoved = (
            !Input.GetAxis(VerticalMouseAxis).Equals(0.0f) ||
            !Input.GetAxis(HorizontalMouseAxis).Equals(0.0f)
        );
        return mouseMoved;
    }

    private void OnMouseDown()
    {
        if (!InAxisTranslationMode)
        {
            InitialPosition = TranslatedObject.transform.position;
        }
        ToggleAxisTranslationMode(!InAxisTranslationMode);
    }

    private void ToggleAxisTranslationMode(bool toggle)
    {
        InAxisTranslationMode = toggle;
        MeshRenderer.material = toggle ? SelectedMaterial : NormalMaterial;
    }

    private void OnEnable()
    {
        ToggleAxisTranslationMode(false);
    }

    private void CancelTranslation()
    {
        TranslatedObject.transform.position = InitialPosition;
        ToggleAxisTranslationMode(false);
    }

    protected abstract void Translate();
}