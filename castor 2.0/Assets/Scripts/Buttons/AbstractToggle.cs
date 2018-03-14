using UnityEngine;
using UnityEngine.UI;

namespace Buttons
{
    [RequireComponent(typeof(Toggle))]
    [RequireComponent(typeof(Image))]
    public abstract class AbstractToggle : MonoBehaviour
    {
        [Header("Sprite if the toggle is on")]
        public Sprite IsOnSprite;
        public SpriteState IsOnSpriteState;

        [Header("Sprite if the toggle is off")]
        public Sprite IsOffSprite;
        public SpriteState IsOffSpriteState;

        protected Toggle Toggle;
        private Image Image;

        public virtual void Awake()
        {
            Toggle = GetComponent<Toggle>();
            Toggle.onValueChanged.AddListener(OnToggleValueChanged);

            Image = GetComponent<Image>();

            OnToggleValueChanged(Toggle.isOn);
        }

        public void OnEnable()
        {
            OnToggleValueChanged(Toggle.isOn);
            OnEnableAction();
        }

        protected abstract void OnEnableAction();

        protected virtual void Update()
        {
            if (HasDetectedKeyBoardShortCut() && Toggle.IsInteractable())
            {
                Toggle.isOn = !Toggle.isOn;
                OnToggleValueChanged(Toggle.isOn);
            }
        }

        protected abstract bool HasDetectedKeyBoardShortCut();
        internal abstract void ExecuteToggleAction(bool isOn);

        public void OnToggleValueChanged(bool isOn)
        {
            SetToggleSprites(isOn);
            ExecuteToggleAction(isOn);
        }

        public void SetToggleSprites(bool isOn)
        {
            SetSpriteAndSpriteState(
                sprite: isOn ? IsOnSprite : IsOffSprite,
                state: isOn ? IsOnSpriteState : IsOffSpriteState
            );
        }

        private void SetSpriteAndSpriteState(Sprite sprite, SpriteState state)
        {
            Image.sprite = sprite;
            Toggle.spriteState = state;
        }
    }
}