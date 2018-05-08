using UnityEngine;
using NUnit.Framework.Internal;
using NUnit.Framework;
using System;
using Registration;
using UnityEditor.SceneManagement;
using System.Collections.Generic;
using System.Linq;
using Fragment;
using DoubleConnectedEdgeList;

namespace Tests.Registration
{
	[TestFixture]
	public class RandomSubSamplingConfigurationTests
	{
		private static Transform validTransform = null;
		private static float validPercentage = 50.0f;
		private static AllPointsSampler.Configuration.NormalProcessing validNormalProcessing = AllPointsSampler.Configuration.NormalProcessing.VertexNormals;

		[TestCase(-005f)]
		[TestCase(+200f)]
		public void SetInvalidPercentage(float percentage)
		{
			Assert.Throws(
				typeof(ArgumentException),
				delegate
				{
					object q = new RandomSubSampling.Configuration(validTransform, validNormalProcessing, percentage);
				}
			);

			RandomSubSampling.Configuration x = new RandomSubSampling.Configuration(validTransform, validNormalProcessing, validPercentage);
			Assert.Throws(
				typeof(ArgumentException),
				delegate
				{
					x.Percentage = percentage;
				}
			);
		}

		[TestCase(0)]
		[TestCase(5)]
		[TestCase(20)]
		[TestCase(100)]
		public void SetValidPercentage(float percentage)
		{
			Assert.DoesNotThrow(
				delegate
				{
					object q = new RandomSubSampling.Configuration(validTransform, validNormalProcessing, percentage);
				}
			);

			RandomSubSampling.Configuration x = new RandomSubSampling.Configuration(validTransform, validNormalProcessing, percentage);
			Assert.DoesNotThrow(
				delegate
				{
					x.Percentage = percentage;
				}
			);
		}

		[TestCase(0, 0)]
		[TestCase(20, 0.2f)]
		[TestCase(50, 0.5f)]
		[TestCase(45.5f, 0.455f)]
		[TestCase(100, 1)]
		public void ValidProbability_WithConstructor(float percentage, float expected)
		{
			RandomSubSampling.Configuration config = new RandomSubSampling.Configuration(validTransform, validNormalProcessing, percentage);

			float actual = config.Probability;
			Assert.AreEqual(expected, actual);
		}

		[TestCase(0, 0)]
		[TestCase(20, 0.2f)]
		[TestCase(50, 0.5f)]
		[TestCase(45.5f, 0.455f)]
		[TestCase(100, 1)]
		public void ValidProbability_WithSetter(float percentage, float expected)
		{
			RandomSubSampling.Configuration config = new RandomSubSampling.Configuration(validTransform, validNormalProcessing, validPercentage);
			config.Percentage = percentage;

			float actual = config.Probability;
			Assert.AreEqual(expected, actual);
		}
	}

	[TestFixture]
	public class RandomSubSamplingTests
	{
		private static string sceneName = "Assets/Scenes/TestSceneSampling.unity";

		[TestCase("cube", AllPointsSampler.Configuration.NormalProcessing.VertexNormals)]
		[TestCase("pyramid", AllPointsSampler.Configuration.NormalProcessing.VertexNormals)]
		[TestCase("transformedCube", AllPointsSampler.Configuration.NormalProcessing.VertexNormals)]
		[TestCase("cube", AllPointsSampler.Configuration.NormalProcessing.NoNormals)]
		[TestCase("pyramid", AllPointsSampler.Configuration.NormalProcessing.NoNormals)]
		[TestCase("transformedCube", AllPointsSampler.Configuration.NormalProcessing.NoNormals)]
		[TestCase("cube", AllPointsSampler.Configuration.NormalProcessing.AreaWeightedSmoothNormals)]
		[TestCase("pyramid", AllPointsSampler.Configuration.NormalProcessing.AreaWeightedSmoothNormals)]
		[TestCase("transformedCube", AllPointsSampler.Configuration.NormalProcessing.AreaWeightedSmoothNormals)]
		public void TestSample_100(string gameObjectName, AllPointsSampler.Configuration.NormalProcessing normalProcessing)
		{
			EditorSceneManager.OpenScene(sceneName);

			//The 'default' child of a mesh contains the stuff we are interested in
			GameObject gameObject = GameObject.Find(gameObjectName).transform.GetChild(0).gameObject;
			DoubleConnectedEdgeListStorage dcelStorage = gameObject.GetComponent<DoubleConnectedEdgeListStorage>();
			dcelStorage.DCEL = DCEL.FromMesh(gameObject.GetComponent<MeshFilter>().sharedMesh);

			SamplingInformation samplingInformation = new SamplingInformation(gameObject);

			RandomSubSampling.Configuration randomConfig = new RandomSubSampling.Configuration(
				referenceTransform: gameObject.transform.root,
				normalProcessing: normalProcessing,
				percentage: 100
			);

			AllPointsSampler.Configuration allPointsConfig = new AllPointsSampler.Configuration(
				referenceTransform: gameObject.transform.root,
				normalProcessing: normalProcessing
			);

			SamplingInformation samplingInfo = new SamplingInformation(gameObject);

			List<Point> expected = new AllPointsSampler(allPointsConfig).Sample(samplingInfo);
			List<Point> actual = new RandomSubSampling(randomConfig).Sample(samplingInfo);

			Assert.That(actual, Is.EquivalentTo(expected));
		}

