using UnityEngine;

[RequireComponent(typeof(FragmentController))]

public class SelectableFragmentController : MonoBehaviour
{
    GameObject Ghost;

    public float scaleFactor = 1.3f;
    public float Alpha = 0.1f;

    private void Awake()
    {
        Ghost = new GameObject();
    }

    void Start()
    {
    }

    void Update()
    {

    }

    public void Populate(GameObject selectable)
    {
        Ghost.name = selectable.name + " ghost";
        Ghost.transform.parent = selectable.transform;

        CopyParenComponentsToGhost(selectable);

        IncreaseGhostSize();

        Ghost.GetComponent<MeshRenderer>().material = BuildGhostMaterial(selectable);
    }

    private void CopyParenComponentsToGhost(GameObject parent){
        Ghost.AddComponent<MeshFilter>(parent.GetComponent<MeshFilter>());
        Ghost.AddComponent<MeshRenderer>(parent.GetComponent<MeshRenderer>());
    }

    private void IncreaseGhostSize(){
        Ghost.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
    }

    private Material BuildGhostMaterial(GameObject parent){
        Material parentMaterial = parent.GetComponent<MeshRenderer>().material;
        Color parentColor = parentMaterial.color;        

        Material ghostMaterial = parent.GetComponent<FragmentController>().DefaultMaterial;

        //Source: https://forum.unity.com/threads/access-rendering-mode-var-on-standard-shader-via-scripting.287002/#post-1911639
        Color ghostColor = new Color(parentColor.r, parentColor.g, parentColor.b, Alpha);
        ghostMaterial.EnableKeyword("_ALPHABLEND_ON");
        ghostMaterial.SetColor("_Color", ghostColor);
        ghostMaterial.SetFloat("_Mode", 3.0f);

        ghostMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        ghostMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        ghostMaterial.SetInt("_ZWrite", 0);
        ghostMaterial.DisableKeyword("_ALPHATEST_ON");
        ghostMaterial.EnableKeyword("_ALPHABLEND_ON");
        ghostMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        ghostMaterial.renderQueue = 3000;

        return ghostMaterial;
    }
}