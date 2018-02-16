using UnityEngine;
using System.Collections;
using RTEditor;

namespace Fragment
{
    public class ICPPointController : MonoBehaviour, RTEditor.IRTEditorEventListener
    {
        #region RTEditorEventListener
        public void OnAlteredByTransformGizmo(Gizmo gizmo) { }

        public bool OnCanBeSelected(ObjectSelectEventArgs selectEventArgs)
        {
            return false;
        }

        public void OnDeselected(ObjectDeselectEventArgs deselectEventArgs) { }

        public void OnSelected(ObjectSelectEventArgs selectEventArgs) { }
        #endregion
    }
}

