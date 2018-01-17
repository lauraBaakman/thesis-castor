using UnityEngine;
using System.Collections;
using NUnit.Framework;

[TestFixture]
public class ColorGeneratorTest
{
    [Test]
    public void TestGetNextColor()
    {
        TestNextColor(new Color(0.55f, 0.00f, 0.20f));
        TestNextColor(new Color(0.29f, 0.22f, 0.57f));
        TestNextColor(new Color(0.93f, 0.33f, 0.20f));
        TestNextColor(new Color(1.00f, 1.00f, 0.70f));
        TestNextColor(new Color(0.61f, 0.85f, 0.58f));
        TestNextColor(new Color(0.16f, 0.45f, 0.69f));
        TestNextColor(new Color(0.78f, 0.16f, 0.25f));
        TestNextColor(new Color(0.98f, 0.62f, 0.31f));
        TestNextColor(new Color(0.88f, 0.96f, 0.53f));
        TestNextColor(new Color(0.99f, 0.85f, 0.47f));
        TestNextColor(new Color(0.34f, 0.72f, 0.58f));
        TestNextColor(new Color(0.55f, 0.00f, 0.20f));
    }

    private void TestNextColor(Color expected){
        Color actual = Utils.ColorGenerator.Instance.GetNextColor();
        Assert.That(actual, Is.EqualTo(expected));        
    }
}
