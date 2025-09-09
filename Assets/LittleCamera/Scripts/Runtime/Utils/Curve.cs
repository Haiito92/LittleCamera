using System;
using UnityEngine;

namespace LittleCamera.Utils
{
    [Serializable]
    public class Curve
    {
        [SerializeField] private Vector3 _a;
        [SerializeField] private Vector3 _b;
        [SerializeField] private Vector3 _c;
        [SerializeField] private Vector3 _d;

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

            Vector3 p0 = GetPosition(0.0f, localToWorldMatrix);
            Vector3 p1 = GetPosition(0.1f, localToWorldMatrix);
            Vector3 p2 = GetPosition(0.2f, localToWorldMatrix);
            Vector3 p3 = GetPosition(0.3f, localToWorldMatrix);
            Vector3 p4 = GetPosition(0.4f, localToWorldMatrix);
            Vector3 p5 = GetPosition(0.5f, localToWorldMatrix);
            Vector3 p6 = GetPosition(0.6f, localToWorldMatrix);
            Vector3 p7 = GetPosition(0.7f, localToWorldMatrix);
            Vector3 p8 = GetPosition(0.8f, localToWorldMatrix);
            Vector3 p9 = GetPosition(0.9f, localToWorldMatrix);
            Vector3 p10 = GetPosition(1.0f, localToWorldMatrix);
            
            Gizmos.DrawLine(p0, p1);
            Gizmos.DrawLine(p1, p2);
            Gizmos.DrawLine(p2, p3);
            Gizmos.DrawLine(p3, p4);
            Gizmos.DrawLine(p4, p5);
            Gizmos.DrawLine(p5, p6);
            Gizmos.DrawLine(p6, p7);
            Gizmos.DrawLine(p7, p8);
            Gizmos.DrawLine(p8, p9);
            Gizmos.DrawLine(p9, p10);
        }
    }
}
