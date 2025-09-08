using LittleCamera.Camera;
using UnityEngine;

namespace LittleCamera.Views
{
    public class FixedView : AView
    {
        [SerializeField] private float _yaw;
        [SerializeField] private float _pitch;
        [SerializeField] private float _roll;
        [SerializeField] private float _fieldOfView;
        
        public override CameraConfiguration GetConfiguration()
        {
            CameraConfiguration configuration = new CameraConfiguration
            {
                Yaw = _yaw,
                Pitch = _pitch,
                Roll = _roll,
                Distance = 0,
                Pivot = transform.position,
                FieldOfView = _fieldOfView
            };

            return configuration;
        }
    }
}
