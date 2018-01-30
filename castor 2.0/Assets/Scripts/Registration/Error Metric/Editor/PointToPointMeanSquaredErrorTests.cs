using UnityEngine;
using System.Collections.Generic;
using NUnit.Framework;

using Registration;

[TestFixture]
public class PointToPointMeanSquaredErrorTests
{
    [Test]
    public void TestComputeError()
    {
        List<Correspondence> correspondences = new List<Correspondence>
        {
            new Correspondence(
                new Vector3(1.0f, 2.0f, 3.0f),
                new Vector3(2.0f, 3.0f, 4.0f)
            ),
            new Correspondence(
                new Vector3(3.4f, 4.5f, 5.6f),
                new Vector3(6.7f, 7.8f, 8.9f)
            ),
            new Correspondence(
                new Vector3(9.1f, 2.3f, 3.4f),
                new Vector3(4.5f, 5.6f, 6.7f)
            )
        };

        float expected = 37.003333333333337f;
        float actual = new PointToPointMeanSquaredError().ComputeError(correspondences);

        Assert.AreEqual(expected, actual);
    }
}
