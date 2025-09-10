using System.Collections.Generic;
using UnityEngine;

namespace LittleCamera.Utils
{
    public static class MathsUtils
    {
        public static Vector3 LinearBezier(Vector3 a, Vector3 b, float t)
        {
            return (1 - t) * a + t * b;
        }
        
        public static Vector3 QuadraticBezier(Vector3 a, Vector3 b, Vector3 c, float t)
        {
            return (1 - t) * LinearBezier(a, b, t) + t * LinearBezier(b, c, t);
        }
        
        public static Vector3 CubicBezier(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float t)
        {
            return (1 - t) * QuadraticBezier(a, b, c, t) + t * QuadraticBezier(b, c, d, t);
        }

        public static Vector3 Bezier(List<Vector3> points, float t)
        {
            if (points.Count == 0) return Vector3.zero;
            if (points.Count == 1) return points[0];
            if (points.Count == 2) return (1 - t) * points[0] + t * points[1];
            
            List<Vector3> firstBezier = points.GetRange(0, points.Count - 2);
            List<Vector3> secondBezier = points.GetRange(1,  points.Count - 1);

            return (1 - t) * Bezier(firstBezier, t) + t * Bezier(secondBezier, t);
        }
    }
}
