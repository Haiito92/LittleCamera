using System;
using UnityEngine;

namespace LittleCamera.Camera
{
    [Serializable]
    public struct CameraConfiguration
    {
        public float Yaw;
        public float Pitch;
        public float Roll;
        
        public Vector3 Pivot;
        public float Distance;
        
        public float FieldOfView;

        public Quaternion GetRotation()
        {
            return Quaternion.Euler(Pitch, Yaw, Roll); 
        }

        public Vector3 GetPosition()
        {
            Vector3 offset = GetRotation() * (Vector3.back * Distance);
            return Pivot + offset;
        }
        
        public void DrawGizmos(Color color)
        {
            Gizmos.color = color;
            Gizmos.DrawSphere(Pivot, 0.25f);
            Vector3 position = GetPosition();
            Gizmos.DrawLine(Pivot, position);
            Gizmos.matrix = Matrix4x4.TRS(position, GetRotation(), Vector3.one);
            Gizmos.DrawFrustum(Vector3.zero, FieldOfView, 0.5f, 0f, UnityEngine.Camera.main.aspect);
            Gizmos.matrix = Matrix4x4.identity;
        }
    }
}
