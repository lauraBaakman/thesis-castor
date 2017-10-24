using System.Collections;
using System.Collections.Generic;

public sealed class FractureFragments
{
	private static readonly FractureFragments instance = new FractureFragments ();

	private List<Fragment> fragments;

	private FractureFragments ()
	{
		fragments = new List<Fragment> ();
	}

	public Fragment AddFragmentFromFile (string path)
	{
		Fragment fragment = Fragment.FromFile (path);
		fragments.Add (fragment);
		return fragment;
	}

	public static FractureFragments Instance {
		get {
			return instance;
		}
	}
}