using UnityEngine;
using System.Collections;

namespace Utils
{
	public class Counter
	{
		public int CurrentCount
		{
			get { return currentCount; }
		}
		private int currentCount;

		public int CountToReach
		{
			get { return countToReach; }
		}
		private int countToReach;

		public Counter(int CountToReach)
		{
			countToReach = CountToReach;
		}

		public void Reset()
		{
			currentCount = 0;
		}

		public void Increase()
		{
			if (currentCount < countToReach) currentCount++;
		}

		public bool IsCompleted()
		{
			return currentCount >= countToReach;
		}

		public void Set(int countToReach)
		{
			Reset();
			this.countToReach = countToReach;
		}
	}

}