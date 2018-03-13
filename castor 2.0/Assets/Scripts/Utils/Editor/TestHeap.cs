using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

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