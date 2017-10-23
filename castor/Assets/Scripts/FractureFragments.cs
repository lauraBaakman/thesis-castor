using System.Collections;
using System.Collections.Generic;

//source: https://msdn.microsoft.com/en-us/library/ff650316.aspx

public sealed class FractureFragments
{
	private static readonly FractureFragments instance = FractureFragments ();

	private FractureFragments ()
	{
	}

	public static FractureFragments intance {
		get {
			return instance; 
		}
	}
}
