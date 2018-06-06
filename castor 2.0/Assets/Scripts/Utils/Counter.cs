using UnityEngine;
using System.Collections;

namespace Utils
{
	public class Counter
	{
		private static int firstCount = 0;

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
			currentCount = firstCount;
		}

		public void Reset()
		{
			currentCount = firstCount;
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

		public bool AtFirstCount()
		{
			return this.currentCount == firstCount;
		}
	}

}