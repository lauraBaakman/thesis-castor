using NUnit.Framework;
using UnityEngine;
using Utils;
namespace Tests.Utils
{
    [TestFixture]
    public class TriangleUtilsTest
    {
        [Test]
        public void TestWindingOrder_CW()
        {
            Vector3 a = new Vector3(-0.1f, +1.4f, -0.5f);
            Vector3 b = new Vector3(+0.1f, +1.4f, -0.4f);
            Vector3 c = new Vector3(-0.1f, +1.1f, -0.1f);


            ;
            TriangleUtils.WindingOrder actual = TriangleUtils.DetermineWindingOrder(a, b, c);
            TriangleUtils.WindingOrder expected = TriangleUtils.WindingOrder.ClockWise;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestWindingOrder_CCW()
        {
            Vector3 a = new Vector3(+0.1f, +1.4f, -0.4f);
            Vector3 b = new Vector3(-0.1f, +1.4f, -0.5f);
            Vector3 c = new Vector3(-0.1f, +1.1f, -0.1f);


            TriangleUtils.WindingOrder actual = TriangleUtils.DetermineWindingOrder(a, b, c);
            TriangleUtils.WindingOrder expected = TriangleUtils.WindingOrder.CounterClockWise;

            Assert.AreEqual(expected, actual);
        }


        [Test]
        public void TestWindingOrder_Colinear()
        {
            Vector3 a = new Vector3(+0.1f, +1.4f, -0.4f);
            Vector3 b = new Vector3(+0.2f, +2.8f, -0.8f);
            Vector3 c = new Vector3(-0.2f, -2.8f, +0.8f);


            TriangleUtils.WindingOrder actual = TriangleUtils.DetermineWindingOrder(a, b, c);
            TriangleUtils.WindingOrder expected = TriangleUtils.WindingOrder.Colinear;

            Assert.AreEqual(expected, actual);
        }
    }

}