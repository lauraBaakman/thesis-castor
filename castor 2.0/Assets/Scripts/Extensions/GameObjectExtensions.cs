using UnityEngine;

public static class GameObjectExtensions
{
    /// <summary>
    /// Find a child gameobject of this gameobject by name.
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
}
