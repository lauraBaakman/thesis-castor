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
}
