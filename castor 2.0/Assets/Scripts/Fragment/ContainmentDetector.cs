using UnityEngine;
using Utils;

[RequireComponent(typeof(MeshCollider))]
public class ContainmentDetector : MonoBehaviour
{
    void Start()
    {
    }

    /// <summary>
    /// Verifies if the object contains the passed position. 
    /// 
    /// Note the position should be in worldspace.
    /// </summary>
    /// <returns><c>true</c>, if the position falls inside the object, <c>false</c> otherwise.</returns>
    /// <param name="position">Position in worldspace.</param>
    public bool GameObjectContains(Vector4D position)
    {
        return false;
    }
}
