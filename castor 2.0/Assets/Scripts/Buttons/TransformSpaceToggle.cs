using UnityEngine;
using UnityEngine.UI;

using RTEditor;

namespace Buttons
{
    /// Class to manage the transform space toggle, if the toggle is on we use worldspace.
    public class TransformSpaceToggle : MonoBehaviour, Fragments.ISelectionControllerListener
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

        /// <summary>
        /// OnEnable calls the listeners of the obejct to make sure it is initialized correctly.
        /// </summary>
        public void OnEnable()
        {
            OnToggleValueChanged(Toggle.isOn);
            OnNumberOfSelectedObjectsChanged(RTEditor.EditorObjectSelection.Instance.NumberOfSelectedObjects);
        }

        /// <summary>
        /// Toggling the worldspace only makes sense if objects are selected.
        /// </summary>
        /// <param name="currentCount">Current number of selected objects.</param>
        public void OnNumberOfSelectedObjectsChanged(int currentCount)
        {
            Toggle.interactable = (currentCount >= 1);
        }

        public void OnToggleValueChanged(bool inWorldSpace)
        {
            SetSpriteAndSpriteState(
                sprite: inWorldSpace ? WorldSpaceSprite : LocalSpaceSprite,
                state: inWorldSpace ? WorldSpaceSpriteState : LocalSpaceSpriteState
            );

            EditorGizmoSystem.Instance.SendMessage(
                methodName: "OnChangeTransformSpace",
                value: inWorldSpace ? TransformSpace.Global : TransformSpace.Local,
                options: SendMessageOptions.RequireReceiver
            );
        }

        private void SetSpriteAndSpriteState(Sprite sprite, SpriteState state)
        {
            Image.sprite = sprite;
            Toggle.spriteState = state;
        }
    }
}

