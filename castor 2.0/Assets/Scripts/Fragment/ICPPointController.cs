using UnityEngine;
using System.Collections;
using RTEditor;

namespace Fragment
{
    public class ICPPointController : MonoBehaviour, RTEditor.IRTEditorEventListener
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

        public void RepresentPoint(Registration.Point point)
        {
            SetName(point);
            SetPosition(point.Position);
            SetColor(point.Color);
            SetScale(defaultScale);

            gameObject.SetActive(true);
        }

        private void SetScale(float scale)
        {
            gameObject.transform.localScale = new Vector3(scale, scale, scale);
        }

        private void SetPosition(Vector3 position)
        {
            gameObject.transform.localPosition = position;
        }

        private void SetName(Registration.Point point)
        {
            gameObject.name = "point: " + point.ToString();
        }

        public void SetColor(Color color)
        {
            meshRenderer.material.color = color;
        }
    }
}

