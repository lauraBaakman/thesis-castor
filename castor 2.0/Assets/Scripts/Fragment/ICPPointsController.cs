using UnityEngine;
using Registration;
using Registration.Messages;
using System.Collections.Generic;

namespace Fragment
{
    public class ICPPointsController : MonoBehaviour, IICPListener
    {
        private static string pointPrefabPath = "ICPPoint";

        private Stack<GameObject> unusedPoints = new Stack<GameObject>();
        private ICPController parentsICPController;

        /// <summary>
        /// Mapping between points, in the reference transform, and the gameobjects used to represent those points.
        /// </summary>
        private Dictionary<Point, ICPPointController> pointGOMapping = new Dictionary<Point, ICPPointController>();

        private void Awake()
        {
            GameObject parent = this.transform.parent.gameObject;
            parentsICPController = parent.GetComponent<ICPController>();
            Debug.Assert(parentsICPController != null, "The parent gameobject of the object that has the " + this.name + " is expected to have an ICPController.");
        }

        public void Start()
        {
            name = transform.parent.gameObject.name + " " + gameObject.name;
        }

        #region Points
        private ICPPointController AddICPPoint(Point point)
        {
            GameObject pointGO = GetPointGO();
            ICPPointController pointController = pointGO.GetComponent<ICPPointController>();
            pointController.RepresentPoint(point);

            pointGOMapping.Add(point, pointController);

            return pointController;
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

        public void OnStepCompleted(ICPStepCompletedMessage message)
        {
            ClearPoints();
        }

        public void OnPreparationStepCompleted(ICPPreparationStepCompletedMessage message)
        {
            ICPFragmentType type = parentsICPController.FragmentType;
            UpdatePoints(message.Correspondences.GetPointsByType(type));
        }

        private void UpdatePoints(IEnumerable<Point> points)
        {
            ICPPointController controller;
            foreach (Point point in points)
            {
                bool succes = pointGOMapping.TryGetValue(point, out controller);
                if (!succes) controller = AddICPPoint(point);
                controller.SetColor(point.Color);
            }
        }
        #endregion
    }
}