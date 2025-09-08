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
        
        public override CameraConfiguration GetConfiguration()
        {
            CameraConfiguration configuration = new CameraConfiguration();

            if (_target == null) return configuration;
            
            Vector3 targetDirection = (_target.transform.position - transform.position).normalized;
            
            configuration.Yaw = Mathf.Atan2(targetDirection.x, targetDirection.y) * Mathf.Rad2Deg;
            configuration.Pitch = Mathf.Asin(targetDirection.y) * Mathf.Rad2Deg;
            configuration.Roll = _roll;
            
            configuration.Distance = 0;
            configuration.Pivot = transform.position;
            
            configuration.FieldOfView = _fieldOfView;

            return configuration;
        }
    }
}
