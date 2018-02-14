using UnityEngine;

using Utils;

namespace Fragment
{
    [RequireComponent(typeof(MeshRenderer))]
    public class MaterialController : MonoBehaviour, IFragmentStateChanged
    {

        private ColorSet ColorSet;
        private MeshRenderer Renderer;

        void Start()
        {
            Renderer = GetComponent<MeshRenderer>();
            ColorSet = new ColorSet(Renderer.material.color);
        }

        public void OnStateChanged(FragmentState newState)
        {
            SetColor(DetermineNewColor(newState));
        }

        private Color DetermineNewColor(FragmentState state)
        {
            if (state.selected) return ColorSet.Selected;
            if (state.locked) return ColorSet.Locked;
            return ColorSet.Normal;
        }

        private void SetColor(Color color){
            Material material = Renderer.material;
            material.color = color;
            Renderer.material = material;
        }
    }
}


