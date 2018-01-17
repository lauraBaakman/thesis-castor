using UnityEngine;

namespace Buttons
{
    public abstract class AbstractButton : MonoBehaviour
    {

        protected virtual void Update()
        {
            DetectKeyBoardShortCut();
        }

        public abstract void OnClick();

        protected abstract void DetectKeyBoardShortCut();
    }
}