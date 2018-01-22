using UnityEngine;
using System.Collections.Generic;
using System;

namespace Registration {
    public class ICPRegisterer {
        private GameObject StaticFragment;
        private GameObject ModelFragment;

        private Settings Settings;

        private Action CallBack;

        public ICPRegisterer(
            GameObject staticFragment, GameObject modelFragment,
            Settings settings = null,
            Action callBack = null
        )
        {
            StaticFragment = staticFragment;
            ModelFragment = modelFragment;
            Settings = settings ?? new Settings();
            CallBack = callBack;
        }

        public void Register()
        {
            Transform transform;
            bool stop = false;
            object correspondences;

            List<Vector3> StaticPoints = SelectPoints(StaticFragment);
            List<Vector3> ModelPoints = SelectPoints(ModelFragment);

            //while (!stop) {
            //    correspondences = ComputeCorrespondences(StaticPoints, ModelPoints);
            //    correspondences = FilterCorrespondences(correspondences);

            //    transform = DetermineTransform(correspondences);
            //    ModelFragment = ApplyTransform(transform, ModelFragment);

            //    stop = StopCondition(StaticFragment, ModelFragment);
            //}

            if (CallBack != null) CallBack();
            SendMessageToAllListeners("OnICPFinished");
        }

        /// <summary>
        /// Selects the points to use for the computation of the correspondences from the gameobejct with the selector specified in the settings object. Notify the fragment of which the points are selected.
        /// </summary>
        /// <returns>The points.</returns>
        /// <param name="fragment">Fragment.</param>
        private List<Vector3> SelectPoints( GameObject fragment )
        {
            Mesh mesh = fragment.GetComponent<MeshFilter>().mesh;
            List<Vector3> points = Settings.PointSelector.Select(mesh);

            //Notify the fragment
            fragment.SendMessage(
                methodName: "OnICPPointsSelected",
                value: points,
                options: SendMessageOptions.DontRequireReceiver
            );

            return points;
        }

        private object FilterCorrespondences( object correspondences )
        {
            throw new NotImplementedException();
        }

        private bool StopCondition( GameObject staticFragment, GameObject modelFragment )
        {
            throw new NotImplementedException();
        }

        private GameObject ApplyTransform( Transform transform, GameObject modelFragment )
        {
            throw new NotImplementedException();
        }

        private Transform DetermineTransform( object correspondences )
        {
            throw new NotImplementedException();
        }

        private object ComputeCorrespondences( object staticPoint, object fragmentPoints )
        {
            throw new NotImplementedException();
        }

        private void SendMessageToAllListeners( string methodName )
        {
            ModelFragment.SendMessage(
                methodName: methodName,
                options: SendMessageOptions.RequireReceiver
            );
            StaticFragment.SendMessage(
                methodName: methodName,
                options: SendMessageOptions.RequireReceiver
            );
        }
    }
}
