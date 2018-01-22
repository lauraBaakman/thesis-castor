using UnityEngine;
using UnityEngine.UI;

namespace Buttons
{
    public abstract class AbstractButton : MonoBehaviour
    {

        protected Button Button;

        protected virtual void Awake()
        {
            Button = GetButton();
            AddListenerToButton();
        }

        private Button GetButton()
        {
            Button button = GetComponent<Button>();
            if (!button) Debug.LogError("Could not get the button component, connecting a listener failed.");
            return button;
        }

        protected void AddListenerToButton()
        {
            Button.onClick.AddListener(OnClick);
        }

        protected virtual void Update()
        {
            if (HasDetectedKeyBoardShortCut() && Button.IsInteractable()) ExecuteButtonAction();
        }

        protected void OnClick()
        {
            ExecuteButtonAction();
        }

        protected abstract bool HasDetectedKeyBoardShortCut();

        protected abstract void ExecuteButtonAction();
    }
}