using UnityEngine;
using System.Collections.Generic;
using System;
using Utils;

namespace Registration
{
    public class ICPRegisterer
    {
        private List<GameObject> Listeners = new List<GameObject>();

        private GameObject StaticFragment;
        private GameObject ModelFragment;

        private Settings Settings;

        private IPointSelector Selector;
        private ICorrespondenceFinder CorrespondenceFinder;
        private List<ICorrespondenceFilter> CorrespondenceFilters = new List<ICorrespondenceFilter>();
        private IPointToPointErrorMetric ErrorMetric;

        private Counter iterationCounter;

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

            AddListener(StaticFragment);
            AddListener(ModelFragment);

            iterationCounter = new Utils.Counter(Settings.MaxNumIterations);

            Selector = new SelectAllPointsSelector(Settings.ReferenceTransform);
            CorrespondenceFinder = new NearstPointCorrespondenceFinder();
            ErrorMetric = new PointToPointMeanSquaredDistance();
        }

        public void AddListener(GameObject listener)
        {
            Listeners.Add(listener);
        }

        public void Register()
        {
            Transform transform;
            bool stop = false;

            iterationCounter.Reset();

            List<Vector3> staticPoints = SelectPoints(StaticFragment);
            List<Vector3> modelPoints = SelectPoints(ModelFragment);

            List<Correspondence> correspondences = ComputeCorrespondences(staticPoints, modelPoints);
            correspondences = FilterCorrespondences(correspondences);

            while (!stop)
            {
                ///Minimize the current error
                transform = DetermineTransform(correspondences);
                ModelFragment = ApplyTransform(transform, ModelFragment);

                // Update the correspondences
                correspondences = ComputeCorrespondences(staticPoints, modelPoints);
                correspondences = FilterCorrespondences(correspondences);

                // Determine if we are done
                stop = TerminateICP(correspondences);
            }

            if (CallBack != null) CallBack();
            SendMessageToAllListeners("OnICPFinished");
        }

        /// <summary>
        /// Selects the points to use for the computation of the correspondences from the gameobejct with the selector specified in the settings object. Notify the fragment of which the points are selected.
        /// </summary>
        /// <returns>The points.</returns>
        /// <param name="fragment">Fragment.</param>
        private List<Vector3> SelectPoints(GameObject fragment)
        {
            Mesh mesh = fragment.GetComponent<MeshFilter>().mesh;
            List<Vector3> points = Selector.Select(fragment.transform, mesh);

            //Notify the fragment
            fragment.SendMessage(
                methodName: "OnICPPointsSelected",
                value: new ICPPointsSelectedMessage(
                    points: points,
                    transform: Settings.ReferenceTransform
                ),
                options: SendMessageOptions.DontRequireReceiver
            );

            return points;
        }

        /// <summary>
        /// Computes the correspondences based on the list of static and model points.
        /// </summary>
        /// <returns>The found correspondences.</returns>
        /// <param name="staticPoints">Points of the static fragment.</param>
        /// <param name="modelPoints">Points of the model fragment.</param>
        private List<Correspondence> ComputeCorrespondences(List<Vector3> staticPoints, List<Vector3> modelPoints)
        {
            List<Correspondence> correspondences = CorrespondenceFinder.Find(staticPoints.AsReadOnly(), modelPoints.AsReadOnly());

            SendMessageToAllListeners(
                methodName: "OnICPCorrespondencesChanged",
                message: new ICPCorrespondencesChanged(
                    correspondences,
                    Settings.ReferenceTransform
                )
            );
            return correspondences;
        }

        private List<Correspondence> FilterCorrespondences(List<Correspondence> correspondences)
        {
            foreach (ICorrespondenceFilter filter in CorrespondenceFilters)
            {
                correspondences = filter.Filter(correspondences);
            }

            SendMessageToAllListeners(
                methodName: "OnICPCorrespondencesChanged",
                message: new ICPCorrespondencesChanged(
                    correspondences,
                    Settings.ReferenceTransform
                )
            );
            return correspondences;
        }

        private GameObject ApplyTransform(Transform transform, GameObject modelFragment)
        {
            return modelFragment;
        }

        private Transform DetermineTransform(List<Correspondence> correspondences)
        {
            return null;
        }

        private bool TerminateICP(List<Correspondence> correspondences)
        {
            /// Make sure we do not exceed the maximum number of iterations
            iterationCounter.Increase();
            if (iterationCounter.IsCompleted()) return true;

            /// Check if our error is small enough
            float error = ErrorMetric.ComputeError(correspondences);
            return error < Settings.ErrorThreshold;
        }

        private void SendMessageToAllListeners(string methodName, Message message = null)
        {
            foreach (GameObject listener in Listeners)
            {
                listener.SendMessage(
                    methodName: methodName,
                    value: message,
                    options: SendMessageOptions.RequireReceiver
                );
            }
        }
    }
}
