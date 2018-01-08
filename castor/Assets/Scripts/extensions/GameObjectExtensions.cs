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

	/// <summary>
	/// Compute the bounds of gameobject, taking all its active children into 
    /// account.
	/// </summary>
	/// <param name="go">Gameobject</param>
	public static Bounds Bounds (this GameObject go)
	{
		Bounds bounds = new Bounds (); 

		Renderer renderer = go.GetComponent<Renderer> ();
		if (renderer != null) {
			bounds.Encapsulate (renderer.bounds);
		}
		Renderer[] renderers = go.GetComponentsInChildren<Renderer> ();
		foreach (Renderer childRenderer in renderers) {
			bounds.Encapsulate (childRenderer.bounds);
		}

		return bounds;
	}
}

