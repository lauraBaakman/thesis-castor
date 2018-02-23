using UnityEngine;
using Registration;
using System.Collections.Generic;

namespace Fragment
{
    public class ICPPointsController : MonoBehaviour, IICPListener
    {
        private static string pointPrefabPath = "ICPPoint";

        private Transform originalParentTransform;
        private Stack<GameObject> unusedPoints = new Stack<GameObject>();

        private Dictionary<Point, ICPPointController> pointGOMapping = new Dictionary<Point, ICPPointController>();

        private void Awake()
        {
            originalParentTransform = transform.parent;
            name = originalParentTransform.name + " " + gameObject.name;
        }

        #region Correspondences
        public void OnICPCorrespondencesChanged(ICPCorrespondencesChanged message)
        {
            ICPPointController controller;
            foreach (Correspondence correspondence in message.Correspondences)
            {
                bool succes = pointGOMapping.TryGetValue(correspondence.ModelPoint, out controller);

                if (!succes)
                {
                    Debug.Log("Could not find the gameobject associated with " + correspondence.ModelPoint);
                    continue;
                }

                controller.SetColor(correspondence.Color);
            }
        }
        #endregion

        #region Points
        public void OnICPPointsSelected(ICPPointsSelectedMessage message)
        {
            transform.SetParent(message.Transform);

            AddICPPoints(message.Points);
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

            pointGOMapping.Add(point, pointController);
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
            transform.SetParent(originalParentTransform);
            pointGOMapping.Clear();
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

        public void OnPreparationStepCompleted(ICPPreparationStepCompletedMessage message) { }
        #endregion
    }

}

