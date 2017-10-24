using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentGO
{
	public GameObject GO { get; private set; }

	private Fragment fragment;

	public FragmentGO (Fragment fragment)
	{
		fragment = fragment;
		GO = new GameObject (fragment.name);

		MeshRenderer renderer = GO.AddComponent<MeshRenderer> ();
		//		renderer.material = Material.Instantiate (material) as Material;

		MeshFilter filter = GO.AddComponent<MeshFilter> ();
		filter.mesh = fragment.mesh;
	}
}
