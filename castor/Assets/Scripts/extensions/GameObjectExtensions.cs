using System;
using UnityEngine;

public static class GameObjectExtensions
{

	//source https://answers.unity.com/answers/641022/view.html
	/// <summary>
	/// Adds a copy component of type T to the gameobject. 
	/// </summary>
	/// <returns>The copied component.</returns>
	/// <param name="go">Gamobject</param>
	/// <param name="toAdd">The component that should be copied to the gameobeject.</param>
	/// <typeparam name="T">The type of the component.</typeparam>
	public static T AddComponent<T> (this GameObject go, T toAdd) where T : Component
	{
		return go.AddComponent<T> ().GetCopyOf (toAdd) as T;
	}

    //Source: https://forum.unity.com/threads/bounds-of-a-whole-hierarchy.4525/#post-1276595
    public static Bounds Bounds(this GameObject go){
        Bounds bounds = GetInitialBounds(go);
        AddChildrenToBounds(go.transform, bounds);
        return bounds;
    }

    private static void AddChildrenToBounds(Transform child, Bounds bounds){
        MeshRenderer renderer;
        foreach (Transform grandChild in child){
            renderer = grandChild.GetComponent<MeshRenderer>();
            if(renderer){
                bounds.Encapsulate(renderer.bounds.min);    
                bounds.Encapsulate(renderer.bounds.max);    
            }

            AddChildrenToBounds(grandChild, bounds);
        }
    }

    private static Bounds GetInitialBounds(GameObject go){
        MeshRenderer renderer = go.GetComponent<MeshRenderer>();
        if (!renderer)
        {
            renderer = go.GetComponentInChildren<MeshRenderer>();
        }
        return renderer.bounds;
    }
}