		[TestCase("cube", AllPointsSampler.Configuration.NormalProcessing.VertexNormals)]
		[TestCase("pyramid", AllPointsSampler.Configuration.NormalProcessing.VertexNormals)]
		[TestCase("transformedCube", AllPointsSampler.Configuration.NormalProcessing.VertexNormals)]
		[TestCase("cube", AllPointsSampler.Configuration.NormalProcessing.NoNormals)]
		[TestCase("pyramid", AllPointsSampler.Configuration.NormalProcessing.NoNormals)]
		[TestCase("transformedCube", AllPointsSampler.Configuration.NormalProcessing.NoNormals)]
		[TestCase("cube", AllPointsSampler.Configuration.NormalProcessing.AreaWeightedSmoothNormals)]
		[TestCase("pyramid", AllPointsSampler.Configuration.NormalProcessing.AreaWeightedSmoothNormals)]
		[TestCase("transformedCube", AllPointsSampler.Configuration.NormalProcessing.AreaWeightedSmoothNormals)]
		public void TestSample_0(string gameObjectName, AllPointsSampler.Configuration.NormalProcessing normalProcessing)
		{
			EditorSceneManager.OpenScene(sceneName);

			//The 'default' child of a mesh contains the stuff we are interested in
			GameObject gameObject = GameObject.Find(gameObjectName).transform.GetChild(0).gameObject;
			DoubleConnectedEdgeListStorage dcelStorage = gameObject.GetComponent<DoubleConnectedEdgeListStorage>();
			dcelStorage.DCEL = DCEL.FromMesh(gameObject.GetComponent<MeshFilter>().sharedMesh);

			SamplingInformation samplingInformation = new SamplingInformation(gameObject);

			RandomSubSampling.Configuration config = new RandomSubSampling.Configuration(
				referenceTransform: gameObject.transform.root,
				normalProcessing: normalProcessing,
				percentage: 0
			);

			SamplingInformation samplingInfo = new SamplingInformation(gameObject);

			List<Point> expected = new List<Point>();
			List<Point> actual = new RandomSubSampling(config).Sample(samplingInfo);

			Assert.That(actual, Is.EquivalentTo(expected));
		}

		[TestCase("cube", 50f, 12, AllPointsSampler.Configuration.NormalProcessing.VertexNormals)]
		[TestCase("transformedCube", 50f, 12, AllPointsSampler.Configuration.NormalProcessing.VertexNormals)]
		[TestCase("pyramid", 50f, 8, AllPointsSampler.Configuration.NormalProcessing.VertexNormals)]
		[TestCase("cube", 30f, 2, AllPointsSampler.Configuration.NormalProcessing.NoNormals)]
		[TestCase("transformedCube", 30f, 2, AllPointsSampler.Configuration.NormalProcessing.NoNormals)]
		[TestCase("pyramid", 30f, 2, AllPointsSampler.Configuration.NormalProcessing.NoNormals)]
		[TestCase("cube", 70f, 6, AllPointsSampler.Configuration.NormalProcessing.AreaWeightedSmoothNormals)]
		[TestCase("transformedCube", 70f, 6, AllPointsSampler.Configuration.NormalProcessing.AreaWeightedSmoothNormals)]
		[TestCase("pyramid", 70f, 4, AllPointsSampler.Configuration.NormalProcessing.AreaWeightedSmoothNormals)]
		public void TestSample(string gameObjectname, float percentage, int expectedSampleSize, AllPointsSampler.Configuration.NormalProcessing normalProcessing)
		{
			EditorSceneManager.OpenScene(sceneName);

			//The 'default' child of a mesh contains the stuff we are interested in
			GameObject gameObject = GameObject.Find(gameObjectname).transform.GetChild(0).gameObject;
			DoubleConnectedEdgeListStorage dcelStorage = gameObject.GetComponent<DoubleConnectedEdgeListStorage>();
			dcelStorage.DCEL = DCEL.FromMesh(gameObject.GetComponent<MeshFilter>().sharedMesh);

			SamplingInformation samplingInformation = new SamplingInformation(gameObject);

			RandomSubSampling.Configuration randomConfig = new RandomSubSampling.Configuration(
				referenceTransform: gameObject.transform.root,
				normalProcessing: normalProcessing,
				percentage: percentage
			);

			SamplingInformation samplingInfo = new SamplingInformation(gameObject);

			AllPointsSampler.Configuration allPointsConfig = new AllPointsSampler.Configuration(
				referenceTransform: gameObject.transform.root,
				normalProcessing: normalProcessing
			);
			List<Point> allPoints = new AllPointsSampler(allPointsConfig).Sample(samplingInfo);

			List<Point> actual = new RandomSubSampling(randomConfig).Sample(samplingInfo);

			Assert.AreEqual(expectedSampleSize, actual.Count);
			Assert.That(actual, Is.SubsetOf(allPoints));

			bool allUnique = actual.GroupBy(x => x).All(g => g.Count() == 1);
			Assert.IsTrue(allUnique);
		}
	}
}
