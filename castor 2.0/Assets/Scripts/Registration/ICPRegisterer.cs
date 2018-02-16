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

        private Counter iterationCounter;

        private Action FinishedCallBack;

        List<Point> StaticPoints;
        List<Point> ModelPoints;
        List<Correspondence> Correspondences;

        public bool HasTerminated
        {
            get { return hasTerminated; }
        }
        private bool hasTerminated;

        public ICPRegisterer(
            GameObject staticFragment, GameObject modelFragment,
            Settings settings,
            Action callBack = null
        )
        {
            StaticFragment = staticFragment;
            staticFragment.SendMessage(
                "OnToggleIsICPFragment",
                Fragment.ICPFragmentType.Static,
                SendMessageOptions.RequireReceiver
            );

            ModelFragment = modelFragment;
            modelFragment.SendMessage(
                "OnToggleIsICPFragment",
                Fragment.ICPFragmentType.Model,
                SendMessageOptions.RequireReceiver
            );

            Settings = settings;
            FinishedCallBack = callBack;

            AddListener(StaticFragment);
            AddListener(ModelFragment);

            iterationCounter = new Counter(Settings.MaxNumIterations);

            hasTerminated = false;

            //The static fragment does not change, consequently its points need only be sampled once.
            StaticPoints = SelectPoints(StaticFragment);
        }

        public void AddListener(GameObject listener)
        {
            Listeners.Add(listener);
        }

        public void PrepareStep()
        {
            if (HasTerminated) return;

            ModelPoints = SelectPoints(ModelFragment);
            StaticFragment.SendMessage(
                methodName: "OnICPPointsSelected",
                value: new ICPPointsSelectedMessage(
                    points: StaticPoints,
                    transform: Settings.ReferenceTransform
                ),
                options: SendMessageOptions.DontRequireReceiver
            );

            Correspondences = ComputeCorrespondences(StaticPoints, ModelPoints);
            Correspondences = FilterCorrespondences(Correspondences);

            SendMessageToAllListeners("OnPreparetionStepCompleted");

            TerminateIfNeeded();
        }

        public void Step()
        {
            if (HasTerminated) return;
            iterationCounter.Increase();

            Matrix4x4 transformationMatrix = Settings.TransFormFinder.FindTransform(Correspondences);
            TransformModelFragment(transformationMatrix);

            SendMessageToAllListeners("OnStepCompleted");

            TerminateIfNeeded();
        }

        private void TerminateIfNeeded()
        {
            if (iterationCounter.IsCompleted()) Terminate(ICPTerminatedMessage.TerminationReason.ExceededNumberOfIterations);
            if (ErrorBelowThreshold()) Terminate(ICPTerminatedMessage.TerminationReason.ErrorBelowThreshold);
        }

        private bool ErrorBelowThreshold()
        {
            float error = Settings.ErrorMetric.ComputeError(Correspondences);
            return error < Settings.ErrorThreshold;
        }

        public void Terminate(ICPTerminatedMessage.TerminationReason reason)
        {
            hasTerminated = true;
            if (FinishedCallBack != null) FinishedCallBack();
            SendMessageToAllListeners(
                methodName: "OnICPTerminated",
                message: new ICPTerminatedMessage(reason)
            );
        }

        /// <summary>
        /// Selects the points to use for the computation of the correspondences from the gameobejct with the selector specified in the settings object. Notify the fragment of which the points are selected.
        /// </summary>
        /// <returns>The points.</returns>
        /// <param name="fragment">Fragment.</param>
        private List<Point> SelectPoints(GameObject fragment)
        {
            Mesh mesh = fragment.GetComponent<MeshFilter>().mesh;
            List<Point> points = Settings.PointSelector.Select(fragment.transform, mesh);

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
        private List<Correspondence> ComputeCorrespondences(List<Point> staticPoints, List<Point> modelPoints)
        {
            List<Correspondence> correspondences = Settings.CorrespondenceFinder.Find(staticPoints.AsReadOnly(), modelPoints.AsReadOnly());

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
            foreach (ICorrespondenceFilter filter in Settings.CorrespondenceFilters)
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

        private void TransformModelFragment(Matrix4x4 transform)
        {
            Fragment.TransformController transformcontroller = ModelFragment.GetComponent<Fragment.TransformController>();
            transformcontroller.TransformFragment(transform, Settings.ReferenceTransform);
        }

        private void SendMessageToAllListeners(string methodName, System.Object message = null)
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
