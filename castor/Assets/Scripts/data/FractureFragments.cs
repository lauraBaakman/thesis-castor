﻿using System.Collections;
using System.Collections.Generic;

public sealed class FractureFragments
{
	private static readonly FractureFragments instance = new FractureFragments ();

	private List<Fragment> fragments;

	private FractureFragments ()
	{
		fragments = new List<Fragment> ();
	}

	/// <summary>
	/// Adds the fragment from file <c>path</c>.
	/// </summary>
	/// <returns>The fragment from file.</returns>
	/// <param name="path">Path contains the absolute path to fragment that should be loaded.</param>
	public Fragment AddFragmentFromFile (string path)
	{
		Fragment fragment = Fragment.FromFile (path);
		fragments.Add (fragment);
		return fragment;
	}

	/// <summary>
	/// Gets the instance of the singleton.
	/// </summary>
	/// <value>The instance represents the list of fracture fragments.</value>
	public static FractureFragments Instance {
		get {
			return instance;
		}
	}
}