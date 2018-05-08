using UnityEngine;

namespace Fragment
{

	public class RandomTransformer : MonoBehaviour
	{

		private void Awake()
		{
			if (Application.isEditor) TransformRandomly();
		}

		private void TransformRandomly()
		{
			transform.rotation = Random.rotation;

			Vector3 translation = new Vector3(
				Random.Range(-2, 2),
				Random.Range(-2, 2),
				Random.Range(-2, 2)
			);
			transform.Translate(translation);
		}


	}
}

