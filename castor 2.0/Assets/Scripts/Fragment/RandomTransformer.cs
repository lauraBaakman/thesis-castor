using UnityEngine;

namespace Fragment
{

	public class RandomTransformer : MonoBehaviour
	{

		private float translationFactor = 0.001f;

		public void OnSetPivot()
		{
			TransformRandomly();
		}

		private void TransformRandomly()
		{
			Bounds bounds;

			Vector3 randomRotation = DetermineRotation();
			Vector3 randomTranslation = DetermineTranslation(out bounds);

			RealExperimentLogger.Instance.Log(
				string.Format("Random transform {0}, translationFactor: {4}, bounds: [{1}], rotation: {2}, translation: {3}",
							 this.gameObject.name,
							 bounds,
							 randomRotation,
							 randomTranslation,
							  translationFactor)
			);

			TransformController transformController = this.GetComponent<TransformController>();

			transformController.RotateFragment(Quaternion.Euler(randomRotation), this.transform);
			transformController.TranslateFragment(randomTranslation, this.transform);
		}

		private Vector3 DetermineRotation()
		{
			Vector3 rotation = new Vector3(
				Random.Range(-5, 5),
				Random.Range(-5, 5),
				Random.Range(-5, 5)
			);
			return rotation;
		}

		private Vector3 DetermineTranslation(out Bounds bounds)
		{
			//Get bounds in local space since we translate in local space
			bounds = gameObject.GetComponent<MeshFilter>().mesh.bounds;

			Vector3 translation = new Vector3(
				Random.Range(-1 * translationFactor * bounds.size.x, +1 * translationFactor * bounds.size.x),
				Random.Range(-1 * translationFactor * bounds.size.y, +1 * translationFactor * bounds.size.y),
				Random.Range(-1 * translationFactor * bounds.size.z, +1 * translationFactor * bounds.size.z)
			);
			return translation;
		}
	}
}

