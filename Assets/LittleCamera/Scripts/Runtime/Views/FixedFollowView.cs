using LittleCamera.Camera;
using UnityEngine;
using UnityEngine.Serialization;

namespace LittleCamera.Views
{
    public class FixedFollowView : AView
    {
        [SerializeField] private float _roll;
        [SerializeField] private float _fieldOfView;
        [SerializeField] private Transform _target;
        
        // Constrain Zone
        [SerializeField] private Transform _centralPoint;
        [SerializeField] private float _yawOffsetMax;
        [SerializeField] private float _pitchOffsetMax;
        
        public override CameraConfiguration GetConfiguration()
        {
            CameraConfiguration configuration = new CameraConfiguration();

            if (_target == null || _centralPoint == null) return configuration;
            
            Vector3 centralPointDirection = (_centralPoint.position - transform.position).normalized;
            Vector3 targetDirection = (_target.position - transform.position).normalized;
            
            configuration.Yaw = ComputeYaw(centralPointDirection, targetDirection);
            configuration.Pitch = ComputePitch(centralPointDirection, targetDirection);
            configuration.Roll = _roll;
            
            configuration.Distance = 0;
            configuration.Pivot = transform.position;
            
            configuration.FieldOfView = _fieldOfView;

            return configuration;
        }

        private float ComputeYaw(Vector3 centralPointDirection, Vector3 targetDirection)
        {
            float centralYaw = Mathf.Atan2(centralPointDirection.x, centralPointDirection.z) * Mathf.Rad2Deg; 
            float targetYaw = Mathf.Atan2(targetDirection.x, targetDirection.z) * Mathf.Rad2Deg;

            float diff = targetYaw - centralYaw;
            float absDiff = Mathf.Abs(diff);

            if (absDiff < _yawOffsetMax)
            {
                return targetYaw;
            }

            return diff > 0 ? centralYaw + _yawOffsetMax : centralYaw - _yawOffsetMax;
        }
        
        private float ComputePitch(Vector3 centralPointDirection, Vector3 targetDirection)
        {
            float centralPitch = -Mathf.Asin(centralPointDirection.y) * Mathf.Rad2Deg;
            float targetPitch = -Mathf.Asin(targetDirection.y) * Mathf.Rad2Deg;

            float diff = targetPitch - centralPitch;
            float absDiff = Mathf.Abs(diff);

            if (absDiff < _pitchOffsetMax)
            {
                return targetPitch;
            }

            return diff > 0 ? centralPitch + _pitchOffsetMax : centralPitch - _pitchOffsetMax;
        }
    }
}
