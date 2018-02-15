using UnityEngine;
using System.Collections;

namespace Fragment
{
    public class TransformController : MonoBehaviour
    {
        public void TransformFragment(Matrix4x4 transformMatrix, Transform referenceTransform)
        {
            TranslateFragment(transformMatrix.ExtractTranslation(), referenceTransform);
            RotateFragment(transformMatrix.ExtratRotation(), referenceTransform);
        }

        private void TranslateFragment(Vector3 translation, Transform referenceTransform)
        {
            transform.Translate(
                translation: translation,
                relativeTo: referenceTransform
            );
        }

        private void RotateFragment(Quaternion rotation, Transform referenceTransform)
        {
            Debug.Log("Ignoring the input rotation for now, let's first make sure we can a fragment around its own center.");
            //Transform worldTransform = transform.root;

            /////Source: https://answers.unity.com/questions/25305/rotation-relative-to-a-transform.html
            //Quaternion fromReferenceToWorld = Quaternion.FromToRotation(ReferenceTransform.forward, worldTransform.forward);
            //Quaternion rotationInWorld = rotationInReferenceTransform * fromReferenceToWorld;

            transform.Rotate(
                eulerAngles: new Vector3(0, 0, 0),
                relativeTo: Space.Self
            );
        }
    }

}

