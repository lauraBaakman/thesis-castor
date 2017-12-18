using UnityEngine;
using System.Collections;

public class SelectionGhostController : MonoBehaviour
{
    public float Alpha = 0.4f;

    public float scaleFactor = 1.1f;

    public void OnMouseDown()
    {
        Debug.Log("Selected Object: Mouse Down!");
        GetComponentInParent<FragmentController>().ToggleSelection(false);
    }

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
