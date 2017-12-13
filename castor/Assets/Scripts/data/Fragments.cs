using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public sealed class Fragments
{
	private static readonly Fragments Instance = new Fragments ();

	private List<Fragment> FragmentList;

	private Fragments ()
	{
		FragmentList = new List<Fragment> ();
	}

	public Fragment AddFragmentFromFile(string path){
		string name = ExtractFragmentNameFromPath (path);

		Mesh mesh = ObjImporter.ImportFile(path);

		Fragment fragment = new Fragment(mesh, name);

		FragmentList.Add (fragment);

		return fragment;
    }

	private string ExtractFragmentNameFromPath(string path){
		return Path.GetFileNameWithoutExtension(path);
	}

	/// <summary>
	/// Gets the instance of the singleton.
	/// </summary>
	/// <value>The instance represents the list of fracture fragments.</value>
	public static Fragments GetInstance {
		get {
			return Instance;
		}
	}
}