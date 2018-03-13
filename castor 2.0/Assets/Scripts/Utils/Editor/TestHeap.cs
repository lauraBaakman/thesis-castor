using System.Collections.Generic;
using NUnit.Framework;
using Ticker;

namespace Tests.Utils
{
    [TestFixture]
    public class MaxHeapTests
    {
        List<int> heapValuesInOrder = new List<int> { 7, 5, 1 };
        MaxHeap<int> heap;


        [SetUp]
        public void Init()
        {
            heap = new MaxHeap<int>(heapValuesInOrder);
        }

        [Test]
        public void Test_Add()
        {
            heap.Add(0);
            heap.Add(3);
            heap.Add(9);

            List<int> expectedValues = new List<int> { 9, 7, 5, 3, 1, 0 };

            foreach (int expected in expectedValues) Assert.That(heap.ExtractMax(), Is.EqualTo(expected));
        }

        [Test]
        public void Test_Peek()
        {
            List<int> expectedHeap = new List<int> { 7, 5, 1 };
            int expectedValue = 7;

            int actualValue = heap.Peek();

            Assert.That(actualValue, Is.EqualTo(expectedValue));

            int idx = 0;
            foreach (int actual in heap) Assert.That(actual, Is.EqualTo(expectedHeap[idx++]));
        }

        [Test]
        public void Test_ExtractMax()
        {
            List<int> expectedHeap = new List<int> { 5, 1 };
            int expectedValue = 7;

            int actualValue = heap.ExtractMax();

            Assert.That(actualValue, Is.EqualTo(expectedValue));

            int idx = 0;
            foreach (int actual in heap) Assert.That(actual, Is.EqualTo(expectedHeap[idx++]));
        }

        [Test]
        public void Test_IsEmpty()
        {
            Assert.IsFalse(heap.IsEmpty());
            heap.ExtractMax();
            heap.ExtractMax();
            heap.ExtractMax();
            Assert.IsTrue(heap.IsEmpty());
        }

        [Test]
        public void Test_WithMesages()
        {
            MaxHeap<Message> messageHeap = new MaxHeap<Message>();

            Message.InfoMessage infomessage1 = new Message.InfoMessage("info1");
            Message.InfoMessage infomessage2 = new Message.InfoMessage("info2");

            Message.WarningMessage warningMessage1 = new Message.WarningMessage("warning1");

            Message.ErrorMessage errorMessage1 = new Message.ErrorMessage("error1");
            Message.ErrorMessage errorMessage2 = new Message.ErrorMessage("error2");

            messageHeap.Add(infomessage1);
            messageHeap.Add(warningMessage1);
            messageHeap.Add(errorMessage1);
            messageHeap.Add(infomessage2);
            messageHeap.Add(errorMessage2);

            Assert.That(messageHeap.ExtractMax(), Is.EqualTo(errorMessage1));
            Assert.That(messageHeap.ExtractMax(), Is.EqualTo(errorMessage2));
            Assert.That(messageHeap.ExtractMax(), Is.EqualTo(warningMessage1));
            Assert.That(messageHeap.ExtractMax(), Is.EqualTo(infomessage1));
            Assert.That(messageHeap.ExtractMax(), Is.EqualTo(infomessage2));
        }
    }

    [TestFixture]
    public class MinHeapTests
    {
        List<int> heapValuesInOrder = new List<int> { 1, 5, 7 };
        MinHeap<int> heap;


        [SetUp]
        public void Init()
        {
            heap = new MinHeap<int>(heapValuesInOrder);
        }

        [Test]
        public void Test_Add()
        {
            heap.Add(0);
            heap.Add(3);
            heap.Add(9);

            List<int> expectedValues = new List<int> { 0, 1, 3, 5, 7, 9 };

            foreach (int expected in expectedValues) Assert.That(heap.ExtractMin(), Is.EqualTo(expected));
        }

        [Test]
        public void Test_Peek()
        {
            List<int> expectedHeap = new List<int> { 1, 5, 7 };
            int expectedValue = 1;

            int actualValue = heap.Peek();

            Assert.That(actualValue, Is.EqualTo(expectedValue));

            int idx = 0;
            foreach (int actual in heap) Assert.That(actual, Is.EqualTo(expectedHeap[idx++]));
        }

        [Test]
        public void Test_ExtractMin()
        {
            List<int> expectedHeap = new List<int> { 5, 7 };
            int expectedValue = 1;

            int actualValue = heap.ExtractMin();

            Assert.That(actualValue, Is.EqualTo(expectedValue));

            int idx = 0;
            foreach (int actual in heap) Assert.That(actual, Is.EqualTo(expectedHeap[idx++]));
        }
    }
}