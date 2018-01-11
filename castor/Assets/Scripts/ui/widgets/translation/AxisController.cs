using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class AxisController : MonoBehaviour {

    public Material SelectedMaterial;
    private Material NormalMaterial;

    private bool InAxisTranslationMode = false;

    private MeshRenderer MeshRenderer;

	void Start () {
        MeshRenderer = gameObject.GetComponent<MeshRenderer>();
        NormalMaterial = MeshRenderer.material;
	}
	
	void Update () {
		
	}

    private void OnMouseDown()
    {
        Debug.Log("Detected A click!");
    }

    private void EnterAxisTranslationMode(){
        InAxisTranslationMode = true;
        MeshRenderer.material = SelectedMaterial;
    }

    private void ExitAxisTranslationMode(){
        InAxisTranslationMode = false;
        MeshRenderer.material = NormalMaterial;
    }

    private void OnEnable()
    {
        ExitAxisTranslationMode();
    }
}
