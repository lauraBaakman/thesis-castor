using UnityEngine;
using DoubleConnectedEdgeList;

namespace Fragment
{
	public class DoubleConnectedEdgeListStorage : MonoBehaviour
	{
		public DCEL DCEL
		{
			get { return doubleConnectedEdgeList; }

			set { doubleConnectedEdgeList = value; }
		}
		private DCEL doubleConnectedEdgeList;
	}
}


