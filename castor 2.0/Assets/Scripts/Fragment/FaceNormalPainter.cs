using UnityEngine;

namespace Fragment
{
    [RequireComponent(typeof(DoubleConnectedEdgeListStorage))]
    public class FaceNormalPainter : MonoBehaviour
    {
        static Material normalMaterial;

        public float normalScale = 1.0f;
        public Color normalColor = Color.white;

        private Transform ReferenceTransform;

        void Start()
        {
            Debug.Log("HI!");
        }

        void Update()
        {

        }
    }
}
