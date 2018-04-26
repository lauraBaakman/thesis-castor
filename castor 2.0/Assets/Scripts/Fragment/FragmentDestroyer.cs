using System.Collections.Generic;
using RTEditor;
using UnityEngine;

namespace Fragment
{
    public class FragmentDestroyer : MonoBehaviour
    {
        public void DestroyFragment()
        {
            foreach (Transform childTransform in this.transform)
            {
                DeleteGameObject(childTransform.gameObject);
            }
            DeleteGameObject(gameObject);
            EditorObjectSelection.Instance.ClearSelection(allowUndoRedo: false);
        }

        private void DeleteGameObject(GameObject child)
        {
            child.SetActive(false);
            child.DestroyAllChildren();
            Destroy(child, 2.0f);
        }
    }

}

