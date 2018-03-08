using UnityEngine;

namespace Fragment
{
    public class TransformController : MonoBehaviour
    {
        Transform pivotPoint;

        public void Awake()
        {
            pivotPoint = transform.Find("Pivot");
            Debug.Assert(pivotPoint, "Could not find the pivot point child object.");
        }

        public void TransformFragment(Matrix4x4 transformMatrix, Transform referenceTransform)
        {
            TranslateFragment(transformMatrix.ExtractTranslation(), referenceTransform);
            RotateFragment(transformMatrix.ExtractRotation(), referenceTransform);
        }

        private void TranslateFragment(Vector3 translation, Transform referenceTransform)
        {
            transform.Translate(
                translation: translation,
                relativeTo: referenceTransform
            );
        }

        private void RotateFragment(Quaternion rotationInReferenceTransform, Transform referenceTransform)
        {
            Transform worldTransform = transform.root;

            /////Source: https://answers.unity.com/questions/25305/rotation-relative-to-a-transform.html
            Quaternion fromReferenceToWorld = Quaternion.FromToRotation(referenceTransform.forward, worldTransform.forward);
            Quaternion rotationInWorld = rotationInReferenceTransform * fromReferenceToWorld;

            //Note the order of the rotations: https://docs.unity3d.com/ScriptReference/Quaternion-eulerAngles.html
            transform.RotateAround(pivotPoint.position, worldTransform.forward, rotationInWorld.ExtractEulerZAngle());
            transform.RotateAround(pivotPoint.position, worldTransform.right, rotationInWorld.ExtractEulerXAngle());
            transform.RotateAround(pivotPoint.position, worldTransform.up, rotationInWorld.ExtractEulerYAngle());
        }
    }
}