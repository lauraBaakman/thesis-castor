using UnityEngine;
using UnityEngine.UI;

namespace Buttons
{
    public abstract class AbstractButton : MonoBehaviour
    {

        protected virtual void Awake()
        {
            AddListenerToButton();
        }

        protected void AddListenerToButton()
        {
            Button button = GetComponent<Button>();
            if (!button) Debug.LogError("Could not get the button component, connecting a listener failed.");
            button.onClick.AddListener(OnClick);
        }

        protected virtual void Update()
        {
            if (HasDetectedKeyBoardShortCut()) ExecuteButtonAction();
        }

        protected void OnClick()
        {
            ExecuteButtonAction();
        }

        protected abstract bool HasDetectedKeyBoardShortCut();

        protected abstract void ExecuteButtonAction();
    }
}