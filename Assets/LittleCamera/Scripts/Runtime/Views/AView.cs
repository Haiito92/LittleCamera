using LittleCamera.Camera;
using UnityEngine;

namespace LittleCamera.Views
{
    public abstract class AView : MonoBehaviour
    {
        [field:SerializeField] public float Weight { get; private set; }
        [SerializeField] private bool _isActiveOnStart;

        private void Start()
        {
            if(_isActiveOnStart) SetActive(true);
        }

        public abstract CameraConfiguration GetConfiguration();

        public void SetActive(bool isActive)
        {
            if (isActive)
            {
                CameraController.Instance.AddView(this);
            }
            else
            {
                CameraController.Instance.RemoveView(this);
            }
        }

        private void OnDrawGizmos()
        {
            GetConfiguration().DrawGizmos(Color.magenta);
        }
    }
}