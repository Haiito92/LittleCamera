using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace LittleCamera.Utils
{
    [Serializable]
    public struct Curve
    {
        public Vector3 A;
        public Vector3 B;
        public Vector3 C;
        public Vector3 D;

        public Vector3 GetPosition(float t)
        {
            return MathsUtils.CubicBezier(A, B, C, D, t);
        }

        public Vector3 GetPosition(float t, Matrix4x4 localToWorldMatrix)
        {
            return localToWorldMatrix.MultiplyPoint(GetPosition(t));
        }

        public void DrawGizmos(Color handlesColor ,Color curveColor, Matrix4x4 localToWorldMatrix)
        {
            Gizmos.color = handlesColor;

            Gizmos.DrawSphere(localToWorldMatrix.MultiplyPoint(A), 0.03f);
            Gizmos.DrawSphere(localToWorldMatrix.MultiplyPoint(B), 0.03f);
            Gizmos.DrawSphere(localToWorldMatrix.MultiplyPoint(C), 0.03f);
            Gizmos.DrawSphere(localToWorldMatrix.MultiplyPoint(D), 0.03f);

            Gizmos.color = curveColor;

            float t = 0;
            while (t <= 1f)
            {
                Gizmos.DrawSphere(GetPosition(t, localToWorldMatrix), 0.02f);
                t += 0.01f;
            }
        }
    }
}
