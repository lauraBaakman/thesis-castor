using UnityEngine;
using System.Collections;

using Utils;
using NUnit.Framework;

[TestFixture]
public class CounterTests
{

    [Test]
    public void Test_Counter()
    {
        int countToReach = 3;

        Counter counter = new Counter(CountToReach: countToReach);
        Assert.AreEqual(counter.CountToReach, countToReach);

        counter.Increase();
        Assert.AreEqual(counter.CurrentCount, 1);
        Assert.AreEqual(counter.CountToReach, countToReach);
        Assert.IsFalse(counter.IsCompleted());

        counter.Increase();
        Assert.AreEqual(counter.CurrentCount, 2);
        Assert.AreEqual(counter.CountToReach, countToReach);
        Assert.IsFalse(counter.IsCompleted());

        counter.Increase();
        Assert.AreEqual(counter.CurrentCount, 3);
        Assert.AreEqual(counter.CountToReach, countToReach);
        Assert.IsTrue(counter.IsCompleted());

        counter.Increase();
        Assert.AreEqual(counter.CurrentCount, 3);
        Assert.AreEqual(counter.CountToReach, countToReach);
        Assert.IsTrue(counter.IsCompleted());

        counter.Reset();
        Assert.AreEqual(counter.CurrentCount, 0);
        Assert.AreEqual(counter.CountToReach, 3);
        Assert.IsFalse(counter.IsCompleted());
    }

}
