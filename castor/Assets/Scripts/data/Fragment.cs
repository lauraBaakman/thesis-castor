using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Fragment
{
	/// <summary>
	/// Gets the mesh of the fragment.
	/// </summary>
	/// <value>The mesh of the fragment.</value>
	public Mesh Mesh { get; private set; }

	/// <summary>
	/// Gets the name of the fragment.
	/// </summary>
	/// <value>The name of the fragment.</value>
	public string Name { get; private set; }

	private GameObject GameObject;

	/// <summary>
	/// Initializes a new instance of the <see cref="Fragment"/> class.
	/// </summary>
	/// <param name="mesh">Mesh contains the mesh that represents the fragment.</param>
	/// <param name="name">Name contains the name of the fragment.</param>
	public Fragment (Mesh mesh, string name)
	{
		this.Mesh = mesh;
		this.Name = name;
		this.GameObject = null;
	}

	/// <summary>
	/// Reades a fragment from a file.
	/// </summary>
	/// <returns>A new fragment with the name of the file as its name.</returns>
	/// <param name="path">Path the absolute path to the file from which the fragment should be read.</param>
	public static Fragment FromFile (string path)
	{
		Mesh mesh = ObjImporter.ImportFile (path);		
		string name = ExtractNameFromPath (path);

		return new Fragment (mesh, name);
	}

	private static string ExtractNameFromPath (string path)
	{
		string name = Path.GetFileNameWithoutExtension (path);
		return name;
	}

	/// <summary>
	/// Gets the gameobject associated with the fragment. If no such gameobject exists it is created.
	/// </summary>
	/// <returns>The gameobject object associated with the fragment.</returns>
	public GameObject GetGameObject ()
	{
		if (GameObject == null) {
			GameObject = CreateGameObject ();
		}
		return GameObject;
	}

	private GameObject CreateGameObject ()
	{	
		GameObject gameobject = new GameObject (Name);

		MeshRenderer renderer = gameobject.AddComponent<MeshRenderer> ();
		renderer.material = new Material (Shader.Find ("Standard"));

		MeshFilter filter = gameobject.AddComponent<MeshFilter> ();
		filter.mesh = Mesh;		

		FragmentComponent fragmentComponent = gameobject.AddComponent<FragmentComponent> ();
		fragmentComponent.Fragment = this;

		return gameobject;
	}
}
