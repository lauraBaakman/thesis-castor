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
        }

        public void AddListener(GameObject listener)
        {
            Listeners.Add(listener);
        }

        public void Register()
        {
            Matrix4x4 transformationMatrix;
            bool stop = false;

            iterationCounter.Reset();

            List<Vector3> staticPoints = SelectPoints(StaticFragment);
            List<Vector3> modelPoints = SelectPoints(ModelFragment);

            List<Correspondence> correspondences = ComputeCorrespondences(staticPoints, modelPoints);
            correspondences = FilterCorrespondences(correspondences);

            while (!stop)
            {
                ///Minimize the current error
                transformationMatrix = Settings.TransFormFinder.FindTransform(correspondences);
                ApplyTransform(transformationMatrix, ModelFragment);

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
            List<Vector3> points = Settings.PointSelector.Select(fragment.transform, mesh);

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

        private void ApplyTransform(Matrix4x4 transform, GameObject modelFragment)
        {
            modelFragment.transform.Translate(
                translation: transform.ExtractTranslation(), 
                relativeTo: Settings.ReferenceTransform
            );
            ApplyRotation(transform.ExtratRotation(), modelFragment);
        }

        private void ApplyRotation(Quaternion rotationInReferenceTransform, GameObject modelFragment){
            Transform worldTransform = modelFragment.transform.root;

            Quaternion fromReferenceToWorld = Quaternion.FromToRotation(Settings.ReferenceTransform.forward, worldTransform.forward);
            Quaternion rotationInWorld = rotationInReferenceTransform * fromReferenceToWorld;

            modelFragment.transform.Rotate(
                eulerAngles: rotationInWorld.eulerAngles,
                relativeTo: Space.World
            );            
        }

        private bool TerminateICP(List<Correspondence> correspondences)
        {
            /// Make sure we do not exceed the maximum number of iterations
            iterationCounter.Increase();
            if (iterationCounter.IsCompleted()) return true;

            /// Check if our error is small enough
            float error = Settings.ErrorMetric.ComputeError(correspondences);
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
