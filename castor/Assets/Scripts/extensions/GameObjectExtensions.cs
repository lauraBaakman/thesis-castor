using UnityEngine;

/// <summary>
/// Game object extensions contains extensions for GameObjects.
/// </summary>
public static class GameObjectExtensions
{

	/// <summary>
	/// Adds a copy component of type T to the gameobject.
    /// source https://answers.unity.com/answers/641022/view.html
	/// </summary>
	/// <returns>The copied component.</returns>
	/// <param name="go">Gamobject</param>
	/// <param name="toAdd">The component that should be copied to the gameobeject.</param>
	/// <typeparam name="T">The type of the component.</typeparam>
	public static T AddComponent<T> (this GameObject go, T toAdd) where T : Component
	{
		return go.AddComponent<T> ().GetCopyOf (toAdd) as T;
	}

    /// <summary>
    /// Finds a child gameobject of this gameobject by name.
    /// </summary>
    /// <returns>The child.</returns>
    /// <param name="go">This gamobject</param>
    /// <param name="childName">The name of the looked for child object.</param>
    public static GameObject FindChildByName(this GameObject go, string childName)
    {
        Transform childTransform = go.transform.Find(childName);
        if (childTransform) return childTransform.gameObject;

        Debug.LogError("Could not find the gameobject with name " + childName);
        return null;
    }

	/// <summary>
	/// Compute the bounds the specified gameobject based on the MeshRenderers attatched to it
	/// and its children.
    /// Source: https://forum.unity.com/threads/bounds-of-a-whole-hierarchy.4525/#post-1276595
	/// </summary>
	/// <param name="go">The gameobject of which the bounds need to be compuated.</param>
	public static Bounds Bounds (this GameObject go)
	{
		Bounds bounds = GetInitialBounds (go);
		bounds = AddChildrenToBounds (go.transform, bounds);
		return bounds;
	}

	private static Bounds AddChildrenToBounds (Transform child, Bounds bounds)
	{
		MeshRenderer renderer;
		foreach (Transform grandChild in child) {
			renderer = grandChild.GetComponent<MeshRenderer> ();
			if (renderer) {
				bounds.Encapsulate (renderer.bounds.min);
				bounds.Encapsulate (renderer.bounds.max);
			}

			bounds = AddChildrenToBounds (grandChild, bounds);
		}
		return bounds;
	}

	private static Bounds GetInitialBounds (GameObject go)
	{
        MeshRenderer renderer = FindMeshRenderer(go);
        Bounds bounds = renderer ? renderer.bounds : new Bounds();

        return bounds;
	}

    private static MeshRenderer FindMeshRenderer(GameObject go){
        MeshRenderer renderer = go.GetComponent<MeshRenderer>();
        if (!renderer) renderer = go.GetComponentInChildren<MeshRenderer>();
        return renderer;
    }
}

