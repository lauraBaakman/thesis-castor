using UnityEngine;
using System.Collections;
using System;

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

    public static float Dot(this Quaternion lhs, Quaternion rhs)
    {
        return (
            lhs.x * rhs.x +
            lhs.y * rhs.y +
            lhs.z * rhs.z +
            lhs.w * rhs.w
        );
    }
}