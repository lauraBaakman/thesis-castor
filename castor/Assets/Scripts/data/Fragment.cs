using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Fragment
{
	public Mesh mesh { get; private set; }

	public string name { get; private set; }

	private GameObject gameobject;

	public Fragment (Mesh mesh, string name)
	{
		this.mesh = mesh;
		this.name = name;
		this.gameobject = null;
	}

	public static Fragment FromFile (string path)
	{
		Mesh mesh = ObjImporter.ImportFile (path);		
		string name = extractNameFromPath (path);

		return new Fragment (mesh, name);
	}

	private static string extractNameFromPath (string path)
	{
		string name = Path.GetFileNameWithoutExtension (path);
		return name;
	}

	public GameObject GameObject ()
	{
		if (gameobject == null) {
			gameobject = createGameObject ();
		}
		return gameobject;
	}

	private GameObject createGameObject ()
	{	
		GameObject gameobject = new GameObject (name);

		MeshRenderer renderer = gameobject.AddComponent<MeshRenderer> ();
		//		renderer.material = Material.Instantiate (material) as Material;

		MeshFilter filter = gameobject.AddComponent<MeshFilter> ();
		filter.mesh = mesh;		

		return gameobject;
	}
}
