using UnityEngine;
using Utils;
using UnityEngine.Assertions;

[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))] //Otherwise the meshrender will give empty bounds
public class ContainmentDetector : MonoBehaviour
{
    public new Collider collider;
    private static float stepSize = 0.0001f;

    private void Start()
    {
        collider = this.GetComponent<MeshCollider>();
    }

    /// <summary>
    /// Verifies if the associated gameobject contains the passed position.
    /// </summary>
    /// <returns><c>true</c>, if the position falls inside the object, <c>false</c> otherwise.</returns>
    /// <param name="point">The point in the passed transform.</param>
    /// <param name="pointTransform">The transform of the passed point.</param>
    public bool GameObjectContains(Vector3 point, Transform pointTransform)
    {
        Vector3 worldSpacePoint = pointTransform.TransformPoint(point);

        return GameObjectContains(worldSpacePoint);
    }

    /// <summary>
    /// Verifies if the object contains the passed position. 
    /// 
    /// Note the position should be in worldspace.
    /// </summary>
    /// <returns><c>true</c>, if the position falls inside the object, <c>false</c> otherwise.</returns>
    /// <param name="point">Position in worldspace of the point in homogeneous coordinates.</param>
    public bool GameObjectContains(Vector4D point)
    {
        Vector3 position = point.xyz.ToUnityVector();

        return GameObjectContains(position);
    }

    /// <summary>
    /// Verifies if the object contains the passed position. 
    /// 
    /// Note the position should be in worldspace.
    /// </summary>
    /// <returns><c>true</c>, if the position falls inside the object, <c>false</c> otherwise.</returns>
    /// <param name="position">Position in worldspace of the point.</param>
    public bool GameObjectContains(Vector3 position)
    {
        //Cheap check, if the point falls outside the objects bounding box there is no need to check further.
        if (!IsInBoundingBox(position)) return false;

        return DetermineContainmentWithRayCasting(position);
    }

    /// <summary>
    /// Check if the point falls within the bounding box of the game object. 
    /// </summary>
    /// <returns><c>true</c>, if the position falls within the bounding box of the gameobject, <c>false</c> otherwise.</returns>
    /// <param name="position">Position.</param>
    private bool IsInBoundingBox(Vector3 position)
    {
        Bounds bounds = GetComponent<Renderer>().bounds;
        return bounds.Contains(position);
    }

    private bool DetermineContainmentWithRayCasting(Vector3 position)
    {
        Vector3 pointOutsideObject = FindPointOutsideGameObject();

        int intersections = 0;

        intersections += CountIntersectionsOnRay(pointOutsideObject, position);
        intersections += CountIntersectionsOnRay(position, pointOutsideObject);

        return IsOdd(intersections);
    }

    private bool IsOdd(int number)
    {
        return (number % 2) != 0;
    }

    private int CountIntersectionsOnRay(Vector3 start, Vector3 end)
    {
        Vector3 direction = (end - start).normalized;
        Vector3 current = start;

        int intersections = 0;

        RaycastHit hit;

        while (current != end)
        {
            if (HasHitThisColliderAlongRay(current, end, out hit))
            {
                intersections++;
                current = hit.point + stepSize * direction;
            }
            else current = end;
        }
        return intersections;
    }

    private bool HasHitThisColliderAlongRay(Vector3 start, Vector3 end, out RaycastHit hit)
    {
        if (Physics.Linecast(start, end, out hit))
        {
            Assert.IsNotNull(this.collider, "The collider of this object should not be null.");

            //Check if we have hit this object, or another object
            return hit.collider.Equals(this.collider);
        };
        return false;
    }

    private Vector3 FindPointOutsideGameObject()
    {
        Bounds bounds = GetComponent<MeshRenderer>().bounds;
        Vector3 position = bounds.center + bounds.extents * 2;

        return position;
    }
}
