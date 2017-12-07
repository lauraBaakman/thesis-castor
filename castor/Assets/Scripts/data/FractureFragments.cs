using System.Collections;
using System.Collections.Generic;

public sealed class FractureFragments
{
	private static readonly FractureFragments Instance = new FractureFragments ();

	private List<Fragment> Fragments;

	private FractureFragments ()
	{
		Fragments = new List<Fragment> ();
	}

	/// <summary>
	/// Adds the fragment from file <c>path</c>.
	/// </summary>
	/// <returns>The fragment from file.</returns>
	/// <param name="path">Path contains the absolute path to fragment that should be loaded.</param>
	public Fragment AddFragmentFromFile (string path)
	{
		Fragment fragment = Fragment.FromFile (path);
		Fragments.Add (fragment);
		return fragment;
	}

	/// <summary>
	/// Gets the instance of the singleton.
	/// </summary>
	/// <value>The instance represents the list of fracture fragments.</value>
	public static FractureFragments GetInstance {
		get {
			return Instance;
		}
	}
}