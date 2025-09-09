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
    }
}
