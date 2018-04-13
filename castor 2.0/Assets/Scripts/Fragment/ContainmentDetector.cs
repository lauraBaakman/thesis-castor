using UnityEngine;
using Utils;

[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))] //Otherwise the meshrender will give empty bounds
public class ContainmentDetector : MonoBehaviour
{
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

        //Cheap check, if the point falls outside the objects bounding box there is no need to check further.
        if (!InBoundingBox(position)) return false;

        return RayCheck(position);
    }

    /// <summary>
    /// Check if the point falls within the bounding box of the game object. 
    /// </summary>
    /// <returns><c>true</c>, if the position falls within the bounding box of the gameobject, <c>false</c> otherwise.</returns>
    /// <param name="position">Position.</param>
    private bool InBoundingBox(Vector3 position)
    {
        Bounds bounds = GetComponent<Renderer>().bounds;
        return bounds.Contains(position);
    }

    private bool RayCheck(Vector3 position)
    {
        Debug.Log("Ray Check");

        Vector3 start = FindPointOutsideGameObject();
        Vector3 goal = position;

        return false;
    }

    private Vector3 FindPointOutsideGameObject()
    {
        Bounds bounds = GetComponent<MeshRenderer>().bounds;
        Vector3 position = bounds.center + bounds.extents * 2;

        //Debug.Log(position);

        return position;
    }
}
