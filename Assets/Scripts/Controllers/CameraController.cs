using Cinemachine;
using UnityEngine;
using CameraState = Enums.CameraState;

namespace Controllers
{
    public class CameraController : MonoBehaviour
    {
        #region Self Variables
        #region Serialized Variables

        [SerializeField] private Animator animator;
        [SerializeField] private CinemachineStateDrivenCamera vmStateCamera;

        #endregion
        #region Private Variables

        private CameraState _cameraState;
        private GameObject _player;

        #endregion
        #endregion
        
        private void Start()
        {
            _player = FindObjectOfType<Managers.PlayerManager>().gameObject;
            SetCamera(_player);
        }
        public void SetCamera(GameObject follow)
        {
            vmStateCamera.Follow = follow.transform;
        }
        public void PlayCamera(CameraState cameraState)
        {
            _cameraState = cameraState;
            switch (_cameraState)
            {
                case CameraState.Runner:
                    animator.SetTrigger(_cameraState.ToString());
                    break;
                case CameraState.Jumping:
                    animator.SetTrigger(_cameraState.ToString());
                    break;
            }
        }
        public void Reset()
        {
            PlayCamera(CameraState.Runner);
            _player = FindObjectOfType<Managers.PlayerManager>().gameObject;
            SetCamera(_player);
        }
    }
}