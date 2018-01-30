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

        public Counter()
        {
            Reset();
        }

        public Counter(int CountToReach) : base()
        {
            countToReach = CountToReach;
        }

        public void Reset()
        {
            currentCount = 0;
            countToReach = 0;
        }

        public void Increase()
        {
            if (currentCount < countToReach) currentCount++;
        }

        public bool IsCompleted()
        {
            if (countToReach == 0) return false;
            return currentCount >= countToReach;
        }

        public void Set(int countToReach)
        {
            Reset();
            this.countToReach = countToReach;
        }
    }

}