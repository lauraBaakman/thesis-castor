using UnityEngine;

namespace Utils
{
    public static class TriangleUtils
    {
        public enum WindingOrder
        {
            CounterClockWise, ClockWise, Colinear
        };

        public static WindingOrder DetermineWindingOrder(Vector3 a, Vector3 b, Vector3 c)
        {
            double doubleSignedArea = (b.x - a.x) * (c.y - a.y) - (c.x - a.x) * (b.y - a.y);

            if (doubleSignedArea < 0) return WindingOrder.ClockWise;
            if (doubleSignedArea > 0) return WindingOrder.CounterClockWise;
            return WindingOrder.Colinear;
        }
    }
}