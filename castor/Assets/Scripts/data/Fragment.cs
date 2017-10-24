using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Fragment
{
	public Mesh mesh { get; private set; }

	public string name { get; private set; }

	public Fragment (Mesh mesh, string name)
	{
		this.mesh = mesh;
		this.name = name;
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
}
