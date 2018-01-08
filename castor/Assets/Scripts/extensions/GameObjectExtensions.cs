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
}

