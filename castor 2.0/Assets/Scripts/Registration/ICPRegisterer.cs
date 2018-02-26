using UnityEngine;
using System.Collections.Generic;
using System;
using Utils;

namespace Registration
{
    public class ICPRegisterer
    {
        private List<GameObject> Listeners = new List<GameObject>();

        private Settings Settings;

        private Counter iterationCounter;

        private Action FinishedCallBack;

        private List<Point> StaticPoints;
        private List<Correspondence> Correspondences;

        public bool HasTerminated
        {
            get { return hasTerminated; }
        }

        #region staticfragment
        private GameObject StaticFragment
        {
            get { return staticFragment; }
            set
            {
                staticFragment = value;
                staticFragment.SendMessage(
                    "OnToggleIsICPFragment",
                    Fragment.ICPFragmentType.Static,
                    SendMessageOptions.RequireReceiver
                );
                AddListener(staticFragment);
            }
        }
        private GameObject staticFragment;
        #endregion

        #region modelfragment
        public GameObject ModelFragment
        {
            get { return modelFragment; }
            set
            {
                modelFragment = value;
                modelFragment.SendMessage(
                    "OnToggleIsICPFragment",
                    Fragment.ICPFragmentType.Model,
                    SendMessageOptions.RequireReceiver
                );
                AddListener(modelFragment);
            }
        }
        private GameObject modelFragment;
        #endregion

        private bool hasTerminated;

        public ICPRegisterer(
            GameObject staticFragment, GameObject modelFragment,
            Settings settings,
            Action callBack = null
        )
        {
            StaticFragment = staticFragment;
            ModelFragment = modelFragment;

            Settings = settings;
            FinishedCallBack = callBack;

            iterationCounter = new Counter(Settings.MaxNumIterations);

            hasTerminated = false;

            //The static fragment does not change, consequently its points need only be sampled once.
            StaticPoints = SelectPoints(StaticFragment);

            SendMessageToAllListeners("OnICPStarted");
        }

        public void AddListener(GameObject listener)
        {
            Listeners.Add(listener);
        }

        public void PrepareStep()
        {
            if (HasTerminated) return;

            Correspondences = ComputeCorrespondences(StaticPoints);
            Correspondences = FilterCorrespondences(Correspondences);

            SendMessageToAllListeners(
                "OnPreparationStepCompleted",
                new ICPPreparationStepCompletedMessage(
                    Correspondences,
                    Settings.ReferenceTransform,
                    //The counter is only updated after the step has been set
                    iterationCounter.CurrentCount + 1
                )
            );

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
            List<Point> points = Settings.PointSelector.Select(new SamplingInformation(fragment.transform, mesh));
            return points;
        }

        /// <summary>
        /// Computes the correspondences based on the list of static and model points.
        /// </summary>
        /// <returns>The found correspondences.</returns>
        /// <param name="staticPoints">Points of the static fragment.</param>
        /// <param name="modelPoints">Points of the model fragment.</param>
        private List<Correspondence> ComputeCorrespondences(List<Point> staticPoints)
        {
            Mesh modelMesh = modelFragment.GetComponent<MeshFilter>().mesh;
            List<Correspondence> correspondences = Settings.CorrespondenceFinder.Find(staticPoints.AsReadOnly(), Settings.PointSelector, new SamplingInformation(modelFragment.transform, modelMesh));
            return correspondences;
        }

        private List<Correspondence> FilterCorrespondences(List<Correspondence> correspondences)
        {
            foreach (ICorrespondenceFilter filter in Settings.CorrespondenceFilters)
            {
                correspondences = filter.Filter(correspondences);
            }
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
