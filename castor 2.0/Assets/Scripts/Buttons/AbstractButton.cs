using UnityEngine;
using UnityEngine.UI;

namespace Buttons
{
    public abstract class AbstractButton : MonoBehaviour
    {

        protected virtual void Awake(){
            AddListenerToButton();
        }

        protected void AddListenerToButton()
        {
            Button button = GetComponent<Button>();
            if (!button) Debug.LogError("Could not get te button component, connecting a listener failed.");
            button.onClick.AddListener(OnClick);
        }

        protected virtual void Update()
        {
            DetectKeyBoardShortCut();
        }

        public abstract void OnClick();

        protected abstract void DetectKeyBoardShortCut();
    }
}