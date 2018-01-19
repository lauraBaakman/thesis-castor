using UnityEngine;
using UnityEngine.UI;

namespace Buttons
{
    /// Class to manage the transform space toggle, if the toggle is on we use worldspace.
    public class TransformSpaceToggle : MonoBehaviour
    {
        public Sprite LocalSpaceSprite;
        public SpriteState LocalSpaceSpriteState;

        private Sprite WorldSpaceSprite;
        private SpriteState WorldSpaceSpriteState;

        private Toggle Toggle;
        private Image Image;

        public void Awake()
        {
            Toggle = GetComponent<Toggle>();
            Toggle.onValueChanged.AddListener(OnToggleValueChanged);

            Image = GetComponent<Image>();
            WorldSpaceSprite = Image.sprite;

            WorldSpaceSpriteState = new SpriteState
            {
                disabledSprite = Toggle.spriteState.disabledSprite,
                highlightedSprite = Toggle.spriteState.highlightedSprite,
                pressedSprite = Toggle.spriteState.pressedSprite
            };
        }

        public void OnToggleValueChanged(bool inWorldSpace)
        {
            SetSpriteAndSpriteState(
                sprite: inWorldSpace ? WorldSpaceSprite : LocalSpaceSprite,
                state: inWorldSpace ? WorldSpaceSpriteState : LocalSpaceSpriteState
            );
        }

        private void SetSpriteAndSpriteState(Sprite sprite, SpriteState state)
        {
            Image.sprite = sprite;
            Toggle.spriteState = state;
        }
    }
}

