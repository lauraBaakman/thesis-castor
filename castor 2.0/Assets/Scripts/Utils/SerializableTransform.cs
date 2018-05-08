using UnityEngine;

namespace Utils
{
	[System.Serializable]
	public class SerializableTransform
	{
		public string name;
		public string world_space_position;
		public string world_space_eulerAngles;

		public SerializableTransform(Transform transform)
		{
			name = transform.name;
			world_space_position = transform.position.ToString();
			world_space_eulerAngles = transform.eulerAngles.ToString();
		}
	}
}

