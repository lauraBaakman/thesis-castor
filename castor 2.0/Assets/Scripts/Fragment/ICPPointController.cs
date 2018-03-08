using UnityEngine;
using RTEditor;

namespace Fragment
{
    public class ICPPointController : MonoBehaviour, IRTEditorEventListener
    {
        public static float defaultScale = 0.01f;
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

        public void RepresentPoint(Registration.Point point, Transform referenceTransfrom)
        {
            SetName(point);
            SetPosition(point.Position, referenceTransfrom);
            SetColor(point.Color);
            SetScale(defaultScale);

            gameObject.SetActive(true);
        }

        private void SetScale(float scale)
        {
            gameObject.transform.localScale = new Vector3(scale, scale, scale);
        }

        private void SetPosition(Vector3 position, Transform referenceTransform)
        {
            Vector3 localPosition = position.ChangeTransformOfPosition(referenceTransform, transform.parent);
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

