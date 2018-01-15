using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// Fragments, list with all the fragments that were imported.
/// </summary>
public sealed class Fragments
{
	private static readonly Fragments Instance = new Fragments ();

    private readonly List<Fragment> FragmentList;

    private Fragments ()
	{
		FragmentList = new List<Fragment> ();
	}

	/// <summary>
	/// Gets the instance of the singleton.
	/// </summary>
	/// <value>The Singleton with fragments.</value>
	public static Fragments GetInstance {
		get {
			return Instance;
		}
	}

	/// <summary>
	/// Add a the fragment from a file. The name of the fragment is determined based on the file name.
	/// </summary>
	/// <returns>The fragment read from the file.</returns>
	/// <param name="path">The absolute path to the file containing the fragment.</param>
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
	/// Removes the fragment from the list of fragments.
	/// </summary>
	/// <returns><c>true</c>, if fragment was succesfully removed, <c>false</c> otherwise.</returns>
	/// <param name="fragment">The fragment to be removed.</param>
	public bool RemoveFragment(Fragment fragment){
		bool succes = FragmentList.Remove (fragment);
		return succes;
	}
}