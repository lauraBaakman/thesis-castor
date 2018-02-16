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
        private static string pointPrefabPath = "ICPPoint";

        private Transform originalParentTransform;
        private Stack<GameObject> unusedPoints = new Stack<GameObject>();

        private void Awake()
        {
            originalParentTransform = transform.parent;
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
            GameObject pointGO = GetPointGO();
            ICPPointController pointController = pointGO.GetComponent<ICPPointController>();
            pointController.RepresentPoint(point);
        }

        private GameObject GetPointGO()
        {
            if (unusedPoints.Count != 0) return unusedPoints.Pop();

            GameObject point = UnityEngine.Object.Instantiate(
                original: Resources.Load(pointPrefabPath),
                parent: transform
            ) as GameObject;

            return point;
        }

        private void ClearPoints()
        {
            foreach (Transform child in transform)
            {
                if (child.gameObject.activeSelf) ClearPoint(child.gameObject);
            }
        }

        private void ClearPoint(GameObject point)
        {
            ICPPointController controller = point.GetComponent<ICPPointController>();
            controller.Reset();

            unusedPoints.Push(point);
        }
        #endregion


        #region Progress
        public void OnICPTerminated(ICPTerminatedMessage message)
        {
            ClearPoints();
        }

        public void OnPreparetionStepCompleted() { }

        public void OnStepCompleted()
        {
            ClearPoints();
        }
        #endregion
    }

}

