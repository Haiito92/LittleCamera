using LittleCamera.Camera;
using UnityEngine;

namespace LittleCamera.Views
{
    public class FixedFollowView : AView
    {
        [Header("Fixed Follow View Parameters")]
        [SerializeField] private float _roll;
        [SerializeField] private float _fieldOfView;
        [SerializeField] private Transform _target;
        
        // Constrain Zone
        [Header("Constrains")]
        [SerializeField] private Transform _centralPoint;
        [SerializeField] private bool _isYawConstrained;
        [SerializeField, Range(0,360)] private float _yawOffsetMax;
        [SerializeField] private bool _isPitchConstrained;
        [SerializeField, Range(0,360)] private float _pitchOffsetMax;
        
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
            float targetYaw = Mathf.Atan2(targetDirection.x, targetDirection.z) * Mathf.Rad2Deg;
            
            if (!_isYawConstrained)
            {
                return targetYaw;
            }
            
            float centralYaw = Mathf.Atan2(centralPointDirection.x, centralPointDirection.z) * Mathf.Rad2Deg; 

            float diff = Mathf.DeltaAngle(centralYaw, targetYaw);
            diff = Mathf.Clamp(diff, -_yawOffsetMax, _yawOffsetMax);
            
            return centralYaw + diff;
        }
        
        private float ComputePitch(Vector3 centralPointDirection, Vector3 targetDirection)
        {
            float targetPitch = -Mathf.Asin(targetDirection.y) * Mathf.Rad2Deg;
            
            if (!_isPitchConstrained)
            {
                return targetPitch;
            }
            
            float centralPitch = -Mathf.Asin(centralPointDirection.y) * Mathf.Rad2Deg;

            float diff = Mathf.Clamp(targetPitch - centralPitch, -_pitchOffsetMax, _pitchOffsetMax);

            return centralPitch + diff;
        }
    }
}
