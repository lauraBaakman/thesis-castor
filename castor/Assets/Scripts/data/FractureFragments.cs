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
}


//public sealed class Singleton
//{
//	private static readonly Singleton instance = new Singleton();
//
//	private Singleton(){}
//
//	public static Singleton Instance
//	{
//		get
//		{
//			return instance;
//		}
//	}
//}