using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public abstract class AbstractAxisController : MonoBehaviour {

    //All stored positions are in world space
    public Material SelectedMaterial;
    private Material NormalMaterial;

    public GameObject TranslatedObject;

    private bool InAxisTranslationMode = false;

    private MeshRenderer MeshRenderer;

    private static string VerticalMouseAxis = "Mouse X";
    private static string HorizontalMouseAxis = "Mouse Y";

    protected float ScalingFactor = 5.0f;

    private Vector3 InitialPosition;
    public Vector3 DirectionVector;

	void Awake () {
        MeshRenderer = gameObject.GetComponent<MeshRenderer>();
        NormalMaterial = MeshRenderer.material;
	}
	
	void Update () {
        if (InAxisTranslationMode && CancelButtonPressed()) CancelTranslation();
        if (InAxisTranslationMode && MouseMoved()) Translate();
	}

    private bool CancelButtonPressed()
    {
        return Input.GetButton("Cancel");
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
        if(!InAxisTranslationMode){
            InitialPosition = TranslatedObject.transform.position;
        }
        ToggleAxisTranslationMode(!InAxisTranslationMode);   
    }

    private void ToggleAxisTranslationMode(bool toggle){
        InAxisTranslationMode = toggle;
        MeshRenderer.material = toggle ? SelectedMaterial : NormalMaterial;        
    }

    private void OnEnable()
    {
        ToggleAxisTranslationMode(false);
    }

    private void CancelTranslation(){
        TranslatedObject.transform.position = InitialPosition;
        ToggleAxisTranslationMode(false);
    }

    protected abstract void Translate();
}