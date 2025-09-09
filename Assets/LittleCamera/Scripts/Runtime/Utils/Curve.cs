using UnityEngine;

namespace LittleCamera.Utils
{
    public class Curve
    {
        private Vector3 _a;
        private Vector3 _b;
        private Vector3 _c;
        private Vector3 _d;

        public Vector3 GetPosition(float t)
        {
            return MathsUtils.CubicBezier(_a, _b, _c, _d, t);
        }

        public Vector3 GetPosition(float t, Matrix4x4 localToWorldMatrix)
        {
            return localToWorldMatrix.MultiplyPoint(GetPosition(t));
        }

        public void DrawGizmos(Color color, Matrix4x4 localToWorldMatrix)
        {
            Gizmos.color = color;

            Gizmos.DrawIcon(localToWorldMatrix.MultiplyPoint(_a), "A");
            Gizmos.DrawIcon(localToWorldMatrix.MultiplyPoint(_b), "B");
            Gizmos.DrawIcon(localToWorldMatrix.MultiplyPoint(_c), "C");
            Gizmos.DrawIcon(localToWorldMatrix.MultiplyPoint(_d), "D");

            Gizmos.DrawIcon(GetPosition(0.0f, localToWorldMatrix), "P0");
            Gizmos.DrawIcon(GetPosition(0.2f, localToWorldMatrix), "P1");
            Gizmos.DrawIcon(GetPosition(0.5f, localToWorldMatrix), "P2");
            Gizmos.DrawIcon(GetPosition(0.7f, localToWorldMatrix), "P3");
            Gizmos.DrawIcon(GetPosition(1.0f, localToWorldMatrix), "P4");
            
            
            
        }
    }
}
