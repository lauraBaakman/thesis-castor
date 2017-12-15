using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace data.Tests
{
    public class MeshQuadTests
    {

        [Test]
        public void FromLine()
        {
            // Set Up
            Vector3 start = new Vector3(1.0f, 2.0f, 3.0f);
            Vector3 end = new Vector3(5.0f, 6.0f, 9.0f);
            float lineWidth = 4.0f;

            // Act
            MeshQuad actual = MeshQuad.FromLine(start, end, lineWidth);

            // Expected Values
            Vector3[] ExpectedVertices = new Vector3[4];
            ExpectedVertices[0] = new Vector3(+2.7489f, +1.4619f, +2.1928f);
            ExpectedVertices[1] = new Vector3(-0.7489f, +2.5381f, +3.8072f);
            ExpectedVertices[2] = new Vector3(+6.7489f, +5.4619f, +8.1928f);
            ExpectedVertices[3] = new Vector3(+3.2511f, +6.5381f, +9.8072f);

            int[] ExpectedTrianglIndices = { 0, 1, 2, 1, 3, 2 };

            Vector3 actualVertex, expectedVertex;
            for (int i = 0; i < actual.Vertices.Length; i++)
            {
                actualVertex = actual.Vertices[i];
                expectedVertex = ExpectedVertices[i];

                Assert.That(actualVertex.x, Is.EqualTo(expectedVertex.x).Within(.0005));
                Assert.That(actualVertex.y, Is.EqualTo(expectedVertex.y).Within(.0005));
                Assert.That(actualVertex.z, Is.EqualTo(expectedVertex.z).Within(.0005));
            }

            int actualIdx, expectedIdx;
            for (int i = 0; i < actual.TriangleIndices.Length; i++)
            {
                actualIdx = actual.TriangleIndices[i];
                expectedIdx = ExpectedTrianglIndices[i];

                Assert.That(actualIdx, Is.EqualTo(expectedIdx));
            }
        }
    }
}