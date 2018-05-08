using UnityEngine;
using UnityEditor.SceneManagement;
using NUnit.Framework;
using Utils;

namespace Tests
{
	public class ContainmentDetectorTest
	{
		private static string sceneName = "Assets/Scenes/TestSceneContainmentDetector.unity";
		private static string demispherename = "hollowdemisphere";

		[Test]
		public void ContainmentDetectorTest_Point_Inside()
		{
			bool expected = true;
			bool actual = ContainmentDetectorTest_Helper("position_inside");

			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void ContainmentDetectorTest_Point_Outside()
		{
			bool expected = false;
			bool actual = ContainmentDetectorTest_Helper("position_outside");

			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void ContainmentDetectorTest_Point_Outside_In_Concavity()
		{
			bool expected = false;
			bool actual = ContainmentDetectorTest_Helper("position_in_concavity");

			Assert.AreEqual(expected, actual);
		}

		private bool ContainmentDetectorTest_Helper(string point_name)
		{
			EditorSceneManager.OpenScene(sceneName);
			GameObject demisphere = GameObject.Find(demispherename);

			GameObject point = GameObject.Find(point_name);
			Vector3 center = point.transform.position;
			Vector4 position = new Vector4(center.x, center.y, center.z, 1.0f);

			ContainmentDetector detector = demisphere.GetComponent<ContainmentDetector>();
			//Awake/update is not run by the test
			detector.collider = demisphere.GetComponent<MeshCollider>();

			return detector.GameObjectContains(position);
		}
	}
}