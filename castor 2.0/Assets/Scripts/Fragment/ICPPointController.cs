using UnityEngine;
using RTEditor;

namespace Fragment
{
    public class ICPPointController : MonoBehaviour, IRTEditorEventListener
    {
        public static float defaultScale = 0.1f;
        private Color defaultColor;
        private MeshRenderer meshRenderer;

        public void Awake()
        {
            meshRenderer = GetComponent<MeshRenderer>();
            defaultColor = meshRenderer.material.color;
        }

        #region RTEditorEventListener
        public void OnAlteredByTransformGizmo(Gizmo gizmo) { }

        public bool OnCanBeSelected(ObjectSelectEventArgs selectEventArgs)
        {
            return false;
        }

        public void OnDeselected(ObjectDeselectEventArgs deselectEventArgs) { }

        public void OnSelected(ObjectSelectEventArgs selectEventArgs) { }
        #endregion

        public void Reset()
        {
            transform.Reset();
            name = "unused ICP point";
            meshRenderer.material.color = defaultColor;

            gameObject.SetActive(false);
        }

        public void RepresentPoint(Registration.Point point, Matrix4x4 referenceToLocal)
        {
            SetName(point);
            SetPosition(point.Position, referenceToLocal);
            SetColor(point.Color);
            SetScale(defaultScale);

            gameObject.SetActive(true);
        }

        private void SetScale(float scale)
        {
            gameObject.transform.localScale = new Vector3(scale, scale, scale);
        }

        private void SetPosition(Vector3 position, Matrix4x4 referenceToLocal)
        {
            Vector3 localPosition = referenceToLocal.MultiplyPoint3x4(position);
            gameObject.transform.localPosition = localPosition;
        }

        private void SetName(Registration.Point point)
        {
            gameObject.name = "point: " + point;
        }

        public void SetColor(Color color)
        {
            meshRenderer.material.color = color;
        }
    }
}

