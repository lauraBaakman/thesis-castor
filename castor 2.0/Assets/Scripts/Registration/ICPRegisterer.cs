using UnityEngine;
using System.Collections.Generic;
using System;

namespace Registration {
    public class ICPRegisterer {
        private GameObject StaticFragment;
        private GameObject ModelFragment;

        private Settings Settings;
        private IPointSelector Selector;

        private Action CallBack;

        public ICPRegisterer(
            GameObject staticFragment, GameObject modelFragment,
            Settings settings,
            Action callBack = null
        )
        {
            StaticFragment = staticFragment;
            ModelFragment = modelFragment;
            Settings = settings;
            CallBack = callBack;

            Selector = new SelectAllPointsSelector(Settings.ReferenceTransform);
        }

        public void Register()
        {
            Transform transform;
            bool stop = false;
            object correspondences;

            List<Vector3> StaticPoints = SelectPoints(StaticFragment);
            List<Vector3> ModelPoints = SelectPoints(ModelFragment);

            while (!stop) {
                correspondences = ComputeCorrespondences(StaticPoints, ModelPoints);
                correspondences = FilterCorrespondences(correspondences);

                transform = DetermineTransform(correspondences);
                ModelFragment = ApplyTransform(transform, ModelFragment);

                stop = StopCondition(StaticFragment, ModelFragment);
            }

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
            List<Vector3> points = Selector.Select(fragment.transform, mesh);

            //Notify the fragment
            fragment.SendMessage(
                methodName: "OnICPPointsSelected",
                value: new ICPPointsSelectedMessage(points),
                options: SendMessageOptions.DontRequireReceiver
            );

            return points;
        }

        private object FilterCorrespondences( object correspondences )
        {
            return null;
        }

        private bool StopCondition( GameObject staticFragment, GameObject modelFragment )
        {
            return true;
        }

        private GameObject ApplyTransform( Transform transform, GameObject modelFragment )
        {
            return modelFragment;
        }

        private Transform DetermineTransform( object correspondences )
        {
            return null;
        }

        private object ComputeCorrespondences( object staticPoint, object fragmentPoints )
        {
            return null;
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