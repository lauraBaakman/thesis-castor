using UnityEngine;

namespace Buttons
{
    public abstract class AbstractButton : MonoBehaviour
    {

        public abstract void OnClick();

        protected abstract void DetectKeyBoardShortCut();
    }
}