using UnityEngine;
using System.Collections;

namespace Fragment {

    public class RandomTransformer : MonoBehaviour {

        private void Awake()
        {
            if (Application.isEditor) TransformRandomly();
        }

        private void TransformRandomly()
        {
            Debug.Log(name + "HI!");
        }


    }
}

