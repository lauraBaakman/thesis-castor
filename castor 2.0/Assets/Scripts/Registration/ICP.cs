using UnityEngine;
using System.Collections.Generic;

namespace Registration
{
    public class IterativeClosestPointRegisterer
    {
        private GameObject StaticFragment;
        private GameObject ModelFragment;

        public IterativeClosestPointRegisterer(GameObject staticFragment, GameObject ModelFragment)
        {
            StaticFragment = staticFragment;
            ModelFragment = ModelFragment;
        }

        public void Register()
        {
            Transform transform;
            bool stop = false;
            object correspondences;

            List<Vector3> StaticPoints = SelectPoints(StaticFragment);
            List<Vector3> ModelPoints = SelectPoints(ModelFragment);

            while (!stop)
            {
                correspondences = ComputeCorrespondences(StaticPoints, ModelPoints);
                correspondences = FilterCorrespondences(correspondences);

                transform = DetermineTransform(correspondences);
                ModelFragment = ApplyTransform(transform, ModelFragment);

                stop = StopCondition(StaticFragment, ModelFragment);
            }
        }

        private List<Vector3> SelectPoints(GameObject fragment)
        {
            IPointSelector selector = new SimplePointSelector();
            Mesh mesh = fragment.GetComponent<MeshFilter>().mesh;
            return selector.Select(mesh);
        }

        private object FilterCorrespondences(object correspondences)
        {
            throw new NotImplementedException();
        }

        private bool StopCondition(GameObject staticFragment, GameObject modelFragment)
        {
            throw new NotImplementedException();
        }

        private Fragment ApplyTransform(Transform transform, GameObject modelFragment)
        {
            throw new NotImplementedException();
        }

        private Transform DetermineTransform(object correspondences)
        {
            throw new NotImplementedException();
        }

        private object ComputeCorrespondences(object staticPoint, object fragmentPoints)
        {
            throw new NotImplementedException();
        }

    }
}
