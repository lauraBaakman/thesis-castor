using UnityEngine;
using UnityEngine.UI;

namespace Buttons
{
    [RequireComponent(typeof(Toggle))]
    [RequireComponent(typeof(Image))]
    public abstract class AbstractToggle : MonoBehaviour
    {
        public Sprite IsOffSprite;
        public SpriteState IsOffSpriteState;

        private Sprite IsOnSprite;
        private SpriteState IsOnSpriteState;

        protected Toggle Toggle;
        private Image Image;

        public void Awake()
        {
            Toggle = GetComponent<Toggle>();
            Toggle.onValueChanged.AddListener(OnToggleValueChanged);

            Image = GetComponent<Image>();
            IsOnSprite = Image.sprite;

            IsOnSpriteState = new SpriteState
            {
                disabledSprite = Toggle.spriteState.disabledSprite,
                highlightedSprite = Toggle.spriteState.highlightedSprite,
                pressedSprite = Toggle.spriteState.pressedSprite
            };
        }

        public void OnEnable()
        {
            OnToggleValueChanged(Toggle.isOn);
            OnEnableAction();
        }

        protected abstract void OnEnableAction();

        public void OnToggleValueChanged(bool isOn)
        {
            SetSpriteAndSpriteState(
                sprite: isOn ? IsOnSprite : IsOffSprite,
                state: isOn ? IsOnSpriteState : IsOffSpriteState
            );

            OnToggleValueChangedAction(isOn);
        }

        public abstract void OnToggleValueChangedAction(bool isOn);

        private void SetSpriteAndSpriteState(Sprite sprite, SpriteState state)
        {
            Image.sprite = sprite;
            Toggle.spriteState = state;
        }
    }
}