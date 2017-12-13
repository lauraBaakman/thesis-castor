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

    public void AddFragment(Fragment fragment){
        Fragments.Add(fragment);
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