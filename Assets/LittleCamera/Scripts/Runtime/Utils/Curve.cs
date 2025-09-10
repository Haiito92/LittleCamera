using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace LittleCamera.Utils
{
    [Serializable]
    public class Curve
    {
        [SerializeField] private List<Vector3> _points;

        public Vector3 GetPosition(float t)
        {
            return MathsUtils.Bezier(_points, t);
        }

        public Vector3 GetPosition(float t, Matrix4x4 localToWorldMatrix)
        {
            return localToWorldMatrix.MultiplyPoint(GetPosition(t));
        }

        public void DrawGizmos(Color handlesColor ,Color curveColor, Matrix4x4 localToWorldMatrix)
        {
            if(_points.Count == 0) return;
            
            Gizmos.color = handlesColor;

            foreach (Vector3 point in _points)
            {
                Gizmos.DrawSphere(localToWorldMatrix.MultiplyPoint(point), 0.03f);
            }
            
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
