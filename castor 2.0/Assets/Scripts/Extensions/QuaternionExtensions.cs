using UnityEngine;
using System.Collections;

public static class QuaternionExtensions
{
    public static float ExtractEulerXAngle(this Quaternion quaternion)
    {
        Vector3 eulerAngles = quaternion.eulerAngles;
        return eulerAngles.x;
    }

    public static float ExtractEulerYAngle(this Quaternion quaternion)
    {
        Vector3 eulerAngles = quaternion.eulerAngles;
        return eulerAngles.y;
    }

    public static float ExtractEulerZAngle(this Quaternion quaternion)
    {
        Vector3 eulerAngles = quaternion.eulerAngles;
        return eulerAngles.z;
    }
}