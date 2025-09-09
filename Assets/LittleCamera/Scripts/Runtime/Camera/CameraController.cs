using System;
using System.Collections.Generic;
using LittleCamera.Views;
using UnityEngine;

namespace LittleCamera.Camera
{
    public class CameraController : MonoBehaviour
    {
        private UnityEngine.Camera _camera;
        private CameraConfiguration _currentCameraConfiguration = new CameraConfiguration();
        private CameraConfiguration _targetCameraConfiguration = new CameraConfiguration();

        private List<AView> _activeViews = new List<AView>(); 
        
        #region Singleton
        public static CameraController Instance { get; private set; }

        private void InitializeSingleton()
        {
            if (Instance != this && Instance != null)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Instance = this;
            }
        }
        #endregion

        #region Unity Lifecylce
        private void Awake()
        {
            InitializeSingleton();
            
            _camera = UnityEngine.Camera.main;
        }

        private void Start()
        {
            _targetCameraConfiguration = ComputeAverageConfiguration();
            _currentCameraConfiguration = _targetCameraConfiguration;
        }

        private void Update()
        {
            _targetCameraConfiguration = ComputeAverageConfiguration();
            LerpCurrentConfiguration();
            ApplyConfiguration();
        }
        #endregion


        #region Configuration Computing and Application

        private CameraConfiguration ComputeAverageConfiguration()
        {
            CameraConfiguration averageConfiguration = new CameraConfiguration();

            if (_activeViews.Count == 0) return averageConfiguration;
            
            float weightsSum = 0f;
            Vector2 yawVectorSum = new Vector3();

            foreach (AView view in _activeViews)
            {
                if(view.Weight == 0) continue;
                
                CameraConfiguration viewConfiguration = view.GetConfiguration();
                
                averageConfiguration.Pitch += viewConfiguration .Pitch * view.Weight;
                averageConfiguration.Roll += viewConfiguration.Roll * view.Weight;
                
                yawVectorSum += new Vector2(Mathf.Cos(viewConfiguration.Yaw * Mathf.Deg2Rad), Mathf.Sin(viewConfiguration.Yaw* Mathf.Deg2Rad)) *
                                view.Weight; // We use vectors to calculate Yaw
                
                
                averageConfiguration.Distance += viewConfiguration.Distance * view.Weight;
                averageConfiguration.Pivot += viewConfiguration.Pivot * view.Weight;
                
                averageConfiguration.FieldOfView += viewConfiguration.FieldOfView * view.Weight;
                

                weightsSum += view.Weight;
            }

            if (weightsSum == 0) weightsSum = 1;

            averageConfiguration.Pitch /= weightsSum;
            averageConfiguration.Roll /= weightsSum;
            averageConfiguration.Yaw = Vector2.SignedAngle(Vector2.right, yawVectorSum);
            
            averageConfiguration.Distance /= weightsSum;
            averageConfiguration.Pivot /= weightsSum;
            
            averageConfiguration.FieldOfView /= weightsSum;

            return averageConfiguration;
        }

        private void LerpCurrentConfiguration()
        {
            
            _currentCameraConfiguration.Yaw += (_targetCameraConfiguration.Yaw - _currentCameraConfiguration.Yaw) * 0.1f;
            _currentCameraConfiguration.Pitch += (_targetCameraConfiguration.Pitch - _currentCameraConfiguration.Pitch) * 0.1f;
            _currentCameraConfiguration.Roll += (_targetCameraConfiguration.Roll - _currentCameraConfiguration.Roll) * 0.1f;
            
            _currentCameraConfiguration.Distance += (_targetCameraConfiguration.Distance - _currentCameraConfiguration.Distance) * 0.1f;
            _currentCameraConfiguration.Pivot += (_targetCameraConfiguration.Pivot - _currentCameraConfiguration.Pivot) * 0.1f;
            
            _currentCameraConfiguration.FieldOfView += (_targetCameraConfiguration.FieldOfView - _currentCameraConfiguration.FieldOfView) * 0.1f;
        }
        
        private void ApplyConfiguration()
        {
            if(_camera == null) return;

            _camera.transform.rotation = _currentCameraConfiguration.GetRotation();
            _camera.transform.position = _currentCameraConfiguration.GetPosition();
            _camera.fieldOfView = _currentCameraConfiguration.FieldOfView;
        }
        #endregion

        #region Views Management
        public void AddView(AView view)
        {
            if(!_activeViews.Contains(view)) _activeViews.Add(view);
        }
        
        public void RemoveView(AView view)
        {
            _activeViews.Remove(view);
        }
        #endregion


        private void OnDrawGizmos()
        {
            _targetCameraConfiguration.DrawGizmos(Color.green);
        }
    }
}
