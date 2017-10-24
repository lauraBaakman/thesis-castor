using System.Collections;
using System.Collections.Generic;

public sealed class FractureFragments
{
	private static readonly FractureFragments instance = new FractureFragments ();

	private FractureFragments ()
	{
		//The Constructor
	}

	public static FractureFragments Instance {
		get {
			return instance;
		}
	}

	public Fragment AddFragmentFromFile (string path)
	{
		Fragment fragment = Fragment.FromFile (path);

		return fragment;
	}
}