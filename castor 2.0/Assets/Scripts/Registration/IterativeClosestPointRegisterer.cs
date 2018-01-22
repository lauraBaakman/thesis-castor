using UnityEngine;
using System.Collections.Generic;
using System;

namespace Registration
{
    public class IterativeClosestPointRegisterer
    {
        private GameObject StaticFragment;
        private GameObject ModelFragment;

        private Settings Settings;

        public IterativeClosestPointRegisterer(GameObject staticFragment, GameObject modelFragment, Settings settings = null)
        {
            StaticFragment = staticFragment;
            ModelFragment = modelFragment;
            Settings = settings ? settings : new Settings();
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
            Mesh mesh = fragment.GetComponent<MeshFilter>().mesh;
            List<Vector3> points = Settings.PointSelector.Select(mesh);
            fragment.SendMessage(
                methodName:,
                value:,
                options:
            );
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
