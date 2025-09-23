using HaiitoCorp.LittleCamera.Camera;
using UnityEngine;

namespace HaiitoCorp.LittleCamera.Views
{
    public abstract class AView : MonoBehaviour
    {
        [field:SerializeField] public float Weight { get; set; }
        // [SerializeField] private bool _isActiveOnStart;

        protected virtual void Start()
        {
            // if(_isActiveOnStart) SetActive(true);
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