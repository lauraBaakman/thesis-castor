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

        void Awake()
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
            if (state.Locked) return MaterialSet.Locked;
            if (state.Selected) return MaterialSet.Selected;
            return MaterialSet.Normal;
        }

        private void SetMaterial(Material material)
        {
            Renderer.material = material;
        }

        public void OnICPStarted()
        {
            SetMaterial(MaterialSet.Registration);
        }

        public void OnICPTerminated(ICPTerminatedMessage message)
        {
            OnStateChanged(GetComponent<StateTracker>().State);
        }
    }
}


