using UnityEngine;

/// <summary>
/// Component attatched to the objects used to indicate selection.
/// </summary>
public class SelectionGhostController : MonoBehaviour
{
    /// <summary>
    /// The alpha value of the ghost objects.
    /// </summary>
    public float Alpha = 0.4f;

    /// <summary>
    /// The scale factor of the ghost objects.
    /// </summary>
    public float scaleFactor = 1.1f;

    /// <summary>
    /// If the user clicks the ghost object the associated object is deselected.
    /// </summary>
    public void OnMouseDown()
    {
        GetComponentInParent<FragmentController>().ToggleSelection(false);
    }

    public void Update()
    {
        if (Input.GetButtonDown("Delete"))
        {
            GetComponentInParent<FragmentController>().DeleteFragment();
        }
        if (Input.GetButtonDown("Hide")){
            GetComponentInParent<FragmentController>().ToggleVisibility(false);
        }
    }

    /// <summary>
    /// Populate the ghost object based on the specified parent, i.e. the objects
    /// whose selection the ghost object indicates.
    /// </summary>
    /// <param name="parent">The parent of the current object.</param>
    public void Populate(GameObject parent)
    {
        name = DetermineName(parent.name);
        transform.parent = parent.transform;

        gameObject.AddComponent<MeshFilter>(
             parent.GetComponent<MeshFilter>()
        );

        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>(
            parent.GetComponent<MeshRenderer>()
        );
        meshRenderer.material = BuildMaterial(
            parent.GetComponent<MeshRenderer>().material.color
        );

        gameObject.AddComponent<MeshCollider>();

        IncreaseSize();
    }

    private string DetermineName(string parentName)
    {
        return parentName + " ghost";
    }

    private void IncreaseSize()
    {
        transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
    }

    private Material BuildMaterial(Color parentColor)
    {
        Material material = FragmentController.DefaultMaterial;

        //Source: https://forum.unity.com/threads/access-rendering-mode-var-on-standard-shader-via-scripting.287002/#post-1911639
        Color ghostColor = new Color(parentColor.r, parentColor.g, parentColor.b, Alpha);
        material.EnableKeyword("_ALPHABLEND_ON");
        material.SetColor("_Color", ghostColor);
        material.SetFloat("_Mode", 3.0f);

        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        material.SetInt("_ZWrite", 0);
        material.DisableKeyword("_ALPHATEST_ON");
        material.EnableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = 3000;

        return material;
    }
}
