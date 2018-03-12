using UnityEngine;

using Utils;
using Registration.Messages;

namespace Fragment
{
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(StateTracker))]
    public class MaterialController : MonoBehaviour, IFragmentStateChanged, IICPStartEndListener
    {
        private MaterialSet MaterialSet;
        private MeshRenderer Renderer;

        void Start()
        {
            Renderer = GetComponent<MeshRenderer>();
            MaterialSet = new MaterialSet(Renderer.material);
        }

        public void OnStateChanged(FragmentState newState)
        {
            SetMaterial(DetermineNewMaterial(newState));
        }

        private Material DetermineNewMaterial(FragmentState state)
        {
            if (state.selected) return MaterialSet.Selected;
            if (state.locked) return MaterialSet.Locked;
            return MaterialSet.Normal;
        }

        private void SetMaterial(Material material)
        {
            Renderer.material = material;
        }
    }
}


