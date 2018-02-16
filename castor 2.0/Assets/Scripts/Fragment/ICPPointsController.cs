using UnityEngine;
using System.Collections;
using Registration;
using System;
using System.Collections.Generic;
using RTEditor;

namespace Fragment
{
    public class ICPPointsController : MonoBehaviour, IICPListener
    {
        private static float pointScale = 0.001f;

        private Transform originalParentTransform;
        private Stack<GameObject> unusedPoints = new Stack<GameObject>();

        private void Awake()
        {
            originalParentTransform = transform;
        }

        #region Correspondences
        public void OnICPCorrespondencesChanged(ICPCorrespondencesChanged message) { }
        #endregion

        #region Points
        public void OnICPPointsSelected(ICPPointsSelectedMessage message)
        {
            transform.SetParent(message.Transform);

            AddICPPoints(message.Points);

            transform.SetParent(originalParentTransform, worldPositionStays: true);
        }

        private void AddICPPoints(List<Point> points)
        {
            foreach (Point point in points) AddICPPoint(point);
        }

        private void AddICPPoint(Point point)
        {
            GameObject pointGO = GetPoint();
            //throw new NotImplementedException();
        }

        private GameObject GetPoint()
        {
            if (unusedPoints.Count != 0) return unusedPoints.Pop();

            return new GameObject();
        }

        #endregion


        #region Progress
        public void OnICPTerminated(ICPTerminatedMessage message) { }

        public void OnPreparetionStepCompleted() { }

        public void OnStepCompleted() { }
        #endregion
    }

}

